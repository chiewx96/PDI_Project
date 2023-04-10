using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using PDI_Feather_Tracking_API.Models.RequestModel;
using PDI_Feather_Tracking_API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PDI_Feather_Tracking_API.Controllers
{
    [ApiController]
    [Route("api/outbound")]
    public class OutboundController : Controller
    {
        private readonly OutboundService _outboundService;

        public OutboundController(OutboundService outboundService)
        {
            _outboundService = outboundService;
        }

        [HttpGet("outbound/{referenceNo}")]
        public ActionResult Outbound(string referenceNo)
        {
            try
            {
                string user_id = get_user_id();
                if (user_id == null)
                    return Unauthorized((false, "error : user is not logged in."));
                var response = _outboundService.Outbound(referenceNo, user_id);
                if (response.status)
                    return Ok(response);
                else
                    return Conflict(response);

            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Outbound(OutboundRequestModel requestModel)
        {
            string error_string = string.Empty;
            try
            {
                if (requestModel == null)
                    error_string = "Request Model cannot be null or empty.";
                else if (string.IsNullOrEmpty(requestModel.ContainerId))
                    error_string = "Container id cannot be empty.";
                else if (requestModel.PackageReferenceNo == null || requestModel.PackageReferenceNo.Count == 0)
                    error_string = "Reference No list cannot be empty.";
                else
                {
                    string user_id = get_user_id();
                    var response = _outboundService.Outbound(requestModel, user_id);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                error_string = ex.Message;
            }
            return ValidationProblem(error_string);
        }

        private string get_user_id()
        {
            return (HttpContext.User.Identity as ClaimsIdentity).FindFirst("id").Value;
        }
    }
}
