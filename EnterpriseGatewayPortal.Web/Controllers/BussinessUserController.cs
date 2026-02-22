using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.ViewModel;
using EnterpriseGatewayPortal.Web.ViewModel.BusinessUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [Authorize]
    public class BussinessUserController : BaseController
    {
        private readonly IBussinessUserService _bussinessUserService;
        private readonly ILocalBusinessUsersService _localBusinessUsersService;
        private IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BussinessUserController> _logger;
        private readonly IOrganizationDetailService _organizationDetailService;
        //private readonly IOrgSignatureTemplateService _orgSignatureTemplateService;
        public BussinessUserController(IAdminLogService adminLogService,
            IBussinessUserService bussinessUserService,
            ILocalBusinessUsersService localBusinessUsersService,
            IWebHostEnvironment environment, IConfiguration configuration, IOrganizationDetailService organizationDetailService,
            ILogger<BussinessUserController> logger) : base(adminLogService)

        {
            _bussinessUserService = bussinessUserService;
            _localBusinessUsersService = localBusinessUsersService;
            _environment = environment;
            _configuration = configuration;
            _logger = logger;
            _organizationDetailService = organizationDetailService;
            //_orgSignatureTemplateService = orgSignatureTemplateService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            string logMessage;
            var ViewModel = new List<BusinessUserListViewModel>();
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var businessUserList = await _localBusinessUsersService.GetAllBusinessUsersByOrgUidAsync(organizationUid);
            if (!businessUserList.Success)
            {
                logMessage = $"Failed to get the Business Users List From Local DB";
                SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                    "Get Business users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                return NotFound();
            }
            var businessUser = (IEnumerable<OrgSubscriberEmail>)businessUserList.Resource;
            if (businessUserList == null)
            {
                logMessage = $"Failed to get the Business Users List From Local DB";
                SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                    "Get Business users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                return NotFound();
            }
            var viewModel = new BusinessUserListViewModel();
            viewModel.BusinesUser = businessUser;

            logMessage = $"Successfully received the Business Users List From Local DB";
            SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                "Get Business users details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

            return View(viewModel);

        }

        [HttpGet]
        public IActionResult Add()
        {
            //var organizationTemplates = await _orgSignatureTemplateService.GetOrganizationTemplatesDTOByUIdAsync(OrganizationId);

            //var templateDto = (OrganizationTemplatesDTO)organizationTemplates.Resource;
            //if (templateDto == null)
            //{
            //    logMessage = $"Failed to get the Organization Template From Local DB";
            //    SendAdminLog(ModuleNameConstants.SignatureTemplates, ServiceNameConstants.SignatureTemplates,
            //        "Get Organization Template List", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
            //    return NotFound();
            //}
            //ViewBag.SignatureTemplate = templateDto.signatureTemplateId;
            return View();
        }

        [HttpGet]
        public IActionResult AddCSV()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<JsonResult> AddBusinessUser([FromBody] IList<BusinessUserAddViewModel> businessUser)
        {
            string logMessage;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            IList<BusinessUserDTO> businessUsers = new List<BusinessUserDTO>();

            foreach (var viewModel in businessUser)
            {
                BusinessUserDTO businessUserDTO = new BusinessUserDTO
                {

                    OrganizationUid = organizationUid,
                    orgContactsEmailId = viewModel.orgContactsEmailId,
                    EmployeeEmail = viewModel.EmployeeEmail,
                    ESealSignatory = viewModel.ESealSignatory,
                    ESealPrepatory = viewModel.ESealPrepatory,
                    Bulksign = viewModel.Bulksign,
                    Delegate = viewModel.Delegate,
                    DigitalFormPrivilege = viewModel.DigitalForm,
                    //  lsaPrivilege = viewModel.LsaPrivilege,
                    Designation = viewModel.Designation,
                    NationalIdNumber = viewModel.NationalIdNumber,
                    PassportNumber = viewModel.PassportNumber,
                    MobileNumber = viewModel.MobileNumber,
                    SignaturePhoto = viewModel.SignaturePhoto,
                    Initial = viewModel.InitialImage,
                    UgpassEmail = viewModel.UgpassEmail,
                    Template = viewModel.ESealPrepatory,
                    SubscriberUid = viewModel.SubscriberUid,
                };

                businessUsers.Add(businessUserDTO);
            }
            var response = await _bussinessUserService.AddBusinessUserCSV(businessUsers);

            if (!response.Success)
            {
                logMessage = $"Failed to Add the Business User details in server DB ";
                SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                    "Get Business users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                return Json(new { Status = "Failed", Title = "Add Business User", Message = response.Message });
            }
            else
            {

                JArray jsonObject = JArray.FromObject(response.Resource);
                List<BusinessUserLocalSavingDTO> orgSubscriberEmailList = jsonObject.ToObject<List<BusinessUserLocalSavingDTO>>();
                List<OrgSubscriberEmail> orgSubscriberEmail = new List<OrgSubscriberEmail>();
                foreach (var viewModel2 in orgSubscriberEmailList)
                {
                    OrgSubscriberEmail businessUserModel = new OrgSubscriberEmail
                    {

                        OrganizationUid = organizationUid,
                        OrgContactsId = viewModel2.orgContactsEmailId,
                        EmployeeEmail = viewModel2.EmployeeEmail,
                        IsEsealSignatory = viewModel2.ESealSignatory,
                        IsEsealPreparatory = viewModel2.ESealPreparatory,
                        // IsBulkSign = viewModel2.Bulksign ? true : false,
                        IsBulkSign = viewModel2.Bulksign,
                        IsDelegate = viewModel2.Delegate,
                        IsDigitalForm = viewModel2.DigitalFormPrivilege,
                        // LsaPrivilege = (sbyte)(viewModel2.lsaPrivilege ? 1 : 0),
                        Designation = viewModel2.Designation,
                        NationalIdNumber = viewModel2.NationalIdNumber,
                        PassportNumber = viewModel2.PassportNumber,
                        MobileNumber = viewModel2.MobileNumber,
                        SignaturePhoto = viewModel2.SignaturePhoto,
                        ShortSignature = viewModel2.Initial,
                        UgpassEmail = viewModel2.UgpassEmail,
                        IsTemplate = viewModel2.ESealPreparatory,
                        SubscriberUid = viewModel2.SubscriberUid,
                        Status = "ACTIVE"
                    };

                    orgSubscriberEmail.Add(businessUserModel);
                }

                var response2 = await _localBusinessUsersService.AddBusinessUsersListAsync(orgSubscriberEmail);
                if (!response2.Success)
                {
                    logMessage = $"Failed to Add the Business Users details in local DB ";
                    SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                        "Add Business users ", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                    return Json(new { Status = "Failed", Title = "Add Business User", Message = response2.Message });
                }
                else
                {
                    logMessage = $"Successfully Added the Business Users in Local DB";
                    SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                        "Add Business users ", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
                    return Json(new { Status = "Success", Title = "Add Business User", Message = response2.Message });
                }
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string logMessage;
            try
            {
                var editBusinessUser = await _localBusinessUsersService.GetBusinessUserByIdAsync(id);
                var district = (OrgSubscriberEmail)editBusinessUser.Resource;

                if (editBusinessUser == null)
                {
                    logMessage = $"Failed to get the Business User From Local DB";
                    SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                        "Get Business user details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                    return NotFound();
                }
                //var organizationTemplates = await _orgSignatureTemplateService.GetOrganizationTemplatesDTOByUIdAsync(OrganizationId);

                //var templateDto = (OrganizationTemplatesDTO)organizationTemplates.Resource;
                //if (templateDto == null)
                //{
                //    logMessage = $"Failed to get the Organization Template From Local DB";
                //    SendAdminLog(ModuleNameConstants.SignatureTemplates, ServiceNameConstants.SignatureTemplates,
                //        "Get Organization Template List", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                //    return NotFound();
                //}
                // ViewBag.SignatureTemplate = templateDto.signatureTemplateId;
                ViewBag.SignaturePhoto = district.SignaturePhoto;
                ViewBag.InitialImage = district.ShortSignature;

                var viewModel = new BusinessUserEditViewModel
                {
                    orgContactsEmailId = district.OrgContactsId,
                    EmployeeEmail = district.EmployeeEmail,
                    ESealSignatory = (bool)district.IsEsealSignatory!,
                    ESealPrepatory = (bool)district.IsEsealPreparatory!,
                    Bulksign = Convert.ToBoolean(district.IsBulkSign),
                    Delegate = Convert.ToBoolean(district.IsDelegate),
                    DigitalForm = Convert.ToBoolean(district.IsDigitalForm),
                    // LsaPrivilege = Convert.ToBoolean(district.LsaPrivilege),
                    Designation = district.Designation,
                    NationalIdNumber = district.NationalIdNumber,
                    PassportNumber = district.PassportNumber,
                    MobileNumber = district.MobileNumber,
                    SignaturePhoto = district.SignaturePhoto,
                    InitialImage = district.ShortSignature,
                    UgpassEmail = district.UgpassEmail,
                    Status = district.Status,
                    Template = Convert.ToBoolean(district.IsEsealPreparatory),
                    SubscriberUid = district.SubscriberUid,
                };

                logMessage = $"Successfully received the Business User details  From Local DB";
                SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                    "Get Business user details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
                return View(viewModel);
            }
            catch (Exception)
            {

                return RedirectToAction("List");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBusinessUser(BusinessUserEditViewModel viewModel)
        {
            string logMessage;
            try
            {
                var editBusinessUser = await _localBusinessUsersService.GetBusinessUserByIdAsync(viewModel.orgContactsEmailId);
                var user = (OrgSubscriberEmail)editBusinessUser.Resource;

                if (user == null)
                {
                    logMessage = $"Failed to get the Business User From Local DB";
                    SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                        "Get Business user details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                    return NotFound();
                }
                var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
                user.OrganizationUid = organizationUid;
                user.OrgContactsId = viewModel.orgContactsEmailId;
                user.EmployeeEmail = viewModel.EmployeeEmail;
                user.IsEsealSignatory = viewModel.ESealSignatory;
                user.IsEsealPreparatory = viewModel.ESealPrepatory;
                user.IsBulkSign = viewModel.Bulksign;
                user.IsDelegate = viewModel.Delegate;
                user.IsDigitalForm = viewModel.DigitalForm;
                // user.LsaPrivilege = (sbyte)(viewModel.LsaPrivilege ? 1 : 0);
                user.Designation = viewModel.Designation;
                user.NationalIdNumber = viewModel.NationalIdNumber;
                user.PassportNumber = viewModel.PassportNumber;
                user.MobileNumber = viewModel.MobileNumber;
                //editBusinessUser.SignaturePhoto= viewModel.SignaturePhoto;
                if (viewModel.SignaturePhoto != null)
                {
                    user.SignaturePhoto = viewModel.SignaturePhoto;
                }
                else
                {
                    user.SignaturePhoto = viewModel.ExistingSignaturePhoto;
                }

                if (viewModel.InitialImage != null)
                {
                    user.ShortSignature = viewModel.InitialImage;
                }
                else
                {
                    user.ShortSignature = viewModel.ExistingInitialImage;
                }

                user.UgpassEmail = viewModel.UgpassEmail;
                user.IsTemplate = viewModel.ESealPrepatory;
                user.SubscriberUid = viewModel.SubscriberUid;
                user.Status = "ACTIVE";
                var businessUserDTO = new BusinessUserDTO()
                {
                    OrganizationUid = organizationUid,
                    orgContactsEmailId = user.OrgContactsId,
                    EmployeeEmail = user.EmployeeEmail,
                    ESealSignatory = (bool)user.IsEsealSignatory,
                    ESealPrepatory = (bool)user.IsEsealPreparatory,
                    Bulksign = Convert.ToBoolean(user.IsBulkSign),
                    Delegate = Convert.ToBoolean(user.IsDelegate),
                    DigitalFormPrivilege = Convert.ToBoolean(user.IsDigitalForm),
                    // lsaPrivilege = Convert.ToBoolean(user.LsaPrivilege),
                    Designation = user.Designation,
                    NationalIdNumber = user.NationalIdNumber,
                    PassportNumber = user.PassportNumber,
                    MobileNumber = user.MobileNumber,
                    SignaturePhoto = user.SignaturePhoto,
                    Initial = user.ShortSignature,
                    UgpassEmail = user.UgpassEmail,

                    Template = Convert.ToBoolean(user.IsEsealPreparatory),
                    SubscriberUid = user.SubscriberUid,
                };

                var response = await _bussinessUserService.UpdateBusinessUserAsync(businessUserDTO);
                if (!response.Success)
                {

                    logMessage = $"Failed to update the Business User in server DB";
                    SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                        "Update Business user details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                    // return RedirectToAction("List");
                    return Json(new { Status = "Failed", Title = "Update Business User", Message = response.Message });
                }
                else
                {
                    var response2 = await _localBusinessUsersService.UpdateBusinessUserAsync(user);
                    if (!response2.Success)
                    {

                        logMessage = $"Failed to update the Business User in local DB";
                        SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                            "Update Business user details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                        // return RedirectToAction("List");
                        return Json(new { Status = "Failed", Title = "Update Business User", Message = response.Message });
                    }
                    else
                    {

                        logMessage = $"Successfully updated the Business User details  in Local DB";
                        SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                            "Update Business user details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                        //return RedirectToAction("List");
                        return Json(new { Status = "Success", Title = "Update Business User", Message = response2.Message });
                    }
                }

            }
            catch (Exception e)
            {

                //return RedirectToAction("List");
                return Json(new { Status = "Failed", Title = "Update Business User", Message = e.Message });

            }
        }

        [HttpGet]
        public IActionResult DownloadCSV()
        {
            // Get the path to the CSV file on the server
            string csvFilePath = Path.Combine(_environment.WebRootPath, "samples/BusinessUserTemplateNew.csv");

            _logger.LogInformation(csvFilePath);

            // Check if the file exists
            if (!System.IO.File.Exists(csvFilePath))
            {
                _logger.LogError("File not Found");
                _logger.LogError(_environment.WebRootPath);
                return NotFound();
            }

            // Set the response content type and headers
            string contentType = "text/csv";
            string fileName = Path.GetFileName(csvFilePath);
            return PhysicalFile(csvFilePath, contentType, fileName);
        }

        public async Task<IActionResult> Delete(string Email, int Id)
        {
            string logMessage;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var AdminEmail = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value);
            var editBusinessUser = await _localBusinessUsersService.GetBusinessUserByIdAsync(Id);
            var user = (OrgSubscriberEmail)editBusinessUser.Resource;

            if (user == null)
            {
                logMessage = $"Failed to get the Business User From Local DB";
                SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                    "Delete Business user", LogMessageType.FAILURE.ToString(), logMessage, UUID, AdminEmail);
                return NotFound();
            }
            user.Status = "DELETED";

            var response = await _bussinessUserService.DeleteBusinessUserAsync(Email, organizationUid);
            if (!response.Success)
            {
                AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);

                logMessage = $"Failed to delete the Business User in server DB";
                SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                    "Update Business user details", LogMessageType.FAILURE.ToString(), logMessage, UUID, AdminEmail);

                return RedirectToAction("List");
            }
            var response2 = await _localBusinessUsersService.DeleteBusinessUserAsync(user);
            if (!response2.Success)
            {
                AlertViewModel alert2 = new AlertViewModel { Message = (response2 == null ? "Internal error please contact to admin" : response2.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert2);

                logMessage = $"Failed to Delete the Business User in local DB";
                SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                    "Delete Business user details", LogMessageType.FAILURE.ToString(), logMessage, UUID, AdminEmail);

                return RedirectToAction("List");
            }
            else
            {
                AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response2.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);

                logMessage = $"Successfully Deleted the Business User  in Local DB";
                SendAdminLog(ModuleNameConstants.BusinessUsers, ServiceNameConstants.BusinessUsers,
                    "Delete Business user", LogMessageType.SUCCESS.ToString(), logMessage, UUID, AdminEmail);

                return RedirectToAction("List");
            }
        }
        [HttpGet]
        public async Task<IActionResult> isEsealAffix(string Email)
        {
            var response = await _localBusinessUsersService.GetBusinessUserByEmployeeEmailAsync(Email);
            if (response == null || !response.Success)
            {
                return Ok(response);
            }
            var businessuser = (OrgSubscriberEmail)response.Resource;
            if (businessuser.IsEsealSignatory == true)
            {
                return Ok(new ServiceResult(true, "This user has Eseal permission"));
            }
            return Ok(new ServiceResult(false, "Sorry you dont have eseal permission"));
        }

        [HttpGet]
        public async Task<IActionResult> isInitialImgPresent(string Email)
        {
            var response = await _localBusinessUsersService.GetBusinessUserByEmailAsync(Email);
            if (response == null || !response.Success)
            {
                return Ok(response);
            }
            var businessuser = (OrgSubscriberEmail)response.Resource;
            if (!string.IsNullOrWhiteSpace(businessuser.ShortSignature))
            {
                return Ok(new ServiceResult(true, "This user has Initial image"));
            }
            return Ok(new ServiceResult(false, "Sorry you don't have Initial image"));
        }

        public async Task<IActionResult> GetOrgDetails(string email)
        {

            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var organizationDetails = await _organizationDetailService.GetOrganizationDetailByUIdAsync(organizationUid);
            if (organizationDetails == null)
            {
                return Ok(new ServiceResult(false, organizationDetails.Message));
            }
            var org = (OrganizationDetail)organizationDetails.Resource;
            string emailInputDomain = email?.Split('@').LastOrDefault();

            var count = org.OrgEmailDomains.Count();
            if (count == 0)
            {
                // If there are no email domains specified for the organization, consider it a match
                return Ok(new ServiceResult(true, "Email domain matches organization domain"));
            }
            else
            {
                foreach (var data in org.OrgEmailDomains)
                {
                    if (data.OrganizationUid == organizationUid)
                    {
                        var domain = data.EmailDomain;
                        if (string.IsNullOrEmpty(domain) || emailInputDomain.Equals(domain.Split('@').LastOrDefault(), StringComparison.OrdinalIgnoreCase))
                        {
                            return Ok(new ServiceResult(true, "Email domain matches organization domain"));
                        }
                    }

                }
            }
            return Ok(new ServiceResult(false, "Email domain does not match", emailInputDomain!));
        }
    }
}