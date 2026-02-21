using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class BusinessUsersRepository : GenericRepository<OrgSubscriberEmail, EnterprisegatewayportalDbContext>, IBusinessUsersRepository
    {
        private readonly ILogger _logger;
        public BusinessUsersRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<bool> IsOrgSubscriberEmailExistsWithUIDAsync(string uid)
        {
            try
            {
                return await Context.OrgSubscriberEmails.AsNoTracking().AnyAsync(u => u.OrganizationUid == uid);
            }
            catch (Exception error)
            {
                Logger.LogError("IsOrgSubscriberEmailExistsWithNameAsync::Database exception: {0}", error);
                return false;
            }
        }

        public async Task<OrgSubscriberEmail> GetOrgSubscriberEmailByIdAsync(int id)
        {
            try
            {
                return await Context.Set<OrgSubscriberEmail>().FindAsync(id);
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrgSubscriberEmailByIdAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrgSubscriberEmail> GetOrgSubscriberEmailByUIDAsync(string uid)
        {
            try
            {
                return await Context.Set<OrgSubscriberEmail>().SingleOrDefaultAsync(u => u.OrganizationUid == uid);
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrgSubscriberEmailByNameAsync:: Database Exception: {0}", error);
                return null;
            }
        }
        public async Task<OrgSubscriberEmail> GetOrgSubscriberEmailByEmailAsync(string Email)
        {
            try
            {
                return await Context.Set<OrgSubscriberEmail>().SingleOrDefaultAsync(u => u.UgpassEmail == Email && u.Status== "ACTIVE");
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrgSubscriberEmailByEmailAsync:: Database Exception: {0}", error);
                return null;
            }
        }
        

        public async Task<OrgSubscriberEmail> GetOrgSubscriberEmailByEmployeeEmailAsync(string Email)
        {
            try
            {
                return await Context.Set<OrgSubscriberEmail>().SingleOrDefaultAsync(u => u.EmployeeEmail == Email && u.Status == "ACTIVE");
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrgSubscriberEmailByEmailAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<OrgSubscriberEmail>> GetAllOrgSubscriberEmailAsync()
        {
            try
            {
                return await Context.Set<OrgSubscriberEmail>().AsNoTracking().ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllOrgSubscriberEmailAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<OrgSubscriberEmail>> GetAllOrgSubscriberEmailwithBulkSignAsync()
        {
            try
            {
                return await Context.Set<OrgSubscriberEmail>()
                    .AsNoTracking()
                    .Where(x => x.IsBulkSign == true)
                    .ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllOrgSubscriberEmailAsync:: Database Exception: {0}", error);
                return Enumerable.Empty<OrgSubscriberEmail>();
            }
        }

        public async Task<IEnumerable<OrgSubscriberEmail>> GetAllOrgSubscriberEmailsByOrgUIDAsync(string uid)
        {
            try
            {
                return await Context.OrgSubscriberEmails.Where(u => u.OrganizationUid == uid && u.Status!="DELETED").ToListAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                _logger.LogError($"Error in GetAllOrgSubscriberEmailsByOrgUIDAsync: {ex}");
                _logger.LogError($"Error in GetAllOrgSubscriberEmailsByOrgUIDAsync: {ex.Message}");
                return null; // Re-throw the exception to propagate it up the call stack
            }
        }

        public async Task<OrgSubscriberEmail?> AddOrgSubscriberEmailAsync(OrgSubscriberEmail model)
        {
            try
            {
                var voter = await Context.Set<OrgSubscriberEmail>().AddAsync(model);
                var addedOrgSubscriberEmail = voter.Entity;
                Context.SaveChanges();
                return addedOrgSubscriberEmail;
            }
            catch (Exception error)
            {
                Logger.LogError("AddOrgSubscriberEmailAsync:: Database Exception: {0}", error);
                return null;
            }
        }
        public async Task<List<OrgSubscriberEmail>> AddOrgSubscriberEmailsListAsync(List<OrgSubscriberEmail> models)
        {
            try
            {
                await Context.Set<OrgSubscriberEmail>().AddRangeAsync(models);
                await Context.SaveChangesAsync();
                return models;
            }
            catch (Exception error)
            {
                Logger.LogError("AddOrgSubscriberEmailsAsync:: Database Exception: {0}", error);
                return null;
            }
        }


        public OrgSubscriberEmail AddOrgSubscriberEmail(OrgSubscriberEmail model)
        {
            try
            {
                Context.Set<OrgSubscriberEmail>().Add(model);
                Context.SaveChanges();
                return model;
            }
            catch (Exception error)
            {
                Logger.LogError("AddOrgSubscriberEmail:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrgSubscriberEmail> UpdateOrgSubscriberEmail(OrgSubscriberEmail model)
        {
            try
            {
                var existingEntity = await Context.Set<OrgSubscriberEmail>().FindAsync(model.OrgContactsId);

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
                Logger.LogError("UpdateOrgSubscriberEmail:: Database Exception: {0}", error);
                return null;
            }
        }

        public bool RemoveOrgSubscriberEmail(OrgSubscriberEmail model)
        {
            try
            {
                Context.Set<OrgSubscriberEmail>().Remove(model);
                Context.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveOrgSubscriberEmail:: Database Exception: {0}", error);
                return false;
            }
        }

        public async Task<bool> RemoveOrgSubscriberEmailById(int id)
        {
            try
            {
                var entity = await Context.Set<OrgSubscriberEmail>().FindAsync(id);

                if (entity == null)
                {
                    return false; // The entity with the given ID was not found.
                }

                Context.Set<OrgSubscriberEmail>().Remove(entity);
                Context.SaveChanges();

                return true; // Entity successfully removed.
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveOrgSubscriberEmailById:: Database Exception: {0}", error);
                return false; // An error occurred while trying to remove the entity.
            }
        }

    }
}
