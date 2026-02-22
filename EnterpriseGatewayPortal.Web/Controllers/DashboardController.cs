using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Web.Models.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IClientService _clientService;
        private readonly IStatisticsService _statisticsService;
        private readonly IBussinessUserService _bussinessUserService;
        private readonly ILocalClientService _localClientService;
        private readonly IConfiguration _configuration;
        private readonly ILocalBusinessUsersService _localbusinessUsersService;
        private readonly IDeviceService _deviceService;
        private readonly IAdminLogService _adminLogService;
        private readonly IUserService _userService;
        private readonly IWalletService _walletService;

        public DashboardController(IAdminLogService adminLogService,
            ILogger<DashboardController> logger,
            IClientService clientService,
            IStatisticsService statisticsService,
            IBussinessUserService bussinessUserService,
            IConfiguration configuration,
            ILocalClientService localClientService,
            ILocalBusinessUsersService localBusinessUsersService,
            IWalletService walletService,
            IUserService userService,
            IDeviceService deviceService) : base(adminLogService)

        {
            _logger = logger;
            _clientService = clientService;
            _statisticsService = statisticsService;
            _bussinessUserService = bussinessUserService;
            _configuration = configuration;
            _localClientService = localClientService;
            _localbusinessUsersService = localBusinessUsersService;
            _deviceService = deviceService;
            _adminLogService = adminLogService;
            _userService = userService;
            _walletService = walletService;
        }
        public IActionResult Index()
        {
            var fgd = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return View();
        }
        [HttpGet]
        public async Task<string[]> GetServiceProviderNames(string request)
        {
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var list = await _clientService.getList(organizationUid);
            var clientslist = new List<string>();
            foreach (var item in list)
            {
                if (item.ApplicationName.Contains(request))
                {
                    clientslist.Add(item.ApplicationName);
                }
            }
            return clientslist.ToArray();
        }
        [HttpGet]
        public async Task<string[]> GetAllServiceProviderNames()
        {
            var list = await _localClientService.ListClientAsync();
            var clientslist = new List<string>();
            foreach (var item in list)
            {
                clientslist.Add(item.ApplicationName!);
            }
            return clientslist.ToArray();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GetServiceProviderGraphDetails(string serviceProviderName)
        {
            var graphDetails = await _statisticsService.GetGraphCountAsync(serviceProviderName);
            if (graphDetails == null)
                return Json(new { Status = "Failed", Message = "Failed to get data" });
            else
                return Json(new { Status = "Success", Message = "Successfully received data", Data = graphDetails });
        }
        [HttpGet]
        public async Task<JsonResult> GetOrganizationGraphDetails()
        {
            var graphDetails = await _statisticsService.GetOranizationGraphCountAsync();
            if (graphDetails == null)
                return Json(new { Status = "Failed", Message = "Failed to get data" });
            else
                return Json(new { Status = "Success", Message = "Successfully received data", Data = graphDetails });
        }
        [HttpGet]
        public async Task<JsonResult> GetOrganizationDetails()
        {

            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var usersCount = await _bussinessUserService.GetAllBusinessUserAsync(organizationUid);
            if (usersCount == null)
            {
                return Json(new { Status = "Failed", Message = "Failed to receive data" });
            }
            DetailsViewModel model = new DetailsViewModel();
            model.Users = usersCount.Count();
            var applicationsCount = await _clientService.getList(organizationUid);
            if (applicationsCount == null)
            {
                return Json(new { Status = "Failed", Message = "Failed to receive data" });
            }
            model.Applications = applicationsCount.Count();
            model.Templates = 10;
            return Json(new { Status = "Success", Message = "Successfully received data", Data = model });
        }
        [HttpGet]
        public async Task<JsonResult> GetOrganizationStatistics()
        {
            var Applicationscount = 0;
            var Businessusercount = 0;
            var templatesCount = 0;
            var AdminUsers = 0;
            var activeCredentialsCount = 0;
            var OrganizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var Applicationslist = await _localClientService.ListClientAsync();
            if (Applicationscount > 0)
            {
                Applicationscount = Applicationslist.Count();
            }
            var businessuser = await _localbusinessUsersService.GetAllBusinessUsersByOrgUidAsync(OrganizationUid);
            if (businessuser != null && businessuser.Success)
            {
                var businessuserlist = (IEnumerable<OrgSubscriberEmail>)businessuser.Resource;
                Businessusercount = businessuserlist.Count();
            }
            var activeCredentialsList = await _walletService.GetAllCredentialsList(OrganizationUid);
            if (activeCredentialsList != null)
            {
                activeCredentialsCount = activeCredentialsList.Where(cred => cred.Status == "ACTIVE").Count(); ;
            }


            var adminUsersList = await _userService.ListUsersAsync();
            if (adminUsersList != null && adminUsersList.Count() > 0)
            {
                AdminUsers = adminUsersList.Count();
            }

            return Json(new { applicationscount = Applicationscount, businessusercount = Businessusercount, templatescount = templatesCount, activeCredentialsCount = activeCredentialsCount, adminuserscount = AdminUsers });
        }


        [HttpGet]
        public async Task<PartialViewResult> GetAdminTimeLine(int count = 10)
        {
            try
            {
                var logs = await _adminLogService.GetLatestLogsAsync(count);
                return PartialView("_AdminTimeline", logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving admin timeline logs.");
                return PartialView("_AdminTimelineError");
            }
        }
        public IActionResult Index1()
        {
            ViewBag.isvalid = true;
            return View("Index");
        }

    }
}
