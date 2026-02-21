//using EnterpriseGatewayPortal.Core.Domain.Models;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using NLog;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class PriceSlabService : IPriceSlabService
//    {
//        private readonly HttpClient _client;
//        private readonly ILogger<PriceSlabService> _logger;
//        private readonly IConfiguration _configuration;
//        public PriceSlabService(HttpClient httpClient, IConfiguration configuration, ILogger<PriceSlabService> logger)
//        {
//            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:PriceModelServiceBaseAddress"]);

//            _logger = logger;
//            _client = httpClient;
//            _configuration = configuration;
//        }


//        public async Task<IEnumerable<PriceSlabDefinitionDTO>> GetAllPriceSlabDefinitionsAsync()
//        {
//            try
//            {
//                _logger.LogInformation("Get all api price slab definitions call start.");
//                HttpResponseMessage response = await _client.GetAsync($"api/get-all-priceslabs");
//                _logger.LogInformation("Get all api price slab definitions call stop.");

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        return JsonConvert.DeserializeObject<IEnumerable<PriceSlabDefinitionDTO>>(apiResponse.Result.ToString());
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
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

//            return null;
//        }

//        public async Task<IEnumerable<OrgPriceSlabDTO>> GetAllOrgPriceSlabDefinitionsAsync()
//        {
//            try
//            {
//                _logger.LogInformation("Get all api Organization price slab definitions call start.");
//                HttpResponseMessage response = await _client.GetAsync($"api/get-all-org-price-slabs");
//                _logger.LogInformation("Get all api Organization price slab definitions call stop.");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        return JsonConvert.DeserializeObject<IEnumerable<OrgPriceSlabDTO>>(apiResponse.Result.ToString());
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
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

//            return null;
//        }

//        public async Task<IList<PriceSlabDefinitionDTO>> GetPriceSlabDefinitionAsync(int serviceId, string stakeholder)
//        {
//            try
//            {
//                _client.DefaultRequestHeaders.Add("DeviceId", "WEB");
//                _client.DefaultRequestHeaders.Add("appVersion", "WEB");

//                _logger.LogInformation("Get all api price slab definitions details call start.");
//                HttpResponseMessage response = await _client.GetAsync($"api/get-price-slab?serviceId={serviceId}&stakeHolder={stakeholder}");
//                _logger.LogInformation("Get all api price slab definitions call stop.");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
//                        return JsonConvert.DeserializeObject<IList<PriceSlabDefinitionDTO>>(result["pricingSlabDefinitionsList"].ToString());
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
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

//            return null;
//        }

//        public async Task<IList<OrgPriceSlabDTO>> GetOrgPriceSlabDefinitionAsync(int serviceId, string organizationUid)
//        {
//            try
//            {
//                _logger.LogInformation("Get all api Organization price slab definitions details call start.");
//                HttpResponseMessage response = await _client.GetAsync($"api/get-org-priceslab?orgId={organizationUid}&serviceId={serviceId}");
//                _logger.LogInformation("Get all api Organization price slab definitions details call stop.");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        return JsonConvert.DeserializeObject<IList<OrgPriceSlabDTO>>(apiResponse.Result.ToString());
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
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

//            return null;
//        }
//    }
//}
