using System;
using System.Collections.Generic;

namespace DfConfig.Service.Models;

public partial class TUser
{
    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="loginId"></param>
    /// <param name="password"></param>
    /// <param name="isAdmin"></param>
    public TUser(string loginId, string password, bool isAdmin)
    {
        LoginId = loginId;
        Password = password;
        IsAdmin = isAdmin;
    }

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
