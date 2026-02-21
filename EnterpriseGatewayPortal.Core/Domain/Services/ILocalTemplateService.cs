using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface ILocalTemplateService
    {
        Task<ServiceResult> AddLocalTemplateAsync(Template model);
        Task<ServiceResult> DeleteLocalTemplateAsync(string tempID);
        Task<ServiceResult> GetLocalTemplateByIdAsync(string tempId);
        Task<ServiceResult> GetLocalTemplateEmailByIdAsync(string tempId);
        Task<ServiceResult> UpdateLocalTemplateAsync(Template model);
        Task<ServiceResult> GetAllLocalTemplatesByIdAsync(string name);
    }
}
