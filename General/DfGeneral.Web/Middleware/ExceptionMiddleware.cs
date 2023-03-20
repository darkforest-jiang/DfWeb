using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGeneral.Web.Middleware
{
    /// <summary>
    /// 异常中间件
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// 执行中间件
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }catch(Exception ex)
            {
                await HandelException(httpContext, ex);
            }
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task HandelException(HttpContext httpContext, Exception ex)
        {
            string url = $"{httpContext.Request.Host}{httpContext.Request.Path}{httpContext.Request.QueryString}";
            string method = httpContext.Request.Method;

            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "text/json;charset=utf-8;";

            var exStr = ReadException(ex);

            string error = string.Empty;
            if(_env.IsDevelopment())
            {
                error = exStr;
            }
            else
            {
                error = "抱歉，服务异常";
            }
            _logger.LogError(exStr);

            await httpContext.Response.WriteAsync(error);
        }

        /// <summary>
        /// 读取异常信息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string ReadException(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{ex.Message} | {ex.StackTrace} | {ex.InnerException}");
            if(ex.InnerException != null)
            {
                sb.Append($"\n{ReadException(ex.InnerException)}");
            }
            return sb.ToString();
        }
    }
}
