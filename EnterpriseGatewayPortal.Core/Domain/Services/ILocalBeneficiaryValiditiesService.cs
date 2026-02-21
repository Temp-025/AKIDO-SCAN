using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface ILocalBeneficiaryValiditiesService
    {
        Task<ServiceResult> GetAllBeneficiaryValiditiesByBeneficiaryIdListAsync(int beneficiaryId);
        Task<ServiceResult> RemoveAllBeneficiaryValiditiesByBeneficiaryIdAsync(int beneficiaryId);
        Task<ServiceResult> AddBeneficiaryValiditiesListAsync(List<BeneficiaryValidity> models);
    }
}
