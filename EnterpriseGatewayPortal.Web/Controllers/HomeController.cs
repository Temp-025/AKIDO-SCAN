using EnterpriseGatewayPortal.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
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

        public JsonResult SetLayout(string Layout)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Path = "/",
                Expires = DateTime.Now.AddDays(1),
            };
            Response.Cookies.Append("NavigationLayout", Layout, cookieOptions);
            if (Layout == "KYCAdminLayout")
            {
                return Json(new { redirectUrl = Url.Action("Index", "KYCDashboard") });
            }
            return Json(new { redirectUrl = Url.Action("Index", "Dashboard") });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetTheme(string data)
        {
            var cookie = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                HttpOnly = true,
                Secure = Request.IsHttps,
                SameSite = SameSiteMode.Lax
            };

            Response.Cookies.Append("theme", data, cookie);
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}