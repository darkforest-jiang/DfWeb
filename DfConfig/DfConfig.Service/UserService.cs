using DfConfig.IService;
using DfConfig.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DfHelper.EF.Base;
using DfConfig.Service.Context;
using Microsoft.EntityFrameworkCore;
using DfConfig.Service.Entities;
using DfGeneral.RequestResponse;

namespace DfConfig.Service;

/// <summary>
/// 用户服务
/// </summary>
public class UserService : BaseService<DbContextBase>, IUserService
{
    public UserService(DbContextBase dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// 是否第一次登录系统
    /// </summary>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<bool> GetIsFirstLogin(CancellationToken ctsToken = default)
    {
        var isExists = await _DbSet.Set<TUser>().AnyAsync(ctsToken);
        return !isExists;
    }

    /// <summary>
    /// 创建账户
    /// </summary>
    /// <param name="loginId"></param>
    /// <param name="password"></param>
    /// <param name="isAdmin"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<Rr<bool>> CreateUser(string loginId, string password, bool isAdmin, CancellationToken ctsToken)
    {
        var isExists = await AnyAsync<TUser>(p => p.LoginId == loginId, ctsToken);
        if(isExists)
        {
            return new Rr<bool> { 
                IsSuccess = false,
                Message = "账户名已存在"
            };
        }
        var user = new TUser(loginId, password, isAdmin);
        await EntityAdd(user, ctsToken);
        return new Rr<bool> { };
    }

    /// <summary>
    /// 修改用户账号名称
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="loginId"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<Rr<bool>> AlterUserLoginId(int userId, string loginId, CancellationToken ctsToken)
    {
        var user = await FirstOrDefault<TUser>(p => p.Id == userId, ctsToken);
        if (user == null)
        {
            return new Rr<bool>
            {
                IsSuccess = false,
                Message = "账户不存在"
            };
        }
        if (user.LoginId != loginId)
        {
            user.LoginId = loginId;
            await EntityEdit(user, ctsToken);
        }
        return new Rr<bool> { };
    }

    /// <summary>
    /// 修改用户密码
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <param name="ctsToken"></param>
    /// <returns></returns>
    public async Task<Rr<bool>> AlterUserPassword(int userId, string password, CancellationToken ctsToken)
    {
        var user = await FirstOrDefault<TUser>(p => p.Id == userId, ctsToken);
        if(user == null)
        {
            return new Rr<bool> { 
                IsSuccess = false,
                Message = "账户不存在"
            };
        }
        if(user.Password != password)
        {
            user.Password = password;
            await EntityEdit(user, ctsToken);
        }
        return new Rr<bool> { };
    }

}
