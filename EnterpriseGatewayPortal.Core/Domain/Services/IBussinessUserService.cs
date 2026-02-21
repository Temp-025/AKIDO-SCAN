using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IBussinessUserService
    {
        Task<ServiceResult> AddBusinessUserAsync(BusinessUserDTO businessUerDto);
        Task<ServiceResult> AddBusinessUserCSV(IList<BusinessUserDTO> businessUserDTO);

        Task<IEnumerable<BusinessUserDTO>> GetAllBusinessUserAsync(string orgId);

        Task<BusinessUserDTO> GetBusinessUserDetailsAsync(int businessUserId);

        Task<ServiceResult> UpdateBusinessUserAsync(BusinessUserDTO businessUser);
        Task<ServiceResult> DeleteBusinessUserAsync(string EmpEmail, string Id);
    }
}
