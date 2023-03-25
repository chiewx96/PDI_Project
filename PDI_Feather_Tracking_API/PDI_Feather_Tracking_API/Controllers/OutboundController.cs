using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
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

        [HttpGet("get-details/{referenceNo}")]
        public ActionResult GetPackageDetailByReferenceNumber(string referenceNo)
        {
            return Ok(_outboundService.GetPackageDetailByReferenceNumber(referenceNo));
        }

        [HttpGet("outbound/{referenceNo}")]
        public ActionResult Outbound(string referenceNo)
        {
            try
            {
                string user_id = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("id").Value;
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

        //[HttpGet("get-details/{referenceNo}")]
        //public ActionResult GetPackageDetailByReferenceNumber(bool newBatch)
        //{
        //    return Ok(_outboundService.GetPackageDetailByReferenceNumber(referenceNo));
        //}
    }
}
