using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Web.Attribute;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.ViewModel.UsageReports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [ServiceFilter(typeof(SessionValidationAttribute))]
    [Authorize]
    public class UsageReportsController : BaseController
    {
        private readonly ILogger<UsageReportsController> _logger;
        private readonly IUsageReportsService _usageReportsService;
        public UsageReportsController(IAdminLogService adminLogService, IUsageReportsService usageReportsService, ILogger<UsageReportsController> logger) : base(adminLogService)
        {
            _logger = logger;
            _usageReportsService = usageReportsService;

        }
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new OrganizationUsageReportUsageViewModel();
            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Index(OrganizationUsageReportUsageViewModel viewModel)
        {
            string logMessage;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var organizationUsageReports = await _usageReportsService.GetOrganizationUsageReports(organizationUid, viewModel.Year);

            if (organizationUsageReports == null)
            {

                logMessage = $"Failed to get Usage Reports.";
                SendAdminLog(ModuleNameConstants.Payments, ServiceNameConstants.Payments,
                    "Get Organization Usage Reports", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                return NotFound();
            }

            logMessage = $"Successfully received Usage Reports.";
            SendAdminLog(ModuleNameConstants.Payments, ServiceNameConstants.Payments,
                "Get Organization Usage Reports", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

            viewModel.OrgUsageReports = new List<OrgUsageReportsViewModel>();
            foreach (var report in organizationUsageReports)
            {
                var model = new OrgUsageReportsViewModel()
                {
                    Id = report.Id,
                    OrgName = OrganizationName,
                    OrganizationId = report.OrganizationId,
                    ReportMonth = report.ReportMonth,
                    ReportYear = report.ReportYear,
                    CreatedOn = report.CreatedOn,
                    TotalInvoiceAmount = report.TotalInvoiceAmount,
                };

                viewModel.OrgUsageReports.Add(model);
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<string> DownloadUsageReport(int reportId)
        {
            return await _usageReportsService.DownloadUsageReport(reportId);
        }

        [HttpGet]
        public async Task<JsonResult> DownloadCurrentMonthOrganizationUsageReport()
        {
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var orgName = OrganizationName;
            var currentDate = DateTime.Now;
            var currentMonth = currentDate.Month;
            var currentYear = currentDate.Year;
            var orgNameWithDate = OrganizationName + '_' + currentYear + '-' + currentMonth;

            var response = await _usageReportsService.DownloadCurrentMonthUsageReport(organizationUid);
            if (response.Success)
            {
                return Json(new { Status = response.Success, Message = response.Message, Result = response.Resource, Organization_Name = orgNameWithDate });
            }
            else
            {
                return Json(new { Status = response.Success, Message = response.Message, Result = string.Empty });
            }
        }

    }
}
