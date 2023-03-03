using DfConfig.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DfGeneral.RequestResponse;
using Microsoft.AspNetCore.Authorization;

namespace DfConfig.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 获取是否第一次登陆
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async ValueTask<Rr<bool>> GetIsFirstLogin()
        {
            var result = await _userService.GetIsFirstLogin();
            return new Rr<bool> { 
                IsSuccess = true,
                Result = result
            };
        }
    }
}
