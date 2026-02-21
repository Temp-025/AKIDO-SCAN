using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface ILocalBeneficiariesService
    {
        Task<ServiceResult> AddBeneficiaryUserAsync(Beneficiary model);
        Task<ServiceResult> GetBeneficiaryUserByIdAsync(int id);
        Task<ServiceResult> UpdateBeneficiaryUserAsync(Beneficiary model);
        Task<ServiceResult> GetAllBeneficiaryUsersBySponsorDigitalIdAsync(string orgUid);
        Task<ServiceResult> AddMultipleBeneficiariesListAsync(List<Beneficiary> models);
    }
}
