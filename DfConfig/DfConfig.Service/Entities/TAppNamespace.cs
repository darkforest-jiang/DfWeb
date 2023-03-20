using System;
using System.Collections.Generic;

namespace DfConfig.Service.Entities;

public partial class TAppNamespace
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 应用Id
    /// </summary>
    public int AppId { get; set; }

    /// <summary>
    /// 命名空间Id
    /// </summary>
    public int NsId { get; set; }

}
