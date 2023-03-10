using EFWeightScan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_API.Models.RequestModel;
using PDI_Feather_Tracking_API.Models.ResponseModel;
using PDI_Feather_Tracking_API.Services.Services;

namespace PDI_Feather_Tracking_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService userService;

        [HttpPost("login")]
        public BooleanMessageModel Login(LoginModel loginModel)
        {
            return userService.TryLogin(loginModel);
        }

    }
}
