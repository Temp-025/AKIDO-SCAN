using Microsoft.Extensions.Logging;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Lookups;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class RoleRepository : GenericRepository<Role, EnterprisegatewayportalDbContext>,
        IRoleRepository
    {
        private readonly ILogger _logger;
        public RoleRepository(ILogger logger,EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<RoleLookupItem>> GetRoleLookupItemsAsync()
        {
            try
            {
                return await Context.Roles
                    .Where(x => x.Status != "DELETED")
                    .Select(x =>
                   new RoleLookupItem
                   {
                       Id = x.Id,
                       Name = x.Name,
                       DisplayName = x.DisplayName,
                       Description = x.Description,
                       Status = x.Status
                   }).AsNoTracking().ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("GetRoleLookupItemsAsync::Database exception: {0}", error);
                return null;
            }
        }

        public async Task<Role> GetRoleByRoleIdWithActivities(int id)
        {
            try
            {
                return await Context.Roles
                    .Include(x => x.RoleActivities)
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception error)
            {
                _logger.LogError("GetRoleByRoleIdWithActivities::Database exception: {0}", error);
                return null;
            }
        }

        public async Task<bool> IsRoleExistsByName(Role role)
        {
            try
            {
                var roleName = role.Name.Trim();
                var displayName = role.DisplayName.Trim();

                return await Context.Roles.AsNoTracking().AnyAsync(u =>
                    u.Name == roleName && u.DisplayName == displayName);
            }
            catch (Exception error)
            {
                _logger.LogError("IsRoleExistsByName::Database exception: {0}", error);
                return false;
            }
        }

    }
}
