using System;
using System.Collections.Generic;

namespace DfConfig.Service.Models;

public partial class TEnv
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 运行环境名称
    /// </summary>
    public string Env { get; set; } = null!;
}
