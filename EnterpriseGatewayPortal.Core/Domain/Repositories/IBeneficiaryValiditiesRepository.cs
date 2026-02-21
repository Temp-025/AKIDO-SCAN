using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IBeneficiaryValiditiesRepository
    {
        Task<List<BeneficiaryValidity>> GetAllBeneficiaryValiditiesByBeneficiaryIdAsync(int beneficiaryId);
        Task<bool> RemoveBeneficiaryValiditiesByBeneficiaryId(int beneficiaryId);
        Task<List<BeneficiaryValidity>> AddBeneficiaryValiditiesOfUsersListAsync(List<BeneficiaryValidity> models);
    }
}
