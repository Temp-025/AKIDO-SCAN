using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IOrganizationDetailRepository : IGenericRepository<OrganizationDetail>
    {
        OrganizationDetail AddOrganizationDetail(OrganizationDetail model);
        Task<OrganizationDetail?> AddOrganizationDetailAsync(OrganizationDetail model);
        Task<IEnumerable<OrganizationDetail>> GetAllOrganizationDetailAsync();
        Task<OrganizationDetail> GetOrganizationDetailByIdAsync(int id);
        Task<OrganizationDetail> GetOrganizationDetailByUIDAsync(string uid);
        Task<bool> IsOrganizationDetailExistsWithUIDAsync(string uid);
        bool RemoveOrganizationDetail(OrganizationDetail model);
        Task<bool> RemoveOrganizationDetailById(int id);
        Task<OrganizationDetail> UpdateOrganizationDetail(OrganizationDetail model);
    }
}
