using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Profi.DAL;
using Profi.Models;
using Profi.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Profi.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _db { get;}


        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;

        }

        public IActionResult Index()
        {
            HomeVM home = new HomeVM
            {
                Testimonials = _db.Testimonials.ToList()
            };
            return View(home);
        }

        public IActionResult Privacy()
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
