using DfConfig.Model.Admin;
using DfGeneral.RequestResponse;
using DfHelper.EF.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.IService;

/// <summary>
/// 管理服务
/// </summary>
public interface IAdminService : IBaseService<DbContext>
{
    /// <summary>
    /// 获取App列表
    /// </summary>
    /// <returns></returns>
    Task<IList<App>?> GetApps(CancellationToken ctsToken = default);

    /// <summary>
    /// 添加App
    /// </summary>
    /// <param name="appKey"></param>
    /// <param name="appName"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task AddApp(string appKey, string appName, CancellationToken ctsToken = default);

    /// <summary>
    /// 修改App
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="appKey"></param>
    /// <param name="appName"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<Rr<int>> AlterApp(int appId, string appKey, string appName, CancellationToken ctsToken = default);

    /// <summary>
    /// 删除App
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<Rr<int>> DelApp(int appId, CancellationToken ctsToken = default);

    /// <summary>
    /// 获取环境列表
    /// </summary>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<IList<AppEnv>?> GetAppEnvs(CancellationToken ctsToken = default);

    /// <summary>
    /// 添加环境
    /// </summary>
    /// <param name="env"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task AddAppEnv(string env, CancellationToken ctsToken = default);

    /// <summary>
    /// 修改环境
    /// </summary>
    /// <param name="envId"></param>
    /// <param name="env"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<Rr<int>> AlterAppEnv(int envId, string env, CancellationToken ctsToken = default);

    /// <summary>
    /// 删除环境
    /// </summary>
    /// <param name="envId"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<Rr<int>> DelAppEnv(int envId, CancellationToken ctsToken = default);

    /// <summary>
    /// 获取App的命名空间列表
    /// </summary>
    /// <param name="appId">null 表示查询所有公共命名空间</param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<IList<AppNamespace>?> GetAppNamespaces(int? appId, CancellationToken ctsToken = default);

    /// <summary>
    /// 添加App命名空间
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="nsKey"></param>
    /// <param name="notes"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<Rr<int>> AddAppNamespace(int? appId, string nsKey, string notes, CancellationToken ctsToken = default);

    /// <summary>
    /// 修改App命名空间
    /// </summary>
    /// <param name="nsId"></param>
    /// <param name="nsKey"></param>
    /// <param name="notes"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<Rr<int>> AlterAppNamespace(int nsId, string nsKey, string notes, CancellationToken ctsToken = default);

    /// <summary>
    /// 删除App命名空间
    /// </summary>
    /// <param name="nsIds"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task DelAppNamespaces(IList<int> nsIds, CancellationToken ctsToken = default);

    /// <summary>
    /// 获取App 配置列表
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="envId"></param>
    /// <param name="nsId"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<IList<AppKv>?> GetAppKvs(int? appId, int envId, int? nsId, CancellationToken ctsToken = default);

    /// <summary>
    /// 添加App配置
    /// </summary>
    /// <param name="appKv"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<Rr<int>> AddAppKv(AppKv appKv, CancellationToken ctsToken = default);

    /// <summary>
    /// 修改App配置
    /// </summary>
    /// <param name="kvId"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="notes"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<Rr<int>> AlterAppKv(int kvId, string key, string value, string? notes, CancellationToken ctsToken = default);

    /// <summary>
    /// 删除App配置
    /// </summary>
    /// <param name="kvIds"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task DelAppKvs(IList<int> kvIds, CancellationToken ctsToken = default);


}
