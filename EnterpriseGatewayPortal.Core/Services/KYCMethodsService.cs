//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using iTextSharp.text.log;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.CodeAnalysis;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http.Json;
//using System.Text;
//using System.Threading.Tasks;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class KYCMethodsService : IKYCMethodsService
//    {
//        private readonly IConfiguration _configuration;
//        private readonly ILogger<KYCMethodsService> _logger;
//        private readonly IHttpClientFactory _httpClientFactory;

//        public KYCMethodsService(IConfiguration configuration, ILogger<KYCMethodsService> logger, IHttpClientFactory httpClientFactory)
//        {
//            _configuration = configuration;
//            _logger = logger;
//            _httpClientFactory = httpClientFactory;
//        }

//        public async Task<IEnumerable<VerificationMethodDTO>> GetKycMethodsListAysnc(string orgUid)
//        {
//            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:VerficationServiceBaseAddress"]);

//            try
//            {
//                _logger.LogInformation("Fetching KYC Methods for Organization: {OrgUid}", orgUid);
//                HttpResponseMessage response = await client.GetAsync($"api/VerificationMethodApi/GetOrganizationVerificationMethods/{orgUid}");
//                _logger.LogInformation("Received response with status code: {StatusCode}", response.StatusCode);

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());

//                    if (apiResponse.Success)
//                    {
//                        return JsonConvert.DeserializeObject<IEnumerable<VerificationMethodDTO>>(apiResponse.Result.ToString());
//                    }
//                    else
//                    {
//                        _logger.LogWarning("API response indicated failure: {Message}", apiResponse.Message);
//                        return Enumerable.Empty<VerificationMethodDTO>();
//                    }

//                }
//                else
//                {
//                    _logger.LogError("Failed to fetch KYC Methods. Status Code: {StatusCode}", response.StatusCode);
//                    return Enumerable.Empty<VerificationMethodDTO>();
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Exception occurred while fetching KYC Methods for Organization: {OrgUid}", orgUid);
//                return Enumerable.Empty<VerificationMethodDTO>();

//            }
//        }

//        public async Task<VerificationMethodStatusDTO> GetOrganizationVerificationMethodByUidAsync(string orgUid, string methodUid)
//        {
//            using var client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:VerficationServiceBaseAddress"]);

//            try
//            {
//                _logger.LogInformation("Fetching Verification Method Status for Method: {MethodUid} and Organization: {OrgUid}", methodUid, orgUid);
//                var response = await client.GetAsync($"api/VerificationMethodApi/GetOrganizationVerificationMethodByUid/{orgUid}/{methodUid}");
//                _logger.LogInformation("Received response with status code: {StatusCode}", response.StatusCode);
//                var content = await response.Content.ReadAsStringAsync();
//                if (response.IsSuccessStatusCode)
//                {
//                    var apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);
//                    if (apiResponse != null && apiResponse.Success)
//                    {
//                        return JsonConvert.DeserializeObject<VerificationMethodStatusDTO>(apiResponse.Result.ToString());
//                    }
//                    _logger.LogWarning("API response indicated failure: {Message}", apiResponse?.Message);
//                    return null;
//                }
//                else
//                {
//                    _logger.LogError("Failed to fetch Verification Method Status. Status Code: {StatusCode}", response.StatusCode);
//                    return null;
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Exception occurred while fetching Verification Method Status for Method: {MethodUid} and Organization: {OrgUid}", methodUid, orgUid);
//                return null;
//            }

//        }

//        public async Task<APIResponse> GetVerificationMethodsStatsAysnc(string orgUid)
//        {
//            using var client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:VerficationServiceBaseAddress"]);

//            try
//            {
//                _logger.LogInformation("Requesting Verification Methods Stats for Organization : {orgUid}", orgUid);

//                var response = await client.GetAsync($"api/OrganizationVerificationMethods/GetVerificationMethodCount/{orgUid}");

//                _logger.LogInformation("Received response with status code: {StatusCode}", response.StatusCode);

//                var content = await response.Content.ReadAsStringAsync();

//                if (response.IsSuccessStatusCode)
//                {
//                    var apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);

//                    if (apiResponse != null && apiResponse.Success)
//                    {
//                        return apiResponse;
//                    }

//                    _logger.LogWarning("API response indicated failure: {Message}", apiResponse?.Message);
//                    return new APIResponse { Success = false, Message = apiResponse?.Message ?? "Unknown error" };
//                }
//                else
//                {
//                    _logger.LogError("Failed to request KYC Method. Status Code: {StatusCode}", response.StatusCode);
//                    return new APIResponse { Success = false, Message = $"API call failed with status {response.StatusCode}" };
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Exception occurred while requesting Verification Methods Stats for Organization: {OrgUid}", orgUid);
//                return new APIResponse { Success = false, Message = "Internal server error occurred." };
//            }

//        }

//        public async Task<APIResponse> RequestKYCMethodAsync(string orgUid, string methodUid)
//        {
//            using var client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:VerficationServiceBaseAddress"]);

//            var payload = new OrganizationVerificationMethodDTO
//            {
//                OrganizationId = orgUid,
//                VerificationMethodUid = methodUid
//            };

//            try
//            {
//                _logger.LogInformation("Requesting KYC Method: {MethodUid} for Organization: {OrgUid}", methodUid, orgUid);

//                // Serialize DTO to JSON and post

//                var jsonBody = JsonConvert.SerializeObject(payload);
//                var httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

//                var response = await client.PostAsync("api/OrganizationVerificationMethods/Add", httpContent);

//                _logger.LogInformation("Received response with status code: {StatusCode}", response.StatusCode);

//                var content = await response.Content.ReadAsStringAsync();

//                if (response.IsSuccessStatusCode)
//                {
//                    var apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);
//                    if (apiResponse != null && apiResponse.Success)
//                    {
//                        return apiResponse;
//                    }

//                    _logger.LogWarning("API response indicated failure: {Message}", apiResponse?.Message);
//                    return new APIResponse { Success = false, Message = apiResponse?.Message ?? "Unknown error" };
//                }
//                else
//                {
//                    _logger.LogError("Failed to request KYC Method. Status Code: {StatusCode}", response.StatusCode);
//                    return new APIResponse { Success = false, Message = $"API call failed with status {response.StatusCode}" };
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Exception occurred while requesting KYC Method: {MethodUid} for Organization: {OrgUid}", methodUid, orgUid);
//                return new APIResponse { Success = false, Message = "Internal server error occurred." };
//            }
//        }

//        public async Task<IEnumerable<VerificationMethodDTO>> GetOrganizationDefaultMethods(string orgUid)
//        {
//            using var client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:VerficationServiceBaseAddress"]);
//            try
//            {
//                _logger.LogInformation("Fetching Default and Active Verification Methods for Organization: {OrgUid}", orgUid);
//                HttpResponseMessage response = await client.GetAsync($"api/VerificationMethodApi/GetOrganizationDefaultMethods/{orgUid}");
//                _logger.LogInformation("Received response with status code: {StatusCode}", response.StatusCode);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return JsonConvert.DeserializeObject<IEnumerable<VerificationMethodDTO>>(apiResponse.Result.ToString());
//                    }
//                    else
//                    {
//                        _logger.LogWarning("API response indicated failure: {Message}", apiResponse.Message);
//                        return Enumerable.Empty<VerificationMethodDTO>();
//                    }
//                }
//                else
//                {
//                    _logger.LogError("Failed to fetch Default and Active Verification Methods. Status Code: {StatusCode}", response.StatusCode);
//                    return Enumerable.Empty<VerificationMethodDTO>();
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Exception occurred while fetching Default and Active Verification Methods for Organization: {OrgUid}", orgUid);
//                return Enumerable.Empty<VerificationMethodDTO>();
//            }
//        }

//        public async Task<IEnumerable<VerificationMethodDTO>> GetAllOrganizationRequestedMethods(string orgUid)
//        {
//            using var client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:VerficationServiceBaseAddress"]);
//            try
//            {
//                _logger.LogInformation("Fetching All Requested Verification Methods for Organization: {OrgUid}", orgUid);
//                HttpResponseMessage response = await client.GetAsync($"api/VerificationMethodApi/GetAllOrganizationRequestedMethods/{orgUid}");
//                _logger.LogInformation("Received response with status code: {StatusCode}", response.StatusCode);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return JsonConvert.DeserializeObject<IEnumerable<VerificationMethodDTO>>(apiResponse.Result.ToString());
//                    }
//                    else
//                    {
//                        _logger.LogWarning("API response indicated failure: {Message}", apiResponse.Message);
//                        return Enumerable.Empty<VerificationMethodDTO>();
//                    }
//                }
//                else
//                {
//                    _logger.LogError("Failed to fetch All Requested Verification Methods. Status Code: {StatusCode}", response.StatusCode);
//                    return Enumerable.Empty<VerificationMethodDTO>();
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Exception occurred while fetching All Requested Verification Methods for Organization: {OrgUid}", orgUid);
//                return Enumerable.Empty<VerificationMethodDTO>();
//            }
//        }

//        public async Task<List<VerificationMethodDTO>> GetPendingVerificationMethods(string orgUid)
//        {
//            using var client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:VerficationServiceBaseAddress"]);
//            try
//            {
//                _logger.LogInformation("Fetching Pending Verification Methods for Organization: {OrgUid}", orgUid);
//                HttpResponseMessage response = await client.GetAsync($"api/VerificationMethodApi/GetPendingVerificationMethods/{orgUid}");
//                _logger.LogInformation("Received response with status code: {StatusCode}", response.StatusCode);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return JsonConvert.DeserializeObject<List<VerificationMethodDTO>>(apiResponse.Result.ToString());
//                    }
//                    else
//                    {
//                        _logger.LogWarning("API response indicated failure: {Message}", apiResponse.Message);
//                        return new List<VerificationMethodDTO>();
//                    }
//                }
//                else
//                {
//                    _logger.LogError("Failed to fetch Pending Verification Methods. Status Code: {StatusCode}", response.StatusCode);
//                    return new List<VerificationMethodDTO>();
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Exception occurred while fetching Pending Verification Methods for Organization: {OrgUid}", orgUid);
//                return new List<VerificationMethodDTO>();
//            }

//        }
//    }
//}
