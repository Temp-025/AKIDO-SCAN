namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[ServiceFilter(typeof(SessionValidationAttribute))]
    //[Authorize]
    //public class SignatureDelegationController : BaseController
    //{
    //    private readonly IDelegationService _delegatorService;
    //    private readonly ILocalBusinessUsersService _localBusinessUsersService;
    //    private readonly IConfiguration _configuration;
    //    private IWebHostEnvironment _environment;
    //    private readonly ILogger<SignatureDelegationController> _logger;
    //    public SignatureDelegationController(IAdminLogService adminLogService, IWebHostEnvironment environment, IDelegationService delegatorService, ILocalBusinessUsersService localBusinessUsersService, ILogger<SignatureDelegationController> logger) : base(adminLogService)
    //    {
    //        _delegatorService = delegatorService;
    //        _environment = environment;
    //        _logger = logger;
    //        _localBusinessUsersService = localBusinessUsersService;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> List()
    //    {
    //        string logMessage;
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //        var response = await _delegatorService.GetDelegatesListByOrgIdAndSuidAsync(apiToken);
    //        if (!response.Success)
    //        {
    //            return Json(new { Status = "Error", Title = "Error", Message = response.Message });
    //        }
    //        var delegationList = (List<DelegatorListDTO>)response.Resource;
    //        var viewModel = new ListViewModel()
    //        {
    //            DelegationList = delegationList
    //        };
    //        logMessage = $"Successfully received the Signature Delegators List From External DB";
    //        SendAdminLog(ModuleNameConstants.SignatureDelegation, ServiceNameConstants.SignatureDelegation,
    //            "Get signature delegators list", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
    //        return View(viewModel);
    //        //return View();

    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> CreateDelegation()
    //    {
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //        var response = await _delegatorService.GetBusinessUsersListByOrgAsync(apiToken);
    //        if (!response.Success)
    //        {
    //            return Json(new { Status = "Error", Title = "Error", Message = response.Message });
    //        }

    //        var jResponse = response.Resource as List<DelegateBusinessUserDTO>;
    //        var viewModel = new AddDelegationViewModel();
    //        viewModel.Emails = jResponse.Select(item => item.Email).ToList();
    //        return View(viewModel);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> AddDelegation([FromBody] AddDelegationViewModel model)
    //    {
    //        try
    //        {
    //            String logMessage;
    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = await _delegatorService.GetBusinessUsersListByOrgAsync(apiToken);
    //            if (!response.Success)
    //            {
    //                return Json(new { Status = "Error", Title = "Error", Message = response.Message });
    //            }
    //            var jsonResponse = response.Resource;
    //            var a = OrganizationName;
    //            var delegationUsersList = response.Resource as List<DelegateBusinessUserDTO>;

    //            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //            var businessUserList = await _localBusinessUsersService.GetAllBusinessUsersByOrgUidAsync(organizationUid);
    //            var businessUser = (IEnumerable<OrgSubscriberEmail>)businessUserList.Resource;

    //            var delegates = new List<Core.DTOs.DelegateRecep>();
    //            var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
    //            var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);


    //            //DateTime startDateTimeUtc = DateTime.SpecifyKind(model.StartDateTime, DateTimeKind.Utc);
    //            //DateTime endDateTimeUtc = DateTime.SpecifyKind(model.EndDateTime, DateTimeKind.Utc);

    //            DelegateConsentData consentData = new DelegateConsentData()
    //            {
    //                DelegatorSuid = userDTO.Suid,
    //                DelegatorName = userDTO.Name,
    //                OrganizationId = organizationUid,
    //                OrganizationName = OrganizationName,
    //                //StartDateTime = startDateTimeUtc,
    //                StartDateTime = model.StartDateTime,
    //                EndDateTime = model.EndDateTime,
    //                //EndDateTime = endDateTimeUtc,
    //                DocumentType = "Type",
    //                RequestDateTime = DateTime.UtcNow,

    //            };

    //            foreach (string email in model.Emails)
    //            {
    //                var matchedUser = delegationUsersList.FirstOrDefault(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

    //                if (matchedUser != null)
    //                {
    //                    var delegateRecep = new Core.DTOs.DelegateRecep
    //                    {
    //                        email = matchedUser.Email,
    //                        suid = matchedUser.Suid,
    //                        fullName = matchedUser.FullName,
    //                        thumbnail = matchedUser.ThumbNailUri
    //                    };

    //                    delegates.Add(delegateRecep);
    //                    consentData.DelegateList.Add(matchedUser.Suid);
    //                }
    //                else
    //                {
    //                    return Json(new { Status = "Failed", Title = "Add", Message = "email does not belong to this org" });
    //                }
    //            }

    //            string consentDataJson = JsonConvert.SerializeObject(consentData);

    //            var saveDelegatorDTO = new SaveDelegatorDTO
    //            {
    //                Model = JsonConvert.SerializeObject(new DelegatorModel
    //                {
    //                    AccessToken = apiToken,
    //                    StartDateTime = model.StartDateTime,
    //                    EndDateTime = model.EndDateTime,
    //                    DocumentType = "Type",
    //                    DelegationStatus = "",
    //                    ConsentData = "",
    //                    DelegatorConsentDataSignature = "",
    //                    Delegatees = delegates
    //                })
    //            };

    //            var response1 = await _delegatorService.SaveDelegatorAsync(saveDelegatorDTO, apiToken);

    //            if (!response1.Success)
    //            {
    //                logMessage = $"Failed to add Delegator User";
    //                SendAdminLog(ModuleNameConstants.SignatureDelegation, ServiceNameConstants.SignatureDelegation,
    //                     "Add Delegator users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //                return Json(new { Status = "Failed", Title = "Create New Delegation", Message = response1.Message });

    //            }
    //            else
    //            {
    //                logMessage = $"Successfully Added Delegator User";
    //                SendAdminLog(ModuleNameConstants.SignatureDelegation, ServiceNameConstants.SignatureDelegation,
    //                     "Add Delegator users details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //                return Json(new { Status = "Success", Title = "Create New Delegation", Message = response1.Message });

    //            }
    //        }
    //        catch (Exception ex)
    //        {

    //            return RedirectToAction("CreateDelegation");
    //        }
    //    }


    //    public async Task<IActionResult> Preview(string DelegationId)
    //    {
    //        String logMessage;
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //        var response = await _delegatorService.GetDelegateDetailsByIdAsync(DelegationId, apiToken);
    //        if (!response.Success)
    //        {
    //            return Json(new { Status = "Error", Title = "Error", Message = response.Message });
    //        }
    //        var jsonResponse = response.Resource;
    //        var preview = (DelegatorListDTO)response.Resource;
    //        if (preview == null)
    //        {
    //            logMessage = $"Failed to get Delegator User Details";
    //            SendAdminLog(ModuleNameConstants.SignatureDelegation, ServiceNameConstants.SignatureDelegation,
    //                 "Preview Delegator users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return NotFound();
    //        }

    //        var delegateesList = new List<DelegateRecep>();
    //        foreach (var dele in preview.delegatees)
    //        {
    //            var delegateRecep = new DelegateRecep()
    //            {
    //                email = dele.delegateeEmail,
    //                fullName = dele.fullName,
    //                thumbnail = dele.thumbnail,
    //                consentStatus = dele.consentStatus,
    //            };
    //            delegateesList.Add(delegateRecep);
    //        }

    //        ViewDelegateViewModel viewDelegateViewModel = new ViewDelegateViewModel()
    //        {
    //            StartDateTime = preview.startDateTime,
    //            EndDateTime = preview.endDateTime,
    //            DelegationStatus = preview.delegationStatus,
    //            DelegationID = DelegationId,
    //            Delegatees = delegateesList,
    //        };

    //        logMessage = $"Successfully Viewed Delegator User Details";
    //        SendAdminLog(ModuleNameConstants.SignatureDelegation, ServiceNameConstants.SignatureDelegation,
    //             "Preview Delegator users details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //        return View(viewDelegateViewModel);
    //    }

    //    public async Task<IActionResult> RevokeDelegate(string DelegationId)
    //    {
    //        try
    //        {
    //            String logMessage;
    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = await _delegatorService.RevokeDelegateAsync(DelegationId, apiToken);

    //            if (!response.Success)
    //            {
    //                logMessage = $"Failed to revoked Delegator ";
    //                SendAdminLog(ModuleNameConstants.SignatureDelegation, ServiceNameConstants.SignatureDelegation,
    //                     "Revoke Delegator users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //                return Json(new { Status = "Failed", Title = "delegation", Message = response.Message });
    //            }
    //            else
    //            {
    //                logMessage = $"Successfully revoked Delegator ";
    //                SendAdminLog(ModuleNameConstants.SignatureDelegation, ServiceNameConstants.SignatureDelegation,
    //                     "Revoke Delegator users details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //                return Json(new { Status = "Success", Title = "Delegation", Message = response.Message });

    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return RedirectToAction("List");
    //        }
    //    }
    //}
}
