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
    public interface IDfTwcClient
    {
        public void Connect();

        public void SendMsg();
    }
}
