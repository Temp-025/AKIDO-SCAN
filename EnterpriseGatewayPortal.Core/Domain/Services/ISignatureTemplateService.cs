using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface ISignatureTemplateService
    {
        Task<ServiceResult> AddSignatureTemplateAsync(SignatureTemplate model);
        Task<ServiceResult> DeleteSignatureTemplateAsync(SignatureTemplate model);
        Task<ServiceResult> DeleteSignatureTemplateByIdAsync(int id);
        Task<ServiceResult> GetAllSignatureTemplateListAsync();
        Task<ServiceResult> GetSignatureTemplateByIdAsync(int id);
        Task<ServiceResult> UpdateSignatureTemplateAsync(SignatureTemplate model);
    }
}
