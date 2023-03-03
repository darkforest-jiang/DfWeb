using DfConfig.IService;
using DfConfig.Model;
using DfConfig.Service.Context;
using DfConfig.Service.Entities;
using DfHelper.EF.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.Service
{
    /// <summary>
    /// 管理服务
    /// </summary>
    public class AdminService : BaseService<DbContextBase>, IAdminService
    {
        public AdminService(DbContextBase dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 获取App列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<AppInfo>?> GetAppList(CancellationToken ctsToken = default)
        {
            var result = await (from a in _DbSet.Set<TApp>()
                         select new AppInfo { 
                             Id = a.Id,
                             AppKey = a.AppKey,
                             AppName = a.AppName
                         }).ToListAsync(ctsToken);
            return result;
        }

    }
}
