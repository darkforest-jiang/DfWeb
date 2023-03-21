using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.Model.IGrpcService.RpRr
{
    /// <summary>
    /// GetAppConfigs
    /// </summary>
    [DataContract]
    public class RpGetAppConfigs
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
    }
}
