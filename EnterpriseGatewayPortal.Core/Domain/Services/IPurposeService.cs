using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IPurposeService
    {
        Task<IEnumerable<PurposesDTO>> GetPurposesListAsync();

        Task<PurposesDTO> GetPurposeByIdAsync(int id);
        Task<ServiceResult> CreatePurposeAsync(PurposesDTO purposes);

        Task<ServiceResult> UpdatePurposeDataAsync(PurposesDTO purposes);

        Task<ServiceResult> DeletePurposeAsync(int id);
    }
}
