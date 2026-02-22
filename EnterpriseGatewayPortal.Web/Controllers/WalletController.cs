using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.Attribute;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.Models;
using EnterpriseGatewayPortal.Web.Utilities;
using EnterpriseGatewayPortal.Web.ViewModel.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;




namespace EnterpriseGatewayPortal.Web.Controllers
{
    [ServiceFilter(typeof(SessionValidationAttribute))]
    [Authorize]
    public class WalletController : BaseController
    {

        private readonly IConfiguration _configuration;
        private readonly IWalletService _walletService;
        private readonly IOrganizationService _organizationService;
        private readonly IRazorRendererHelper _razorRendererHelper;
        private readonly DataExportService _dataExportService;
        private readonly ILogger<WalletController> _logger;
        private IWebHostEnvironment _environment;

        public WalletController(IAdminLogService adminLogService, IWalletService walletService, IWebHostEnvironment environment, IOrganizationService organizationService,
            IConfiguration configuration, IRazorRendererHelper razorRendererHelper, DataExportService dataExportService, ILogger<WalletController> logger

           ) : base(adminLogService)
        {

            _configuration = configuration;
            _walletService = walletService;
            _organizationService = organizationService;
            _razorRendererHelper = razorRendererHelper;
            _dataExportService = dataExportService;
            _logger = logger;
            _environment = environment;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new List<CredentialsListViewModel>();

            var CrendentialList = await _walletService.GetAllCredentialsList(OrganizationId);
            var privilage = await _organizationService.GetPrevilagesAsync(OrganizationId);
            if (privilage == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "Get all wallet privilages", LogMessageType.FAILURE.ToString(), "Fail to get wallet privilages", UUID, Email);

            }
            if (CrendentialList == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "Get all wallet List", LogMessageType.FAILURE.ToString(), "Fail to get wallet list", UUID, Email);
                return NotFound();
            }
            else
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "Get all wallet List", LogMessageType.SUCCESS.ToString(), "Get wallet lists", UUID, Email);

                foreach (var item in CrendentialList)
                {
                    viewModel.Add(new CredentialsListViewModel
                    {
                        Id = item.Id,
                        CredentialName = item.CredentialName,
                        CredentialId = item.CredentialId,
                        CredentialUId = item.CredentialUId,
                        VerificationDocType = item.VerificationDocType,
                        AuthenticationScheme = item.AuthenticationScheme,
                        CategoryId = item.CategoryId,
                        OrganizationId = item.OrganizationId,
                        ServiceDetails = item.ServiceDetails,
                        Status = item.Status,
                        DisplayName = item.DisplayName,
                        CreatedDate = item.CreatedDate,

                    });
                }
            }
            if (privilage != null)
            {
                ViewBag.WalletCertificateStatus = privilage.WalletCertificateStatus;
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var categoryList = await GetCategotyList();
            var authSchemeTypesList = await GetAuthSchemeTypesList();
            var defaultDataAttributes = await _walletService.GetAttrbutesList();
            var orgCategories = await _walletService.GetOrgCategoriesList();

            _logger.LogInformation("categoryList: {CategoryList}", categoryList);
            _logger.LogInformation("authSchemeTypesList: {AuthSchemaTypesList}", authSchemeTypesList);
            _logger.LogInformation("defaultDataAttributes: {Default Attributes}", defaultDataAttributes);
            _logger.LogInformation("orgCategories: {OrgCategories}", orgCategories);

            var CategotyViewModel = new CredentialAddViewModel
            {
                CategoryList = categoryList,
                AuthSchemeTypesList = authSchemeTypesList,
                DefaultDataAttributes = defaultDataAttributes.DataAttributes,
                OrganizationCategories = orgCategories,
            };
            return View(CategotyViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> TestCredential(string CredentialUid, string verificationType)
        {

            var credentialInDb = await _walletService.GetCredentialById(CredentialUid);
            var testCredentail = new TestCredentialViewModel
            {
                CredentialId = CredentialUid,
                DocumentType = verificationType,
                CredentialName = credentialInDb.CredentialName,
                DisplayName = credentialInDb.DisplayName,
                DataAttributes = credentialInDb.DataAttributes,
                AuthenticationScheme = credentialInDb.AuthenticationScheme,
                ServiceDetails = credentialInDb.ServiceDetails,
                VerificationDocType = credentialInDb.VerificationDocType,
                Status = credentialInDb.Status,
                CategoryId = credentialInDb.CategoryId,
                Logo = credentialInDb.Logo,
            };

            return View(testCredentail);
        }

        [HttpGet]
        public async Task<IActionResult> ViewCredentialDetailsPDFView(string id)
        {
            try
            {
                var credentialInDb = await _walletService.GetCredentialById(id);
                if (credentialInDb == null)
                {
                    SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                    return NotFound();
                }


                var credentialViewModel = new PdfViewModel
                {

                    CredentialId = credentialInDb.CredentialId,
                    CredentialName = credentialInDb.CredentialName,
                    AuthenticationScheme = credentialInDb.AuthenticationScheme,
                    DataAttributes = credentialInDb.DataAttributes,
                    CategoryId = credentialInDb.CategoryId,
                    VerificationDocType = credentialInDb.VerificationDocType,
                    OrganizationId = credentialInDb.OrganizationId,
                    Status = credentialInDb.Status,
                    CredentialUId = id,
                    Logo = credentialInDb.Logo,
                    DisplayName = credentialInDb.DisplayName,
                    credential_localId = 1,
                    Email = Email,


                };

                var categoryName = await GetCategoryNameById(credentialInDb.CategoryId!);
                credentialViewModel.Category = categoryName;

                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.SUCCESS.ToString(), "Get Credential details of ", UUID, Email);

                return View(credentialViewModel);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetPDFBytes(string id)
        {
            var credentialInDb = await _walletService.GetCredentialById(id);
            if (credentialInDb == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                return NotFound();
            }

            var credentialViewModel = new PdfViewModel
            {

                CredentialId = credentialInDb.CredentialId,
                CredentialName = credentialInDb.CredentialName,
                AuthenticationScheme = credentialInDb.AuthenticationScheme,
                DataAttributes = credentialInDb.DataAttributes,
                CategoryId = credentialInDb.CategoryId,
                VerificationDocType = credentialInDb.VerificationDocType,
                OrganizationId = credentialInDb.OrganizationId,
                Status = credentialInDb.Status,
                CredentialUId = id,
                Logo = credentialInDb.Logo,
                DisplayName = credentialInDb.DisplayName,
                Email = Email

            };

            var partialName = "/Views/Wallet/ViewCredentialDetailsDownloadPdfView.cshtml";
            var htmlContent = _razorRendererHelper.RenderPartialToString(partialName, credentialViewModel);
            byte[] pdfBytes = _dataExportService.GeneratePdf(htmlContent);


            return Json(new { Status = "Success", Title = "Export wallet credentials", Message = "Successfully Generated PDF bytes", Result = pdfBytes });


        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryList([FromBody] CredentialAddViewModel credentialAddViewModel)
        {

            CredentialListDTO CredentialAddDTO = new CredentialListDTO()
            {

                CredentialName = credentialAddViewModel.CredentialName,
                CredentialId = Guid.NewGuid().ToString(),
                VerificationDocType = credentialAddViewModel.VerificationDocType,
                DataAttributes = credentialAddViewModel.DataAttributes,
                AuthenticationScheme = credentialAddViewModel.AuthenticationScheme,
                CategoryId = credentialAddViewModel.Category,
                OrganizationId = OrganizationId,
                Logo = credentialAddViewModel.Logo,
                DisplayName = credentialAddViewModel.DisplayName,
                trustUrl = credentialAddViewModel.CredentialTrustUrl,
                validity = int.Parse(credentialAddViewModel.validity!),
                categories = credentialAddViewModel.SelectedOrgCategoryIds,


            };

            var response = await _walletService.AddCredentialsAsync(CredentialAddDTO);
            if (response == null || !response.Success)
            {
                return Json(new { success = false, message = response == null ? "Internal error please contact to admin" : response.Message });
            }
            else
            {

                return Json(new { success = true, message = response.Message, result = response.Resource });

            }
        }

        [HttpPost]
        public async Task<IActionResult> TestCredential([FromBody] TestCredentialRequestDTO testCredentialRequestDTO)
        {


            var response = await _walletService.TestCredentialByDocumentId(testCredentialRequestDTO);
            if (response == null || !response.Success)
            {
                return Json(new { success = false, message = response == null ? "Internal error please contact to admin" : response.Message });
            }
            else
            {

                return Json(new { success = true, message = response.Message });

            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewCredentialDetails(string id)
        {

            var credentialInDb = await _walletService.GetCredentialById(id);
            if (credentialInDb == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                return NotFound();
            }
            var orgCategories = await _walletService.GetOrgCategoriesList();
            if (orgCategories == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "Organization categories", LogMessageType.FAILURE.ToString(), "Fail to get Organization categories", UUID, Email);
                AlertViewModel alert = new AlertViewModel { Message = "Internal error please contact to admin" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("Index");


            }
            var credentialViewModel = new CredentialsViewModel
            {
                Id = credentialInDb.Id,
                CredentialId = credentialInDb.CredentialId,
                CredentialName = credentialInDb.CredentialName,
                AuthenticationScheme = credentialInDb.AuthenticationScheme,
                DataAttributes = credentialInDb.DataAttributes,
                CategoryId = credentialInDb.CategoryId,
                VerificationDocType = credentialInDb.VerificationDocType,
                OrganizationId = credentialInDb.OrganizationId,
                Status = credentialInDb.Status,
                CredentialUId = id,
                Logo = credentialInDb.Logo,
                DisplayName = credentialInDb.DisplayName,
                CredentialTrustUrl = credentialInDb.trustUrl,
                validity = credentialInDb.validity.ToString(),
                OrganizationCategories = orgCategories,
                SelectedOrgCategoryIds = credentialInDb.categories,

            };

            var categoryName = await GetCategoryNameById(credentialInDb.CategoryId!);
            credentialViewModel.Category = categoryName;

            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.SUCCESS.ToString(), "Get Credential details of ", UUID, Email);

            return View(credentialViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ViewWalletConfigurationDetails(string CredentialUId)
        {
            var walletConfiguration = new List<WalletConfigurationViewModel>();
            var WalletConfigInDb = await _walletService.GetWalletConfiguration(CredentialUId);
            if (WalletConfigInDb == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Wallet Configuration details", UUID, Email);
                return NotFound();
            }

            foreach (var item in WalletConfigInDb)
            {
                walletConfiguration.Add(new WalletConfigurationViewModel
                {
                    format = item.format,
                    bindingMethod = item.bindingMethod,
                    supportedMethod = item.supportedMethod
                });
            }


            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.SUCCESS.ToString(), "Get Credential details of ", UUID, Email);
            return PartialView("_ViewWalletConfigurationDetails", walletConfiguration);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string uid)
        {

            var credentialInDb = await _walletService.GetCredentialById(uid);
            if (credentialInDb == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                return NotFound();
            }

            var authSchemeTypesList = await GetAuthSchemeTypesList();
            var categoryList = await GetCategotyList2();
            var orgCategories = await _walletService.GetOrgCategoriesList();

            var credentialViewModel = new CredentialsViewModel
            {
                Id = credentialInDb.Id,
                CredentialId = credentialInDb.CredentialId,
                CredentialName = credentialInDb.CredentialName,
                AuthenticationScheme = credentialInDb.AuthenticationScheme,
                DataAttributes = credentialInDb.DataAttributes,
                CategoryId = credentialInDb.CategoryId,
                VerificationDocType = credentialInDb.VerificationDocType,
                OrganizationId = credentialInDb.OrganizationId,
                Status = credentialInDb.Status,
                CredentialUId = uid,
                Logo = credentialInDb.Logo,
                DisplayName = credentialInDb.DisplayName,
                CredentialTrustUrl = credentialInDb.trustUrl,
                AuthSchemeTypesList = authSchemeTypesList,
                CategoryList = categoryList,
                validity = credentialInDb.validity.ToString(),
                OrganizationCategories = orgCategories,
                SelectedOrgCategoryIds = credentialInDb.categories,

            };

            var categoryName = await GetCategoryNameById(credentialInDb.CategoryId!);
            credentialViewModel.Category = categoryName;

            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.SUCCESS.ToString(), "Get Credential details of ", UUID, Email);

            return View(credentialViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory([FromBody] CredentialAddViewModel credentialAddViewModel)
        {
            string logMessage;
            try
            {
                var categoryList = await GetCategotyList();


                CredentialListDTO dto = new CredentialListDTO()
                {
                    Id = credentialAddViewModel.Id,
                    CredentialName = credentialAddViewModel.CredentialName,
                    CredentialId = credentialAddViewModel.CredentialId,
                    VerificationDocType = credentialAddViewModel.VerificationDocType,
                    DataAttributes = credentialAddViewModel.DataAttributes,
                    AuthenticationScheme = credentialAddViewModel.AuthenticationScheme,
                    //CategoryId = credentialAddViewModel.Category,
                    OrganizationId = OrganizationId,
                    Logo = credentialAddViewModel.Logo,
                    DisplayName = credentialAddViewModel.DisplayName,
                    CredentialUId = credentialAddViewModel.CredentialId,
                    trustUrl = credentialAddViewModel.CredentialTrustUrl,
                    validity = int.Parse(credentialAddViewModel.validity!),
                    categories = credentialAddViewModel.SelectedOrgCategoryIds,

                };
                foreach (var category in categoryList)
                {
                    if (category.Text == credentialAddViewModel.Category)
                    {
                        dto.CategoryId = category.Value;
                        break;
                    }
                }

                var response = await _walletService.UpdateCredentialsAsync(dto);

                if (response.Success)
                {

                    logMessage = $"Successfully Updated credential category";
                    SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials,
                         "Credentials category update", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                    return Json(new { success = true, message = response.Message });

                }
                else
                {
                    logMessage = $"Failed to Update credential category";
                    SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials,
                         "Credentials category update", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                    return Json(new { success = false, message = response.Message });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RevokeCategory([FromBody] RevokeCredentialViewModel revokeCredentialViewModel)
        {

            RevokeCredentialDTO CredentialRevokeDTO = new RevokeCredentialDTO()
            {
                DocumentId = revokeCredentialViewModel.DocumentID,
                CredentialId = revokeCredentialViewModel.Credential
            };

            var response = await _walletService.RevokeredentialsAsync(CredentialRevokeDTO);
            if (response == null || !response.Success)
            {
                return Json(new { success = false, message = response == null ? "Internal error please contact to admin" : response.Message });
            }
            else
            {

                return Json(new { success = true, message = response.Message });

            }
        }

        [HttpGet]
        public async Task<IActionResult> Revoke()
        {

            var credentialList = await GetCredentialList();

            var CategotyViewModel = new RevokeCredentialViewModel
            {
                CredentialList = credentialList
            };
            return View(CategotyViewModel);
        }
        public async Task<string> GetCategoryNameById(string categoryId)
        {

            var result = await _walletService.GetCategoryNamesAndIdAysnc();
            foreach (var entry in result)
            {
                var parts = entry.Split(',');

                if (parts.Length == 2 && parts[1].Trim() == categoryId)
                {
                    _logger.LogInformation("GetCategoryNamesAndIdAysnc categoryId: {0}", parts[0].Trim());
                    return parts[0].Trim();
                }
            }

            _logger.LogInformation("GetCategoryNamesAndIdAysnc categoryId notfound.");
            return null;
        }


        async Task<List<SelectListItem>> GetCategotyList()
        {
            var result = await _walletService.GetCategoryNamesAndIdAysnc();
            _logger.LogInformation("GetCategoryNamesAndIdAysnc result: ", result);

            var list = new List<SelectListItem>();
            if (result == null)
            {
                return list;
            }
            else
            {
                foreach (var org in result)
                {
                    var orgobj = org.Split(",");
                    list.Add(new SelectListItem { Text = orgobj[0], Value = orgobj[1] });
                }

                return list;
            }
        }

        public async Task<List<SelectListItem>> GetAuthSchemeTypesList()
        {
            var result = await _walletService.GetAuthSchemeTypesAysnc(); // assuming it returns Dictionary<string, string>
            var list = new List<SelectListItem>();

            if (result == null)
            {
                return list;
            }

            foreach (var kvp in result)
            {
                list.Add(new SelectListItem
                {
                    Text = kvp.Value,  // Display value (e.g., "ID_WALLET")
                    Value = kvp.Key    // Underlying value (e.g., "WALLET")
                });
            }

            return list;
        }


        async Task<List<SelectListItem>> GetCategotyList2()
        {
            var result = await _walletService.GetCategoryNamesAndIdAysnc();
            var list = new List<SelectListItem>();
            if (result == null)
            {
                return list;
            }
            else
            {
                foreach (var org in result)
                {
                    var orgobj = org.Split(",");
                    list.Add(new SelectListItem { Text = orgobj[0], Value = orgobj[0] });
                }

                return list;
            }
        }

        async Task<List<SelectListItem>> GetCredentialList()
        {

            var result = await _walletService.GetCredentialNamesAndIdAysnc(OrganizationId);
            var list = new List<SelectListItem>();
            if (result == null)
            {
                return list;
            }
            else
            {
                foreach (var org in result)
                {
                    var orgobj = org.Split(",");
                    list.Add(new SelectListItem { Text = orgobj[0], Value = orgobj[1] });
                }

                return list;
            }
        }

        public async Task<IActionResult> GetDefaultAttributes(string CredentialName)
        {
            if (CredentialName == "pid")
            {
                var pidList = await _walletService.GetPidAttrbutesList();

                return Json(new { success = true, message = "Successfully get the attributes", result = pidList });

            }
            else if (CredentialName == "mDL")
            {
                var mdlList = await _walletService.GetMdlAttrbutesList();

                return Json(new { success = true, message = "Successfully get the attributes", result = mdlList });

            }
            else if (CredentialName == "SocialBenefitCard")
            {
                var socialBenefitList = await _walletService.GetSocialBenefitAttrbutesList();

                return Json(new { success = true, message = "Successfully get the attributes", result = socialBenefitList });

            }
            else
            {
                return Json(new { success = false, message = "Attributes cannot be populated." });
            }

        }


        [HttpPost]


        private IActionResult DownloadFile(byte[] fileContents)
        {
            return File(fileContents, "application/pdf", "Credential_Report");

        }

        [HttpGet]
        public IActionResult DownloadAttributesCSV()
        {
            // Get the path to the CSV file on the server
            string csvFilePath = System.IO.Path.Combine(_environment.WebRootPath, "samples/Attributes_CSV.csv");

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
            string fileName = System.IO.Path.GetFileName(csvFilePath);
            return PhysicalFile(csvFilePath, contentType, fileName);
        }
    }
}
