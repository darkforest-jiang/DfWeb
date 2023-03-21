using DfConfig.IService;
using DfConfig.Model.Classes.Config;
using DfConfig.Model.IGrpcService;
using DfConfig.Model.IGrpcService.RpRr;
using DfGeneral.RequestResponse;

namespace DfConfig.Server.GrpcServices;

/// <summary>
/// 配置服务Grpc
/// </summary>
public class ConfigGrpcService : IConfigGrpcService
{
    private readonly IConfigService _configService;

    public ConfigGrpcService(IConfigService configService)
    {
        _configService = configService;
    }

    /// <summary>
    /// 获取AppConfigs
    /// </summary>
    /// <param name="rp"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async ValueTask<Rr<IList<AppConfig>>> GetAppConfigs(RpGetAppConfigs rp, CancellationToken ctsToken = default)
    {
        var result = await _configService.GetAppConfigs(rp.AppKey, rp.Env, ctsToken);
        return new Rr<IList<AppConfig>>
        {
            Result = result
        };
    }

    /// <summary>
    /// 获取AppConfig
    /// </summary>
    /// <param name="rp"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<Rr<AppConfig>> GetAppConfig(RpGetAppConfig rp, CancellationToken ctsToken = default)
    {
        var result = await _configService.GetAppConfig(rp.AppKey, rp.Env, rp.Key, ctsToken);
        return new Rr<AppConfig>
        {
            Result = result
        };
    }

}
