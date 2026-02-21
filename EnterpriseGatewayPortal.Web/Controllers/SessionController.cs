using Microsoft.AspNetCore.Mvc;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            TempData["Message"] = "Session Expired.";
            return RedirectToAction("Index", "Login");
        }
    }
}
