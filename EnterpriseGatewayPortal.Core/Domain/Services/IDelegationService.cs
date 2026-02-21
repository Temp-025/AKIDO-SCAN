using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IDelegationService
    {

        Task<ServiceResult> GetDelegatesListByOrgIdAndSuidAsync(string token);
        Task<ServiceResult> GetBusinessUsersListByOrgAsync(string token);
        Task<ServiceResult> SaveDelegatorAsync(SaveDelegatorDTO delegateDTO, string token);
        Task<ServiceResult> GetDelegateDetailsByIdAsync(string id, string token);
        Task<ServiceResult> RevokeDelegateAsync(string id, string token);

    }
}
