using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_API.Models;

namespace PDI_Feather_Tracking_API.Controllers
{
    // [Controller("User")]
    [ApiController]
    public class UserController : Controller
    {
        //private readonly InventoryDbContext dbContext = new InventoryDbContext();

        //// GET: UserController
        //[HttpGet("search")]
        //public List<User>? Index()
        //{
        //    var _list = dbContext.Users.AsNoTracking().ToList();
        //    return _list;
        //}

        //// GET: UserController/Details/5
        //[HttpGet("details/{id}")]
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: UserController/Create
        //[HttpGet("details")]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: UserController/Create
        //[HttpPost("create")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

    
    }
}
