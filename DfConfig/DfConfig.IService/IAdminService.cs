using DfConfig.Model;
using DfHelper.EF.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.IService;

/// <summary>
/// 管理服务
/// </summary>
public interface IAdminService : IBaseService<DbContext>
{
    /// <summary>
    /// 获取App列表
    /// </summary>
    /// <returns></returns>
    Task<IList<AppInfo>?> GetAppList(CancellationToken ctsToken = default);
}
