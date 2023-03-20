using DfConfig.Model.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.IService;

/// <summary>
/// 配置服务
/// </summary>
public interface IConfigService
{
    /// <summary>
    /// 获取AppConfig
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="envId"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<IList<AppConfig>?> GetAppConfigs(int appId, int envId, CancellationToken ctsToken = default);

    /// <summary>
    /// 获取AppConfig
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="envId"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<AppConfig?> GetAppConfig(int appId, int envId, string key, CancellationToken ctsToken = default);
}
