using System;
using System.Collections.Generic;

namespace DfConfig.Service.Entities;

public partial class TNamespace
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 命名空间
    /// </summary>
    public string NameSpace { get; set; } = null!;

    /// <summary>
    /// AppId
    /// </summary>
    public int? AppId { get; set; }

    /// <summary>
    /// 命名空间属性 公共=1 私有=0
    /// </summary>
    public bool IsPublic { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Notes { get; set; }
}
