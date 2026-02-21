using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IOrganizationCertificateRepository : IGenericRepository<OrganizationCertificate>
    {
        OrganizationCertificate AddOrganizationCertificate(OrganizationCertificate model);
        Task<OrganizationCertificate?> AddOrganizationCertificateAsync(OrganizationCertificate model);
        Task<IEnumerable<OrganizationCertificate>> GetAllOrganizationCertificateAsync();
        Task<OrganizationCertificate> GetOrganizationCertificateByIdAsync(int id);
        Task<OrganizationCertificate> GetOrganizationCertificateByUIDAsync(string uid);
        Task<bool> IsOrganizationCertificateExistsWithUIDAsync(string uid);
        bool RemoveOrganizationCertificate(OrganizationCertificate model);
        Task<bool> RemoveOrganizationCertificateById(int id);
        Task<OrganizationCertificate> UpdateOrganizationCertificate(OrganizationCertificate model);
        Task<OrganizationCertificate> GetOrganizationActiveCertificateByUIDAsync();
    }
}
