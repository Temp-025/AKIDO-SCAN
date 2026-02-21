using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ILogger<StatisticsService> _logger;
        private readonly ILocalClientService _localClientService;
        private readonly HttpClient _client;
        public StatisticsService(ILogger<StatisticsService> logger, IConfiguration configuration,
            HttpClient httpClient,
            ILocalClientService localClientService)
        {
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:StatisticsServiceBaseAddress"]!);
            _logger = logger;
            httpClient.Timeout = TimeSpan.FromSeconds(30);
            _client = httpClient;
            _localClientService = localClientService;
        }
        public async Task<GraphDTO> GetOranizationGraphCountAsync()
        {
            var clientList = await _localClientService.ListClientAsync();

            List<string> clientIdList = new List<string>();

            foreach (var client in clientList)
            {
                clientIdList.Add(client.ClientId);
            }

            string commaSeparatedClientId = string.Join(",", clientIdList);
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"central-log/api/statistics/list/{commaSeparatedClientId}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<GraphDTO>(apiResponse.Result!.ToString()!);
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
        public async Task<GraphDTO> GetGraphCountAsync(string serviceProviderName)
        {

            var serviceProviderClient = await _localClientService.GetClientByAppNameAsync(serviceProviderName);
            if (serviceProviderClient == null)
            {
                return null;
            }
            var serviceProviderClientId = serviceProviderClient.ClientId;
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"central-log/api/statistics/{serviceProviderClientId}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<GraphDTO>(apiResponse.Result!.ToString()!);
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
        public async Task<IEnumerable<AdminActivity>> GetAdminLogReportAsync(int page = 1)
        {

            _client.Timeout = TimeSpan.FromSeconds(30);

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"enterprise-logs/api/audit-logs/{page}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject logsArray = (JObject)JToken.FromObject(apiResponse.Result);
                        return JsonConvert.DeserializeObject<IEnumerable<AdminActivity>>(logsArray["logs"].ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }
    }
}
