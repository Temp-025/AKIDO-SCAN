using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PartyPollingBoothPortal.Core.DTOs;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IRoleManagementService _roleActivityService;
        private readonly HttpClient _client;
        private readonly IUserService _userService;
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOrganizationDetailService _organizationDetailService;
        private readonly IOrganizationService _organizationService;
        private readonly LoginHelper _helper;
        private readonly ILocalBusinessUsersService _localBusinessUsersService;
        private readonly IDeviceService _deviceService;
        private readonly IClientService _clientService;
        private readonly ILocalClientService _localClientService;
        private readonly IConfigurationService _configurationService;
        public LoginController(IAdminLogService adminLogService, HttpClient client,
            IRoleManagementService roleActivityService,
            IUserService userService,
            ILogger<LoginController> logger,
            IConfiguration configuration,
            LoginHelper helper,
            IOrganizationService organizationService,
            IOrganizationDetailService organizationDetailService,
            ILocalBusinessUsersService localBusinessUsersService,
            IDeviceService deviceService,
            IClientService clientService, ILocalClientService localClientService,
            IConfigurationService configurationService) : base(adminLogService)
        {

            _userService = userService;
            _logger = logger;
            _configuration = configuration;
            _helper = helper;
            _organizationDetailService = organizationDetailService;
            _organizationService = organizationService;
            _localBusinessUsersService = localBusinessUsersService;
            _deviceService = deviceService;
            _clientService = clientService;
            _client = client;
            _configurationService = configurationService;
            _roleActivityService = roleActivityService;
            _localClientService = localClientService;
        }

        public async Task<IActionResult> Index()
        {

            _logger.LogInformation("Login started");

            if (User.Identity.IsAuthenticated)
            {
                var isidplogin = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IDPLogin").Value;
                if (isidplogin == "false")
                {
                    var id = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value);

                    var status = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserStatus").Value);

                    return RedirectToAction("Index3", "Login", new { Status = status, Id = id });
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            var licenseResult = await _deviceService.ReadLicence();

            if (licenseResult == null || !licenseResult.Success)
            {
                ViewBag.Licenceinvalid = true;

                return View();
            }

            var OrganizationDetailsindb = await _organizationDetailService.GetAllOrganizationDetailListAsync();

            if (OrganizationDetailsindb == null || !OrganizationDetailsindb.Success)
            {
                ViewBag.error = "Internal Error";

                ViewBag.error_description = "Something went wrong";

                return View("Errorp");
            }

            var OrganizationDetailsList = (IEnumerable<OrganizationDetail>)OrganizationDetailsindb.Resource;

            if (OrganizationDetailsList == null || OrganizationDetailsList.Count() == 0)
            {
                _logger.LogInformation("organization details 0");

                return RedirectToAction("Index", "OrganizationCompleteDetails");
            }
            if (Request.Cookies.ContainsKey("MyCookieName"))
            {
                var cookieValue = Request.Cookies["MyCookieName"];

                Response.Cookies.Delete("MyCookieName");

                ViewBag.SessionExpired = true;

                return View();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index1(LoginModel loginModel)
        {
            var response = new Response();

            var userinDb = await _userService.GetUserByEmail(loginModel.Email);
            if (userinDb == null)
            {
                return NotFound();
            }

            var licenseResult = await _deviceService.ReadLicence();

            if (licenseResult == null || !licenseResult.Success)
            {
                return null;
            }

            var licenceDetails = (LicenceDTO)licenseResult.Resource;

            var clientDetails = await _localClientService.GetClientByClientIdAsync(licenceDetails.clientId);
            if (clientDetails == null)
            {
                return null;
            }

            var role = userinDb.RoleId == 1 ? "ADMIN" :
                      userinDb.RoleId == 8 ? "KYCADMIN" :
                      userinDb.RoleId == 4 ? "BUSINESSUSER" :
                      "BUSINESSUSER";

            var roleInDb = await _roleActivityService.GetRoleAsync((int)userinDb.RoleId!);

            if (roleInDb == null)
            {
                _logger.LogError("Login Callback : get user role details failed");
                return NotFound();
            }

            _logger.LogInformation("Login Callback  : get user role details successfully");
            var organizationDetails = await _organizationDetailService.GetOrganizationDetailByUIdAsync(clientDetails.OrganizationUid);

            var org = (OrganizationDetail)organizationDetails.Resource;
            var organizationName = org.OrgName;
            var rolesList = GetAccessibleModuleList(roleInDb.RoleActivities).Result;

            var privilegeResponse = await _organizationService.GetPrevilagesAsync(org.OrganizationUid);

            if (privilegeResponse != null && privilegeResponse.Privileges != null)
            {
                foreach (var item in privilegeResponse.Privileges)
                {
                    rolesList.Add(new Claim(ClaimTypes.Role, item));
                }
            }

            var adminUser = "";
            var kycAdminUser = "";

            if (role == "ADMIN")
            {
                adminUser = "true";
            }
            if (role == "KYCADMIN")
            {
                kycAdminUser = "true";
            }


            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userinDb.Name),
                    new Claim(ClaimTypes.Email,userinDb.Email!),
                    new Claim("UserRoleID",userinDb.RoleId.ToString()!),
                    new Claim("UserStatus",userinDb.Status.ToString()),
                    new Claim(ClaimTypes.UserData,userinDb.Id.ToString()),
                    new Claim(ClaimTypes.Role,role),
                    new Claim(ClaimTypes.NameIdentifier, userinDb.Uuid!),
                    new Claim("SpocEmail",org.SpocUgpassEmail!),
                    new Claim("userEmail",userinDb.Email!),
                    new Claim("userRoleName",role),
                    new Claim("OrganizationName",organizationName!),
                    new Claim("PrePaidOrPostPiad", org.EnablePostPaidOption.ToString()!),
                    new Claim("IDPLogin","false"),
                    new Claim("AdminUserExist",adminUser),
                    new Claim("KYCAdminUserExist",kycAdminUser),
                    new Claim("OrganizationUid",clientDetails.OrganizationUid),


                }, CookieAuthenticationDefaults.AuthenticationScheme);

            if (rolesList.Count > 0)
                identity.AddClaims(rolesList);


            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties();

            identity.AddClaim(new Claim("UserExist", "false"));

            properties.IsPersistent = true;

            properties.AllowRefresh = false;

            int SessionValidatonTime = _configuration.GetSection("SessionValidationTime").Get<int>();

            properties.ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(SessionValidatonTime));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Path = "/",
                Expires = DateTime.Now.AddDays(1),
                Secure = Request.IsHttps
            };

            Response.Cookies.Append("MyCookieName", "CookieValue", cookieOptions);

            SendAdminLog(ModuleNameConstants.Login, ServiceNameConstants.Login, "User Login", LogMessageType.SUCCESS.ToString(), "User login successfully", userinDb.Uuid!, userinDb.Email!, null, userinDb.Name);

            if (userinDb.Status == "NEW")
            {
                return RedirectToAction("ChangePassword", "UserDashboard", new { id = userinDb.Id, isFirstLogin = true });
            }
            else if (userinDb.Status == "CHANGE_PASSWORD")
            {
                return RedirectToAction("SetSecurityQuestion", "UserDashboard");
            }
            //else if(userinDb.RoleId == 4)
            //{ 
            //    return RedirectToAction("DashboardView", "Dashboard");        
            //}
            else
            {
                //return RedirectToAction("IsFirstLogin", "Login");
                if (role == "KYCADMIN")
                {
                    return RedirectToAction("Index", "KYCDashboard");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
        }

        public async Task<IActionResult> CheckPassword(LoginModel loginModel)
        {
            var response = new Response();
            var result = await _userService.IsUserValid(loginModel.Email, loginModel.Password);
            if (result == null)
            {
                response.Success = false;
                response.Message = "Invalid details";
                return Ok(response);
            }
            if (!result.Success)
            {
                var user = result.Result;
                if (user != null)
                {
                    SendAdminLog(ModuleNameConstants.Login, ServiceNameConstants.Login, "User Login Failed", LogMessageType.SUCCESS.ToString(), "User login Failed", user.Uuid!, user.Email!, null, user.Name);
                }
                response.Success = false;
                response.Message = result.Message;
                return Ok(response);
            }
            response.Success = true;
            response.Message = "Success";
            return Ok(response);
        }

        public IActionResult Index3(string Status, int Id)
        {
            if (Status == "NEW")
            {
                return RedirectToAction("ChangePassword", "UserDashboard", new { id = Id, isFirstLogin = true });
            }
            else if (Status == "CHANGE_PASSWORD")
            {
                return RedirectToAction("SetSecurityQuestion", "UserDashboard");
            }
            else
            {
                //return RedirectToAction("IsFirstLogin", "Login");
                return RedirectToAction("Index", "Dashboard");
            }
        }

        public async Task<bool> IsUserExist(string Email)
        {
            return await _userService.IsUserExist(Email);
        }

        [HttpGet]
        public async Task<IActionResult> Index4()
        {
            var state = Guid.NewGuid().ToString("N");
            var nonce = Guid.NewGuid().ToString("N");
            return Redirect(await _helper.GetAuthorizationUrl(state, nonce));
        }

        public async Task<IActionResult> CallBack()
        {
            try
            {
                var licenseResult = await _deviceService.ReadLicence();
                if (licenseResult == null || !licenseResult.Success)
                {
                    return null;
                }

                var licenceDetails = (LicenceDTO)licenseResult.Resource;

                var clientDetails = await _localClientService.GetClientByClientIdAsync(licenceDetails.clientId);
                if (clientDetails == null)
                {
                    return null;
                }

                _logger.LogInformation("Login Callback started");
                _logger.LogInformation(clientDetails.ToString());

                var mail_id = "";

                var orgUID = clientDetails.OrganizationUid;
                var openid = _configuration.GetValue<Boolean>("openid");
                if (!string.IsNullOrEmpty(Request.Query["error"]) && !string.IsNullOrEmpty(Request.Query["error_description"]))
                {
                    ViewBag.error = Request.Query["error"].ToString();
                    ViewBag.error_description = Request.Query["error_description"].ToString();
                    return View("Errorp");
                }
                string code = Request.Query["code"].ToString();
                if (string.IsNullOrEmpty(code))
                {
                    ViewBag.error = "Invalid code";
                    ViewBag.error_description = "The code value is empty string or null";
                    return View("Errorp");
                }
                JObject TokenResponse = _helper.GetAccessToken(code).Result;

                if (TokenResponse.ContainsKey("error") && TokenResponse.ContainsKey("error_description"))
                {
                    ViewBag.error = TokenResponse["error"].ToString();
                    ViewBag.error_description = TokenResponse["error_description"].ToString();
                    return View("Errorp");
                }
                var accessToken = TokenResponse["access_token"].ToString();

                _logger.LogInformation("Access Token : " + accessToken);

                if (string.IsNullOrEmpty(accessToken))
                {
                    ViewBag.error = "Invalid code";
                    ViewBag.error_description = "The code value is empty string or null";
                    return View("Errorp");
                }
                var idtoken = "";
                UserSessionObj user = new UserSessionObj();
                if (openid == true)
                {
                    idtoken = TokenResponse["id_token"].ToString();
                    if (string.IsNullOrEmpty(idtoken))
                    {
                        ViewBag.error = "Invalid id token";
                        ViewBag.error_description = "Id token is empty";
                        return View("Errorp");
                    }
                    ClaimsPrincipal userObj = await _helper.ValidateJwt(idtoken);
                    var daesClaim = userObj.FindFirst("daes_claims")?.Value ?? "";
                    UserObj userdata = JsonConvert.DeserializeObject<UserObj>(daesClaim);
                    user.Uuid = userdata.suid;
                    user.fullname = userdata.name;
                    user.dob = userdata.birthdate;
                    user.mailId = userdata.email;
                    mail_id = userdata.email;
                    user.LoginProfile = userdata.login_profile;
                }
                else
                {
                    JObject userObj = _helper.GetUserInfo(accessToken).Result;
                    if (userObj.ContainsKey("error") && userObj.ContainsKey("error_description"))
                    {
                        ViewBag.error = userObj["error"].ToString();
                        ViewBag.error_description = userObj["error_description"].ToString();
                        return View("Errorp");
                    }
                    user.Uuid = userObj["suid"].ToString();
                    user.fullname = userObj["name"].ToString();
                    user.dob = userObj["birthdate"].ToString();
                    user.mailId = userObj["email"].ToString();
                    user.mobileNo = userObj["phone"].ToString();
                    mail_id = user.mailId;
                    var loginProfile = userObj["login_profile"] != null ? userObj["login_profile"].ToString() : null;
                    if (loginProfile != null)
                    {
                        user.LoginProfile = JsonConvert.DeserializeObject<IList<LoginProfile>>(loginProfile);
                    }
                }
                var organizationDetails = await _organizationDetailService.GetOrganizationDetailByUIdAsync(orgUID);

                var org = (OrganizationDetail)organizationDetails.Resource;

                if (org == null)
                {
                    ViewBag.error = "Internal Error";
                    ViewBag.error_description = "Something went wrong";
                    return View("Errorp");
                }

                var organizationName = org.OrgName;

                _logger.LogInformation("Login Callback  : get user role details successfully");


                var identity = new ClaimsIdentity(new[] {
                     new Claim("IDPLogin","true"),
                     new Claim("SpocEmail",org.SpocUgpassEmail!),
                     new Claim("OrganizationUid",clientDetails.OrganizationUid),
                     new Claim("OrganizationName",organizationName!),
                     new Claim("Suid", user.Uuid),
                     new Claim("PrePaidOrPostPiad", org.EnablePostPaidOption.ToString()!),
                 },
                CookieAuthenticationDefaults.AuthenticationScheme);

                var expires_in = int.Parse(TokenResponse["expires_in"].ToString());

                OrgSubscriberEmail businessuser = new OrgSubscriberEmail();

                bool isuserexist = false;

                var isUserLinked = false;

                if (user.LoginProfile != null)
                {
                    isUserLinked = user.LoginProfile.Any(lp => lp.OrgnizationId == orgUID);
                }

                _logger.LogInformation("Is User Linked : " + isUserLinked);

                var response = await _localBusinessUsersService.GetBusinessUserByEmailAsync(mail_id);

                if (response != null && response.Success && isUserLinked)
                {
                    businessuser = (OrgSubscriberEmail)response.Resource;
                    identity.AddClaim(new Claim("employeEmail", businessuser.EmployeeEmail!));
                    if (businessuser.IsTemplate == true)
                    {
                        identity.AddClaim(new Claim("Template", "true"));
                    }
                    else
                    {
                        identity.AddClaim(new Claim("Template", "false"));
                    }
                    if (businessuser.IsBulkSign == true)
                    {
                        identity.AddClaim(new Claim("BulkSign", "true"));
                    }
                    else
                    {
                        identity.AddClaim(new Claim("BulkSign", "false"));
                    }
                    identity.AddClaim(new Claim("EsealPreparatory", businessuser.IsEsealPreparatory == true ? "true" : "false"));
                    identity.AddClaim(new Claim("EsealSignatory", businessuser.IsEsealSignatory == true ? "true" : "false"));

                    isuserexist = true;

                    identity.AddClaim(new Claim("UserExist", "true"));
                }
                else
                {
                    identity.AddClaim(new Claim("UserExist", "false"));

                    var user1 = await _userService.GetUserByEmail(user.mailId);

                    if ((user1.RoleId == 4 && isUserLinked == false) || user1.RoleId != 1 || user1.RoleId != 8)
                    {
                        return RedirectToAction("Logout", "Login");
                    }

                    if (user1 == null)
                    {
                        return RedirectToAction("Logout", "Login");
                    }
                }
                var userinDb = await _userService.GetUserByEmail(user.mailId);

                if (userinDb == null)
                {
                    identity.AddClaim(new Claim("AdminUserExist", "false"));
                    identity.AddClaim(new Claim("KYCAdminUserExist", "false"));
                    userinDb = new User()
                    {
                        Id = 0,
                        Uuid = user.Uuid,
                        Email = user.mailId,
                        MobileNumber = user.mobileNo,
                        Name = user.fullname,
                        RoleId = 4
                    };
                }
                else
                {
                    if (userinDb.RoleId == 1)
                    {
                        identity.AddClaim(new Claim("AdminUserExist", "true"));
                    }
                    else
                    {
                        identity.AddClaim(new Claim("AdminUserExist", "false"));
                    }
                    if (userinDb.RoleId == 8)
                    {
                        identity.AddClaim(new Claim("KYCAdminUserExist", "true"));
                    }
                    else
                    {
                        identity.AddClaim(new Claim("KYCAdminUserExist", "false"));
                    }
                }
                //var role = ((userinDb.RoleId == 1) ? "ADMIN" : "BUSINESSUSER");

                var role = userinDb.RoleId == 1 ? "ADMIN" :
                      userinDb.RoleId == 8 ? "KYCADMIN" :
                      userinDb.RoleId == 4 ? "BUSINESSUSER" :
                      "BUSINESSUSER";

                var roleInDb = await _roleActivityService.GetRoleAsync((int)userinDb.RoleId!);

                if (roleInDb == null)
                {
                    _logger.LogError("Login Callback : get user role details failed");
                    return NotFound();
                }

                var rolesList = GetAccessibleModuleList(roleInDb.RoleActivities).Result;

                var privilegeResponse = await _organizationService.GetPrevilagesAsync(org.OrganizationUid);

                if (privilegeResponse != null && privilegeResponse.Privileges != null)
                {
                    foreach (var item in privilegeResponse.Privileges)
                    {
                        rolesList.Add(new Claim(ClaimTypes.Role, item));
                    }
                }

                if (rolesList.Count > 0)
                    identity.AddClaims(rolesList);

                if (openid)
                {
                    identity.AddClaim(new Claim("ID_Token", idtoken));
                }

                identity.AddClaim(new Claim("AccessToken", accessToken));

                identity.AddClaim(new Claim(ClaimTypes.Name, userinDb.Name));

                identity.AddClaim(new Claim(ClaimTypes.Email, userinDb.Email!));

                identity.AddClaim(new Claim("UserRoleID", userinDb.RoleId.ToString()!));

                identity.AddClaim(new Claim(ClaimTypes.Role, role));

                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userinDb.Uuid!));

                identity.AddClaim(new Claim("userEmail", userinDb.Email!));
                identity.AddClaim(new Claim("userRoleName", role));

                identity.AddClaim(new Claim(ClaimTypes.UserData, userinDb.Id.ToString()));

                UserDTO userDTO = new UserDTO()
                {
                    Name = user.fullname,
                    Email = user.mailId,
                    Suid = user.Uuid,
                    OrganizationId = "",
                    OrganizationName = "",
                    AccountType = "Organization",
                    AccessTokenExpiryTime = DateTime.Now.AddSeconds(expires_in)
                };

                if (isuserexist)
                {
                    userDTO.OrganizationId = orgUID;

                    userDTO.OrganizationName = organizationName;

                    userDTO.Email = businessuser.EmployeeEmail;

                    var apiToken = _helper.generateApiToken(userDTO, expires_in);

                    identity.AddClaim(new Claim("apiToken", apiToken));
                }
                var userObject = JsonConvert.SerializeObject(userDTO);

                identity.AddClaim(new Claim("user", userObject));

                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties();

                properties.IsPersistent = true;

                properties.AllowRefresh = false;

                int SessionValidatonTime = _configuration.GetSection("SessionValidationTime").Get<int>();

                properties.ExpiresUtc = DateTime.UtcNow.AddSeconds(Convert.ToDouble(expires_in));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Path = "/",
                    Expires = DateTime.Now.AddDays(1),
                    Secure = Request.IsHttps,

                };
                if (Request.Cookies["NavigationLayout"] != null)
                {
                    Response.Cookies.Delete("NavigationLayout");
                }
                SendAdminLog(ModuleNameConstants.Login, ServiceNameConstants.Login, "User Login", LogMessageType.SUCCESS.ToString(), "User login successfully", userinDb.Uuid!, userinDb.Email!, null, user.fullname);

                Response.Cookies.Append("MyCookieName", "CookieValue", cookieOptions);
                if (userinDb != null)
                {
                    if (userinDb.Status == "NEW")
                    {
                        return RedirectToAction("ChangePassword", "UserDashboard", new { id = userinDb.Id, isFirstLogin = true });
                    }
                    else if (userinDb.Status == "CHANGE_PASSWORD")
                    {
                        return RedirectToAction("SetSecurityQuestion", "UserDashboard");
                    }
                }
                //return RedirectToAction("Index", "Dashboard");
                if (role == "KYCADMIN")
                {
                    return RedirectToAction("Index", "KYCDashboard");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ViewBag.error = ex.Message.ToString();
                ViewBag.error_description = "Something went wrong";
                return View("Errorp");
            }
        }

        public async Task<IActionResult> IsFirstLogin()
        {
            var OrganizationDetailsindb = await _organizationDetailService.GetAllOrganizationDetailListAsync();

            if (OrganizationDetailsindb == null || !OrganizationDetailsindb.Success)
            {
                ViewBag.error = "Internal Error";
                ViewBag.error_description = "Something went wrong";
                return View("Errorp");
            }
            var OrganizationDetailsList = (IEnumerable<OrganizationDetail>)OrganizationDetailsindb.Resource;
            if (OrganizationDetailsList == null || OrganizationDetailsList.Count() == 0)
            {
                return RedirectToAction("Index", "OrganizationCompleteDetails");
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }

        [HttpGet]
        public IActionResult SessionExpired()
        {
            ViewBag.SessionExpired = true;
            return View();
        }

        public IActionResult Logout()
        {
            ViewBag.logout = true;
            return View("Index");
        }

        public NetworkInterface GetAdapterByName(string name)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    return adapter;
                }
            }
            return null;
        }

        public ServiceResult GetMacAddress()
        {
            //string adapterName = "Wi-Fi";

            ////NetworkInterface adapter = GetAdapterByName(adapterName);
            //NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            //foreach (NetworkInterface adapter in adapters)
            //{

            //    PhysicalAddress macAddress = adapter.GetPhysicalAddress();

            //    byte[] bytes = macAddress.GetAddressBytes();

            //    string macAddressString = BitConverter.ToString(bytes);

            //    _logger.LogError(adapter.Name+"    "+ macAddressString);
            //}
            //_logger.LogError("Mac Address Not Found");

            //return new ServiceResult("Mac Address not found");

            string adapterName = "Wi-Fi";

            NetworkInterface adapter = GetAdapterByName(adapterName);

            if (adapter != null)
            {
                PhysicalAddress macAddress = adapter.GetPhysicalAddress();

                byte[] bytes = macAddress.GetAddressBytes();

                string macAddressString = BitConverter.ToString(bytes);

                return new ServiceResult(true, "MacAddress Found", macAddressString);
            }
            _logger.LogError("Mac Address Not Found");
            return new ServiceResult("Mac Address not found");
        }

        [HttpGet]
        public async Task<JsonResult> ActivateLicence()
        {
            var licenseResult = await _deviceService.ReadLicence();

            if (licenseResult == null || !licenseResult.Success)
            {
                ViewBag.Licenceinvalid = true;

                return Json(new { Status = "Failed", Message = "Failed to Activate Licence" });
            }

            var licenceDetails = (LicenceDTO)licenseResult.Resource;

            var result = await _deviceService.ActiveLicence(licenceDetails.clientId, licenceDetails.licenseType);

            if (result == null || !result.Success)
            {
                return Json(new { Status = "Failed", Message = "Failed to Activate Licence" });
            }

            return Json(new { Status = "Success", Message = "Licence Activated Successfully" });
        }

        [NonAction]
        private async Task<List<Claim>> GetAccessibleModuleList(IEnumerable<RoleActivity> roleActivities = null)
        {
            var activityLookupItems = await _roleActivityService.GetActivityLookupItemsAsync();
            if (activityLookupItems == null)
            {
                _logger.LogError("Login Callback : get all activity list failed");
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.Login, "Login callback",
                    LogMessageType.FAILURE.ToString(), "Fail to get all activities list", UUID, Email, FullName);
                throw new Exception("Fail to get role activity");
            }
            var activityListItems = new List<Claim>();

            if (roleActivities != null)
            {
                foreach (var activity in activityLookupItems)
                {
                    var ActivityName = roleActivities.Any(x => x.ActivityId == activity.Id && (bool)x.IsEnabled!) ? activity.DisplayName : "";
                    if (!String.IsNullOrEmpty(ActivityName))
                    {
                        if (ActivityName == "Reports")
                        {
                            ActivityName = ActivityName + "_" + activity.Id;
                            activityListItems.Add(new Claim(ClaimTypes.Role, ActivityName));
                        }
                        else
                        {
                            activityListItems.Add(new Claim(ClaimTypes.Role, ActivityName));
                        }
                    }
                    var isChecker = roleActivities.Any(x => x.ActivityId == activity.Id && (bool)x.IsEnabled! && x.IsChecker == true);
                    if (activity.McSupported && isChecker)
                    {
                        var CheckerActivityName = activity.DisplayName + " Checker";
                        activityListItems.Add(new Claim(ClaimTypes.Role, CheckerActivityName));
                    }
                    if (!String.IsNullOrEmpty(ActivityName))
                    {
                        var Name = "";
                        var isParentExsist = activityLookupItems.Any(x => x.Id == activity.ParentId);
                        if (isParentExsist)
                            Name = activityLookupItems.First(x => x.Id == activity.ParentId).DisplayName;

                        var isTitleAdded = activityListItems.Any(x => x.Value == Name);
                        if (!isTitleAdded && Name != "")
                            activityListItems.Add(new Claim(ClaimTypes.Role, Name));
                    }
                }
            }
            else
            {

                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.Login,
                    "Get Accessible Module List", LogMessageType.FAILURE.ToString(), "Fail to get activity info", UUID, Email, FullName);
            }


            return activityListItems;
        }

    }
}
