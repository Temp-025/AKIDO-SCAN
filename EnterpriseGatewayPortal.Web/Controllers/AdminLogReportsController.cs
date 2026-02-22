using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.ExtensionMethods;
using EnterpriseGatewayPortal.Web.Models;
using EnterpriseGatewayPortal.Web.Utilities;
using EnterpriseGatewayPortal.Web.ViewModel.AdminLogReports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [Authorize]
    public class AdminLogReportsController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IAdminLogReportsService _adminlogReportService;
        private readonly Core.Utilities.BackgroundService _backgroundService;
        private readonly IEmailSenderService _emailSender;
        private readonly IAdminLogService _adminLogService;
        private readonly ILogger<AdminLogReportsController> _logger;
        public AdminLogReportsController(IEmailSenderService emailSender,
            IAdminLogReportsService adminlogReportService,
            Core.Utilities.BackgroundService backgroundService,
            IConfiguration configuration,
            ILogger<AdminLogReportsController> logger,
            IAdminLogService adminLogService) : base(adminLogService)
        {
            _emailSender = emailSender;
            _adminlogReportService = adminlogReportService;
            _backgroundService = backgroundService;
            _configuration = configuration;
            _logger = logger;
            _adminLogService = adminLogService;
        }

        [HttpGet]

        public IActionResult Reports()
        {
            return View(new AdminLogReportViewModel());
        }

        [HttpGet]
        public async Task<string[]> GetAdminLogNames(string searchUser)
        {
            return await _adminLogService.GetAllAdminLogNames(searchUser);
        }


        [HttpGet]

        public async Task<IActionResult> ReportsByPage(int page)
        {
            string logMessage;

            var definition = new
            {
                UserName = "",
                ModuleName = "",
                StartDate = "",
                EndDate = "",
                PerPage = 0,
                TotalCount = 0
            };

            var json = TempData["SearchAdminReports"] as string;

            if (string.IsNullOrEmpty(json))
                throw new InvalidOperationException("SearchAdminReports Temp data cannot be null.");

            var searchDetails = JsonConvert.DeserializeAnonymousType(json, definition);
            TempData.Keep("SearchAdminReports");

            var localLogReports = await _adminLogService.GetLocalAdminLogReportsAsync(searchDetails.StartDate,
                        searchDetails.EndDate,
                        searchDetails.UserName,
                        searchDetails.ModuleName,
                        page,
                        searchDetails.PerPage);
            if (localLogReports == null)
            {
                // Push the log to Admin Log Server
                logMessage = $"Failed to get admin reports";
                SendAdminLog(ModuleNameConstants.AdminReports, ServiceNameConstants.AdminReports,
                    "Get Admin Reports", LogMessageType.FAILURE.GetValue(), logMessage, UUID, Email);

                return NotFound();
            }


            AdminLogReportViewModel viewModel = new AdminLogReportViewModel
            {
                UserName = searchDetails.UserName,
                ModuleName = Enum.TryParse<ModuleName>(searchDetails.ModuleName, out var outServiceName) ? outServiceName : (ModuleName?)null,
                StartDate = Convert.ToDateTime(searchDetails.StartDate),
                EndDate = Convert.ToDateTime(searchDetails.EndDate),
                PerPage = searchDetails.PerPage,
                Reports = localLogReports
            };

            // Push the log to Admin Log Server
            logMessage = $"Successfully received admin reports";
            SendAdminLog(ModuleNameConstants.AdminReports, ServiceNameConstants.AdminReports,
                "Get Admin Reports", LogMessageType.SUCCESS.GetValue(), logMessage, UUID, Email);

            return View("Reports", viewModel);
        }


        [HttpGet]

        public JsonResult ExportAdminReports(string exportType)
        {
            string logMessage;

            var definition = new
            {
                UserName = "",
                ModuleName = "",
                StartDate = "",
                EndDate = "",
                PerPage = 0,
                TotalCount = 0
            };

            var json = TempData["SearchAdminReports"] as string;

            if (string.IsNullOrEmpty(json))
            {
                throw new InvalidOperationException("SearchAdminReports Temp data cannot be null");
            }

            var searchDetails = JsonConvert.DeserializeAnonymousType(json, definition);
            TempData.Keep("SearchAdminReports");

            if (searchDetails.TotalCount > 1000000)
            {
                // Push the log to Admin Log Server
                logMessage = $"Failed to export admin report";
                SendAdminLog(ModuleNameConstants.AdminReports, ServiceNameConstants.AdminReports,
                    "Export Admin Reports", LogMessageType.FAILURE.GetValue(), logMessage, UUID, Email);

                return Json(new { Status = "Failed", Title = "Export Admin Report", Message = "Cannot export more than 1 million records at a time. Please select another filter" });
            }

            string fullName = base.FullName;
            string email = base.Email;
            _backgroundService.FireAndForgetAsync<DataExportService>(async (sender) =>
            {
                await sender.ExportAdminReportToFile(exportType, fullName, email, searchDetails.StartDate, searchDetails.EndDate,
                    searchDetails.UserName, searchDetails.ModuleName, searchDetails.TotalCount);
            });

            return Json(new { Status = "Success", Title = "Export Admin Report", Message = "Your request has been processed successfully. Please check your email to download the reports" });
        }

        [HttpGet]

        public PartialViewResult ExportTypes()
        {
            return PartialView("_AdminExportTypes");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reports([FromForm] AdminLogReportViewModel viewModel)
        {
            string logMessage;


            string userName = (viewModel?.UserName ?? string.Empty)
                    .Split('(')[0]
                    .Trim();

            if (string.IsNullOrEmpty(userName))
            {
                AlertViewModel alert = new AlertViewModel { Message = ("Username Cannot be Empty") };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                viewModel.Reports = null;
                return View("Reports", viewModel);
            }

            var localLogReports = await _adminLogService.GetLocalAdminLogReportsAsync(viewModel.StartDate!.Value.ToString("yyyy-MM-dd 00:00:00"),
                       viewModel.EndDate!.Value.ToString("yyyy-MM-dd 23:59:59"),
                       userName,
                       viewModel.ModuleName!.GetValue(),
                       perPage: viewModel.PerPage!.Value);

            if (localLogReports == null || localLogReports.Count == 0)
            {
                // Push the log to Admin Log Server
                logMessage = $"Failed to get admin reports";
                SendAdminLog(ModuleNameConstants.AdminReports, ServiceNameConstants.AdminReports,
                    "Get Admin Reports", LogMessageType.FAILURE.GetValue(), logMessage, UUID, Email);

                AlertViewModel alert = new AlertViewModel { Message = ("User Name is not found") };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                viewModel.Reports = null;
                return View("Reports", viewModel);
            }

            // Push the log to Admin Log Server
            logMessage = $"Successfully received admin reports";
            SendAdminLog(ModuleNameConstants.AdminReports, ServiceNameConstants.AdminReports,
                "Get Admin Reports", LogMessageType.SUCCESS.GetValue(), logMessage, UUID, Email);

            if (viewModel is null ||
                viewModel.StartDate is null ||
                viewModel.EndDate is null)
            {
                return NotFound();
            }

            TempData["SearchAdminReports"] = JsonConvert.SerializeObject(
                new
                {

                    UserName = userName,
                    //ModuleName = viewModel.ModuleName.GetValue(),
                    StartDate = viewModel.StartDate.Value.ToString("yyyy-MM-dd 00:00:00"),
                    EndDate = viewModel.EndDate.Value.ToString("yyyy-MM-dd 23:59:59"),
                    PerPage = viewModel.PerPage,
                    TotalCount = localLogReports.TotalCount
                });

            viewModel.Reports = localLogReports;
            return View("Reports", viewModel);
        }


    }
}
