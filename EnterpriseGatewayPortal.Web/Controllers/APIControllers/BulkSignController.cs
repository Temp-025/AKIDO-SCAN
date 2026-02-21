using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseGatewayPortal.Web.Controllers.APIControllers
{
    [Route("api")]
    [ApiController]
    public class BulkSignController : ControllerBase
    {
        private readonly ILocalBulkSignService _localBulkSignService;
        public BulkSignController(ILocalBulkSignService localBulkSignService)
        {
            _localBulkSignService = localBulkSignService;
        }

        [HttpPost]
        [Route("bulksign/bulksigned-document")]
        public async Task<IActionResult> BulkSignCallBack(BulkSignCallBackDTO bulkSignCallBackDTO)
        {
            APIResponse response;

            var result = await _localBulkSignService.BulkSignCallBackAsync(bulkSignCallBackDTO);
            if (!result.Success)
            {
                response = new APIResponse
                {
                    Success = result.Success,
                    Message = result.Message,
                    Result = result.Resource
                };
            }
            else
            {
                response = new APIResponse
                {
                    Success = result.Success,
                    Message = result.Message,
                    Result = result.Resource
                };
            }

            return Ok(response);
        }
    }
}
