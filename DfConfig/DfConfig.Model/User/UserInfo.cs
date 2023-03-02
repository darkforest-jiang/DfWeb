using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfConfig.Model.User
{
    /// <summary>
    /// 账号信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 主键Id 自增
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginId { get; set; } = null!;

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
