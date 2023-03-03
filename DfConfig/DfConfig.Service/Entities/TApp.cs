using System;
using System.Collections.Generic;

namespace DfConfig.Service.Entities;

public partial class TApp
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 主键 应用的Id
    /// </summary>
    public string AppKey { get; set; } = null!;

    /// <summary>
    /// 应用名称
    /// </summary>
    public string AppName { get; set; } = null!;
}
