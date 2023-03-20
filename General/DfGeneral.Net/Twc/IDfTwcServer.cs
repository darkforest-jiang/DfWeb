using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGeneral.Net.Twc
{
    /// <summary>
    /// 双向通信Server
    /// </summary>
    public interface IDfTwcServer
    {
        public void Start();

        public void Stop();

        public void SendMsg();
    }
}
