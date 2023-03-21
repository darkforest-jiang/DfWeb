using DfConfig.IService;
using DfConfig.Model.Classes.Config;
using DfConfig.Service.Context;
using DfConfig.Service.Entities;
using DfHelper.EF.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.Service
{
    public class ConfigService : BaseService<DbContextBase>, IConfigService
    {
        public ConfigService(DbContextBase dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 获取AppConfig
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="env"></param>
        /// <param name="ctsToken"></param>
        /// <returns></returns>
        public async Task<IList<AppConfig>?> GetAppConfigs(string appKey, string env, CancellationToken ctsToken = default)
        {
            var result = await (from a in _DbSet.Set<TApp>().AsNoTracking().Where(p=>p.AppKey == appKey)
                                join b in _DbSet.Set<TKv>() on a.Id equals b.AppId
                                join c in _DbSet.Set<TNamespace>() on b.NsId equals c.Id into c1 from c2  in c1.DefaultIfEmpty()
                                join d in _DbSet.Set<TEnv>() on b.EnvId equals d.Id
                                where d.Env == env
                         select new AppConfig { 
                             Key = b.Key,
                             Value = b.Value,
                             NsKey = c2.NameSpaceKey
                         }).ToListAsync(ctsToken);
            return result;
        }

        /// <summary>
        /// 获取AppConfig
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="envId"></param>
        /// <param name="ctsToken"></param>
        /// <returns></returns>
        public async Task<AppConfig?> GetAppConfig(string appKey, string env, string key, CancellationToken ctsToken = default)
        {
            var rk = AppConfig.GetRealKey(key);

            var result = await (from a in _DbSet.Set<TApp>().AsNoTracking().Where(p => p.AppKey == appKey)
                                join b in _DbSet.Set<TKv>() on a.Id equals b.AppId
                                join c in _DbSet.Set<TNamespace>() on b.NsId equals c.Id into c1
                                from c2 in c1.DefaultIfEmpty()
                                join d in _DbSet.Set<TEnv>() on b.EnvId equals d.Id
                                where d.Env == env && b.Key == rk.realKey
                                select new AppConfig
                                {
                                    Key = b.Key,
                                    Value = b.Value,
                                    NsKey = c2.NameSpaceKey
                                }).FirstOrDefaultAsync(ctsToken);
            return result;
        }
    }
}
