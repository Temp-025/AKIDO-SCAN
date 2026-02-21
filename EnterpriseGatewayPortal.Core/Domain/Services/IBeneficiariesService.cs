using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IBeneficiariesService
    {
        Task<ServiceResult> GetAllBeneficiariesServicesAsync();
        Task<ServiceResult> GetBeneficiariesDetailsIdAsync(int Id);
        Task<ServiceResult> GetAllBeneficiariesListByOrgIdAsync(string orgId);

        Task<ServiceResult> SaveBeneficiaryDetails(BeneficiariesSendDTO beneficiariesSendDTO);
        Task<ServiceResult> UpdateBeneficiaryDetails(BeneficiariesUpdateDTO beneficiariesUpdateDTO);
        Task<ServiceResult> AddMultipleBeneficiariesAsync(IList<BeneficiariesSendDTO> beneficiaries);
    }
}
