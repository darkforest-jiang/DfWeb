using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGeneral.RequestResponse
{
    /// <summary>
    /// 请求
    /// </summary>
    public class Rp<T>
    {
        /// <summary>
        /// 参数
        /// </summary>
        public T Parm { get; set; }
    }
}
