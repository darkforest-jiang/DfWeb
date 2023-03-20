using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static IServiceCollection AddDfConfigClient(this IServiceCollection services)
        {


            return services;
        }
    }
}
