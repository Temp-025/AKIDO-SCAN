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
    public interface ILocalClientService
    {
        Task<ClientResponse> CreateClientAsync(Client client, bool makerCheckerFlag = false);
        Task<Client> GetClientAsync(int id);
        Task<Client> GetClientByAppNameAsync(string appName);
        Task<Client> GetClientByClientIdAsync(string clientId);
        Task<ClientResponse> UpdateClientAsync(Client client,
            ClientsSaml2 clientsSaml2,
            bool makerCheckerFlag = false);
        Task<ClientResponse> DeleteClientAsync(int id, string updatedBy,
            bool makerCheckerFlag = false);
        Task<IEnumerable<Client>> ListClientAsync();
        Task<ClientResponse> UpdateClientState(int id, bool isApproved, string reason = null);
        Task<ClientResponse> DeActivateClientAsync(int id);
        Task<ClientResponse> ActivateClientAsync(int id);
        Task<IEnumerable<Client>> ListClientByOrganizationIdAsync(string orgID);
        Task<IEnumerable<Client>> ListOAuth2ClientAsync();
        //Task<string> GetSaml2Config(string clientId);
        //Task<string[]> GetAllowedOrigins(string origin);
        Task<string[]> GetAllClientAppNames(string request);
        Task<ClientsCount> GetAllClientsCount();
        Task<Dictionary<string, string>> EnumClientIds();
        Task<IEnumerable<Client>> ListClientByOrgUidAsync(string OrgUid);
        Task<ClientResponse> DeleteClientByClientId(string clientId);
    }
}
