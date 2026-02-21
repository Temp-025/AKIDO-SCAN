using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class OrgEmailDomainRepository : GenericRepository<OrgEmailDomain, EnterprisegatewayportalDbContext>, IOrgEmailDomainRepository
    {
        private readonly ILogger _logger;
        public OrgEmailDomainRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<bool> IsOrgEmailDomainExistsWithUIDAsync(string uid)
        {
            try
            {
                return await Context.OrgEmailDomains.AsNoTracking().AnyAsync(u => u.OrganizationUid == uid);
            }
            catch (Exception error)
            {
                Logger.LogError("IsOrgEmailDomainExistsWithNameAsync::Database exception: {0}", error);
                return false;
            }
        }

        public async Task<OrgEmailDomain> GetOrgEmailDomainByIdAsync(int id)
        {
            try
            {
                return await Context.Set<OrgEmailDomain>().FindAsync(id);
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrgEmailDomainByIdAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrgEmailDomain> GetOrgEmailDomainByOrgUidAsync(string uid)
        {
            try
            {
                return await Context.Set<OrgEmailDomain>().SingleOrDefaultAsync(u => u.OrganizationUid == uid);
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrgEmailDomainByOrgUidAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<OrgEmailDomain>> GetAllOrgEmailDomainAsync()
        {
            try
            {
                return await Context.Set<OrgEmailDomain>().AsNoTracking().ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllOrgEmailDomainAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrgEmailDomain?> AddOrgEmailDomainAsync(OrgEmailDomain model)
        {
            try
            {
                var voter = await Context.Set<OrgEmailDomain>().AddAsync(model);
                var addedOrgEmailDomain = voter.Entity;
                Context.SaveChanges();
                return addedOrgEmailDomain;
            }
            catch (Exception error)
            {
                Logger.LogError("AddOrgEmailDomainAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public OrgEmailDomain AddOrgEmailDomain(OrgEmailDomain model)
        {
            try
            {
                Context.Set<OrgEmailDomain>().Add(model);
                Context.SaveChanges();
                return model;
            }
            catch (Exception error)
            {
                Logger.LogError("AddOrgEmailDomain:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrgEmailDomain> UpdateOrgEmailDomain(OrgEmailDomain model)
        {
            try
            {
                var existingEntity = await Context.Set<OrgEmailDomain>().FindAsync(model.OrgDomainId);

                if (existingEntity == null)
                {
                    // The entity doesn't exist in the database; handle this case as needed
                    Logger.LogError("UpdateOrgEmailDomain:: Entity not found in the database.");
                    return null; // Or you can throw an exception or take other appropriate action.
                }

                // The entity exists; update it
                Context.Entry(existingEntity).CurrentValues.SetValues(model);
                Context.SaveChanges();
                return model;
            }
            catch (Exception error)
            {
                Logger.LogError("UpdateOrgEmailDomain:: Database Exception: {0}", error);
                return null;
            }
        }

        public bool RemoveOrgEmailDomain(OrgEmailDomain model)
        {
            try
            {
                Context.Set<OrgEmailDomain>().Remove(model);
                Context.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveOrgEmailDomain:: Database Exception: {0}", error);
                return false;
            }
        }

        public async Task<bool> RemoveOrgEmailDomainById(int id)
        {
            try
            {
                var entity = await Context.Set<OrgEmailDomain>().FindAsync(id);

                if (entity == null)
                {
                    return false; // The entity with the given ID was not found.
                }

                Context.Set<OrgEmailDomain>().Remove(entity);
                Context.SaveChanges();

                return true; // Entity successfully removed.
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveOrgEmailDomainById:: Database Exception: {0}", error);
                return false; // An error occurred while trying to remove the entity.
            }
        }

    }
}
