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
    public class OrganizationCertificateRepository : GenericRepository<OrganizationCertificate, EnterprisegatewayportalDbContext>, IOrganizationCertificateRepository
    {
        private readonly ILogger _logger;
        public OrganizationCertificateRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<bool> IsOrganizationCertificateExistsWithUIDAsync(string uid)
        {
            try
            {
                return await Context.OrganizationCertificates.AsNoTracking().AnyAsync(u => u.OrganizationUid == uid);
            }
            catch (Exception error)
            {
                Logger.LogError("IsOrganizationCertificateExistsWithNameAsync::Database exception: {0}", error);
                return false;
            }
        }

        public async Task<OrganizationCertificate> GetOrganizationCertificateByIdAsync(int id)
        {
            try
            {
                return await Context.Set<OrganizationCertificate>().FindAsync(id);
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrganizationCertificateByIdAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrganizationCertificate> GetOrganizationCertificateByUIDAsync(string uid)
        {
            try
            {
                return await Context.Set<OrganizationCertificate>().SingleOrDefaultAsync(u => u.OrganizationUid == uid);
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrganizationCertificateByNameAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<OrganizationCertificate>> GetAllOrganizationCertificateAsync()
        {
            try
            {
                return await Context.Set<OrganizationCertificate>().AsNoTracking().ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllOrganizationCertificateAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrganizationCertificate?> AddOrganizationCertificateAsync(OrganizationCertificate model)
        {
            try
            {
                var voter = await Context.Set<OrganizationCertificate>().AddAsync(model);
                var addedOrganizationCertificate = voter.Entity;
                Context.SaveChanges();
                return addedOrganizationCertificate;
            }
            catch (Exception error)
            {
                Logger.LogError("AddOrganizationCertificateAsync:: Database Exception: {0}", error);
                return null;
            }
        }
        public async Task<OrganizationCertificate> GetOrganizationActiveCertificateByUIDAsync()
        {
            try
            {
                return await Context.Set<OrganizationCertificate>().SingleOrDefaultAsync(u => u.CertificateStatus == "ACTIVE");
            }
            catch (Exception error)
            {
                Logger.LogError("GetOrganizationCertificateByNameAsync:: Database Exception: {0}", error);
                return null;
            }
        }
        public OrganizationCertificate AddOrganizationCertificate(OrganizationCertificate model)
        {
            try
            {
                Context.Set<OrganizationCertificate>().Add(model);
                Context.SaveChanges();
                return model;
            }
            catch (Exception error)
            {
                Logger.LogError("AddOrganizationCertificate:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<OrganizationCertificate> UpdateOrganizationCertificate(OrganizationCertificate model)
        {
            try
            {
                var existingEntity = await Context.Set<OrganizationCertificate>().FindAsync(model.CertificateSerialNumber);

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
                Logger.LogError("UpdateOrganizationCertificate:: Database Exception: {0}", error);
                return null;
            }
        }

        public bool RemoveOrganizationCertificate(OrganizationCertificate model)
        {
            try
            {
                Context.Set<OrganizationCertificate>().Remove(model);
                Context.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveOrganizationCertificate:: Database Exception: {0}", error);
                return false;
            }
        }

        public async Task<bool> RemoveOrganizationCertificateById(int id)
        {
            try
            {
                var entity = await Context.Set<OrganizationCertificate>().FindAsync(id);

                if (entity == null)
                {
                    return false; // The entity with the given ID was not found.
                }

                Context.Set<OrganizationCertificate>().Remove(entity);
                Context.SaveChanges();

                return true; // Entity successfully removed.
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveOrganizationCertificateById:: Database Exception: {0}", error);
                return false; // An error occurred while trying to remove the entity.
            }
        }

    }
}
