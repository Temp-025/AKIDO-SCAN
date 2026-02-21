//using EnterpriseGatewayPortal.Core.Domain.Models;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class BalanceSheetsService : IBalanceSheetsService
//    {
//        private readonly HttpClient _client;
//        private readonly ILogger<BalanceSheetsService> _logger;
//        private readonly IConfiguration _configuration;

//        public BalanceSheetsService(HttpClient httpClient, IConfiguration configuration, ILogger<BalanceSheetsService> logger)
//        {
//            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:PriceModelServiceBaseAddress"]);

//            _logger = logger;
//            _client = httpClient;
//            _configuration = configuration;

//        }
//        public async Task<ServiceResult> GetBalanceSheetDetailsAsync(string orgId)

//        {
//            try
//            {
//                HttpResponseMessage response = await _client.GetAsync($"api/get-bal-sheet-org?orgId={orgId}");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var balanceSheet = JsonConvert.DeserializeObject<IEnumerable<OrganizationBalanceSheetDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, balanceSheet);
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
