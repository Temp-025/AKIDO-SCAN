using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<bool> IsClientExistsAsync(string clientName);
        Task<bool> IsClientExistsWithNameAsync(Client client);
        Task<bool> IsClientExistsWithRedirecturlAsync(Client client);
        Task<bool> IsClientExistsWithAppUrlAsync(Client client);
        Task<bool> IsClientExistsWithAppNameAsync(Client client);
        Task<Client> GetClientByClientIdAsync(string clientId);
        Task<Client> GetClientByAppNameAsync(string appName);
        Task<IEnumerable<Client>> ListClientByOrganizationIdAsync(string orgID);
        Task<IEnumerable<Client>> ListOAuth2ClientAsync();
        Task<IEnumerable<Client>> ListAllClient();
        Task<string[]> GetAllowedOrigins();
        Task<string[]> GetAllClientAppNames(string request);
        Task<int> GetActiveClientsCount();
        Task<int> GetInActiveClientsCount();
        Task<IEnumerable<Client>> ListClientByOrgUidAsync(string OrgUid);
    }
}
