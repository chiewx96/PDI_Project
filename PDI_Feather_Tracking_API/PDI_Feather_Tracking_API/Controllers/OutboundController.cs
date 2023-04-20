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

        [HttpGet("cancel/{batch_no}")]
        public ActionResult CancelOutbound(string batch_no)
        {
            string error_string = string.Empty;
            try
            {
                _outboundService.cancelOutbound(batch_no);
                return Ok(batch_no);
            }
            catch (Exception ex)
            {
                error_string = ex.Message;
            }
            return ValidationProblem(error_string);
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
