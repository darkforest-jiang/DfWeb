using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.Model.Admin;

/// <summary>
/// App命令空间
/// </summary>
public class AppNamespace
{
    /// <summary>
    /// AppId
    /// </summary>
    public int? AppId { get; set; }

    /// <summary>
    /// 命名空间Id
    /// </summary>
    public int NsId { get; set; }

    /// <summary>
    /// 命名空间
    /// </summary>
    public string NsKey { get; set; }

    /// <summary>
    /// 命名空间属性 公共=1 私有=0
    /// </summary>
    public int IsPublic { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Notes { get; set; }
}
