using DfConfig.Model.Classes.Config;
using DfConfig.Model.IGrpcService.RpRr;
using DfGeneral.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.Model.IGrpcService;

/// <summary>
/// 配置服务Grpc
/// </summary>
[ServiceContract]
public interface IConfigGrpcService
{
    /// <summary>
    /// 获取AppConfigs
    /// </summary>
    /// <param name="rp"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    [OperationContract]
    ValueTask<Rr<IList<AppConfig>>> GetAppConfigs(RpGetAppConfigs rp, CancellationToken ctsToken = default);

    /// <summary>
    /// 获取AppConfig
    /// </summary>
    /// <param name="rp"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    [OperationContract]
    Task<Rr<AppConfig>> GetAppConfig(RpGetAppConfig rp, CancellationToken ctsToken = default);
}
