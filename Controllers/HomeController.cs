using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectC_v2.Database;
using ProjectC_v2.Models;

namespace ProjectC_v2.Controllers
{
    public class HomeController : Controller
    {
        private WebshopDbContext _db;

        public HomeController(WebshopDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
