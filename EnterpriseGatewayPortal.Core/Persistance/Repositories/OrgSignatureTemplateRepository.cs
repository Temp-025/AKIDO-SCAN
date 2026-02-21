using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class OrgSignatureTemplateRepository : GenericRepository<OrgSignatureTemplate, EnterprisegatewayportalDbContext>, IOrgSignatureTemplateRepository
    {
        private readonly ILogger _logger;
        public OrgSignatureTemplateRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<bool> IsOrgSignatureTemplateExistsWithUIDAsync(string uid)
        {
            try
            {
                return await Context.OrgSignatureTemplates.AsNoTracking().AnyAsync(u => u.OrganizationUid == uid);
            }
            catch (Exception error)
            {
                Logger.LogError("IsOrgSignatureTemplateExistsWithNameAsync::Database exception: {0}", error);
                return false;
            }
        }

        public async Task<OrgSignatureTemplate> GetOrgSignatureTemplateByIdAsync(int id)
        {
            try
            {
                return await Context.Set<OrgSignatureTemplate>().FindAsync(id);
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrgSignatureTemplateByIdAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrgSignatureTemplate> GetOrgSignatureTemplateByUIDAsync(string uid)
        {
            try
            {
                return await Context.Set<OrgSignatureTemplate>().SingleOrDefaultAsync(u => u.OrganizationUid == uid);
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrgSignatureTemplateByNameAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<OrgSignatureTemplate>> GetAllOrgSignatureTemplateAsync()
        {
            try
            {
                return await Context.Set<OrgSignatureTemplate>().AsNoTracking().ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllOrgSignatureTemplateAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<OrgSignatureTemplate>> GetAllOrgSignatureTemplateByUIDAsync(string uid)
        {
            try
            {
                return await Context.Set<OrgSignatureTemplate>().AsNoTracking().Where(u => u.OrganizationUid == uid).ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllOrgSignatureTemplateAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrgSignatureTemplate?> AddOrgSignatureTemplateAsync(OrgSignatureTemplate model)
        {
            try
            {
                var voter = await Context.Set<OrgSignatureTemplate>().AddAsync(model);
                var addedOrgSignatureTemplate = voter.Entity;
                Context.SaveChanges();
                return addedOrgSignatureTemplate;
            }
            catch (Exception error)
            {
                Logger.LogError("AddOrgSignatureTemplateAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public OrgSignatureTemplate AddOrgSignatureTemplate(OrgSignatureTemplate model)
        {
            try
            {
                Context.Set<OrgSignatureTemplate>().Add(model);
                Context.SaveChanges();
                return model;
            }
            catch (Exception error)
            {
                Logger.LogError("AddOrgSignatureTemplate:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrgSignatureTemplate> UpdateOrgSignatureTemplate(OrgSignatureTemplate model)
        {
            try
            {
                var existingEntity = await Context.Set<OrgSignatureTemplate>().FindAsync(model.Id);

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
                Logger.LogError("UpdateOrgSignatureTemplate:: Database Exception: {0}", error);
                return null;
            }
        }

        public bool RemoveOrgSignatureTemplate(OrgSignatureTemplate model)
        {
            try
            {
                Context.Set<OrgSignatureTemplate>().Remove(model);
                Context.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveOrgSignatureTemplate:: Database Exception: {0}", error);
                return false;
            }
        }

        public async Task<bool> RemoveOrgSignatureTemplateById(int id)
        {
            try
            {
                var entity = await Context.Set<OrgSignatureTemplate>().FindAsync(id);

                if (entity == null)
                {
                    return false; // The entity with the given ID was not found.
                }

                Context.Set<OrgSignatureTemplate>().Remove(entity);
                Context.SaveChanges();

                return true; // Entity successfully removed.
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveOrgSignatureTemplateById:: Database Exception: {0}", error);
                return false; // An error occurred while trying to remove the entity.
            }
        }

    }
}
