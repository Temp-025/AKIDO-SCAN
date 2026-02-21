using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class BeneficiaryValiditiesRepository : GenericRepository<BeneficiaryValidity, EnterprisegatewayportalDbContext>, IBeneficiaryValiditiesRepository
    {
        private readonly ILogger _logger;
        public BeneficiaryValiditiesRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }


        public async Task<List<BeneficiaryValidity>> GetAllBeneficiaryValiditiesByBeneficiaryIdAsync(int beneficiaryId)
        {
            try
            {
                return await Context.Set<BeneficiaryValidity>()
                            .Where(bv => bv.BeneficiaryId == beneficiaryId)
                            .ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllBeneficiaryValiditiesByBeneficiaryIdAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<bool> RemoveBeneficiaryValiditiesByBeneficiaryId(int beneficiaryId)
        {
            try
            {
                // Retrieve all records with the specified BeneficiaryId
                var entities = await Context.Set<BeneficiaryValidity>()
                    .Where(bv => bv.BeneficiaryId == beneficiaryId)
                    .ToListAsync();

                if (!entities.Any())
                {
                    return false; // No entities found with the given BeneficiaryId.
                }

                // Remove all the retrieved entities
                Context.Set<BeneficiaryValidity>().RemoveRange(entities);
                await Context.SaveChangesAsync();

                return true; // All entities successfully removed.
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveBeneficiaryValiditiesByBeneficiaryId:: Database Exception: {0}", error);
                return false; // An error occurred while trying to remove the entities.
            }
        }

        public async Task<List<BeneficiaryValidity>> AddBeneficiaryValiditiesOfUsersListAsync(List<BeneficiaryValidity> models)
        {
            try
            {
                await Context.Set<BeneficiaryValidity>().AddRangeAsync(models);
                await Context.SaveChangesAsync();
                return models;
            }
            catch (Exception error)
            {
                Logger.LogError("AddBeneficiaryValiditiesOfUsersListAsync:: Database Exception: {0}", error);
                return null;
            }
        }

    }
}
