using EnterpriseGatewayPortal.Core.Domain.Lookups;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IRoleManagementService
    {
        Task<IEnumerable<RoleLookupItem>> GetRoleLookupItemsAsync();

        Task<IEnumerable<ActivityLookupItem>> GetActivityLookupItemsAsync();

        Task<Role> GetRoleAsync(int id);

        Task<RoleResponse> AddRoleAsync(Role role, IDictionary<int, bool> selectedActivityIds,
            bool makerCheckerFlag = false);

        Task<RoleResponse> UpdateRoleAsync(Role role, IDictionary<int, bool> selectedActivityIds,
            bool makerCheckerFlag = false);

        Task<RoleResponse> DeleteRoleAsync(int id, string updatedBy,
            bool makerCheckerFlag = false);
        Task<RoleResponse> UpdateRoleState(int id, bool isApproved, string reason = null);
        Task<RoleResponse> ActivateRoleAsync(int id);
        Task<RoleResponse> DeActivateRoleAsync(int id);
    }
}
