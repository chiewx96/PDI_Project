using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using PDI_Feather_Tracking_API.Models;
using PDI_Feather_Tracking_API.Services.Services;
using PDI_Feather_Tracking_API.Services.ServicesImpl;
using PDI_Feather_Tracking_API.Models.ResponseModel;

namespace PDI_Feather_Tracking_API.Controllers
{
    // [Controller("User")]
    [Route("handheld")]
    [ApiController]
    public class OutboundController : Controller
    {
        private readonly OutboundService _outboundService;

        public OutboundController(OutboundServiceImpl outboundService)
        {
            _outboundService = outboundService;
        }

        [HttpGet("get-details")]
        public InventoryRecords? GetPackageDetailByReferenceNumber(string referenceNo)
        {
            return _outboundService.GetPackageDetailByReferenceNumber(referenceNo);
        }

        [HttpGet("outbound/{referenceNo}")]
        public BooleanMessageModel Outbound(string referenceNo)
        {
            return _outboundService.Outbound(referenceNo);
        }
    }
}
