using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Web.Attribute;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [ServiceFilter(typeof(SessionValidationAttribute))]
    [Authorize]
    public class BalanceSheetsController : BaseController
    {
        private readonly ILogger<BalanceSheetsController> _logger;
        private readonly IBalanceSheetsService _balanceSheetsService;
        public BalanceSheetsController(IAdminLogService adminLogService, IBalanceSheetsService balanceSheetsService, ILogger<BalanceSheetsController> logger) : base(adminLogService)
        {
            _logger = logger;
            _balanceSheetsService = balanceSheetsService;

        }

        [HttpGet]
        public async Task<IActionResult> BalanceList()
        {
            string logMessage;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var balanceSheets = await _balanceSheetsService.GetBalanceSheetDetailsAsync(organizationUid);

            if (balanceSheets == null || !balanceSheets.Success)
            {

                logMessage = $"Failed to get Balance Sheets.";
                SendAdminLog(ModuleNameConstants.Payments, ServiceNameConstants.Payments,
                    "Get Organization Balance sheets", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                AlertViewModel alert = new AlertViewModel { IsSuccess = false, Message = balanceSheets.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("Index", "Dashboard");
            }

            logMessage = $"Successfully received Balance Sheets.";
            SendAdminLog(ModuleNameConstants.Payments, ServiceNameConstants.Payments,
                "Get Organization Balance sheets", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

            return View(balanceSheets.Resource);
        }
    }
}
