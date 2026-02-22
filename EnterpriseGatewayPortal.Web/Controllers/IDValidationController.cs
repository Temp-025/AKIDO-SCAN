using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [Authorize]
    public class IdValidationController : Controller
    {
        private readonly Core.Domain.Services.IClientService _clientService;
        private readonly IOrganizationService _organizationService;
        private readonly IKYCLogReportsService _kycLogReportsService;

        public IdValidationController(
            Core.Domain.Services.IClientService clientService,
            IOrganizationService organizationService,
            IKYCLogReportsService kycLogReportsService)
        {
            _clientService = clientService;
            _organizationService = organizationService;
            _kycLogReportsService = kycLogReportsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> ValidateSignedData([FromBody] ValidateSignedDataRequestDTO signedDataRequest)

        {

            if (signedDataRequest == null ||

                string.IsNullOrEmpty(signedDataRequest.SignedData)

                || string.IsNullOrWhiteSpace(signedDataRequest.SignedData))

            {

                return Ok(new APIResponse()

                {

                    Success = false,

                    Message = "Signed data is required.",

                    Result = null

                });

            }

            var response = await _kycLogReportsService.ValidateSignedDataAsync(signedDataRequest.SignedData, signedDataRequest.KycMethod);

            return Ok(new APIResponse()

            {

                Success = response.Success,

                Message = response.Message,

                Result = response.Resource

            });

        }
    }
}
