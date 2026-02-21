//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
//using NLog;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class BeneficiariesService : IBeneficiariesService
//    {
//        private readonly HttpClient _client;
//        private readonly ILogger<BeneficiariesService> _logger;
//        private readonly IConfiguration _configuration;

//        public BeneficiariesService(HttpClient httpClient, IConfiguration configuration, ILogger<BeneficiariesService> logger)
//        {
//            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:OrganizationOnboardingServiceBaseAddress"]);

//            _logger = logger;
//            _client = httpClient;
//            _configuration = configuration;
//        }

//        public async Task<ServiceResult> GetAllBeneficiariesServicesAsync()
//        {
//            try
//            {
//                _logger.LogInformation("Get All Sponsor Beneficiaries Service Privilages List by Org Id api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/get/active/beneficiary-privileges");
//                _logger.LogInformation("Get All Sponsor Beneficiaries Service Privilages List by Org Id api call end");

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var result = JsonConvert.DeserializeObject<IEnumerable<BeneficiariesServicesDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}");
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                return new ServiceResult(ex.Message);
//            }


//        }


//        public async Task<ServiceResult> GetBeneficiariesDetailsIdAsync(int Id)
//        {
//            try
//            {

//                _logger.LogInformation("Get Beneficiaries Details by Id api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/get/beneficiary/by/id/{Id}");
//                _logger.LogInformation("Get Beneficiaries Details by Id api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result = JsonConvert.DeserializeObject<BeneficiariesGetDetailsDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);

//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                return new ServiceResult(ex.Message);
//            }

//        }

//        public async Task<ServiceResult> GetAllBeneficiariesListByOrgIdAsync(string orgId)
//        {
//            try
//            {

//                _logger.LogInformation("Get All Sponsor Beneficiaries List by Org Id api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/get/all/beneficiaries/by/sponsor-id/{orgId}");
//                _logger.LogInformation("Get All Sponsor Beneficiaries List by Org Id api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result = JsonConvert.DeserializeObject<IEnumerable<SponsorBeneficiaryDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);

//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                return new ServiceResult(ex.Message);
//            }

//        }

//        public async Task<ServiceResult> SaveBeneficiaryDetails(BeneficiariesSendDTO beneficiariesSendDTO)
//        {
//            try
//            {
//                _logger.LogInformation("Save Beneficiary Details API call start");
//                string jsonPayload = JsonConvert.SerializeObject(beneficiariesSendDTO, 
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

//                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = await _client.PostAsync($"api/add/beneficiaries", content);

//                _logger.LogInformation("Save Beneficiary Details API call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());

//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);

//                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }

//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                              $"with status code={response.StatusCode}");
//                    return new ServiceResult("Internal Error");

//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);

//            }
//            return new ServiceResult(false, "An error occurred while saving Beneficiary. Please try later.");


//        }

//        public async Task<ServiceResult> UpdateBeneficiaryDetails(BeneficiariesUpdateDTO beneficiariesUpdateDTO)
//        {
//            try
//            {
//                _logger.LogInformation("Update Beneficiary Details API call start");
//                string jsonPayload = JsonConvert.SerializeObject(beneficiariesUpdateDTO, 
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

//                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = await _client.PostAsync($"api/update/beneficiaries", content);

//                _logger.LogInformation("Update Beneficiary Details API call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());

//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);

//                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }

//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                              $"with status code={response.StatusCode}");
//                    return new ServiceResult("Internal Error");

//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);

//            }
//            return new ServiceResult(false, "An error occurred while saving Beneficiary. Please try later.");


//        }

//        public async Task<ServiceResult> AddMultipleBeneficiariesAsync(IList<BeneficiariesSendDTO> beneficiaries)
//        {
//            try
//            {
//                string json = JsonConvert.SerializeObject(beneficiaries,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
//                _logger.LogInformation("Add Multiple Beneficiaries API call start");

//                var response = await _client.PostAsync($"api/add/multiple/beneficiaries", content);
//                _logger.LogInformation("Add Multiple Beneficiaries API call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}");
//                }

//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            _logger.LogInformation("AddMultiplebeneficiaries end");
//            return new ServiceResult(false, "An error occurred while adding the multiple Beneficiaries. Please try later.");
//        }
//    }
//}
