using DfConfig.IService;
using DfConfig.Model.Classes.Admin;
using DfConfig.Service.Context;
using DfConfig.Service.Entities;
using DfGeneral.RequestResponse;
using DfHelper.EF.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DfConfig.Service;

/// <summary>
/// 管理服务
/// </summary>
public class AdminService : BaseService<DbContextBase>, IAdminService
{
    public AdminService(DbContextBase dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// 获取App列表
    /// </summary>
    /// <returns></returns>
    public async Task<IList<App>?> GetApps(CancellationToken ctsToken = default)
    {
        var result = await (from a in _DbSet.Set<TApp>()
                     select new App { 
                         Id = a.Id,
                         AppKey = a.AppKey,
                         AppName = a.AppName
                     }).ToListAsync(ctsToken);
        return result;
    }

    /// <summary>
    /// 添加App
    /// </summary>
    /// <param name="appKey"></param>
    /// <param name="appName"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task AddApp(string appKey, string appName, CancellationToken ctsToken = default)
    {
       await  EntityAdd(new TApp { 
           AppKey  = appKey,
           AppName = appName
       }, ctsToken);
    }

    /// <summary>
    /// 修改App
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="appKey"></param>
    /// <param name="appName"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<Rr<int>> AlterApp(int appId, string appKey, string appName, CancellationToken ctsToken = default)
    {
        var app = await FirstOrDefault<TApp>(p => p.Id == appId, ctsToken);
        if(app == null)
        {
            return new Rr<int>
            {
                Code = 0,
                Message = "App不存在"
            };
        }

        app.AppKey = appKey;
        app.AppName = appName;
        await EntityEdit(app, ctsToken);
        return new Rr<int> { };
    }

    /// <summary>
    /// 删除App
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<Rr<int>> DelApp(int appId, CancellationToken ctsToken = default)
    {
        var app = await FirstOrDefault<TApp>(p => p.Id == appId, ctsToken);
        if (app == null)
        {
            return new Rr<int>
            {
                Code = 0,
                Message = "App不存在"
            };
        }

        await using (var trans = await _DbSet.Database.BeginTransactionAsync(ctsToken))
        {
            try
            {
                //删除配置
                await EntityDelete<TKv>(p => p.AppId == app.Id, ctsToken);
                //删除拥有的命名空间
                await EntityDelete<TAppNamespace>(p => p.AppId == app.Id, ctsToken);
                //删除命名空间
                await EntityDelete<TNamespace>(p => p.AppId == app.Id, ctsToken);

                //删除应用
                await EntityDelete(app, ctsToken);

                await trans.CommitAsync(ctsToken);
            }catch(Exception ex)
            {
                await trans.RollbackAsync(ctsToken);
                throw new Exception(ex.Message, ex);
            }
        }

        return new Rr<int> { };
    }

    /// <summary>
    /// 获取环境列表
    /// </summary>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<IList<AppEnv>?> GetAppEnvs(CancellationToken ctsToken = default)
    {
        var result = await (from a in _DbSet.Set<TEnv>()
                     select new AppEnv { 
                         Id = a.Id,
                         Env = a.Env
                     }).ToListAsync(ctsToken);
        return result;
    }

    /// <summary>
    /// 添加环境
    /// </summary>
    /// <param name="env"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task AddAppEnv(string env, CancellationToken ctsToken = default)
    {
        await EntityAdd(new TEnv { 
            Env = env
        }, ctsToken);
    }

    /// <summary>
    /// 修改环境
    /// </summary>
    /// <param name="envId"></param>
    /// <param name="env"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<Rr<int>> AlterAppEnv(int envId, string env, CancellationToken ctsToken = default)
    {
        var appenv = await FirstOrDefault<TEnv>(p => p.Id == envId, ctsToken);
        if (appenv == null)
        {
            return new Rr<int>
            {
                Code = 0,
                Message = "环境不存在"
            };
        }

        appenv.Env = env;
        await EntityEdit(appenv, ctsToken);
        return new Rr<int> { };
    }

    /// <summary>
    /// 删除环境
    /// </summary>
    /// <param name="envId"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<Rr<int>> DelAppEnv(int envId, CancellationToken ctsToken = default)
    {
        var appenv = await FirstOrDefault<TEnv>(p => p.Id == envId, ctsToken);
        if (appenv == null)
        {
            return new Rr<int>
            {
                Code = 0,
                Message = "环境不存在"
            };
        }

        await using (var trans = await _DbSet.Database.BeginTransactionAsync(ctsToken))
        {
            try
            {
                await EntityDelete<TKv>(p => p.EnvId == appenv.Id, ctsToken);

                await EntityDelete(appenv, ctsToken);

                await trans.CommitAsync(ctsToken);
            }
            catch(Exception ex)
            {
                await trans.RollbackAsync(ctsToken);
                throw new Exception(ex.Message, ex);
            }
        }

        return new Rr<int> { };
    }

    /// <summary>
    /// 获取App的命名空间列表
    /// </summary>
    /// <param name="appId">null 表示查询所有公共命名空间</param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<IList<AppNamespace>?> GetAppNamespaces(int? appId, CancellationToken ctsToken = default)
    {
        if (appId != null)
        {
            var result = await (from a in _DbSet.Set<TAppNamespace>()
                          join b in _DbSet.Set<TNamespace>() on a.NsId equals b.Id
                          where a.AppId == appId
                          select new AppNamespace
                          {
                              AppId = a.AppId,
                              NsId = a.NsId,
                              NsKey = b.NameSpaceKey,
                              IsPublic = b.IsPublic,
                              Notes = b.Notes
                          }).ToListAsync(ctsToken);
            return result;
        }
        else
        {
            var result = await (from a in  _DbSet.Set<TNamespace>().Where(p=>p.AppId == null)
                                select new AppNamespace
                                {
                                    NsId = a.Id,
                                    NsKey = a.NameSpaceKey,
                                    IsPublic = a.IsPublic,
                                    Notes = a.Notes
                                }).ToListAsync(ctsToken);
            return result;
        }
    }

    /// <summary>
    /// 添加App命名空间
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="ns"></param>
    /// <param name="notes"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<Rr<int>> AddAppNamespace(int? appId, string nsKey, string notes, CancellationToken ctsToken = default)
    {
        if (appId != null)
        {
            var app = await FirstOrDefault<TApp>(p => p.Id == appId, ctsToken);
            if (app == null)
            {
                return new Rr<int>
                {
                    Code = 0,
                    Message = "App不存在"
                };
            }
        }

        TNamespace tnamespace = new TNamespace { 
            NameSpaceKey = nsKey,
            AppId = appId,
            IsPublic = appId == null ? 1 : 0,
            Notes = notes
        };
        if(appId != null)
        {
            TAppNamespace tappnamespace = new TAppNamespace { 
                AppId = appId!.Value
            };
            await using (var trans = await _DbSet.Database.BeginTransactionAsync(ctsToken))
            {
                try
                {
                    await EntityAdd(tnamespace, ctsToken);
                    tappnamespace.NsId = tnamespace.Id;

                    await EntityAdd(tappnamespace, ctsToken);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    await trans.RollbackAsync(ctsToken);
                    throw new Exception(ex.Message, ex);
                }
            }
        }
        else
        {
            await EntityAdd(tnamespace, ctsToken);
        }

        return new Rr<int>();
    }

    /// <summary>
    /// 修改App命名空间
    /// </summary>
    /// <param name="nsId"></param>
    /// <param name="ns"></param>
    /// <param name="nsKey"></param>
    /// <param name="isPublic"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<Rr<int>> AlterAppNamespace(int nsId, string nsKey, string notes, CancellationToken ctsToken = default)
    {
        var tns = await FirstOrDefault<TNamespace>(p => p.Id == nsId, ctsToken);
        if(tns == null)
        {
            return new Rr<int> { 
                Code = 0,
                Message = "命名空间不存在"
            };
        }

        tns.NameSpaceKey = nsKey;
        tns.Notes = notes;
        await EntityEdit(tns, ctsToken);
        return new Rr<int>();
    }

    /// <summary>
    /// 删除App命名空间
    /// </summary>
    /// <param name="nsIds"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task DelAppNamespaces(IList<int> nsIds, CancellationToken ctsToken = default)
    {

        await using (var trans = await _DbSet.Database.BeginTransactionAsync(ctsToken))
        {
            try
            {
                await EntityDelete<TKv>(p => p.NsId != null && nsIds.Contains(p.NsId!.Value), ctsToken);
                await EntityDelete<TAppNamespace>(p =>nsIds.Contains(p.NsId), ctsToken);
                await EntityDelete<TNamespace>(p=> nsIds.Contains(p.Id), ctsToken);

                await trans.CommitAsync(ctsToken);
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync(ctsToken);
                throw new Exception(ex.Message, ex);
            }
        }
    }

    /// <summary>
    /// 获取App 配置列表
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="envId"></param>
    /// <param name="nsId"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<IList<AppKv>?> GetAppKvs(int? appId, int envId, int? nsId, CancellationToken ctsToken = default)
    {
        var result = await (from a in _DbSet.Set<TKv>().Where(p => p.AppId == appId
                     && p.EnvId == envId && p.NsId == nsId)
                     select new AppKv { 
                         Id = a.Id,
                         AppId = a.AppId,
                         EnvId = a.EnvId,
                         Key = a.Key,
                         Value = a.Value,
                         NsId = a.NsId,
                         Notes = a.Notes
                     }).ToListAsync(ctsToken);
        return result;
    }

    /// <summary>
    /// 添加App配置
    /// </summary>
    /// <param name="appKv"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<Rr<int>> AddAppKv(AppKv appKv, CancellationToken ctsToken = default)
    {
        if(appKv.AppId != null)
        {
            var tapp = await FirstOrDefault<TApp>(p => p.Id == appKv.AppId, ctsToken);
            if(tapp == null)
            {
                return new Rr<int> { 
                    Code = 0,
                    Message = "应用不存在" 
                };
            }
        }
        var tenv = await FirstOrDefault<TEnv>(p => p.Id == appKv.EnvId, ctsToken);
        if (tenv == null)
        {
            return new Rr<int> { 
                Code = 0,
                Message = "引用运行环境不存在"
            };
        }
        if (appKv.NsId != null)
        {
            var tnamespace = await FirstOrDefault<TNamespace>(p => p.Id == appKv.NsId, ctsToken);
            if(tnamespace == null)
            {
                return new Rr<int> { 
                    Code = 0,
                    Message = "命名空间不存在"
                };
            }
        }

        var tkv = new TKv { 
            AppId = appKv.AppId,
            EnvId = appKv.EnvId,
            Key = appKv.Key,
            Value = appKv.Value,
            NsId = appKv.NsId,
            Notes = appKv.Notes
        };
        await EntityAdd(tkv, ctsToken);

        return new Rr<int> { };
    }

    /// <summary>
    /// 修改App配置
    /// </summary>
    /// <param name="kvId"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="notes"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<Rr<int>> AlterAppKv(int kvId, string key, string value, string? notes, CancellationToken ctsToken = default)
    {
        var tkv = await FirstOrDefault<TKv>(p => p.Id == kvId, ctsToken);
        if(tkv == null)
        {
            return new Rr<int> { 
                Code = 0,
                Message = "配置不存在"
            };
        }

        tkv.Key = key;
        tkv.Value = value;
        await EntityEdit(tkv, ctsToken);
        return new Rr<int> { };
    }

    /// <summary>
    /// 删除App配置
    /// </summary>
    /// <param name="kvIds"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task DelAppKvs(IList<int> kvIds, CancellationToken ctsToken = default)
    {
        await EntityDelete<TKv>(p => kvIds.Contains(p.Id), ctsToken);
    }

}
