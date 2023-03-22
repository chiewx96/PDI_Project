using EFWeightScan;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PDI_Feather_Tracking_API.Models;
using PDI_Feather_Tracking_API.Models.RequestModel;
using PDI_Feather_Tracking_API.Models.ResponseModel;
using PDI_Feather_Tracking_API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            var aaa = HttpContext.GetTokenAsync("access_token");
            string username = General.ValidateToken(aaa.Result);
            return Ok(_userService.LogoutAll());
        }

        //[AllowAnonymous]
        //[HttpPost("getSampleLogin")]
        //public ActionResult getSampleLogin(LoginModel user)
        //{
        //    if (user.username == "admin" && user.password == "admin")
        //    {
        //        return Ok(General.GenerateToken("admin"));
        //    }
        //    return Unauthorized();

        //}
    }
}
