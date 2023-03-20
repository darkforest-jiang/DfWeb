using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.Model.Admin;

/// <summary>
/// 环境
/// </summary>
public class AppEnv
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
