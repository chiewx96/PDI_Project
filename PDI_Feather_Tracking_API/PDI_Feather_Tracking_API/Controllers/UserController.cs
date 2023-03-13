using EFWeightScan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_API.Models.RequestModel;
using PDI_Feather_Tracking_API.Models.ResponseModel;
using PDI_Feather_Tracking_API.Services;

namespace PDI_Feather_Tracking_API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public ActionResult Login(Object? obj)
        {
            LoginModel loginModel = (LoginModel)obj;
            return Ok(_userService.TryLogin(loginModel));
        }


        [HttpPost("logout")]
        public ActionResult Logout()
        {
            return Ok(_userService.Logout());
        }


        [HttpPost("logout-all")]
        public ActionResult LogoutAll()
        {
            return Ok(_userService.LogoutAll());
        }
    }
}
