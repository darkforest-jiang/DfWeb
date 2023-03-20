using DfConfig.IService;
using DfConfig.Model.Admin;
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
        public async ValueTask<Rr<IList<App>?>> GetApps()
        {
            var result =  await _adminService.GetApps();
            return new Rr<IList<App>?> { 
                Result = result
            };
        }
    }
}
