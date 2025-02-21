using System.Diagnostics;
using BloodBowlMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodBowlMVC.Controllers
{
    public class RosterController : Controller
    {
        private readonly ILogger<RosterController> _logger;

        public RosterController(ILogger<RosterController> logger)
        {
            _logger = logger;
        }

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
    }
}
