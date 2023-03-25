using Microsoft.AspNetCore.Mvc;

namespace IPAddressManagement.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected string GetCurrentUser()
        {
            return "";
        }
    }
}
