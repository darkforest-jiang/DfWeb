using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.ClientFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGeneral.Grpc
{
    public static class GrpcClientHelper
    {
        private static readonly SocketsHttpHandler _channelOptionsHttpHandler = new SocketsHttpHandler
        {
            //参考：Performance best practices with gRPC
            //https://docs.microsoft.com/zh-cn/aspnet/core/grpc/performance?view=aspnetcore-6.0
            //keeps connection alive
            PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
            KeepAlivePingDelay = TimeSpan.FromSeconds(60),
            KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
            //allows channel to add additional HTTP/2 connections
            EnableMultipleHttp2Connections = true,
        };

        private static readonly ServiceConfig _channelOptionsServiceConfig = new ServiceConfig
        {
            MethodConfigs =
            {
                new MethodConfig
                {
                    Names = { MethodName.Default },
                    //暂时性故障重试策略： https://docs.microsoft.com/zh-cn/aspnet/core/grpc/retries?view=aspnetcore-6.0
                    RetryPolicy = new RetryPolicy
                    {
                        MaxAttempts = 5,
                        InitialBackoff = TimeSpan.FromSeconds(1),
                        MaxBackoff = TimeSpan.FromSeconds(5),
                        BackoffMultiplier = 1.5,
                        RetryableStatusCodes = { StatusCode.Unavailable }
                    }
                }
            }
        };

        /// <summary>
        /// 添加CodeFirst的Grpc客户端，按照我的配置。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="serviceUri">服务地址</param>
        public static IHttpClientBuilder AddMyCodeFirstGrpcClient<T>(this IServiceCollection services, string serviceUri) where T : class
        {
            return services.AddCodeFirstGrpcClient<T>(factoryOptions =>
            {
                factoryOptions.Address = new Uri(serviceUri);
                factoryOptions.ChannelOptionsActions.Add(channelOptions =>
                {
                    channelOptions.HttpHandler = _channelOptionsHttpHandler;
                    channelOptions.ServiceConfig = _channelOptionsServiceConfig;
                });
            })
                //截止时间和取消传播：https://docs.microsoft.com/zh-cn/aspnet/core/grpc/clientfactory?view=aspnetcore-6.0#deadline-and-cancellation-propagation
                //钟卫的注释：
                //微软的官方框架，操作的第二个参数是ServerCallContext。但protobuf-net.Grpc用的却是CallContext，CallContext有一个属性是ServerCallContext。
                //这个配置经过测试，证实Deadline和CancellationToken确实是传播了，即使客户端调用服务端没有传递CallContext参数。
                .EnableCallContextPropagation(options => options.SuppressContextNotFoundErrors = true);
        }
    }
}
