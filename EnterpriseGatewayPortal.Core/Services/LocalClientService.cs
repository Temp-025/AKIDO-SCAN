using EnterpriseGatewayPortal.Core.Constants;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class LocalClientService : ILocalClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LocalClientService> _logger;
        private readonly IMCValidationService _mcValidationService;

        public LocalClientService(ILogger<LocalClientService> logger,
            IUnitOfWork unitOfWork,
            IMCValidationService mcValidationService
            )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

            _mcValidationService = mcValidationService;
        }

        public async Task<ClientResponse> CreateClientAsync(Client client,
            bool makerCheckerFlag = false)
        {
            _logger.LogInformation("--->CreateClientAsync");
            var isEnabled = false;
            int actvityId = 0;

            // Get Start Time
            var startTime = DateTime.Now.ToString("s");

            var isExists = await _unitOfWork.Client.IsClientExistsWithNameAsync(
                client);
            if (true == isExists)
            {
                _logger.LogError("Client already exists with given client id");
                return new ClientResponse("Client already exists with given" +
                    " Client Id");
            }

            isExists = await _unitOfWork.Client.IsClientExistsWithAppNameAsync(
                client);
            if (true == isExists)
            {
                _logger.LogError("Client already exists with given application name");
                return new ClientResponse("Client already exists with given" +
                    " Name");
            }

            isExists = await _unitOfWork.Client.IsClientExistsWithRedirecturlAsync(
                client);
            if (true == isExists)
            {
                _logger.LogError("Client already exists with given redirect url");
                return new ClientResponse("Client already exists with given" +
                    " Redirect url");
            }

            isExists = await _unitOfWork.Client.IsClientExistsWithAppUrlAsync(
                client);
            if (true == isExists)
            {
                _logger.LogError("Client already exists with given application url");
                return new ClientResponse("Client already exists with given" +
                    " Application url");
            }

            // Check isMCEnabled
            if (client.Type == "SAML2")
            {
                isEnabled = await _mcValidationService.IsMCEnabled(
                ActivityIdConstants.ClientSaml2ActivityId);
                actvityId = ActivityIdConstants.ClientSaml2ActivityId;
            }
            if (client.Type == "OAUTH2")
            {
                isEnabled = await _mcValidationService.IsMCEnabled(
                ActivityIdConstants.ClientActivityId);
                actvityId = ActivityIdConstants.ClientActivityId;
            }

            if (false == makerCheckerFlag && true == isEnabled)
            {
                // Validation in makerchecker table
                var response = await _mcValidationService.IsCheckerApprovalRequired
                    (actvityId,
                    "CREATE", client.CreatedBy,
                    JsonConvert.SerializeObject(client));
                if (!response.Success)
                {
                    _logger.LogError("CheckApprovalRequired Failed");
                    return new ClientResponse(response.Message);
                }
                if (response.Result)
                {
                    return new ClientResponse(client, "Your request sent for approval");
                }
            }

            try
            {
                client.CreatedDate = DateTime.Now;
                client.ModifiedDate = DateTime.Now;
                client.Status = "ACTIVE";
                client.Hash = "na";
                //client.PublicKeyCert = Convert.ToBase64String(Encoding.UTF8.GetBytes(client.PublicKeyCert));
                if (client.GrantTypes.Contains("authorization_code_with_pkce"))
                {
                    client.WithPkce = true;
                }
                else
                {
                    client.WithPkce = false;
                }

                await _unitOfWork.Client.AddAsync(client);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation("<---CreateClientAsync");

                return new ClientResponse(client, "Client created successfully");
            }
            catch
            {
                _logger.LogError("client AddAsync failed");
                _logger.LogInformation("<---CreateClientAsync");
                return new ClientResponse("An error occurred while creating the client." +
                    " Please contact the admin.");
            }
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<Client> GetClientAsync(int id)
        {
            _logger.LogInformation("--->GetClientAsync");
            var clientInDb = await _unitOfWork.Client.GetByIdAsync(id);
            if (null == clientInDb)
            {
                return null;
            }

            if (null != clientInDb.PublicKeyCert)
            {
                try
                {
                    clientInDb.PublicKeyCert = Encoding.UTF8.GetString(Convert.FromBase64String(
                        clientInDb.PublicKeyCert));
                }
                catch (Exception error)
                {
                    _logger.LogError("GetClientAsync Failed: {0}", error.Message);
                }
            }



            return clientInDb;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<Client> GetClientByAppNameAsync(string appName)
        {
            _logger.LogInformation("--->GetClientByAppNameAsync");
            var clientInDb = await _unitOfWork.Client.GetClientByAppNameAsync(appName);
            if (null == clientInDb)
            {
                return null;
            }



            return clientInDb;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<Client> GetClientByClientIdAsync(string clientId)
        {
            _logger.LogDebug("--->GetClientByClientIdAsync");
            var clientInDb = await _unitOfWork.Client.GetClientByClientIdAsync(clientId);
            if (null == clientInDb)
            {
                return null;
            }


            return clientInDb;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public async Task<ClientResponse> UpdateClientMCOffAsync(Client client,
            ClientsSaml2 clientsSaml2)
        {

            var isExists = await _unitOfWork.Client.IsClientExistsAsync(client.ClientId);
            if (false == isExists)
            {
                _logger.LogError("Client not found");
                return new ClientResponse("Client not found");
            }
            _unitOfWork.Client.Reload(client);
            var clientInDb = _unitOfWork.Client.GetById(client.Id);
            if (null == clientInDb)
            {
                _logger.LogError("Client not found");
                return new ClientResponse("Client not found");
            }

            _unitOfWork.Client.Reload(clientInDb);


            if (clientInDb.RedirectUri != client.RedirectUri)
            {
                isExists = await _unitOfWork.Client.IsClientExistsWithRedirecturlAsync(
                    client);
                if (true == isExists)
                {
                    _logger.LogError("Another client with redirect_url found");
                    return new ClientResponse("Another client with redirect_url found");
                }
            }

            if (clientInDb.ApplicationName != client.ApplicationName)
            {
                isExists = await _unitOfWork.Client.IsClientExistsWithAppNameAsync(
                    client);
                if (true == isExists)
                {
                    _logger.LogError("Another client with application name found");
                    return new ClientResponse("Another client with application name found");
                }
            }

            if (clientInDb.ApplicationUrl != client.ApplicationUrl)
            {
                isExists = await _unitOfWork.Client.IsClientExistsWithAppUrlAsync(
                    client);
                if (true == isExists)
                {
                    _logger.LogError("Another client with application url");
                    return new ClientResponse("Another client with application url");
                }
            }

            if (clientInDb.Status == "DELETED")
            {
                _logger.LogError("Client is already deleted");
                return new ClientResponse("Client is already deleted");
            }
            try
            {

                //clientInDb.Id = client.Id;
                clientInDb.Scopes = client.Scopes;
                clientInDb.RedirectUri = client.RedirectUri;
                clientInDb.ClientSecret = client.ClientSecret;
                clientInDb.ClientId = client.ClientId;
                clientInDb.ResponseTypes = client.ResponseTypes;
                clientInDb.LogoutUri = client.LogoutUri;
                clientInDb.ModifiedDate = DateTime.Now;
                clientInDb.EncryptionCert = client.EncryptionCert;
                if (null != clientInDb.PublicKeyCert)
                    clientInDb.PublicKeyCert = Convert.ToBase64String(
                        Encoding.UTF8.GetBytes(client.PublicKeyCert ?? ""));
                clientInDb.GrantTypes = client.GrantTypes;
                clientInDb.ApplicationName = client.ApplicationName;
                clientInDb.ApplicationType = client.ApplicationType;
                clientInDb.ApplicationUrl = client.ApplicationUrl;
                clientInDb.OrganizationUid = client.OrganizationUid;

                //if (client.Type == "SAML2")
                //{
                //    clientInDb.ClientsSaml2s = new List<ClientsSaml2>()
                //        { clientsSaml2};
                //}

                _unitOfWork.Client.Update(clientInDb);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation("<---UpdateClient");
                return new ClientResponse(client, "Client updated successfully");
            }
            catch (Exception error)
            {
                _logger.LogError("Client Update failed : {0}", error.Message);
                return new ClientResponse("An error occurred while updating the client." +
                    " Please contact the admin.");
            }

        }


        public async Task<ClientResponse> UpdateClientAsync(Client ipClient,
            ClientsSaml2 clientsSaml2,
            bool makerCheckerFlag = false)
        {
            _logger.LogInformation("--->UpdateClientAsync");

            var isEnabled = false;
            int activityId = 0;

            Client client = ipClient;



            var isExists = await _unitOfWork.Client.IsClientExistsAsync(client.ClientId);
            if (false == isExists)
            {
                _logger.LogError("Client not found");
                return new ClientResponse("Client not found");
            }
            var allClients = await _unitOfWork.Client.GetAllAsync();

            foreach (var item in allClients)
            {
                if (item.ClientId != client.ClientId)
                {
                    if (item.RedirectUri == client.RedirectUri)
                    {
                        _logger.LogError("Client already exists with given redirect uri");
                        return new ClientResponse("Client already exists with given redirect uri");
                    }
                    if (item.ApplicationName == client.ApplicationName)
                    {
                        _logger.LogError("Client already exists with given application name");
                        return new ClientResponse("Client already exists with given application name");
                    }
                    if (item.ApplicationUrl == client.ApplicationUrl)
                    {
                        _logger.LogError("Client already exists with given application name");
                        return new ClientResponse("Client already exists with given application name");
                    }
                }
            }





            var clientInDb = _unitOfWork.Client.GetById(client.Id);
            if (null == clientInDb)
            {
                _logger.LogError("Client not found");
                return new ClientResponse("Client not found");
            }


            if (clientInDb.Status == "DELETED")
            {
                _logger.LogError("Client is already deleted");
                return new ClientResponse("Client is already deleted");
            }

            // Check isMCEnabled
            if (client.Type == "SAML2")
            {
                isEnabled = await _mcValidationService.IsMCEnabled(
                ActivityIdConstants.ClientSaml2ActivityId);
                activityId = ActivityIdConstants.ClientSaml2ActivityId;
            }
            if (client.Type == "OAUTH2")
            {
                isEnabled = await _mcValidationService.IsMCEnabled(
                ActivityIdConstants.ClientActivityId);
                activityId = ActivityIdConstants.ClientActivityId;
            }

            ClientRequest clientRequest = new ClientRequest()
            {
                client = client,
                ClientSaml2 = clientsSaml2
            };


            if (false == makerCheckerFlag && true == isEnabled)
            {
                _unitOfWork.DisableDetectChanges();

                // Validation in makerchecker table
                var response = await _mcValidationService.IsCheckerApprovalRequired(
                    activityId,
                    "UPDATE",
                    client.UpdatedBy,
                    JsonConvert.SerializeObject(clientRequest));
                if (!response.Success)
                {
                    _logger.LogError("CheckApprovalRequired Failed");
                    return new ClientResponse(response.Message);
                }
                if (response.Result)
                {
                    return new ClientResponse(client, "Your request sent for approval");
                }
            }
            try
            {
                clientInDb = await _unitOfWork.Client.GetClientByClientIdAsync(client.ClientId);
                if (null == clientInDb)
                {
                    _logger.LogError("Client not found");
                    return new ClientResponse("Client not found");
                }

                //clientInDb.Id = client.Id;
                clientInDb.Scopes = client.Scopes;
                clientInDb.RedirectUri = client.RedirectUri;
                clientInDb.ClientSecret = client.ClientSecret;
                clientInDb.ClientId = client.ClientId;
                clientInDb.ResponseTypes = client.ResponseTypes;
                clientInDb.LogoutUri = client.LogoutUri;
                clientInDb.ModifiedDate = DateTime.Now;
                clientInDb.EncryptionCert = client.EncryptionCert;
                //if(null != clientInDb.PublicKeyCert)
                //    clientInDb.PublicKeyCert = Convert.ToBase64String(
                //        Encoding.UTF8.GetBytes(client.PublicKeyCert));
                clientInDb.GrantTypes = client.GrantTypes;
                clientInDb.ApplicationName = client.ApplicationName;
                clientInDb.ApplicationType = client.ApplicationType;
                clientInDb.ApplicationUrl = client.ApplicationUrl;
                clientInDb.OrganizationUid = client.OrganizationUid;

                //if (client.Type == "SAML2")
                //{
                //    clientInDb.ClientsSaml2s = new List<ClientsSaml2>()
                //        { clientsSaml2};
                //}

                _unitOfWork.Client.Update(clientInDb);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation("<---UpdateClient");
                return new ClientResponse(client, "Application updated successfully");
            }
            catch (Exception error)
            {
                _logger.LogError("Client Update failed : {0}", error.Message);
                return new ClientResponse("An error occurred while updating the client." +
                    " Please contact the admin.");
            }
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public async Task<ClientResponse> DeleteClientAsync(int id, string updatedBy,
            bool makerCheckerFlag = false)
        {
            var clientInDb = new Client();

            clientInDb = await _unitOfWork.Client.GetByIdAsync(id);
            if (null == clientInDb)
            {
                return new ClientResponse("Client not found");
            }

            // Check isMCEnabled
            var isEnabled = await _mcValidationService.IsMCEnabled(
                ActivityIdConstants.ClientActivityId);

            if (false == makerCheckerFlag && true == isEnabled)
            {
                // Validation in makerchecker table
                var response = await _mcValidationService.IsCheckerApprovalRequired(
                    ActivityIdConstants.ClientActivityId,
                    "DELETE",
                    updatedBy,
                    JsonConvert.SerializeObject(new { Id = id, UpdatedBy = updatedBy }));
                if (!response.Success)
                {
                    _logger.LogError("CheckApprovalRequired Failed");
                    return new ClientResponse(response.Message);
                }
                if (response.Result)
                {
                    return new ClientResponse(clientInDb, "Your request sent for approval");
                }
            }
            try
            {

                clientInDb.ModifiedDate = DateTime.Now;
                clientInDb.UpdatedBy = updatedBy;
                clientInDb.Status = "DELETED";

                _unitOfWork.Client.Update(clientInDb);
                await _unitOfWork.SaveAsync();

                return new ClientResponse(clientInDb, "Client deleted successfully");
            }
            catch
            {
                return new ClientResponse("An error occurred while deleting the client." +
                    " Please contact the admin.");
            }

        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<ClientResponse> DeActivateClientAsync(int id)
        {
            var clientInDb = await _unitOfWork.Client.GetByIdAsync(id);
            if (null == clientInDb)
            {
                return new ClientResponse("Client not found");
            }

            clientInDb.Status = "DEACTIVATED";

            try
            {
                _unitOfWork.Client.Update(clientInDb);
                await _unitOfWork.SaveAsync();

                return new ClientResponse(clientInDb);
            }
            catch
            {
                return new ClientResponse("An error occurred while deleting the client." +
                    " Please contact the admin.");
            }

        }

        public async Task<ClientResponse> ActivateClientAsync(int id)
        {
            var clientInDb = await _unitOfWork.Client.GetByIdAsync(id);
            if (null == clientInDb)
            {
                return new ClientResponse("Client not found");
            }

            clientInDb.Status = "ACTIVE";

            try
            {
                _unitOfWork.Client.Update(clientInDb);
                await _unitOfWork.SaveAsync();

                return new ClientResponse(clientInDb);
            }
            catch
            {
                return new ClientResponse("An error occurred while deleting the client." +
                    " Please contact the admin.");
            }

        }
        public async Task<IEnumerable<Client>> ListClientAsync()
        {
            return await _unitOfWork.Client.ListAllClient();
        }

        public async Task<IEnumerable<Client>> ListClientByOrganizationIdAsync(string orgID)
        {
            return await _unitOfWork.Client.ListClientByOrganizationIdAsync(orgID);
        }


        public async Task<IEnumerable<Client>> ListClientByOrgUidAsync(string OrgUid)
        {
            return await _unitOfWork.Client.ListClientByOrgUidAsync(OrgUid);
        }

        public async Task<IEnumerable<Client>> ListOAuth2ClientAsync()
        {
            return await _unitOfWork.Client.ListOAuth2ClientAsync();
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public async Task<ClientResponse> UpdateClientState(int id,
            bool isApproved,
            string reason = null)
        {
            var clientInDB = await _unitOfWork.Client.GetByIdAsync(id);
            if (clientInDB == null)
            {
                return new ClientResponse("Role not found");
            }

            if (isApproved)
            {
                clientInDB.Status = "ACTIVE";
            }
            else
            {
                clientInDB.Status = "BLOCKED";
                //.BlockedReason = reason;
            }
            try
            {
                _unitOfWork.Client.Update(clientInDB);
                await _unitOfWork.SaveAsync();

                return new ClientResponse(clientInDB);
            }
            catch (Exception)
            {
                // Do some logging stuff
                return new ClientResponse($"An error occurred while changing Status" +
                    $" of the client. Please contact the admin.");
            }
        }

        public async Task<string[]> GetAllClientAppNames(string request)
        {
            return await _unitOfWork.Client.GetAllClientAppNames(request);
        }

        public async Task<ClientsCount> GetAllClientsCount()
        {
            var activeClientsCount = await _unitOfWork.Client.GetActiveClientsCount();
            var inactiveClientsCount = await _unitOfWork.Client.GetInActiveClientsCount();

            ClientsCount clientsCount = new ClientsCount();
            clientsCount.Active = activeClientsCount;
            clientsCount.InActive = inactiveClientsCount;
            return clientsCount;
        }

        public async Task<Dictionary<string, string>> EnumClientIds()
        {
            var response = new Dictionary<string, string>();
            var clientInDb = await _unitOfWork.Client.ListOAuth2ClientAsync();
            if (null == clientInDb)
            {
                return null;
            }

            foreach (var item in clientInDb)
            {
                response.Add(item.ClientId, item.ApplicationName!);
            }

            return response;
        }
        public async Task<ClientResponse> DeleteClientByClientId(string clientId)
        {
            var clientInDb = new Client();

            clientInDb = await _unitOfWork.Client.GetClientByClientIdAsync(clientId);
            if (null == clientInDb)
            {
                return new ClientResponse("Client not found");
            }

            try
            {

                clientInDb.ModifiedDate = DateTime.Now;
                clientInDb.Status = "DELETED";

                _unitOfWork.Client.Update(clientInDb);
                await _unitOfWork.SaveAsync();

                return new ClientResponse(clientInDb, "Client deleted successfully");
            }
            catch
            {
                return new ClientResponse("An error occurred while deleting the client." +
                    " Please contact the admin.");
            }

        }
    }
}
