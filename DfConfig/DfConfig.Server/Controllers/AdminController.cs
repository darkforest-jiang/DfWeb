using DfConfig.IService;
using DfConfig.Model;
using DfGeneral.RequestResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DfConfig.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// 获取App列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async ValueTask<Rr<IList<AppInfo>?>> GetAppList()
        {
            var result =  await _adminService.GetAppList();
            return new Rr<IList<AppInfo>?> { 
                Result = result
            };
        }
    }
}
