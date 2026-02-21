using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Web.ViewModel.KYCDevices;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Core.Services;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [Authorize]
    public class KYCDeviceController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<KYCDeviceController> _logger;
        private readonly IKYCLogReportsService _kycLogReportsService;
        private readonly IDeviceService _deviceService;
        public KYCDeviceController(ILogger<KYCDeviceController> logger,
            IAdminLogService adminLogService,
            IDeviceService deviceService,
            IKYCLogReportsService kycLogReportsService,
            IConfiguration configuration) : base(adminLogService)
        {
            _logger = logger;
            _kycLogReportsService = kycLogReportsService;
            _deviceService = deviceService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var orgId = _configuration["KycOrganizationUid"];
            var result = await _kycLogReportsService.GetKycDevicesOfOrg(orgId);
            var viewModel = new KYCDevicesViewModel();

            if (result.Success && result.Resource is List<string> devices)
            {
                viewModel.Devices = devices;
            }
            else
            {
                _logger.LogError("Failed to retrieve KYC devices: " + result.Message);
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> BlockDevice(string deviceId)
        {
            var orgId = _configuration["KycOrganizationUid"];

            var licenseResult = await _deviceService.ReadLicence();

            if (licenseResult == null || !licenseResult.Success)
            {
                return null;
            }

            var licenceDetails = (LicenceDTO)licenseResult.Resource;

            KycDeviceBlockDTO dto = new KycDeviceBlockDTO()
            {
                DeviceId = deviceId,
                OrganizationId = orgId,
                ClientId = licenceDetails.clientId,
            };
            var response = await _kycLogReportsService.BlockKycDevice(dto);
            if (!response.Success)
            {
                return Json(new { success = false, message = response.Message });
            }

            return Json(new { success = true, message = response.Message, result = response.Resource });
        }
    }
}
