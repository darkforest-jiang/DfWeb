using DfHelper.EF;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.Service.Context
{
    /// <summary>
    /// ContextBase
    /// </summary>
    public class DbContextBase : DbContext
    {
        // 数据库链接字符串
        // mysql: server=localhost;database=dfconfig;port=3306;user=root;password=root;charset=utf8mb4;sslmode=none;maxpoolsize=1000;
        //mysql-version: 8.0.29-mysql
        // sqlserver: 
        // oracle: 

        public DbContextBase() { }

        public DbContextBase(DbContextOptions<MysqlContext> options) : base(options)
        {

        }

        /// <summary>
        /// 获取context
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="conStr"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static DbContextBase GetDbContext(DbType dbtype, string conStr, string version)
        {
            switch (dbtype)
            {
                default:
                case DbType.Mysql:
                    var builder = new DbContextOptionsBuilder<MysqlContext>();
                    builder.UseMySql(conStr, ServerVersion.Parse(version));
                    builder.LogTo(Console.WriteLine);
                    var options = builder.Options;
                    return new MysqlContext(options);
            }
            
        }

    }
}
