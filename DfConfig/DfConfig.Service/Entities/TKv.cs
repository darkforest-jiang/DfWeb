using System;
using System.Collections.Generic;

namespace DfConfig.Service.Entities;

public partial class TKv
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

    /// <summary>
    /// 发布状态 0-未发布 已发布
    /// </summary>
    public int Status { get; set; }
}
