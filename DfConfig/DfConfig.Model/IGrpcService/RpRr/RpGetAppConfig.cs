using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.Model.IGrpcService.RpRr
{
    /// <summary>
    /// RpGetAppConfig
    /// </summary>
    public class RpGetAppConfig
    {
        /// <summary>
        /// AppKey
        /// </summary>
        [DataMember]
        public string AppKey { get; set; }

        /// <summary>
        /// 运行环境
        /// </summary>
        [DataMember]
        public string Env { get; set; }

        /// <summary>
        /// 配置Key
        /// </summary>
        [DataMember]
        public string Key { get; set; }
    }
}
