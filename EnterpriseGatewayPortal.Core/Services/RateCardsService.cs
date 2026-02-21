//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
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

//    public class RateCardsService : IRateCardsService
//    {
//        private readonly HttpClient _client;
//        private readonly ILogger<RateCardsService> _logger;
//        private readonly IConfiguration _configuration;

//        public RateCardsService(HttpClient httpClient, IConfiguration configuration, ILogger<RateCardsService> logger)
//        {
//            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:PriceModelServiceBaseAddress"]);

//            _logger = logger;
//            _client = httpClient;
//            _configuration = configuration;

//        }
//        public async Task<ServiceResult> GetAllRateCardsAsync()
//        {
//            try
//            {
//                _logger.LogInformation("Get api of Rate Cards List start.");
//                HttpResponseMessage response = await _client.GetAsync($"api/get-all-rate-cards");
//                _logger.LogInformation("Get api of Rate Cards List stop.");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        //return JsonConvert.DeserializeObject<IEnumerable<RateCardDTO>>(apiResponse.Result.ToString());
//                        var result = JsonConvert.DeserializeObject<IEnumerable<RateCardsDTO>>(apiResponse.Result.ToString());
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
//    }
//}
