using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IClientService
    {
        Task<List<ClientListDTO>> getList(string organizationUid);
        Task<ServiceResult> SaveClient(SaveClientDTO model);
        Task<ClientsEditViewModel> GetClientAsync(string Id);
        Task<ServiceResult> UpdateClientAsync(UpdateClientDTO model);
        Task<IEnumerable<Client>> getAllClientsList(string organizationUid);
        Task<ClientResponse> DeleteClientByClientId(string clientId);
    }
}
