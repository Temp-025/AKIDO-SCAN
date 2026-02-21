using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
//using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class BeneficiariesRepository : GenericRepository<Beneficiary, EnterprisegatewayportalDbContext>, IBeneficiariesRepository
    {
        private readonly ILogger _logger;
        public BeneficiariesRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Beneficiary>> GetAllBeneficiariesAsync()
        {
            try
            {
                return await Context.Set<Beneficiary>().AsNoTracking().ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllBeneficiariesAsync:: Database Exception: {0}", error);
                return null;
            }
        }
        public async Task<IEnumerable<Beneficiary>> GetAllBeneficiariesBySponsorDigitalIdAsync(string org_id)
        {
            try
            {
                return await Context.Beneficiaries
                    .Where(u => u.SponsorDigitalId == org_id && u.Status == "ACTIVE")
                    .OrderByDescending(u => u.CreatedOn)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                _logger.LogError($"Error in GetAllBeneficiariesBySponsorDigitalIdAsync: {ex}");
                _logger.LogError($"Error in GetAllBeneficiariesBySponsorDigitalIdAsync: {ex.Message}");
                return null; // Re-throw the exception to propagate it up the call stack
            }
        }

        public async Task<bool> IsBeneficiaryEmailExistsAsync(string email)
        {
            try
            {
                return await Context.Beneficiaries.AsNoTracking().AnyAsync(u => u.BeneficiaryUgpassEmail == email);
            }
            catch (Exception error)
            {
                Logger.LogError("IsBeneficiaryEmailExistsAsync::Database exception: {0}", error);
                return false;
            }
        }
        public async Task<Beneficiary?> AddBeneficiariaryUserAsync(Beneficiary model)
        {
            try
            {
                var voter = await Context.Set<Beneficiary>().AddAsync(model);
                var addedBeneficiaryUser = voter.Entity;
                Context.SaveChanges();
                return addedBeneficiaryUser;
            }
            catch (Exception error)
            {
                Logger.LogError("AddBeneficiariaryUserAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<List<Beneficiary>> AddMultipleBeneficiaryUsersListAsync(List<Beneficiary> models)
        {
            try
            {
                await Context.Set<Beneficiary>().AddRangeAsync(models);
                await Context.SaveChangesAsync();
                return models;
            }
            catch (Exception error)
            {
                Logger.LogError("AddMultipleBeneficiaryUsersListAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<Beneficiary> GetBeneficiariesByIdAsync(int id)
        {
            try
            {
                //return await Context.Set<Beneficiary>().FindAsync(id);
                return await Context.Set<Beneficiary>()
                            .Include(b => b.BeneficiaryValidities).AsNoTracking()
                            .FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception error)
            {
                Logger.LogError("GetBeneficiariesByIdAsync:: Database Exception: {0}", error);
                return null;
            }
        }
        public async Task<Beneficiary> UpdateBeneficiaries(Beneficiary model) //on-going code
        {
            try
            {
                var existingEntity = await Context.Beneficiaries
                    .Include(b => b.BeneficiaryValidities)
                    .FirstOrDefaultAsync(b => b.Id == model.Id);

                //var validities = await Context.BeneficiaryValidities
                //.Where(v => v.BeneficiaryId == model.Id)
                //.AsNoTracking()
                //.ToListAsync();

                if (existingEntity != null)
                {
                    // Update the Beneficiary's main properties
                    //Context.Entry(existingEntity).CurrentValues.SetValues(model);
                    Context.Entry(existingEntity).Property(e => e.SponsorDigitalId).CurrentValue = model.SponsorDigitalId;
                    Context.Entry(existingEntity).Property(e => e.SponsorName).CurrentValue = model.SponsorName;
                    Context.Entry(existingEntity).Property(e => e.SponsorType).CurrentValue = model.SponsorType;
                    Context.Entry(existingEntity).Property(e => e.SponsorExternalId).CurrentValue = model.SponsorExternalId;
                    Context.Entry(existingEntity).Property(e => e.BeneficiaryDigitalId).CurrentValue = model.BeneficiaryDigitalId;
                    Context.Entry(existingEntity).Property(e => e.BeneficiaryName).CurrentValue = model.BeneficiaryName;
                    Context.Entry(existingEntity).Property(e => e.BeneficiaryType).CurrentValue = model.BeneficiaryType;
                    Context.Entry(existingEntity).Property(e => e.SponsorPaymentPriorityLevel).CurrentValue = model.SponsorPaymentPriorityLevel;
                    Context.Entry(existingEntity).Property(e => e.BeneficiaryNin).CurrentValue = model.BeneficiaryNin;
                    Context.Entry(existingEntity).Property(e => e.BeneficiaryPassport).CurrentValue = model.BeneficiaryPassport;
                    Context.Entry(existingEntity).Property(e => e.BeneficiaryMobileNumber).CurrentValue = model.BeneficiaryMobileNumber;
                    Context.Entry(existingEntity).Property(e => e.BeneficiaryOfficeEmail).CurrentValue = model.BeneficiaryOfficeEmail;
                    Context.Entry(existingEntity).Property(e => e.BeneficiaryUgpassEmail).CurrentValue = model.BeneficiaryUgpassEmail;
                    Context.Entry(existingEntity).Property(e => e.BeneficiaryConsentAcquired).CurrentValue = model.BeneficiaryConsentAcquired;
                    Context.Entry(existingEntity).Property(e => e.SignaturePhoto).CurrentValue = model.SignaturePhoto;
                    Context.Entry(existingEntity).Property(e => e.Designation).CurrentValue = model.Designation;
                    Context.Entry(existingEntity).Property(e => e.Status).CurrentValue = model.Status;
                    Context.Entry(existingEntity).Property(e => e.CreatedOn).CurrentValue = model.CreatedOn;
                    Context.Entry(existingEntity).Property(e => e.UpdatedOn).CurrentValue = model.UpdatedOn;



                    // Update or add BeneficiaryValidities
                    //foreach (var validity in model.BeneficiaryValidities)
                    //{

                    //    existingEntity.BeneficiaryValidities.Add(validity);
                    //}

                    // Save changes to the context
                    await Context.SaveChangesAsync();

                    // Return the updated entity
                    return existingEntity;
                }

                return null;
            }
            catch (Exception error)
            {
                Logger.LogError("UpdateBeneficiaries:: Database Exception: {0}", error);
                return null;
            }
        }

    }
}
