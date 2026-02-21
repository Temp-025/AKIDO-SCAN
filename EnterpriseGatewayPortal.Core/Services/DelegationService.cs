//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
//using NuGet.Common;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class DelegationService : IDelegationService
//    {
//        private readonly HttpClient _client;
//        private readonly ILogger<DelegationService> _logger;
//        private readonly IConfiguration _configuration;

//        public DelegationService(HttpClient httpClient, IConfiguration configuration, ILogger<DelegationService> logger)
//        {
//            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:DelegationServiceBaseAddress"]);

//            _logger = logger;
//            _client = httpClient;
//            _configuration = configuration;
//        }
//        public async Task<ServiceResult> GetDelegatesListByOrgIdAndSuidAsync(string token)
//        {
//            try
//            {
//                _logger.LogInformation("Get All Delegates List api call start");
//                _client.DefaultRequestHeaders.Add("x-access-token", token);
//                HttpResponseMessage response =  await _client.GetAsync($"api/delegate/getdelegatelist");
//                _logger.LogInformation("Get All Delegates List api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var result = JsonConvert.DeserializeObject<IEnumerable<DelegatorListDTO>>(apiResponse.Result.ToString());
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

//        public async Task<ServiceResult> GetBusinessUsersListByOrgAsync(string token)
//        {
//            try
//            {
//                _logger.LogInformation("Get All Delegates List api call start");
//                _client.DefaultRequestHeaders.Add("x-access-token", token);
//                HttpResponseMessage response = await _client.GetAsync($"api/delegate/getbusinessuserslist");
//                _logger.LogInformation("Get All Delegates List api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var result = JsonConvert.DeserializeObject<List<DelegateBusinessUserDTO>>(apiResponse.Result.ToString());
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

//        public async Task<ServiceResult> SaveDelegatorAsync(SaveDelegatorDTO delegateDTO, string token)
//        {
//            try
//            {
//                _logger.LogInformation("Get All Delegates List api call start");

//                HttpResponseMessage response;
//                using (var multipartFormContent = new MultipartFormDataContent())
//                {
//                    multipartFormContent.Add(new StringContent(delegateDTO.Model), "Model");


//                    _client.DefaultRequestHeaders.Add("x-access-token", token);

//                     response = await _client.PostAsync($"api/delegate/savenewdelegate", multipartFormContent);
//                    _logger.LogInformation("Get All Delegates List api call end");
//                }
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(true, apiResponse.Message);
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

//        public async Task<ServiceResult> GetDelegateDetailsByIdAsync(string id, string token)
//        {
//            try
//            {
//                _logger.LogInformation("Get Delegates details by id api call start");
//                _client.DefaultRequestHeaders.Add("x-access-token", token);
//                HttpResponseMessage response = await _client.GetAsync($"api/delegate/getdelegatedetails/?id={id}");
//                _logger.LogInformation("Get Delegates details by id api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var result = JsonConvert.DeserializeObject<DelegatorListDTO>(apiResponse.Result.ToString());
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(true, apiResponse.Message, result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }

//                }
//                return null;
//            }
//            catch (Exception ex)
//            {

//                _logger.LogError(ex, ex.Message);
//                return new ServiceResult(ex.Message);
//            }
//        }

//        public async Task<ServiceResult> RevokeDelegateAsync(string id, string token)
//        {
//            try
//            {
//                _logger.LogInformation("Revoke Delegate by id api call start");
//                _client.DefaultRequestHeaders.Add("x-access-token", token);
//                HttpResponseMessage response = await _client.PostAsync($"api/delegate/revokedelegate/?id={id}", null);
//                _logger.LogInformation("Revoke Delegate by id api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {

//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(true, apiResponse.Message);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }

//                }
//                return null;
//            }
//            catch (Exception ex)
//            {

//                _logger.LogError(ex, ex.Message);
//                return new ServiceResult(ex.Message);
//            }
//        }


//    }
//}
