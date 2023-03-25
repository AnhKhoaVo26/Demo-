using IPAddressManagement.Areas.Admin.Repositories;
using IPAddressManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace IPAddressManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IIpsRepository iPaddresssRepository;

        public HomeController(IIpsRepository iPaddresssRepository, ILogger<HomeController> logger)
        {
            this.iPaddresssRepository = iPaddresssRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AvailableIps()
        {
            var ipaddress = iPaddresssRepository.GetIps().Where(i => i.Status == true);
            return View(ipaddress);
        }

        public IActionResult Contact()
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