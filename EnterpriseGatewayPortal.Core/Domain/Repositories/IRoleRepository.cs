using EnterpriseGatewayPortal.Core.Domain.Lookups;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<IEnumerable<RoleLookupItem>> GetRoleLookupItemsAsync();

        Task<Role> GetRoleByRoleIdWithActivities(int id);
        Task<bool> IsRoleExistsByName(Role role);
    }
}
