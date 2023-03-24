using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDI_Feather_Tracking_API.Models.RequestModel;
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

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login(LoginModel loginModel)
        {
            //LoginModel loginModel = (LoginModel)obj;
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

        [AllowAnonymous]
        [HttpPost("getSampleLogin")]
        public ActionResult getSampleLogin(LoginModel user)
        {
            if (user.username == "admin" && user.password == "admin")
            {
                return Ok(TokenService.GenerateToken("id", "admin"));
            }
            return Unauthorized();

        }
    }
}
