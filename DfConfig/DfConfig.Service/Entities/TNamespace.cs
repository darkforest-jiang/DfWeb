using System;
using System.Collections.Generic;

namespace DfConfig.Service.Models;

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
    /// 应用表主键Id 空表示公共的
    /// </summary>
    public int? AppId { get; set; }
}
