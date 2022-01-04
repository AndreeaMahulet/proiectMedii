using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using project.Models;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models.LibraryViewModels;

namespace project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CompanyContext _context;
        public HomeController(CompanyContext context)
        {
            _context = context;
        }
        //public HomeController(ILogger<HomeController> logger)
       // {
           // _logger = logger;
       // }

        public IActionResult Index()
        {
            return View();
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
        public async Task<ActionResult> Statistics()
        {
            IQueryable<ManagerGroup> data =
            from manager in _context.Managers
            group manager by manager.ManagerSinceDate into dateGroup
            select new ManagerGroup()
            {
                StartDate = dateGroup.Key,
                EmployeeCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }
    }
}
