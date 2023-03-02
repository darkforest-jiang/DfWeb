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
using DfConfig.Service.Models;

namespace DfConfig.Service;

/// <summary>
/// 用户服务
/// </summary>
public class UserService : BaseService<DbContextBase>, IUserService
{
    public UserService(DbContextBase dbContext) : base(dbContext)
    {
    }

    public Task<bool> CreateUser(UserInfo user, CancellationToken ctsToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EditUser(int userId, string newLoginId, string newPassword, CancellationToken ctsToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> GetIsFirstLogin(CancellationToken ctsToken = default)
    {
        var isExists = await _DbSet.Set<TUser>().AnyAsync(ctsToken);
        return !isExists;
    }

    public Task<UserInfo?> GetUser(string loginId, CancellationToken ctsToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<UserInfo>?> GetUsers(CancellationToken ctsToken)
    {
        throw new NotImplementedException();
    }
}
