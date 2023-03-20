using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.Model.Admin;

/// <summary>
/// App配置
/// </summary>
public class AppKv
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 应用表主键Id
    /// </summary>
    public int? AppId { get; set; }

    /// <summary>
    /// 环境表主键Id
    /// </summary>  
    public int EnvId { get; set; }

    /// <summary>
    /// Key
    /// </summary>
    public string Key { get; set; } = null!;

    /// <summary>
    /// Value
    /// </summary>
    public string Value { get; set; } = null!;

    /// <summary>
    /// 命名空间Id
    /// </summary>
    public int? NsId { get; set; }

    /// <summary>
    /// 注释
    /// </summary>
    public string? Notes { get; set; }

}
