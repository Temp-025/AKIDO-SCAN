using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.ViewModel.ESealRegistration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [Authorize]
    public class ESealRegistrationController : BaseController
    {
        private readonly IOrganizationService _organizationService;
        private readonly IConfiguration _configuration;
        private readonly IOrganizationDetailService _organizationDetailService;
        private readonly IOrgEmailDomainService _organizationEmailDomainService;
        public ESealRegistrationController(IAdminLogService adminLogService, IOrganizationService organizationService, IConfiguration configuration, IOrganizationDetailService organizationDetailService, IOrgEmailDomainService orgEmailDomainService) : base(adminLogService)

        {
            _organizationService = organizationService;
            _configuration = configuration;
            _organizationDetailService = organizationDetailService;
            _organizationEmailDomainService = orgEmailDomainService;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetOrganizationById()
        {
            string logMessage;
            string emailDomain = null;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var organizationDetails = await _organizationDetailService.GetOrganizationDetailByUIdAsync(organizationUid);
            if (organizationDetails == null)
            {
                logMessage = $"Failed to get organization details with OragnizationUid {organizationUid}";
                SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                    "Get Organization details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                return NotFound();
            }
            var org = (OrganizationDetail)organizationDetails.Resource;
            foreach (var data in org.OrgEmailDomains)
            {
                if (data.OrganizationUid == organizationUid)
                {
                    emailDomain = data.EmailDomain;
                }
            }
            if (org == null)
            {
                logMessage = $"Failed to get organization details with OragnizationUid {organizationUid}";
                SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                    "Get Organization details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                return NotFound();
            }

            var templateList = await _organizationService.GetSignatureTemplateListAsyn();
            if (templateList == null)
            {
                logMessage = $"Failed to get organization signature templates list";
                SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                    "Get Organization signature templates list", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                return NotFound();
            }
            var previlageResponse = await _organizationService.GetPrevilagesAsync(organizationUid);
            var esealCertificateStatus = await _organizationService.GetEsealCertificateStatus(organizationUid);
            if (esealCertificateStatus == null)
            {
                return NotFound();

            }

            string[] address = org.CorporateOfficeAddress.Split(';');
            ESealRegistrationEditViewModel viewModel = new ESealRegistrationEditViewModel
            {
                OrganizationId = org.OrganizationDetailsId,
                OrganizationUID = org.OrganizationUid,
                OrganizationName = org.OrgName,
                OrganizationEmail = org.OrganizationEmail,
                UniqueRegdNo = org.UniqueRegdNo,
                TaxNo = org.TaxNo,
                CorporateOfficeAddress1 = address[0],
                CorporateOfficeAddress2 = address[1],
                Country = address[2],
                Pincode = address[3],
                Status = org.OrganizationStatus,
                ResizedEsealImage = org.ESealImage,
                SpocUgpassEmail = org.SpocUgpassEmail,
                AgentUrl = org.AgentUrl,
                EnablePostPaidOption = (bool)org.EnablePostPaidOption!,
                //TemplateList = templateList,
                CreatedBy = org.CreatedBy,
                EmailDomain = emailDomain,
                Previlages = previlageResponse.Privileges,


            };
            if (esealCertificateStatus.Success)
            {
                var esealStatus = JsonConvert.DeserializeObject<EsealCertificateStatusDTO>(esealCertificateStatus.Resource.ToString()!);
                viewModel.certificateStatus = esealStatus.certificateStatus;
                viewModel.certificateEndDate = esealStatus.certificateEndDate;
                viewModel.certificateStartDate = esealStatus.certificateStartDate;
            }
            else
            {
                viewModel.certificateStatus = esealCertificateStatus.Message;
            }
            logMessage = $"Successfully received organization details for {org.OrgName} Oragnization.";
            SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                "Get Organization details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
            return View("_Edit", viewModel);
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetKYCOrganizationById()
        {
            string logMessage;
            string emailDomain = null;
            var organizationUid = _configuration["KycOrganizationUid"];
            if (organizationUid == null)
            {
                logMessage = $"Failed to get organization ID From Configuration";
                SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                    "Get Organization ID", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                return NotFound();
            }
            var organizationDetails = await _organizationService.GetOrganizationDetailsByUIdAsync(organizationUid);
            if (organizationDetails == null)
            {
                logMessage = $"Failed to get organization details with OragnizationUid {organizationUid}";
                SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                    "Get Organization details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                return NotFound();
            }
            var org = (OrganizationDTO)organizationDetails.Resource;
            foreach (var data in org.OrgEmailDomains)
            {
                if (data.OrganizationUid == organizationUid)
                {
                    emailDomain = data.EmailDomain;
                }
            }
            if (org == null)
            {
                logMessage = $"Failed to get organization details with OragnizationUid {organizationUid}";
                SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                    "Get Organization details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                return NotFound();
            }

            var templateList = await _organizationService.GetSignatureTemplateListAsyn();
            if (templateList == null)
            {
                logMessage = $"Failed to get organization signature templates list";
                SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                    "Get Organization signature templates list", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                return NotFound();
            }
            var previlageResponse = await _organizationService.GetPrevilagesAsync(organizationUid);
            var esealCertificateStatus = await _organizationService.GetEsealCertificateStatus(organizationUid);
            if (esealCertificateStatus == null)
            {
                return NotFound();

            }

            string[] address = org.CorporateOfficeAddress.Split(';');
            ESealRegistrationEditViewModel viewModel = new ESealRegistrationEditViewModel
            {
                //OrganizationId = org.OrganizationDetailsId,
                OrganizationUID = org.OrganizationUid,
                OrganizationName = org.OrganizationName,
                OrganizationEmail = org.OrganizationEmail,
                UniqueRegdNo = org.UniqueRegdNo,
                TaxNo = org.TaxNo,
                CorporateOfficeAddress1 = address[0],
                CorporateOfficeAddress2 = address[1],
                Country = address[2],
                Pincode = address[3],
                Status = org.Status,
                ResizedEsealImage = org.ESealImage,
                SpocUgpassEmail = org.SpocUgpassEmail,
                AgentUrl = org.AgentUrl,
                EnablePostPaidOption = (bool)org.EnablePostPaidOption,
                //TemplateList = templateList,
                CreatedBy = org.CreatedBy,
                EmailDomain = emailDomain,
                Previlages = previlageResponse.Privileges,


            };
            if (esealCertificateStatus.Success)
            {
                var esealStatus = JsonConvert.DeserializeObject<EsealCertificateStatusDTO>(esealCertificateStatus.Resource.ToString()!);
                viewModel.certificateStatus = esealStatus.certificateStatus;
                viewModel.certificateEndDate = esealStatus.certificateEndDate;
                viewModel.certificateStartDate = esealStatus.certificateStartDate;
            }
            else
            {
                viewModel.certificateStatus = esealCertificateStatus.Message;
            }
            logMessage = $"Successfully received organization details with OragnizationUid {organizationUid}";
            SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                "Get Organization details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
            return View("_editKYC", viewModel);
        }

        [NonAction]
        private List<DocumentListItem> GetCheckedDocuments(List<DocumentListItem> documentListItems,
           List<string> checkedDocumentList = null)
        {
            if (checkedDocumentList != null)
            {
                foreach (var document in documentListItems)
                {
                    if (checkedDocumentList.Contains(document.DisplayName))
                    {
                        document.IsSelected = true;
                    }
                }
            }

            return documentListItems;
        }

        private IEnumerable<OrganizationUser> GetOrganizationUsers(IList<OrganizationUser> orgUserList)
        {
            if (orgUserList != null)
            {
                foreach (var orgUser in orgUserList)
                    yield return orgUser;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAgentUrl(UpdateSpocAndAgentUrlViewModel viewModel)
        {
            string logMessage;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            AgentUrlAndSpocUpdateDTO gentUrlAndSpocUpdateDTO = new AgentUrlAndSpocUpdateDTO
            {
                OrganizationUid = organizationUid,
                SpocUgpassEmail = null,
                AgentUrl = viewModel.AgentUrl,
            };

            var response = await _organizationService.UpdateAgentUrlAsync(gentUrlAndSpocUpdateDTO);
            if (!response.Success)
            {
                logMessage = $"Failed to update agentUrl Or Spoc Email in server database";
                SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                    "Get Organization details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                return Json(new { Status = "Failed", Title = "Organization Details", Message = response.Message });
            }
            else
            {
                gentUrlAndSpocUpdateDTO.SpocUgpassEmail = viewModel.SpocUgpassEmail;
                var response2 = await _organizationDetailService.UpdateAgentUrlAndSpocEmailAsync(gentUrlAndSpocUpdateDTO);
                {
                    if (!response2.Success)
                    {
                        logMessage = $"Failed to update agentUrl Or Spoc Email in local Database";
                        SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                            "Get Organization details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                        return Json(new { Status = "Failed", Title = "Organization Details", Message = response2.Message });
                    }
                    else
                    {
                        logMessage = $"Successfully updated the AgentUrl Or Spoc Email in local Database";
                        SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                            "Get Organization details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                        return Json(new { Status = "Success", Title = "Organization Details", Message = response2.Message });
                    }

                }

            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateSpocEmail(UpdateSpocAndAgentUrlViewModel viewModel)
        {
            string logMessage;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            AgentUrlAndSpocUpdateDTO gentUrlAndSpocUpdateDTO = new AgentUrlAndSpocUpdateDTO
            {
                OrganizationUid = organizationUid,
                SpocUgpassEmail = viewModel.SpocUgpassEmail,
                AgentUrl = null,
            };

            var response = await _organizationService.UpdateSpocEmailAsync(gentUrlAndSpocUpdateDTO);
            if (!response.Success)
            {
                logMessage = $"Failed to update agentUrl Or Spoc Email in server database";
                SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                    "Get Organization details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                return Json(new { Status = "Failed", Title = "Organization Details", Message = response.Message });
            }
            else
            {
                gentUrlAndSpocUpdateDTO.AgentUrl = viewModel.AgentUrl;
                var response2 = await _organizationDetailService.UpdateAgentUrlAndSpocEmailAsync(gentUrlAndSpocUpdateDTO);
                {
                    if (!response2.Success)
                    {
                        logMessage = $"Failed to update agentUrl Or Spoc Email in local Database";
                        SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                            "Get Organization details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                        return Json(new { Status = "Failed", Title = "Organization Details", Message = response2.Message });
                    }
                    else
                    {
                        logMessage = $"Successfully updated the AgentUrl Or Spoc Email in local Database";
                        SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                            "Get Organization details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                        return Json(new { Status = "Success", Title = "Organization Details", Message = response2.Message });
                    }

                }

            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateEmailDomain(UpdateEmailDomainViewModel viewModel)
        {
            string logMessage;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            EmailDomainUpdateDTO getEmailDomainUpdateDTO = new EmailDomainUpdateDTO
            {
                OrganizationUid = organizationUid,
                EmailDomain = viewModel.EmailDomain,

            };

            var response = await _organizationService.UpdateEmailDomainAsync(getEmailDomainUpdateDTO);
            if (!response.Success)
            {
                logMessage = $"Failed to update Email Domain in server database";
                SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                    "Get Organization details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                return Json(new { Status = "Failed", Title = "Organization Details", Message = response.Message });
            }
            else
            {
                int emailDomainuniqueId = 0;
                var organizationDetails = await _organizationDetailService.GetOrganizationDetailByUIdAsync(organizationUid);
                var org = (OrganizationDetail)organizationDetails.Resource;
                if (org.OrgEmailDomains.Count() == 0)
                {
                    OrgEmailDomain addEmailDomain = new OrgEmailDomain
                    {
                        OrganizationUid = organizationUid,
                        EmailDomain = viewModel.EmailDomain,
                        OrganizationU = org,
                        OrgDomainId = emailDomainuniqueId,
                    };

                    var response3 = await _organizationEmailDomainService.AddOrgEmailDomainAsync(addEmailDomain);
                    {
                        if (!response3.Success)
                        {
                            logMessage = $"Failed to update agentUrl Or Spoc Email in local Database";
                            SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                                "Get Organization details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                            return Json(new { Status = "Failed", Title = "Organization Details", Message = response3.Message });
                        }
                        else
                        {
                            logMessage = $"Successfully updated the AgentUrl Or Spoc Email in local Database";
                            SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                                "Get Organization details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                            return Json(new { Status = "Success", Title = "Organization Details", Message = response3.Message });
                        }

                    }
                }
                else
                {
                    foreach (var data in org.OrgEmailDomains)
                    {
                        if (data.OrganizationUid == organizationUid)
                        {
                            emailDomainuniqueId = data.OrgDomainId;
                        }
                    }
                    OrgEmailDomain getEmailDomainUpdate = new OrgEmailDomain
                    {
                        OrganizationUid = organizationUid,
                        EmailDomain = viewModel.EmailDomain,
                        OrganizationU = org,
                        OrgDomainId = emailDomainuniqueId,
                    };
                    var response2 = await _organizationEmailDomainService.UpdateOrgEmailDomainAsync(getEmailDomainUpdate);
                    {
                        if (!response2.Success)
                        {
                            logMessage = $"Failed to update agentUrl Or Spoc Email in local Database";
                            SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                                "Get Organization details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                            return Json(new { Status = "Failed", Title = "Organization Details", Message = response2.Message });
                        }
                        else
                        {
                            logMessage = $"Successfully updated the AgentUrl Or Spoc Email in local Database";
                            SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
                                "Get Organization details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                            return Json(new { Status = "Success", Title = "Organization Details", Message = response2.Message });
                        }

                    }
                }

            }

        }

    }
}
