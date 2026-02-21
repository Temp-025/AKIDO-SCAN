using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IOrgEmailDomainService
    {
        Task<ServiceResult> AddOrgEmailDomainAsync(OrgEmailDomain model);
        Task<ServiceResult> DeleteOrgEmailDomainAsync(OrgEmailDomain model);
        Task<ServiceResult> DeleteOrgEmailDomainByIdAsync(int id);
        Task<ServiceResult> GetAllOrgEmailDomainListAsync();
        Task<ServiceResult> GetOrgEmailDomainByIdAsync(int id);
        Task<ServiceResult> GetOrgEmailDomainByOrgUidAsync(string uid);
        Task<ServiceResult> UpdateOrgEmailDomainAsync(OrgEmailDomain model);
    }
}
