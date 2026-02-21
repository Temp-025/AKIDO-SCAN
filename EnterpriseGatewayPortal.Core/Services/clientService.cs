using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class clientService : IClientService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<clientService> _logger;
        public clientService(HttpClient httpClient,
            IConfiguration configuration,
            ILogger<clientService> logger)
        {
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:ClientServiceServiceBaseAddress"]!);
            _client = httpClient;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<List<ClientListDTO>> getList(string organizationUid)
        {
            var orgUID = organizationUid;
            try
            {
                string url = $"ClientApi/getListByOrgUID?orgUID={orgUID}";
                var response = _client.GetAsync(url).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JArray jArray = JArray.FromObject(apiResponse.Result);

                        var jsonResponseList = JsonConvert.DeserializeObject<List<ClientListDTO>>(jArray.ToString());
                        return jsonResponseList;
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<ServiceResult> SaveClient(SaveClientDTO model)
        {
            try
            {
                var orgUID = _configuration["OrganizationUid"];
                string url = "ClientApi/SaveClient";


                string json = JsonConvert.SerializeObject(model);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = _client.PostAsync(url, content).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);

                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message, apiResponse.Result);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<ClientsEditViewModel> GetClientAsync(string Id)
        {
            try
            {
                var orgUID = _configuration["OrganizationUid"];
                string url = "ClientApi/GetClientByClientID?clientId=" + Id;
                var response = _client.GetAsync(url).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject jobject = JObject.FromObject(apiResponse.Result);


                        var jsonResponseList = JsonConvert.DeserializeObject<ClientsEditViewModel>(jobject.ToString());
                        return jsonResponseList;
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<ServiceResult> UpdateClientAsync(UpdateClientDTO model)
        {
            try
            {

                string url = "ClientApi/UpdateClient";


                string json = JsonConvert.SerializeObject(model);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = _client.PostAsync(url, content).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<IEnumerable<Client>> getAllClientsList(string organizationUid)
        {
            var orgUID = organizationUid;
            try
            {
                string url = $"ClientApi/getclientsListByOrgUID?orgUID={orgUID}"; ;
                var response = _client.GetAsync(url).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JArray jArray = JArray.FromObject(apiResponse.Result);

                        var jsonResponseList = JsonConvert.DeserializeObject<IEnumerable<Client>>(jArray.ToString());
                        return jsonResponseList;
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<ClientResponse> DeleteClientByClientId(string clientId)
        {
            try
            {
                var orgUID = _configuration["OrganizationUid"];
                string url = "ClientApi/Delete?clientId=" + clientId;
                var response = _client.PostAsync(url, null).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ClientResponse(null, "Client deleted successfully");
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ClientResponse(apiResponse.Message);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }

        }
    }
}
