using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class UsageReportsService : IUsageReportsService
    {
        private readonly HttpClient _client;
        private readonly ILogger<UsageReportsService> _logger;
        private readonly IConfiguration _configuration;
        public UsageReportsService(HttpClient httpClient, IConfiguration configuration, ILogger<UsageReportsService> logger)
        {
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:PriceModelServiceBaseAddress"]!);

            _logger = logger;
            _client = httpClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<OrganizationUsageReportDTO>> GetOrganizationUsageReports(string organizationUid, string year)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get-org-usage-report?orgId={organizationUid}&year={year}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<OrganizationUsageReportDTO>>(apiResponse.Result!.ToString()!);
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

        public async Task<string> DownloadUsageReport(int reportId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/download-pdf?id={reportId}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return Convert.ToString(apiResponse.Result);
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

        public async Task<ServiceResult> DownloadCurrentMonthUsageReport(string organizationUid)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get-report/{organizationUid}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message, Convert.ToString(apiResponse.Result)!);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message, Convert.ToString(apiResponse.Result)!);
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

            return new ServiceResult(false, "Failed to download the report");
        }
    }
}
