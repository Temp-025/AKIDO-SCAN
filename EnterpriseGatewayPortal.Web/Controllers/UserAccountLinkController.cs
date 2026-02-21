using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    public class UserAccountLinkController : BaseController
    {

        public UserAccountLinkController(IAdminLogService adminLogService) : base(adminLogService)
        {
        }

        public IActionResult LinkAccount()
        {
            return View();
        }
    }
}
