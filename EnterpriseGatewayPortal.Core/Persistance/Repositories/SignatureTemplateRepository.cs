using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class SignatureTemplateRepository : GenericRepository<SignatureTemplate, EnterprisegatewayportalDbContext>, ISignatureTemplateRepository
    {
        private readonly ILogger _logger;
        public SignatureTemplateRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<bool> IsSignatureTemplateExistsWithIDAsync(int id)
        {
            try
            {
                return await Context.SignatureTemplates.AsNoTracking().AnyAsync(u => u.Id == id);
            }
            catch (Exception error)
            {
                Logger.LogError("IsSignatureTemplateExistsWithNameAsync::Database exception: {0}", error);
                return false;
            }
        }

        public async Task<SignatureTemplate> GetSignatureTemplateByIdAsync(int id)
        {
            try
            {
                return await Context.Set<SignatureTemplate>().FindAsync(id);
            }
            catch (Exception error)
            {
                Logger.LogError("GetSignatureTemplateByIdAsync:: Database Exception: {0}", error);
                return null;
            }
        }        

        public async Task<IEnumerable<SignatureTemplate>> GetAllSignatureTemplateAsync()
        {
            try
            {
                return await Context.Set<SignatureTemplate>().AsNoTracking().ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllSignatureTemplateAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<SignatureTemplate?> AddSignatureTemplateAsync(SignatureTemplate model)
        {
            try
            {
                var voter = await Context.Set<SignatureTemplate>().AddAsync(model);
                var addedSignatureTemplate = voter.Entity;
                Context.SaveChanges();
                return addedSignatureTemplate;
            }
            catch (Exception error)
            {
                Logger.LogError("AddSignatureTemplateAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<SignatureTemplate> UpdateSignatureTemplate(SignatureTemplate model)
        {
            try
            {
                var existingEntity = await Context.Set<SignatureTemplate>().FindAsync(model.Id);

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
                Logger.LogError("UpdateSignatureTemplate:: Database Exception: {0}", error);
                return null;
            }
        }

        public bool RemoveSignatureTemplate(SignatureTemplate model)
        {
            try
            {
                Context.Set<SignatureTemplate>().Remove(model);
                Context.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveSignatureTemplate:: Database Exception: {0}", error);
                return false;
            }
        }

        public async Task<bool> RemoveSignatureTemplateById(int id)
        {
            try
            {
                var entity = await Context.Set<SignatureTemplate>().FindAsync(id);

                if (entity == null)
                {
                    return false; // The entity with the given ID was not found.
                }

                Context.Set<SignatureTemplate>().Remove(entity);
                Context.SaveChanges();

                return true; // Entity successfully removed.
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveSignatureTemplateById:: Database Exception: {0}", error);
                return false; // An error occurred while trying to remove the entity.
            }
        }

    }
}
