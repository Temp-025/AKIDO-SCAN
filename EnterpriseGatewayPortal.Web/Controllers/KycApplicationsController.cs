namespace EnterpriseGatewayPortal.Web.Controllers
{
    //public class KycApplicationsController : BaseController
    //{
    //    private readonly ILogger<KycApplicationsController> _logger;
    //    private readonly IConfiguration _configuration;
    //    private readonly IKycApplicationService _kycApplicationService;

    //    public KycApplicationsController(IAdminLogService adminLogService, IConfiguration configuration, IKycApplicationService kycApplicationService, ILogger<KycApplicationsController> logger) : base(adminLogService)
    //    {
    //        _logger = logger;
    //        _configuration = configuration;
    //        _kycApplicationService = kycApplicationService;
    //    }
    //    public async Task<IActionResult> Index()
    //    {
    //        string logMessage;
    //        var orgId = _configuration["KycOrganizationUid"];
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var response = await _kycApplicationService.GetKycApplicationsListByOrgId(orgId);
    //        if (response == null)
    //        {

    //            logMessage = $"Failed to get kyc application list.";
    //            SendAdminLog(ModuleNameConstants.Payments, ServiceNameConstants.Payments,
    //                "Get kyc applications list failed", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //            AlertViewModel alert = new AlertViewModel { IsSuccess = false, Message = "Failed to get kyc application list." };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);

    //            return RedirectToAction("Index", "Dashboard");
    //        }
    //        var list = (List<KycApplicationDTO>)response.Resource;


    //        KycApplicationListViewModel viewModel = new KycApplicationListViewModel()
    //        {
    //            KycApplications = list
    //        };

    //        return View(list);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> Create()
    //    {

    //        return View();
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> Edit(int id)
    //    {
    //        var orgId = _configuration["KycOrganizationUid"];
    //        var orgName= _configuration["KycOrganizationName"];
    //        var response = await _kycApplicationService.GetKycApplicationById(id);
    //        var details = (KycApplicationDTO)response.Resource;

    //        if (response == null)
    //        {
    //            return NotFound();
    //        }

    //        var viewModel = new KycApplicationNewViewModel()
    //        {
    //            Id = details.Id,
    //            ApplicationName = details.ApplicationName,
    //            OrganizationId = details.OrganizationId,
    //            ClientId = details.ClientId,
    //            ClientSecret = details.ClientSecret,
    //            OrgName = orgName

    //        };

    //        SendAdminLog(ModuleNameConstants.Applications, ServiceNameConstants.Applications, "View kyc Applications details",
    //        LogMessageType.SUCCESS.ToString(), "Get kyc Applications details of " + details.ApplicationName + " successfully ", UUID, Email);

    //        return View(viewModel);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Save(KycApplicationNewViewModel viewModel)
    //    {
    //        var orgId = _configuration["KycOrganizationUid"];
    //        var dto = new KycApplicationDTO()
    //        {
    //            OrganizationId = orgId,
    //            ApplicationName= viewModel.ApplicationName,
    //        };
    //        var response = await _kycApplicationService.SaveKycApplication(dto);

    //        if (response == null || !response.Success)
    //        {
    //            AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);
    //            return RedirectToAction("Index");
    //        }
    //        else
    //        {
    //            SendAdminLog(ModuleNameConstants.Applications, ServiceNameConstants.Applications, "Create new kyc Applications", LogMessageType.SUCCESS.ToString(), "Created New kyc Application with application name " + viewModel.ApplicationName + " Successfully", UUID, Email);

    //            AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);
    //            return RedirectToAction("Index");
    //        }

    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> Update(KycApplicationNewViewModel viewModel)
    //    {
    //        var orgId = _configuration["KycOrganizationUid"];
    //        var dto = new KycApplicationDTO()
    //        {
    //            Id = viewModel.Id,
    //            ClientId = viewModel.ClientId,
    //            ClientSecret = viewModel.ClientSecret,
    //            OrganizationId = orgId,
    //            ApplicationName = viewModel.ApplicationName,
    //        };
    //        var response = await _kycApplicationService.UpdateKycApplication(dto);

    //        if (response == null || !response.Success)
    //        {
    //            AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);
    //            return RedirectToAction("Index");
    //        }
    //        else
    //        {
    //            SendAdminLog(ModuleNameConstants.Applications, ServiceNameConstants.Applications, "update Applications", LogMessageType.SUCCESS.ToString(), "Updated kyc Application with application name " + viewModel.ApplicationName + " Successfully", UUID, Email);

    //            AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);
    //            return RedirectToAction("Index");
    //        }

    //    }
    //}
}
