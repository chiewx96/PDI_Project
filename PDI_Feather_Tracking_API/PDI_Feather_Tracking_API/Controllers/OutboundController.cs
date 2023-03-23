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
            var token = (HttpContext.User.Identity as ClaimsIdentity).FindFirst("id").Value;
            return Ok(_outboundService.Outbound(referenceNo, null));
        }

        //[HttpGet("get-details/{referenceNo}")]
        //public ActionResult GetPackageDetailByReferenceNumber(bool newBatch)
        //{
        //    return Ok(_outboundService.GetPackageDetailByReferenceNumber(referenceNo));
        //}
    }
}
