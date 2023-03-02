using System;
using System.Collections.Generic;

namespace DfConfig.Service.Models;

public partial class TApp
{
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
