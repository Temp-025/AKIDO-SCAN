namespace EnterpriseGatewayPortal.Web.Controllers
{
    //public class UserClaimsController : BaseController
    //{
    //    private readonly IUserClaimsService _userClaimService;
    //    public UserClaimsController(IAdminLogService adminLogService, IUserClaimsService userClaimsService) : base(adminLogService)
    //    {
    //        _userClaimService = userClaimsService;
    //    }
    //    [HttpGet]
    //    public async Task<IActionResult> List()
    //    {
    //        var viewModel = new List<UserClaimsListViewModel>();

    //        var CliamList = await _userClaimService.GetUserClaimsListAsync();
    //        if (CliamList == null)
    //        {
    //            SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.UserClaimsConfiguration, "Get all UserClaims Configuration List", LogMessageType.FAILURE.ToString(), "Fail to get UserClaims Configuration list", UUID, Email);
    //            return NotFound();
    //        }
    //        else
    //        {
    //            SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.UserClaimsConfiguration, "Get all UserClaims Configuration List", LogMessageType.SUCCESS.ToString(), "Get UserClaims Configuration list success", UUID, Email);

    //            foreach (var item in CliamList)
    //            {
    //                viewModel.Add(new UserClaimsListViewModel
    //                {
    //                    Id = item.Id,
    //                    Name = item.Name,
    //                    DisplayName = item.DisplayName,
    //                    Description = item.Description,
    //                    UserConsent = item.UserConsent
    //                });
    //            }
    //        }
    //        return View(viewModel);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> New()
    //    {
    //        return View();
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> Edit(int id)
    //    {

    //        var UserCliamInDb = await _userClaimService.GetUserClaimsByIdAsync(id);
    //        if (UserCliamInDb == null)
    //        {
    //            SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.UserClaimsConfiguration, "View UserClaims Configuration client details", LogMessageType.FAILURE.ToString(), "Fail to get UserClaims Configuration details", UUID, Email);
    //            return NotFound();
    //        }

    //        var userClaimEditViewModel = new UserClaimsEditViewModel
    //        {
    //            Id = UserCliamInDb.Id,
    //            Name = UserCliamInDb.Name,
    //            DisplayName = UserCliamInDb.DisplayName,
    //            Description = UserCliamInDb.Description,
    //            UserConsent = UserCliamInDb.UserConsent,
    //            DefaultClaim = UserCliamInDb.DefaultClaim,
    //            Metadata = UserCliamInDb.MetadataPublish,

    //        };

    //        SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.UserClaimsConfiguration, "View UserClaims Configuration details", LogMessageType.SUCCESS.ToString(), "Get UserClaims Configuration details of " + UserCliamInDb.DisplayName + " successfully ", UUID, Email);

    //        return View(userClaimEditViewModel);
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> Save(UserClaimsNewViewModel viewModel)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return View("New", viewModel);
    //        }

    //        var userclaim = new UserClaimsDTO()
    //        {
    //            Name = viewModel.Name,
    //            DisplayName = viewModel.DisplayName,
    //            Description = viewModel.Description,
    //            UserConsent = viewModel.UserConsent,
    //            DefaultClaim = viewModel.DefaultClaim,
    //            MetadataPublish = viewModel.Metadata,
    //            CreatedBy = UUID,
    //            UpdatedBy = UUID
    //        };

    //        var response = await _userClaimService.CreateUserClaimsAsync(userclaim);
    //        if (response == null || !response.Success)
    //        {
    //            SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.UserClaimsConfiguration, "Create new UserClaims Configuration", LogMessageType.FAILURE.ToString(), "Fail to create UserClaims Configuration", UUID, Email);
    //            Alert alert = new Alert { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);
    //            return View("New", viewModel);
    //        }
    //        else
    //        {
    //            SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.UserClaimsConfiguration, "Create new UserClaims Configuration", LogMessageType.SUCCESS.ToString(), "Created New UserClaims Configuration with name " + viewModel.DisplayName + " Successfully", UUID, Email);
    //            Alert alert = new Alert { IsSuccess = true, Message = response.Message };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);

    //            return RedirectToAction("List");
    //        }
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> Update(UserClaimsEditViewModel viewModel)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return View("Edit", viewModel);
    //        }



    //        var UserCliamInDb = await _userClaimService.GetUserClaimsByIdAsync(viewModel.Id);
    //        if (UserCliamInDb == null)
    //        {
    //            SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.UserClaimsConfiguration, "Update UserClaims Configuration", LogMessageType.FAILURE.ToString(), "Fail to get UserClaims Configuration details", UUID, Email);

    //            return View("Edit", viewModel);
    //        }


    //        UserCliamInDb.Id = viewModel.Id;
    //        UserCliamInDb.Name = viewModel.Name;
    //        UserCliamInDb.DisplayName = viewModel.DisplayName;
    //        UserCliamInDb.Description = viewModel.Description;
    //        UserCliamInDb.UserConsent = viewModel.UserConsent;
    //        UserCliamInDb.DefaultClaim = viewModel.DefaultClaim;
    //        UserCliamInDb.MetadataPublish = viewModel.Metadata;
    //        UserCliamInDb.UpdatedBy = UUID;

    //        var response = await _userClaimService.UpdateUserClaimsDataAsync(UserCliamInDb);
    //        if (response == null || !response.Success)
    //        {
    //            SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.UserClaimsConfiguration, "Update UserClaims Configuration", LogMessageType.FAILURE.ToString(), "Fail to update UserClaims Configuration details of  name " + viewModel.DisplayName, UUID, Email);
    //            Alert alert = new Alert { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);
    //            return View("Edit", viewModel);
    //        }
    //        else
    //        {
    //            SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.UserClaimsConfiguration, "Update UserClaims Configuration", LogMessageType.SUCCESS.ToString(), (response.Message != "Your request sent for approval" ? "Updated UserClaims Configuration details of  name " + viewModel.DisplayName + " successfully" : "Request for Update UserClaims Configuration details of application name " + viewModel.DisplayName + " has send for approval "), UUID, Email);

    //            Alert alert = new Alert { IsSuccess = true, Message = response.Message };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);

    //            return RedirectToAction("List");
    //        }
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Delete(int id)
    //    {
    //        try
    //        {
    //            var response = await _userClaimService.DeleteUserClaimsAsync(id);
    //            if (response != null || response.Success)
    //            {

    //                Alert alert = new Alert { IsSuccess = true, Message = response.Message };
    //                TempData["Alert"] = JsonConvert.SerializeObject(alert);
    //                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.UserClaimsConfiguration, "Delete UserClaims Configuration", LogMessageType.SUCCESS.ToString(), "Delete UserClaims Configuration successfully", UUID, Email);
    //                return new JsonResult(true);
    //            }
    //            else
    //            {
    //                Alert alert = new Alert { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
    //                TempData["Alert"] = JsonConvert.SerializeObject(alert);
    //                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.UserClaimsConfiguration, "Delete UserClaims Configuration", LogMessageType.FAILURE.ToString(), "Fail to delete UserClaims Configuration", UUID, Email);
    //                return new JsonResult(true);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.UserClaimsConfiguration, "Delete UserClaims Configuration", LogMessageType.FAILURE.ToString(), "Fail to delete UserClaims Configuration " + e.Message, UUID, Email);
    //            return StatusCode(500, e);
    //        }
    //    }
    //}
}
