using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [Authorize]
    public class OrganizationCompleteDetailsController : Controller
    {
        private readonly ILogger<OrganizationCompleteDetailsController> _logger;
        private readonly IOrganizationService _organizationService;
        private readonly IConfiguration _configuration;
        private readonly IBussinessUserService _bussinessUserService;
        private readonly IOrganizationDetailService _organizationDetailService;
        private readonly ILocalBusinessUsersService _localBusinessUsersService;
        private readonly ITemplateService _templateService;
        private readonly IOrgSignatureTemplateService _orgSignatureTemplateService;
        private readonly IClientService _clientService;
        private readonly ILocalClientService _localClientService;
        private readonly IESealDetailService _eSealDetailService;
        private readonly IOrganizationCertificateService _organizationCertificateService;
        private readonly ISignatureTemplateService _signatureTemplateService;
        private readonly ILocalTemplateService _localTemplateService;
        private readonly IDocumentTemplatesService _documentTemplatesService;
        private readonly ISubscriberOrgTemplateService _subscriberOrgTemplateService;
        private readonly IDeviceService _deviceService;
        private readonly IConfigurationService _configurationService;
        public OrganizationCompleteDetailsController(ILogger<OrganizationCompleteDetailsController> logger,
            IOrganizationService organizationService,
            IConfiguration configuration,
            IBussinessUserService bussinessUserService,
            IOrganizationDetailService organizationDetailService,
            ILocalBusinessUsersService localBusinessUsersService,
            ITemplateService templateService,
            IOrgSignatureTemplateService orgSignatureTemplateService,
            IClientService clientService,
            ILocalClientService localClientService,
            IESealDetailService eSealDetailService,
            IOrganizationCertificateService organizationCertificateService,
            ISignatureTemplateService signatureTemplateService,
            ILocalTemplateService localTemplateService,
            IDocumentTemplatesService documentTemplatesService,
            ISubscriberOrgTemplateService subscriberOrgTemplateService,
            IDeviceService deviceService,
            IConfigurationService configurationService)
        {
            _logger = logger;
            _organizationService = organizationService;
            _configuration = configuration;
            _bussinessUserService = bussinessUserService;
            _organizationDetailService = organizationDetailService;
            _localBusinessUsersService = localBusinessUsersService;
            _templateService = templateService;
            _orgSignatureTemplateService = orgSignatureTemplateService;
            _clientService = clientService;
            _localClientService = localClientService;
            _eSealDetailService = eSealDetailService;
            _organizationCertificateService = organizationCertificateService;
            _signatureTemplateService = signatureTemplateService;
            _localTemplateService = localTemplateService;
            _documentTemplatesService = documentTemplatesService;
            _subscriberOrgTemplateService = subscriberOrgTemplateService;
            _deviceService = deviceService;
            _configurationService = configurationService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Oraganizations deatils fetch started");

                var licenseResult = await _deviceService.ReadLicence();

                if (licenseResult == null || !licenseResult.Success)
                {
                    return null;
                }

                var licenceDetails = (LicenceDTO)licenseResult.Resource;

                var clientDetails = await _clientService.GetClientAsync(licenceDetails.clientId);
                if (clientDetails == null)
                {
                    return null;
                }

                var organizationUid = clientDetails.OrganizationId;
                if (organizationUid == null)
                {
                    _logger.LogError("Organization UID is Null");
                    return NotFound();
                }
                var organizationDetails = await _organizationService.GetOrganizationDetailsByUId(organizationUid);
                if (organizationDetails == null)
                {
                    return NotFound();
                }
                var orgDetails = new OrganizationDetail()
                {
                    AgentUrl = organizationDetails.AgentUrl,
                    OrganizationUid = organizationDetails.OrganizationUid,
                    OrgName = organizationDetails.OrganizationName,
                    OrganizationEmail = organizationDetails.OrganizationEmail,
                    UniqueRegdNo = organizationDetails.UniqueRegdNo,
                    TaxNo = organizationDetails.TaxNo,
                    CorporateOfficeAddress = organizationDetails.CorporateOfficeAddress,
                    OrganizationStatus = organizationDetails.Status,
                    ESealImage = organizationDetails.ESealImage,
                    SpocUgpassEmail = organizationDetails.SpocUgpassEmail,
                    EnablePostPaidOption = organizationDetails.EnablePostPaidOption,
                    CreatedBy = organizationDetails.CreatedBy,
                };
                var response = await _organizationDetailService.AddOrganizationDetailAsync(orgDetails);
                var businessuserslist = await _bussinessUserService.GetAllBusinessUserAsync(organizationUid);

                List<OrgSubscriberEmail> businessusers = new List<OrgSubscriberEmail>();
                if (businessuserslist != null)
                {
                    foreach (var item in businessuserslist)
                    {
                        var user = new OrgSubscriberEmail()
                        {
                            OrgContactsId = item.orgContactsEmailId,
                            OrganizationUid = item.OrganizationUid,
                            EmployeeEmail = item.EmployeeEmail,
                            IsEsealSignatory = item.ESealSignatory,
                            IsEsealPreparatory = item.ESealPrepatory,
                            IsBulkSign = item.Bulksign,
                            IsOrgSignatory = item.Signatory,
                            Designation = item.Designation,
                            SignaturePhoto = item.SignaturePhoto,
                            IsTemplate = item.Template,
                            UgpassEmail = item.UgpassEmail,
                            PassportNumber = item.PassportNumber,
                            NationalIdNumber = item.NationalIdNumber,
                            MobileNumber = item.MobileNumber,
                            UgpassUserLinkApproved = item.UgpassUserLinkApproved,
                            SubscriberUid = item.SubscriberUid,
                            Status = item.Status,
                            IsDelegate = item.Delegate,
                        };
                        businessusers.Add(user);
                    }
                }

                var result = await _localBusinessUsersService.AddBusinessUsersListAsync(businessusers);

                var templatesList = await _templateService.GetSignatureTemplateListAsync();

                foreach (var template in templatesList)
                {
                    SignatureTemplate signatureTemplate = new SignatureTemplate()
                    {
                        Id = template.Id,
                        TemplateId = template.TemplateId,
                        DisplayName = template.DisplayName,
                        Type = template.Type,
                        SamplePreview = template.SamplePreview
                    };
                    var signaturetemplateResponse = await _signatureTemplateService.AddSignatureTemplateAsync(signatureTemplate);
                }

                OrganizationTemplatesDTO signatureTemplates = await _templateService.GetOrganizationTemplates(organizationUid);

                var result1 = await _orgSignatureTemplateService.AddOrgSignatureTemplateAsync(signatureTemplates);

                var clientList = await _clientService.getAllClientsList(organizationUid);

                foreach (var client in clientList)
                {
                    var clientresponse = await _localClientService.CreateClientAsync(client);
                }

                var certificate = await _eSealDetailService.GetCertificateDetails(organizationUid);

                var certificateresult = await _organizationCertificateService.AddOrganizationCertificateAsync(certificate);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound();
            }
            return RedirectToAction("Index", "Dashboard");
        }

        private IEnumerable<OrganizationUser> GetOrganizationUsers(IList<OrganizationUser> orgUserList)
        {
            if (orgUserList != null)
            {
                foreach (var orgUser in orgUserList)
                    yield return orgUser;
            }
        }

    }
}

