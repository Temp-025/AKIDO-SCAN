using EnterpriseGatewayPortal.Web.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [Authorize]
    public class LogoutController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly LoginHelper _helper;
        public LogoutController(IConfiguration configuration, LoginHelper helper) 
        {
            _configuration = configuration;
            _helper = helper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (Request.Cookies.ContainsKey("MyCookieName"))
            {
                var cookieValue = Request.Cookies["MyCookieName"];
                Response.Cookies.Delete("MyCookieName");
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]

        public async Task<IActionResult> Index1()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }

            var state = Guid.NewGuid().ToString("N");

            //HttpContext.Session.Remove("Nonce");
            //HttpContext.Session.SetString("state", state);

            var idToken = "";
            var isOpenId = _configuration.GetValue<bool>("OpenId_Connect");
            if (isOpenId)
            {
                idToken = HttpContext.User.Claims.FirstOrDefault(c => c.Type ==
                        "ID_Token").Value;
            }
            return Redirect(await _helper.GetLogoutUrl(idToken, state));
        }
        [HttpGet]
        public async Task<IActionResult> Index2()
        {
            if (Request.Cookies.ContainsKey("MyCookieName"))
            {
                var cookieValue = Request.Cookies["MyCookieName"];
                Response.Cookies.Delete("MyCookieName");
            }
            if (HttpContext.User.Claims.FirstOrDefault(c => c.Type ==
                        "IDPLogin").Value == "true")
            {
                return RedirectToAction("Index1", "Logout");
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

    }
}
