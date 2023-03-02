using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DfConfig.Model.User;
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
    /// <param name="user"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<bool> CreateUser(UserInfo user, CancellationToken ctsToken);

    /// <summary>
    /// 修改账户
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="newLoginId"></param>
    /// <param name="newPassword"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<bool> EditUser(int userId, string newLoginId, string newPassword, CancellationToken ctsToken);

    /// <summary>
    /// 获取用户
    /// </summary>
    /// <param name="loginId"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<UserInfo?> GetUser(string loginId, CancellationToken ctsToken);

    /// <summary>
    /// 获取所有用户
    /// </summary>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    Task<IList<UserInfo>?> GetUsers(CancellationToken ctsToken);
}
