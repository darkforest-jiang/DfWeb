using System;
using System.Collections.Generic;

namespace DfConfig.Service.Models;

public partial class TAppClient
{
    /// <summary>
    /// 主键Id 自增
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 应用Id
    /// </summary>
    public int AppId { get; set; }

    /// <summary>
    /// 客户端Ip
    /// </summary>
    public string ClientIp { get; set; } = null!;

    /// <summary>
    /// 客户端端口号
    /// </summary>
    public int ClientPort { get; set; }

    /// <summary>
    /// 环境Id
    /// </summary>
    public int? EnvId { get; set; }
}
