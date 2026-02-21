//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class KycApplicationService : IKycApplicationService
//    {
//        private readonly HttpClient _client;
//        private readonly ILogger<KycApplicationService> _logger;

//        public KycApplicationService(HttpClient httpClient, IConfiguration configuration, ILogger<KycApplicationService> logger)
//        {
//            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:WalletSeerviceBaseAddress"]);

//            _logger = logger;
//            _client = httpClient;
//        }

//        public async Task<ServiceResult> GetKycApplicationsListByOrgId(string orgUid)
//        {
//            try
//            {

//                _logger.LogInformation("Get kyc applications list by orgId api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/KycServices/GetKycApplicationsListByOrgId?orgId={orgUid}");
//                _logger.LogInformation("Get kyc applications list by orgId api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result = JsonConvert.DeserializeObject<IList<KycApplicationDTO>>(apiResponse.Result.ToString());
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

//        public async Task<ServiceResult> GetKycApplicationById(int id)
//        {
//            try
//            {

//                _logger.LogInformation("Get kyc applications details by Id api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/KycServices/GetKycApplicationById?id={id}");
//                _logger.LogInformation("Get kyc applications details by Id api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result = JsonConvert.DeserializeObject<KycApplicationDTO>(apiResponse.Result.ToString());
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

//        public async Task<ServiceResult> SaveKycApplication(KycApplicationDTO dto)
//        {
//            try
//            {

//                string url = "api/KycServices/SaveKycApplication";


//                string json = JsonConvert.SerializeObject(dto);

//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                var response = _client.PostAsync(url, content).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);

//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message, apiResponse.Result);
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//        public async Task<ServiceResult> UpdateKycApplication(KycApplicationDTO dto)
//        {
//            try
//            {

//                string url = "api/KycServices/UpdateKycApplication";


//                string json = JsonConvert.SerializeObject(dto);

//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                var response = _client.PostAsync(url, content).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);

//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message, apiResponse.Result);
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//    }
//}
