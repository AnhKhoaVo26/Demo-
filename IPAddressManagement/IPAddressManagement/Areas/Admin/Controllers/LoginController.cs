using Microsoft.AspNetCore.Mvc;

namespace IPAddressManagement.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //GET: /Admi/Login
        public IActionResult Index()
        {
            return View();
        }
    }
}
