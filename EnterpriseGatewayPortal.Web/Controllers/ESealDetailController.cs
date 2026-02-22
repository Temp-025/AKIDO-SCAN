using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.Utilities;
using EnterpriseGatewayPortal.Web.ViewModel.ESealDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EnterpriseGatewayPortal.Web.Controllers
{
    [Authorize]
    public class ESealDetailController : BaseController
    {
        private readonly IESealDetailService _eSeal;
        private readonly IOrganizationCertificateService _organizationCertificateService;
        private readonly IOrganizationDetailService _organizationDetailService;
        private readonly IConfiguration _configuration;

        public ESealDetailController(IAdminLogService adminLogService,
            IESealDetailService eSealDetail,
            IOrganizationCertificateService organizationCertificateService,
            IOrganizationDetailService organizationDetailService,
            IConfiguration configuration) : base(adminLogService)
        {
            _eSeal = eSealDetail;
            _organizationCertificateService = organizationCertificateService;
            _organizationDetailService = organizationDetailService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            string logMessage;
            try
            {
                var OrganizationDetailsindb = await _organizationDetailService.GetAllOrganizationDetailListAsync();
                var OrganizationDetailsList = (IEnumerable<OrganizationDetail>)OrganizationDetailsindb.Resource;
                var OrganizationDetails = OrganizationDetailsList.First();

                if (OrganizationDetails == null)
                {
                    logMessage = $"Failed to get the organization details List From Local DB";
                    SendAdminLog(ModuleNameConstants.ESeal, ServiceNameConstants.ESeal,
                        "Get Organization details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                    return NotFound();
                }
                var EsealCertificateDetailsinDb = await _organizationCertificateService.GetOrganizationActiveCertificateByAsync();
                var EsealCertificate = (OrganizationCertificate)EsealCertificateDetailsinDb.Resource;
                if (EsealCertificate == null)
                {
                    logMessage = $"Failed to get the Eseal Certificate details From Local DB";
                    SendAdminLog(ModuleNameConstants.ESeal, ServiceNameConstants.ESeal,
                        "Get Eseal Certificate details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                    return NotFound();
                }
                ESealDetailsViewModel viewModel = new()
                {
                    certificateEndDate = EsealCertificate.CerificateExpiryDate.ToShortDateString(),
                    certificateStartDate = EsealCertificate.CertificateIssueDate.ToShortDateString(),
                    certificateStatus = EsealCertificate.CertificateStatus,
                    ResizedEsealImage = OrganizationDetails.ESealImage
                };
                // Return the view with the populated view model

                logMessage = $"Successfully received Eseal details From Local DB";
                SendAdminLog(ModuleNameConstants.ESeal, ServiceNameConstants.ESeal,
                    "Get Organization details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                return View("Index", viewModel);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ModelState + "\n" + ex.Message);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeTransparentImage(EsealImageDTO esealImageDTO)
        {
            APIResponse response = new();
            try
            {
                var base64 = ImageProcesser.MakeImageTransparent(esealImageDTO.base64);
                if (base64 == null)
                {
                    response.Success = false;
                    return Ok(response);
                }
                else
                {
                    response.Success = true;
                    response.Result = base64;
                    return Ok(response);
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(string eSealImage)
        {
            string logMessage;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            if (string.IsNullOrEmpty(eSealImage))
            {
                return Json(new { Status = "Failed", Title = "ESeal Logo Update", Message = "Image Not Found" });
            }

            var response = await _eSeal.UpdateEsealLogo(eSealImage, organizationUid);

            if (!response.Success)
            {
                logMessage = $"Failed to update the Eseal logo in server DB";
                SendAdminLog(ModuleNameConstants.ESeal, ServiceNameConstants.ESeal,
                    "Update Eseal logo ", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                return Json(new { Status = "Failed", Title = "ESeal Logo Update", response.Message });
            }

            ESealImageUpdateDTO eSealImageUpdateDTO = new ESealImageUpdateDTO();

            eSealImageUpdateDTO.eSealImage = eSealImage;
            eSealImageUpdateDTO.orgUid = organizationUid;
            var response1 = await _organizationDetailService.UpdateESealImageAsync(eSealImageUpdateDTO);
            if (!response.Success)
            {
                logMessage = $"Failed to update the Eseal logo in local DB";
                SendAdminLog(ModuleNameConstants.ESeal, ServiceNameConstants.ESeal,
                    "Update Eseal logo ", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                return Json(new { Status = "Failed", Title = "ESeal Logo Update", response.Message });
            }
            else
            {
                logMessage = $"Successfully Updated Eseal logo in Local DB";
                SendAdminLog(ModuleNameConstants.ESeal, ServiceNameConstants.ESeal,
                    "Update Eseal logo", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                return Json(new { Status = "Success", Title = "ESeal Logo Update", response.Message });
            }

        }
    }
}