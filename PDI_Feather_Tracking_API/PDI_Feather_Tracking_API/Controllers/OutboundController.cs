using Microsoft.AspNetCore.Mvc;
using PDI_Feather_Tracking_API.Models;
using PDI_Feather_Tracking_API.Models.ResponseModel;
using PDI_Feather_Tracking_API.Services;

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

        [HttpGet("get-details")]
        public ActionResult GetPackageDetailByReferenceNumber(string referenceNo)
        {
            return Ok(_outboundService.GetPackageDetailByReferenceNumber(referenceNo));
        }

        [HttpGet("outbound/{referenceNo}")]
        public ActionResult Outbound(string referenceNo)
        {
            return Ok(_outboundService.Outbound(referenceNo));
        }
    }
}
