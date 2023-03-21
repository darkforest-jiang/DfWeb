using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.ClientFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DfGeneral.Grpc;
using DfConfig.Model.IGrpcService;

namespace DfConfig.Client
{
    /// <summary>
    /// 配置服务扩展
    /// </summary>
    public static class DfConfigExtensions
    {
        /// <summary>
        /// 注入DfConfig.Client
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDfConfigClient(this IServiceCollection services, Action<DfConfigOption> setupAction)
        {
            DfConfigOption opt = new DfConfigOption();
            setupAction(opt);

            services.AddMyCodeFirstGrpcClient<IConfigGrpcService>(opt.ConfigServiceUrl);

            return services;
        }
    }
}
