using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.Attribute;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Controllers;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.Utilities;
using EnterpriseGatewayPortal.Web.ViewModel.DocumentTemplates;
using EnterpriseGatewayPortal.Web.ViewModel.QrSubscribeCredential;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[ServiceFilter(typeof(SessionValidationAttribute))]
[Authorize]
public class QrSubscribeCredentialController : BaseController
{

    private readonly IConfiguration _configuration;
    private readonly IWalletService _walletService;
    private readonly IQrCredentialService _qrCredentialService;
    private readonly IOrganizationService _organizationService;
    private readonly IRazorRendererHelper _razorRendererHelper;
    private readonly DataExportService _dataExportService;
    private readonly IDocumentService _documentService;
    private readonly ILocalBusinessUsersService _localBusinessUsersService;

    public QrSubscribeCredentialController(IAdminLogService adminLogService, IWalletService walletService, IQrCredentialService qrCredentialService, ILocalBusinessUsersService localBusinessUsersService, IOrganizationService organizationService,
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
        _localBusinessUsersService = localBusinessUsersService;

    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var viewModel = new List<QrSubscribeCredentialViewModel>();

        var CrendentialList = await _qrCredentialService.GetAllQRCredentialsListByOrgId(OrganizationId);
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
                viewModel.Add(new QrSubscribeCredentialViewModel
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
    public async Task<IActionResult> SubscribeNewQRCredentialList()
    {
        var credentialNamesList = await GetVerifibleCredentialNameList();
        var viewModel = new SubscribeQrCredentialList
        {

            CredentialNameList = credentialNamesList,
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> QrCredentialSubscribtionDetails(string CredentialUId)
    {


        var privilage = await _organizationService.GetPrevilagesAsync(OrganizationId);
        if (privilage == null)
        {
            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "Get all wallet privilages", LogMessageType.FAILURE.ToString(), "Fail to get wallet privilages", UUID, Email);

        }

        var credentialInDb = await _qrCredentialService.GetQRCredentialById(CredentialUId);
        if (credentialInDb == null)
        {
            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
            return NotFound();
        }
        var bulkSignerListDetails = await _localBusinessUsersService.GetBulkSignerListAsync();
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


        var qrConfiguration = new QrSubscribeCredentialViewModel
        {
            attributes = credentialInDb.dataAttributes,
            BulkSignerEmails = bulkSignerList,
            credentialId = CredentialUId,
            organizationId = OrganizationId,
            CredentialNameList = credentialNamesList,
            CredentialName = credentialInDb.credentialName,

        };



        SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.SUCCESS.ToString(), "Get Credential details of ", UUID, Email);




        return PartialView("_QrCredentialSubscribtionDetails", qrConfiguration);
    }

    [HttpPost]
    public async Task<IActionResult> AddQRCredentialVerificationRquest([FromBody] QrCredentialVerifierDTO verifyCredential)
    {


        var response = await _qrCredentialService.AddQRVerifyCredentialRequestAsync(verifyCredential);
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
    public async Task<IActionResult> EditQRCredentialSubscribtionDetails(string CredentialUId, int id)
    {


        var privilage = await _organizationService.GetPrevilagesAsync(OrganizationId);
        if (privilage == null)
        {
            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "Get all wallet privilages", LogMessageType.FAILURE.ToString(), "Fail to get wallet privilages", UUID, Email);

        }


        var bulkSignerListDetails = await _localBusinessUsersService.GetBulkSignerListAsync();
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
        var credentialDetails = await _qrCredentialService.GetQRVerifyCredentialById(id);
        if (credentialDetails == null)
        {
            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.FAILURE.ToString(), "Fail to get Credential details", UUID, Email);
            return NotFound();
        }



        var credentialNamesList = await GetVerifibleCredentialNameList();



        var QrConfiguration = new QrSubscribeCredentialViewModel
        {
            id = id,
            attributes = credentialDetails.attributes,
            BulkSignerEmails = bulkSignerList,
            credentialId = CredentialUId,
            organizationId = OrganizationId,
            CredentialNameList = credentialNamesList,
            CredentialName = credentialDetails.credentialName,

            Selectedemails = credentialDetails.emails,

            status = credentialDetails.status,
        };



        SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential details", LogMessageType.SUCCESS.ToString(), "Get Credential details of ", UUID, Email);

        return View(QrConfiguration);
    }


    [HttpPost]
    public async Task<IActionResult> UpdateCredentialVerificationRquest([FromBody] QrCredentialVerifierDTO verifyCredential)
    {


        var response = await _qrCredentialService.UpdateVerifyQRCredentialRequestAsync(verifyCredential);
        if (response == null || !response.Success)
        {
            return Json(new { success = false, message = response == null ? "Internal error please contact to admin" : response.Message });
        }
        else
        {

            return Json(new { success = true, message = response.Message });

        }
    }


    async Task<List<SelectListItem>> GetVerifibleCredentialNameList()
    {
        var result = await _qrCredentialService.GetQRCredentialNamesAndIdListAysnc(OrganizationId);
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

