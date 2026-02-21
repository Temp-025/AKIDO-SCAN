//using EnterpriseGatewayPortal.Core.Domain.Models;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class BuyCreditsService: IBuyCreditsService
//    {

//        private readonly HttpClient _client;
//        private readonly ILogger<BeneficiariesService> _logger;
//        private readonly IConfiguration _configuration;

//        public BuyCreditsService(HttpClient httpClient, IConfiguration configuration, ILogger<BeneficiariesService> logger)
//        {
//            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:BuyCreditsServiceBaseAddress"]);
//            httpClient.DefaultRequestHeaders.Add("appVersion", "WEB");
//            httpClient.DefaultRequestHeaders.Add("osVersion", "WEB");
//            httpClient.DefaultRequestHeaders.Add("deviceId", "WEB");
//            _logger = logger;
//            _client = httpClient;
//            _configuration = configuration;
//            _client.Timeout = TimeSpan.FromMinutes(5);
//        }


//        public async Task<ServiceResult> GetAllPrevillages(ServicesBySUIDDTO servicesBySUIDDTO)
//        {
//            try
//            {

//                _logger.LogInformation("Get all Previlages api call start");
//                string jsonPayload = JsonConvert.SerializeObject(servicesBySUIDDTO,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

//                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = await _client.PostAsync($"api/post/onboarding/dataframe", content);

//                _logger.LogInformation("Get all Previlages api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result = JsonConvert.DeserializeObject<GetServicesDTO>(apiResponse.Result.ToString());
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

//        public async Task<ServiceResult> GetAllAvailableCredits(AvailableCreditsDTO availableCreditsDTO)
//        {
//            try
//            {

//                _logger.LogInformation("Get available credits api call start");
//                string jsonPayload = JsonConvert.SerializeObject(availableCreditsDTO,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

//                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = await _client.PostAsync($"api/post/onboarding/dataframe", content);

//                _logger.LogInformation("Get available credits api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result = JsonConvert.DeserializeObject<ExistingCreditsDTO>(apiResponse.Result.ToString());
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


//        public async Task<ServiceResult> GetRateCard(GetRateCardDTO getRateCardDTO)
//        {
//            try
//            {

//                _logger.LogInformation("Get RateCards api call start");
//                string jsonPayload = JsonConvert.SerializeObject(getRateCardDTO,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

//                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = await _client.PostAsync($"api/post/onboarding/dataframe", content);

//                _logger.LogInformation("Get RateCards api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result = JsonConvert.DeserializeObject<PriceSlabDTO>(apiResponse.Result.ToString());
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

//        public async Task<ServiceResult> ProceedCheckout(PaymentBillCreditsServiceDTO paymentBillCreditsServiceDTO)
//        {
//            try
//            {

//                _logger.LogInformation("Proceed checkout api call start");
//                string jsonPayload = JsonConvert.SerializeObject(paymentBillCreditsServiceDTO,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

//                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = await _client.PostAsync($"api/post/onboarding/dataframe", content);

//                _logger.LogInformation("Proceed checkout by Id api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result = JsonConvert.DeserializeObject<PaymentTransactionDTO>(apiResponse.Result.ToString());
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



//    }




//}

