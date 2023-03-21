using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGeneral.Web
{
    /// <summary>
    /// 后台服务基类
    /// </summary>
    public abstract class DfBackgroundService : IHostedService, IDisposable
    {
        private Task _task;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        protected abstract Task DoAsync(CancellationToken cancellationToken);
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _task = DoAsync(_cts.Token);
            if(_task.IsCompleted)
            {
                return _task;
            }

            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_task == null)
            {
                return;
            }
            try
            {
                // Signal cancellation to the executing method
                _cts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_task, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public virtual void Dispose()
        {
            _cts.Cancel();
        }

    }
}
