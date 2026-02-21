using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IOrgSignatureTemplateService
    {
        Task<ServiceResult> AddOrgSignatureTemplateAsync(OrganizationTemplatesDTO model);
        Task<ServiceResult> DeleteOrgSignatureTemplateAsync(OrgSignatureTemplate model);
        Task<ServiceResult> DeleteOrgSignatureTemplateByIdAsync(int id);
        Task<ServiceResult> GetAllOrgSignatureTemplateListAsync();
        Task<ServiceResult> GetOrganizationTemplatesDTOByUIdAsync(string uid);
        Task<ServiceResult> GetOrgSignatureTemplateByIdAsync(int id);
        Task<ServiceResult> UpdateOrgSignatureTemplateAsync(OrganizationTemplatesDTO model);
    }
}
