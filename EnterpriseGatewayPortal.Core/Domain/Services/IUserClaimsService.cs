using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IUserClaimsService
    {
        Task<IEnumerable<UserClaimsDTO>> GetUserClaimsListAsync();

        Task<UserClaimsDTO> GetUserClaimsByIdAsync(int id);
        Task<ServiceResult> CreateUserClaimsAsync(UserClaimsDTO claims);

        Task<ServiceResult> UpdateUserClaimsDataAsync(UserClaimsDTO claims);

        Task<ServiceResult> DeleteUserClaimsAsync(int id);
    }
}
