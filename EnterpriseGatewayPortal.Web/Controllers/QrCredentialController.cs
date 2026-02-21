using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.Attribute;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.Utilities;
using EnterpriseGatewayPortal.Web.ViewModel.QrCredential;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;




namespace EnterpriseGatewayPortal.Web.Controllers
{
    [ServiceFilter(typeof(SessionValidationAttribute))]
    [Authorize]
    public class QrCredentialController : BaseController
    {

        private readonly IConfiguration _configuration;
        private readonly IWalletService _walletService;
        private readonly IQrCredentialService _qrCredentialService;
        private readonly IOrganizationService _organizationService;
        private readonly IRazorRendererHelper _razorRendererHelper;
        private readonly DataExportService _dataExportService;
        private readonly IDocumentService _documentService;

        public QrCredentialController(IAdminLogService adminLogService, IWalletService walletService, IQrCredentialService qrCredentialService, IOrganizationService organizationService,
            IConfiguration configuration, IRazorRendererHelper razorRendererHelper, DataExportService dataExportService,
            IDocumentService documentService

           ) : base(adminLogService)
        {

            _configuration = configuration;
            _walletService = walletService;
            _organizationService = organizationService;
            _razorRendererHelper = razorRendererHelper;
            _dataExportService = dataExportService;
            _documentService = documentService;
            _qrCredentialService = qrCredentialService;

        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var viewModel = new List<QrCredentialViewModel>();

            var CrendentialList = await _qrCredentialService.GetAllQRCredentialsList(OrganizationId);
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
                    viewModel.Add(new QrCredentialViewModel
                    {
                        Id = item.Id,
                        CredentialName = item.credentialName,
                        CredentialId = item.credentialId,
                        VerificationDocType = item.verificationDocType,
                        AuthenticationScheme = item.authenticationScheme,
                        Category = item.categoryId,
                        OrganizationId = item.organizationId,
                        Status = item.status,
                        DisplayName = item.displayName,
                        createdDate = item.createdDate,
                        CredentialUId = item.credentialUId,

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
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddQRCredential([FromBody] QrCredentialViewModel qrCredentialAddViewModel)
        {

            QrCredentialDTO QrCredentialAddDTO = new QrCredentialDTO()
            {

                credentialName = qrCredentialAddViewModel.CredentialName,
                credentialId = Guid.NewGuid().ToString(),
                verificationDocType = qrCredentialAddViewModel.VerificationDocType,
                dataAttributes = qrCredentialAddViewModel.DataAttributes,
                authenticationScheme = qrCredentialAddViewModel.AuthenticationScheme,
                categoryId = qrCredentialAddViewModel.Category,
                organizationId = OrganizationId,
                logo = qrCredentialAddViewModel.Logo,
                displayName = qrCredentialAddViewModel.DisplayName,
                portraitVerificationRequired = qrCredentialAddViewModel.portraitVerificationRequired


            };

            var response = await _qrCredentialService.AddQRCredentialsAsync(QrCredentialAddDTO);
            if (response == null || !response.Success)
            {
                return Json(new { success = false, message = response == null ? "Internal error please contact to admin" : response.Message });
            }
            else
            {

                return Json(new { success = true, message = response.Message, result = response.Resource });

            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            var credentialInDb = await _qrCredentialService.GetQRCredentialById(id);
            if (credentialInDb == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                return NotFound();
            }


            var qrCredentialViewModel = new QrCredentialViewModel
            {
                Id = credentialInDb.Id,
                CredentialId = credentialInDb.credentialId,
                CredentialName = credentialInDb.credentialName,

                DataAttributes = credentialInDb.dataAttributes,

                OrganizationId = credentialInDb.organizationId,
                Status = credentialInDb.status,
                CredentialUId = id,

                DisplayName = credentialInDb.displayName,

            };


            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.SUCCESS.ToString(), "Get Credential details of ", UUID, Email);

            return View(qrCredentialViewModel);
        }

        [HttpGet]
        public IActionResult QRTestCredential(string CredentialUid)
        {


            var testCredentail = new QrTestCredentialViewModel
            {
                CredentialId = CredentialUid,

            };

            return View(testCredentail);
        }


        [HttpPost]
        public async Task<IActionResult> QRTestCredential([FromBody] QrTestCredentialViewModel qrTestCredential)
        {
            try
            {
                if (qrTestCredential.Data == null)
                {
                    return Json(new { success = false, message = "QRTestCredential Data field is required." });
                }
                // Deserialize Data string to actual object
                var result = JsonConvert.DeserializeObject<QrTestCredentialDTO>(qrTestCredential.Data);

                // Optional: assign CredentialId from wrapper view model
                result.CredentialId = qrTestCredential.CredentialId;

                var response = await _qrCredentialService.QRTestCredentialByDocumentId(result);
                if (response == null || !response.Success)
                {
                    return Json(new { success = false, message = response == null ? "Internal error please contact to admin" : response.Message });
                }
                else
                {

                    return Json(new { success = true, message = response.Message });

                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Failed to deserialize: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewQRCredentialDetails(string id)
        {

            var credentialInDb = await _qrCredentialService.GetQRCredentialById(id);
            if (credentialInDb == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                return NotFound();
            }


            var qrCredentialViewModel = new QrCredentialViewModel
            {
                Id = credentialInDb.Id,
                CredentialId = credentialInDb.credentialId,
                CredentialName = credentialInDb.credentialName,

                DataAttributes = credentialInDb.dataAttributes,

                OrganizationId = credentialInDb.organizationId,
                Status = credentialInDb.status,
                CredentialUId = id,

                DisplayName = credentialInDb.displayName,

            };


            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.SUCCESS.ToString(), "Get Credential details of ", UUID, Email);

            return View(qrCredentialViewModel);
        }

        async Task<List<SelectListItem>> GetCategotyList()
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
                    list.Add(new SelectListItem { Text = orgobj[0], Value = orgobj[1] });
                }

                return list;
            }
        }


    }
}
