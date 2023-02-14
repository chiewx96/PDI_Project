using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using PDI_Feather_Tracking_API.Models;

namespace PDI_Feather_Tracking_API.Controllers
{
    // [Controller("User")]
    [Route("handheld")]
    [ApiController]
    public class HandheldController : Controller
    {
        //private readonly InventoryDbContext dbContext = new InventoryDbContext();

        [HttpPost("get-details")]
        public ActionResult GetPackageDetailByReferenceNumber(string referenceNo)
        {
            return View();
        }
    }
}
