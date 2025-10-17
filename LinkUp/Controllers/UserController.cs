using System.Diagnostics;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountServiceForWebApp _service;

        public UserController(ILogger<HomeController> logger, IAccountServiceForWebApp service)
        {
            _logger = logger;
            _service = service;
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
