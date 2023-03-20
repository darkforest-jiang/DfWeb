using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.Model.Admin;

/// <summary>
/// 应用信息
/// </summary>
public class App
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 应用的Key
    /// </summary>
    public string AppKey { get; set; } = null!;

    /// <summary>
    /// 应用名称
    /// </summary>
    public string AppName { get; set; } = null!;
}
