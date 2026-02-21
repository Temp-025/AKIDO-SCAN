using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class ScopeService : IScopeService
    {
        private readonly HttpClient _client;
        private readonly ILogger<ScopeService> _logger;
        public ScopeService(HttpClient httpClient, IConfiguration configuration, ILogger<ScopeService> logger)
        {
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:MultiPivotServiceBaseAddress"]!);
            _logger = logger;
            _client = httpClient;
        }

        public async Task<IEnumerable<UserClaimDTO>> GetScopeAttributeList()
        {

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/GetAttributeList");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var scopes = JsonConvert.DeserializeObject<IEnumerable<UserClaimDTO>>(apiResponse.Result.ToString()!);
                        return scopes;
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }

        public async Task<ServiceResult> CreateScopeAsync(Scope scope)
        {
            try
            {

                _logger.LogInformation("Create Profile api call start");
                string jsonPayload = JsonConvert.SerializeObject(scope,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync($"api/MultiPivot/AddProfile", content);

                _logger.LogInformation("Create Profile api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        _logger.LogInformation(apiResponse.Message);
                        var result = JsonConvert.DeserializeObject<Scope>(Convert.ToString(apiResponse.Result)!);
                        return new ServiceResult(true, apiResponse.Message, result!);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                    return new ServiceResult("Internal Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ServiceResult(ex.Message);
            }

        }

        public async Task<Scope> GetScopeById(int id)
        {

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/EditProfile/{id}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var pivots = JsonConvert.DeserializeObject<Scope>(Convert.ToString(apiResponse.Result.ToString()!));
                        return pivots;

                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }

        public async Task<ServiceResult> DeleteScopeById(int id)
        {

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/DeleteProfile/{id}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var pivots = JsonConvert.DeserializeObject<Scope>(apiResponse.Result.ToString()!);
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                    return new ServiceResult(false, "Error While Deleting");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }


        public async Task<ServiceResult> UpdateScopeAsync(Scope scope)
        {
            try
            {

                _logger.LogInformation("Create Profile api call start");
                string jsonPayload = JsonConvert.SerializeObject(scope,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync($"api/MultiPivot/UpdateProfile", content);

                _logger.LogInformation("Create Profile api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        _logger.LogInformation(apiResponse.Message);
                        var result = JsonConvert.DeserializeObject<Scope>(apiResponse.Result.ToString()!);
                        return new ServiceResult(true, apiResponse.Message, result!);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                    return new ServiceResult("Internal Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ServiceResult(ex.Message);
            }

        }
    }
}
