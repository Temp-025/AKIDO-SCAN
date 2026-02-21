//using EnterpriseGatewayPortal.Web.ViewModel.KycLogReports;
namespace EnterpriseGatewayPortal.Web.Controllers
{

    //[Route("[controller]")]
    //[Authorize]
    //public class KYCMethodsController : BaseController
    //{
    //    private readonly IConfiguration _configuration;

    //    private readonly Core.Utilities.BackgroundService _backgroundService;
    //    private readonly IEmailSenderService _emailSender;
    //    private readonly IAdminLogService _adminLogService;
    //    private IKYCLogReportsService _kYCLogReportsService;
    //    private readonly IKYCMethodsService _kYCMethodsService;
    //    private readonly ILogger<AdminLogReportsController> _logger;
    //    public KYCMethodsController(IEmailSenderService emailSender,

    //        Core.Utilities.BackgroundService backgroundService,
    //        IKYCLogReportsService kYCLogReportsService,
    //        IConfiguration configuration,
    //        ILogger<AdminLogReportsController> logger,
    //        IKYCMethodsService kYCMethodsService,
    //        IAdminLogService adminLogService) : base(adminLogService)
    //    {
    //        _emailSender = emailSender;

    //        _backgroundService = backgroundService;
    //        _kYCLogReportsService = kYCLogReportsService;
    //        _configuration = configuration;
    //        _logger = logger;
    //        _adminLogService = adminLogService;
    //        _kYCMethodsService = kYCMethodsService;
    //    }
    //    public async Task<IActionResult> Index()
    //    {
    //        var orgId=_configuration["KycOrganizationUid"];
    //        var orgName = _configuration["KycOrganizationName"];

    //        var methods = await _kYCLogReportsService.GetKycMethodsListAysnc(orgId);

    //        var viewModel = new KycMethodViewModel
    //        {
    //            orgName = orgName,
    //            Methods = methods.Select(m => new KycMethodItem
    //            {
    //                MethodName = m,
    //                Pricing = 5.25M,    
    //                Status = "Active"   
    //            }).ToList()
    //        };

    //        return View(viewModel);
    //    }
    //    public async Task<IActionResult> RequestVerificationMethod()
    //    {


    //        var orgId = _configuration["KycOrganizationUid"];
    //        var orgName = _configuration["KycOrganizationName"];
    //        var KycMethods = await _kYCMethodsService.GetKycMethodsListAysnc(orgId);
    //        var verificationStatsResponse = await _kYCMethodsService.GetVerificationMethodsStatsAysnc(orgId);

    //        VerificationMethodsStatsItem verificationStats = null;

    //        if (verificationStatsResponse is APIResponse apiResponse && apiResponse.Success)
    //        {
    //            var resultJson = JsonConvert.SerializeObject(apiResponse.Result);
    //            verificationStats = JsonConvert.DeserializeObject<VerificationMethodsStatsItem>(resultJson);
    //        }
    //        else
    //        {
    //            verificationStats = new VerificationMethodsStatsItem();
    //        }
    //        var methods = KycMethods;

    //        var viewModel = new KycMethodViewModel
    //        {
    //            orgName = orgName,
    //            Stats = verificationStats,
    //            Methods = KycMethods
    //                .Where(m => m.Requested)
    //                .Select(m => new KycMethodItem
    //                {
    //                    MethodUid = m.MethodUid,
    //                    MethodName = m.MethodName,
    //                    Pricing = m.Pricing,
    //                    Status = m.Status,
    //                    Description = m.Description,
    //                    MethodType = m.MethodType,
    //                    TargetSegments = m.TargetSegments,
    //                    ProcessingTime = m.ProcessingTime,
    //                    ConfidenceThreshold = m.ConfidenceThreshold,
    //                    RequestStatus = m.RequestStatus,
    //                    IsRequested = m.Requested,
    //                    MandatoryAttributes = m.MandatoryAttributes,
    //                    OptionalAttributes = m.OptionalAttributes,
    //                })
    //                .ToList(),

    //        };
    //        return View(viewModel);
    //    }

    //    public async Task<IActionResult> AllVerificationMethods()
    //    {
    //        var orgId = _configuration["KycOrganizationUid"];
    //        var KycMethods = await _kYCMethodsService.GetKycMethodsListAysnc(orgId);

    //        var viewModel = new KycMethodViewModel
    //        {
    //            Methods = KycMethods
    //                .Where(m => !m.Requested)
    //                .Select(m => new KycMethodItem
    //                {
    //                    MethodUid = m.MethodUid,
    //                    MethodName = m.MethodName,
    //                    Pricing = m.Pricing,
    //                    Status = m.Status,
    //                    Description = m.Description,
    //                    MethodType = m.MethodType,
    //                    TargetSegments = m.TargetSegments,
    //                    ProcessingTime = m.ProcessingTime,
    //                    ConfidenceThreshold = m.ConfidenceThreshold,
    //                    RequestStatus = m.RequestStatus,
    //                    IsRequested = m.Requested,
    //                    MandatoryAttributes = m.MandatoryAttributes,
    //                    OptionalAttributes = m.OptionalAttributes,
    //                })
    //                .ToList(),

    //        };

    //        return View(viewModel);
    //    }

    //    public async Task<IActionResult> VerificationMethods(string status)
    //    {
    //        var orgId = _configuration["KycOrganizationUid"];
    //        var KycMethods = await _kYCMethodsService.GetKycMethodsListAysnc(orgId);

    //        var viewModel = new KycMethodViewModel
    //        {
    //            Methods = KycMethods
    //                .Where(m => m.Requested)
    //                .Select(m => new KycMethodItem
    //                {
    //                    MethodUid = m.MethodUid,
    //                    MethodName = m.MethodName,
    //                    Pricing = m.Pricing,
    //                    Status = m.Status,
    //                    Description = m.Description,
    //                    MethodType = m.MethodType,
    //                    TargetSegments = m.TargetSegments,
    //                    ProcessingTime = m.ProcessingTime,
    //                    ConfidenceThreshold = m.ConfidenceThreshold,
    //                    RequestStatus = m.RequestStatus,
    //                    IsRequested = m.Requested,
    //                })
    //                .ToList(),
    //                filterStatus = status

    //        };

    //        return View(viewModel);
    //    }

    //    public async Task<IActionResult> VerificationMethodStatus(string methodUid)
    //    {
    //        var orgId = _configuration["KycOrganizationUid"];

    //        var methodStatusResponse = await _kYCMethodsService.GetOrganizationVerificationMethodByUidAsync(orgId, methodUid);

    //        return View(methodStatusResponse);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> SubmitKYCMethodRequest(string methodUid)
    //    {
    //        var orgId = _configuration["KycOrganizationUid"];
    //        var result = await _kYCMethodsService.RequestKYCMethodAsync(orgId, methodUid);
    //        return Json(new { success = true, message = "KYC Method request submitted successfully.", data = result });
    //    }

    //    public async Task<IActionResult> GetActiveMethods()
    //    {
    //        var orgId = _configuration["KycOrganizationUid"];
    //        var methods = await _kYCMethodsService.GetOrganizationDefaultMethods(orgId);

    //        var activeMethods = methods
    //            .Where(m => string.Equals(m.Status, "Active", StringComparison.OrdinalIgnoreCase))
    //            .Select(m => new KycMethodItem
    //            {
    //                MethodUid = m.MethodUid,
    //                MethodName = m.MethodName,
    //                Pricing = m.Pricing,
    //                Status = m.Status,
    //                Description = m.Description,
    //                MethodType = m.MethodType,
    //                TargetSegments = m.TargetSegments,
    //                ProcessingTime = m.ProcessingTime,
    //                ConfidenceThreshold = m.ConfidenceThreshold,
    //                RequestStatus = m.RequestStatus,
    //                IsRequested = m.Requested,
    //                MandatoryAttributes = m.MandatoryAttributes,
    //                OptionalAttributes = m.OptionalAttributes,
    //                RequestedDate = m.RequestedDate,
    //                ModifiedDate = m.ModifiedDate
    //            })
    //            .ToList();

    //        var viewModel = new ActiveMethodsViewModel
    //        {
    //            ActiveMethods = activeMethods
    //        };

    //        return PartialView("_ActiveMethodsPartial", viewModel);
    //    }

    //    public async Task<IActionResult> RequestNewMethods()
    //    {
    //        var orgId = _configuration["KycOrganizationUid"];
    //        var methods = await _kYCMethodsService.GetPendingVerificationMethods(orgId);
    //        var viewModel = new RequestNewMethodsViewModel
    //        {
    //            RequestNewMethods = methods
    //                .Select(m => new KycMethodItem
    //                {
    //                    MethodUid = m.MethodUid,
    //                    MethodName = m.MethodName,
    //                    Pricing = m.Pricing,
    //                    Status = m.Status,
    //                    Description = m.Description,
    //                    MethodType = m.MethodType,
    //                    TargetSegments = m.TargetSegments,
    //                    ProcessingTime = m.ProcessingTime,
    //                    ConfidenceThreshold = m.ConfidenceThreshold,
    //                    RequestStatus = m.RequestStatus,
    //                    IsRequested = m.Requested,
    //                    MandatoryAttributes = m.MandatoryAttributes,
    //                    OptionalAttributes = m.OptionalAttributes
    //                })
    //                .ToList()
    //        };
    //        return PartialView("_RequestNewMethodsPartial", viewModel);
    //    }

    //    public async Task<IActionResult> AllRequestedMethods()
    //    {
    //        var orgId = _configuration["KycOrganizationUid"];
    //        var methods = await _kYCMethodsService.GetAllOrganizationRequestedMethods(orgId);
    //        var allRequestedMethods = methods
    //            .Where(m => m.Requested)
    //            .Select(m => new KycMethodItem
    //            {
    //                MethodUid = m.MethodUid,
    //                MethodName = m.MethodName,
    //                Pricing = m.Pricing,
    //                Status = m.Status,
    //                Description = m.Description,
    //                MethodType = m.MethodType,
    //                TargetSegments = m.TargetSegments,
    //                ProcessingTime = m.ProcessingTime,
    //                ConfidenceThreshold = m.ConfidenceThreshold,
    //                RequestStatus = m.RequestStatus,
    //                IsRequested = m.Requested,
    //                MandatoryAttributes = m.MandatoryAttributes,
    //                OptionalAttributes = m.OptionalAttributes,
    //                RequestedDate = m.RequestedDate,
    //                ModifiedDate = m.ModifiedDate
    //            })
    //            .ToList();
    //        var viewModel = new AllRequestedMethodsViewModel
    //        {
    //            AllRequestedMethods = allRequestedMethods
    //        };
    //        return PartialView("_AllRequestsPartial", viewModel);
    //    }

    //    public async Task<IActionResult> GetAnalyticsData()
    //    {
    //        var orgId = _configuration["KycOrganizationUid"];
    //        return PartialView("_Analytics");
    //    }

    //}
}