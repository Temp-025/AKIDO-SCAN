using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseGatewayPortal.Web.Controllers.APIControllers
{
    [Route("api/eg")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Route("documents/getdocdisplaydetails")]
        public async Task<IActionResult> GetDocumentDisplayDetaildById(string id)
        {
            var result = await _documentService.GetDocumentDisplayDetaildByIdAsync(id);

            return Ok(new APIResponse()
            {
                Success = result.Success,
                Message = result.Message,
                Result = result.Resource
            });
        }
    }
}
