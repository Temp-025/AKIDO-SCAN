using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class OrganizationDetailRepository : GenericRepository<OrganizationDetail, EnterprisegatewayportalDbContext>, IOrganizationDetailRepository
    {
        private readonly ILogger _logger;
        public OrganizationDetailRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<bool> IsOrganizationDetailExistsWithUIDAsync(string uid)
        {
            try
            {
                return await Context.OrganizationDetails.AsNoTracking().AnyAsync(u => u.OrganizationUid == uid);
            }
            catch (Exception error)
            {
                Logger.LogError("IsOrganizationDetailExistsWithNameAsync::Database exception: {0}", error);
                return false;
            }
        }

        public async Task<OrganizationDetail> GetOrganizationDetailByIdAsync(int id)
        {
            try
            {
                return await Context.Set<OrganizationDetail>().FindAsync(id);
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrganizationDetailByIdAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrganizationDetail> GetOrganizationDetailByUIDAsync(string uid)
        {
            try
            {
                return await Context.Set<OrganizationDetail>()
                    .Include(o => o.OrgEmailDomains)
                    .SingleOrDefaultAsync(u => u.OrganizationUid == uid);
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrganizationDetailByNameAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<OrganizationDetail>> GetAllOrganizationDetailAsync()
        {
            try
            {
                return await Context.Set<OrganizationDetail>().AsNoTracking().ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllOrganizationDetailAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrganizationDetail?> AddOrganizationDetailAsync(OrganizationDetail model)
        {
            try
            {
                var voter = await Context.Set<OrganizationDetail>().AddAsync(model);
                var addedOrganizationDetail = voter.Entity;
                Context.SaveChanges();
                return addedOrganizationDetail;
            }
            catch (Exception error)
            {
                Logger.LogError("AddOrganizationDetailAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public OrganizationDetail AddOrganizationDetail(OrganizationDetail model)
        {
            try
            {
                Context.Set<OrganizationDetail>().Add(model);
                Context.SaveChanges();
                return model;
            }
            catch (Exception error)
            {
                Logger.LogError("AddOrganizationDetail:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrganizationDetail> UpdateOrganizationDetail(OrganizationDetail model)
        {
            try
            {
                var existingEntity = await Context.Set<OrganizationDetail>().FindAsync(model.OrganizationDetailsId);

                if (existingEntity == null)
                {
                    // The entity doesn't exist in the database; handle this case as needed
                    Logger.LogError("UpdateCommittee:: Entity not found in the database.");
                    return null; // Or you can throw an exception or take other appropriate action.
                }

                // The entity exists; update it
                Context.Entry(existingEntity).CurrentValues.SetValues(model);
                Context.SaveChanges();
                return model;
            }
            catch (Exception error)
            {
                Logger.LogError("UpdateOrganizationDetail:: Database Exception: {0}", error);
                return null;
            }
        }

        public bool RemoveOrganizationDetail(OrganizationDetail model)
        {
            try
            {
                Context.Set<OrganizationDetail>().Remove(model);
                Context.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveOrganizationDetail:: Database Exception: {0}", error);
                return false;
            }
        }

        public async Task<bool> RemoveOrganizationDetailById(int id)
        {
            try
            {
                var entity = await Context.Set<OrganizationDetail>().FindAsync(id);

                if (entity == null)
                {
                    return false; // The entity with the given ID was not found.
                }

                Context.Set<OrganizationDetail>().Remove(entity);
                Context.SaveChanges();

                return true; // Entity successfully removed.
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveOrganizationDetailById:: Database Exception: {0}", error);
                return false; // An error occurred while trying to remove the entity.
            }
        }

    }
}
