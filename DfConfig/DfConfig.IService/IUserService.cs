using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DfConfig.Model.Classes.User;
using DfGeneral.RequestResponse;
using DfHelper.EF.Base;
using Microsoft.EntityFrameworkCore;

namespace DfConfig.IService;

/// <summary>
/// 用户服务
/// </summary>
public interface IUserService : IBaseService<DbContext>
{
    /// <summary>
    /// 是否第一次登录系统
    /// </summary>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<bool> GetIsFirstLogin(CancellationToken ctsToken = default);

    /// <summary>
    /// 创建账户
    /// </summary>
    /// <param name="loginId"></param>
    /// <param name="password"></param>
    /// <param name="isAdmin"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<Rr<bool>> CreateUser(string loginId, string password, bool isAdmin, CancellationToken ctsToken);

    /// <summary>
    /// 修改用户账号名称
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="loginId"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<Rr<bool>> AlterUserLoginId(int userId, string loginId, CancellationToken ctsToken);

    /// <summary>
    /// 修改用户密码
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<Rr<bool>> AlterUserPassword(int userId, string password, CancellationToken ctsToken);

}
