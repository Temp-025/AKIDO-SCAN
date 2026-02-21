using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.Attribute;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.ViewModel.DocumentTemplates;
using EnterpriseGatewayPortal.Web.ViewModel.VerifyWalletCredential;
using EnterpriseGatewayPortal.Web.ViewModel.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [ServiceFilter(typeof(SessionValidationAttribute))]
    [Authorize]
    public class WalletCredentialVerificationController : BaseController
    {

        private readonly IConfiguration _configuration;
        private readonly IWalletService _walletService;
        private readonly IOrganizationService _organizationService;
        private readonly ILocalBusinessUsersService _localBusinessUsersService;

        public WalletCredentialVerificationController(IAdminLogService adminLogService, ILocalBusinessUsersService localBusinessUsersService, IWalletService walletService, IOrganizationService organizationService,
            IConfiguration configuration

           ) : base(adminLogService)
        {

            _configuration = configuration;
            _walletService = walletService;
            _organizationService = organizationService;
            _localBusinessUsersService = localBusinessUsersService;

        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var viewModel = new List<VerifyCredentialListViewModel>();

            var CrendentialList = await _walletService.GetAllCredentialsListByOrgId(OrganizationId);
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
                    viewModel.Add(new VerifyCredentialListViewModel
                    {
                        id = item.id,
                        credentialName = item.credentialName,
                        credentialId = item.credentialId,
                        organizationName = item.organizationName,

                        emails = item.emails,
                        status = item.status,
                        createdDate = item.createdDate
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
        public async Task<IActionResult> SubscribeNewCredentialList()
        {
            var credentialNamesList = await GetVerifibleCredentialNameList();
            var viewModel = new SubscribeCredentialList
            {

                CredentialNameList = credentialNamesList,
            };

            return View(viewModel);
        }



        [HttpGet]
        public async Task<IActionResult> CredentialSubscribtionDetails(string CredentialUId)
        {
            try
            {

                var privilage = await _organizationService.GetPrevilagesAsync(OrganizationId);
                if (privilage == null)
                {
                    SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "Get all wallet privilages", LogMessageType.FAILURE.ToString(), "Fail to get wallet privilages", UUID, Email);

                }
                var WalletConfigInDb = await _walletService.GetWalletConfiguration(CredentialUId);
                if (WalletConfigInDb == null)
                {
                    SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Wallet Configuration details", UUID, Email);
                    return NotFound();
                }
                var credentialInDb = await _walletService.GetCredentialById(CredentialUId);
                if (credentialInDb == null)
                {
                    SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                    return NotFound();
                }
                var credentialConsent = await _walletService.GetCredentialConsentDetails();
                if (credentialConsent == null)
                {
                    SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential Consent details", UUID, Email);
                    return NotFound();
                }
                //var bulkSignerListDetails = await _localBusinessUsersService.GetBulkSignerListAsync();
                var bulkSignerListDetails = await _localBusinessUsersService.GetAllBusinessUsersByOrgUidAsync(OrganizationId);
                var org = (IEnumerable<OrgSubscriberEmail>)bulkSignerListDetails.Resource;
                var list = new List<string>();
                if (org == null)
                {
                    SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                    return NotFound();
                }
                //==================

                list.AddRange(
                    org.Select(o => o.EmployeeEmail)
                       .Where(email => !string.IsNullOrWhiteSpace(email))
                       .Select(email => email!)
                       .Distinct()
                );

                BulkSignerListViewModel bulkSignerList = new BulkSignerListViewModel
                {
                    bulkSignerList = list,


                };

                var credentialNamesList = await GetVerifibleCredentialNameList();

                var domainListItems = credentialConsent
                    .Select(domain => new SelectListItem
                    {
                        Text = domain.displayName,
                        Value = domain.id
                    }).ToList();


                var walletConfiguration = new WalletConfigurationViewModel
                {
                    DataAttributes = credentialInDb.DataAttributes,
                    BulkSignerEmails = bulkSignerList,
                    credentialId = CredentialUId,
                    organizationId = OrganizationId,
                    CredentialNameList = credentialNamesList,
                    CredentialName = credentialInDb.DisplayName,
                    Originalconfiguration = (List<WalletConfigurationDTO>)WalletConfigInDb,
                    DomainConsent = credentialConsent.ToList(),
                    DomainsList = domainListItems
                };



                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.SUCCESS.ToString(), "Get Credential details of ", UUID, Email);

                return PartialView("_CredentialSubscribtionDetails", walletConfiguration);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditCredentialSubscribtionDetails(string CredentialUId, int id)
        {


            var privilage = await _organizationService.GetPrevilagesAsync(OrganizationId);
            if (privilage == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "Get all wallet privilages", LogMessageType.FAILURE.ToString(), "Fail to get wallet privilages", UUID, Email);

            }
            var WalletConfigInDb = await _walletService.GetWalletConfiguration(CredentialUId);
            if (WalletConfigInDb == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Wallet Configuration details", UUID, Email);
                return NotFound();
            }
            var credentialInDb = await _walletService.GetCredentialById(CredentialUId);
            if (credentialInDb == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                return NotFound();
            }
            // var bulkSignerListDetails = await _localBusinessUsersService.GetBulkSignerListAsync();
            var bulkSignerListDetails = await _localBusinessUsersService.GetAllBusinessUsersByOrgUidAsync(OrganizationId);
            var org = (IEnumerable<OrgSubscriberEmail>)bulkSignerListDetails.Resource;
            var list = new List<string>();
            if (org == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                return NotFound();
            }
            //==================

            list.AddRange(
                org.Select(o => o.EmployeeEmail)
                   .Where(email => !string.IsNullOrWhiteSpace(email))
                   .Select(email => email!)
                   .Distinct()
            );

            BulkSignerListViewModel bulkSignerList = new BulkSignerListViewModel
            {
                bulkSignerList = list,


            };
            var credentialDetails = await _walletService.GetVerifyCredentialById(id);
            if (credentialDetails == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                return NotFound();
            }



            var credentialNamesList = await GetVerifibleCredentialNameList();



            var walletConfiguration = new WalletConfigurationViewModel
            {
                Id = id,
                DataAttributes = credentialInDb.DataAttributes,
                BulkSignerEmails = bulkSignerList,
                credentialId = CredentialUId,
                organizationId = OrganizationId,
                CredentialNameList = credentialNamesList,
                CredentialName = credentialInDb.DisplayName,
                Selectedconfiguration = credentialDetails.configuration,
                Selectedemails = credentialDetails.emails,
                Originalconfiguration = (List<WalletConfigurationDTO>)WalletConfigInDb,
                status = credentialDetails.status,
            };



            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.SUCCESS.ToString(), "Get Credential details of ", UUID, Email);

            return View(walletConfiguration);
        }

        [HttpPost]
        public async Task<IActionResult> AddCredentialVerificationRquest([FromBody] VerifyCredentialDTO verifyCredential)
        {


            var response = await _walletService.AddVerifyCredentialRequestAsync(verifyCredential);
            if (response == null || !response.Success)
            {
                return Json(new { success = false, message = response == null ? "Internal error please contact to admin" : response.Message });
            }
            else
            {

                return Json(new { success = true, message = response.Message });

            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCredentialVerificationRquest([FromBody] VerifyCredentialDTO verifyCredential)
        {


            var response = await _walletService.UpdateVerifyCredentialRequestAsync(verifyCredential);
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
        public async Task<IActionResult> ViewCredentialDetails(string CredentialUId)
        {

            var credentialInDb = await _walletService.GetCredentialById(CredentialUId);
            if (credentialInDb == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                return NotFound();
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
                OrganizationId = OrganizationId,
                Status = credentialInDb.Status,
                CredentialUId = CredentialUId,
                Logo = credentialInDb.Logo,
                DisplayName = credentialInDb.DisplayName,

            };

            var categoryName = await GetCategoryNameById(credentialInDb.CategoryId!);
            credentialViewModel.Category = categoryName;

            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.SUCCESS.ToString(), "Get Credential details of ", UUID, Email);


            return PartialView("_ViewCredentialDetails", credentialViewModel);
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
            return PartialView("_ViewCredentialConfigurationDetails", walletConfiguration);

        }

        [HttpGet]
        public async Task<IActionResult> ViewVerifyCredentialDetails(int id)
        {

            var credentialInDb = await _walletService.GetVerifyCredentialById(id);
            if (credentialInDb == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
                return NotFound();
            }


            var verifyCredentialViewModel = new VerifyCredentialDTO
            {
                id = credentialInDb.id,
                credentialId = credentialInDb.credentialId,
                credentialName = credentialInDb.credentialName,


                attributes = credentialInDb.attributes,
                emails = credentialInDb.emails,
                organizationId = OrganizationId,
                organizationName = credentialInDb.organizationName,
                createdDate = credentialInDb.createdDate,
                status = credentialInDb.status,
                configuration = credentialInDb.configuration


            };


            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.SUCCESS.ToString(), "Get Credential details of ", UUID, Email);

            return View(verifyCredentialViewModel);
        }

        async Task<List<SelectListItem>> GetVerifibleCredentialNameList()
        {
            var result = await _walletService.GetCredentialNamesAndIdListAysnc(OrganizationId);
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

        public async Task<string> GetCategoryNameById(string categoryId)
        {

            var result = await _walletService.GetCategoryNamesAndIdAysnc();
            // Iterate through each string and check if it matches the categoryId
            foreach (var entry in result)
            {
                var parts = entry.Split(',');

                if (parts.Length == 2 && parts[1].Trim() == categoryId)
                {
                    // Return the first part (category name) if a match is found
                    return parts[0].Trim();
                }
            }

            // Return null if no match is found
            return null;
        }
    }
}
