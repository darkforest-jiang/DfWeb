using DfConfig.IService;
using DfConfig.Model.Config;
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
    public class ConfigService : BaseService<DbContextBase>, IConfigService
    {
        public ConfigService(DbContextBase dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 获取AppConfig
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="envId"></param>
        /// <param name="ctsToken"></param>
        /// <returns></returns>
        public async Task<IList<AppConfig>?> GetAppConfigs(int appId, int envId, CancellationToken ctsToken = default)
        {
            var result = await (from a in _DbSet.Set<TKv>().AsNoTracking().Where(p => p.AppId == appId && p.EnvId == envId)
                                join b in _DbSet.Set<TNamespace>() on a.NsId equals b.Id into b1 from b2  in b1.DefaultIfEmpty()
                         select new AppConfig { 
                             Key = a.Key,
                             Value = a.Value,
                             NsKey = b2.NameSpaceKey
                         }).ToListAsync(ctsToken);
            return result;
        }

        /// <summary>
        /// 获取AppConfig
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="envId"></param>
        /// <param name="ctsToken"></param>
        /// <returns></returns>
        public async Task<AppConfig?> GetAppConfig(int appId, int envId, string key, CancellationToken ctsToken = default)
        {
            var rk = AppConfig.GetRealKey(key);

            var result = await (from a in _DbSet.Set<TKv>().AsNoTracking().Where(p => p.AppId == appId && p.EnvId == envId && p.Key == rk.nsKey)
                                join b in _DbSet.Set<TNamespace>() on a.NsId equals b.Id into b1
                                from b2 in b1.DefaultIfEmpty()
                                where b2.NameSpaceKey == rk.nsKey
                                select new AppConfig
                                {
                                    Key = a.Key,
                                    Value = a.Value,
                                    NsKey = b2.NameSpaceKey
                                }).FirstOrDefaultAsync(ctsToken);
            return result;
        }
    }
}
