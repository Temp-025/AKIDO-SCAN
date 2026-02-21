
using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IOrgEmailDomainRepository
    {
        OrgEmailDomain AddOrgEmailDomain(OrgEmailDomain model);
        Task<OrgEmailDomain?> AddOrgEmailDomainAsync(OrgEmailDomain model);
        Task<IEnumerable<OrgEmailDomain>> GetAllOrgEmailDomainAsync();
        Task<OrgEmailDomain> GetOrgEmailDomainByIdAsync(int id);
        Task<OrgEmailDomain> GetOrgEmailDomainByOrgUidAsync(string uid);
        Task<bool> IsOrgEmailDomainExistsWithUIDAsync(string uid);
        bool RemoveOrgEmailDomain(OrgEmailDomain model);
        Task<bool> RemoveOrgEmailDomainById(int id);
        Task<OrgEmailDomain> UpdateOrgEmailDomain(OrgEmailDomain model);
    }
}
