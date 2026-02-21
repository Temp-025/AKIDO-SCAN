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
    public class BussinessUserService : IBussinessUserService
    {
        private readonly HttpClient _client;
        private readonly ILogger<OrganizationService> _logger;

        public BussinessUserService(HttpClient httpClient,
            IConfiguration configuration,
            ILogger<OrganizationService> logger)
        {
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:OrganizationOnboardingServiceBaseAddress"]!);


            _client = httpClient;
            _client.Timeout = TimeSpan.FromMinutes(10);
            _logger = logger;
        }

        public async Task<IEnumerable<BusinessUserDTO>> GetAllBusinessUserAsync(string orgId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get/business/users/{orgId}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<BusinessUserDTO>>(Convert.ToString(apiResponse.Result)!);
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
        public async Task<ServiceResult> AddBusinessUserAsync(BusinessUserDTO businessUerDto)
        {
            try
            {
                _logger.LogInformation("AddOrganizationAsync start");


                string json = JsonConvert.SerializeObject(businessUerDto,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Add Organization api call start");
                HttpResponseMessage response = await _client.PostAsync("api/business-user/save", content);
                _logger.LogInformation("Add Organization api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
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
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            _logger.LogInformation("AddBusinessUserAsync end");
            return new ServiceResult(false, "An error occurred while creating the business. Please try later.");
        }

        public async Task<ServiceResult> AddBusinessUserCSV(IList<BusinessUserDTO> businessUserDTO)
        {

            try
            {
                string json = JsonConvert.SerializeObject(businessUserDTO,
                   new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync($"api/post/add-multiple-business-users", content);
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

            return new ServiceResult(false, "An error occurred while Adding the StakeHolder. Please try later.");
        }

        public async Task<BusinessUserDTO> GetBusinessUserDetailsAsync(int businessUserId)
        {
            try
            {
                _logger.LogInformation("Get organization details by business user id api call start");
                HttpResponseMessage response = await _client.GetAsync($"api/get/business/user/by/{businessUserId}");
                _logger.LogInformation("Get organization details by  business user id api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<BusinessUserDTO>(Convert.ToString(apiResponse.Result)!);
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

        public async Task<ServiceResult> UpdateBusinessUserAsync(BusinessUserDTO businessUser)
        {
            try
            {

                string json = JsonConvert.SerializeObject(businessUser,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync("api/update/business/user", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
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
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while updating the BusinessUser. Please try later.");
        }
        public async Task<ServiceResult> DeleteBusinessUserAsync(string EmpEmail, string Id)
        {
            try
            {


                HttpResponseMessage response = await _client.PostAsync($"api/delete/organisation/business-user/{Id}/{EmpEmail}", null);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
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
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while updating the BusinessUser. Please try later.");
        }
    }

}

