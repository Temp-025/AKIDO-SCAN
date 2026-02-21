using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IBeneficiariesRepository
    {
        Task<Beneficiary?> AddBeneficiariaryUserAsync(Beneficiary model);
        Task<bool> IsBeneficiaryEmailExistsAsync(string email);
        Task<Beneficiary> GetBeneficiariesByIdAsync(int id);
        Task<Beneficiary> UpdateBeneficiaries(Beneficiary model);
        Task<IEnumerable<Beneficiary>> GetAllBeneficiariesAsync();
        Task<IEnumerable<Beneficiary>> GetAllBeneficiariesBySponsorDigitalIdAsync(string org_id);
        Task<List<Beneficiary>> AddMultipleBeneficiaryUsersListAsync(List<Beneficiary> models);
    }
}
