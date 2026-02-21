using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Core.Exceptions;
using EnterpriseGatewayPortal.Core.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class AdminLogReportsService : IAdminLogReportsService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdminLogReportsService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminLogReportsService(IConfiguration configuration,
            ILogger<AdminLogReportsService> logger,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PaginatedList<AdminLogReportDTO>> GetAdminLogReportAsync(string startDate, string endDate, string userName = null,
            string moduleName = null, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");

            if (String.IsNullOrEmpty(_configuration["APIServiceLocations:AdminLogServiceBaseAddress"]))
            {
                throw new Exception("Admin Log Service Base Address is null.");
            }

            client.BaseAddress = new Uri(_configuration["APIServiceLocations:AdminLogServiceBaseAddress"]!);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        UserName = userName,
                        ModuleName = moduleName,
                        StartDate = startDate,
                        EndDate = endDate,
                        PerPage = perPage
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/{page}", content))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream stream = await response.Content.ReadAsStreamAsync())
                        {
                            using (StreamReader streamReader = new StreamReader(stream))
                            {
                                using (JsonReader reader = new JsonTextReader(streamReader))
                                {
                                    JsonSerializer serializer = new JsonSerializer();
                                    APIResponse apiResponse = serializer.Deserialize<APIResponse>(reader);
                                    if (apiResponse.Success)
                                    {
                                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                                        var logs = JsonConvert.DeserializeObject<IEnumerable<AdminLogReportDTO>>(Convert.ToString(result["data"])!);
                                        return new PaginatedList<AdminLogReportDTO>(logs!, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
                                    }
                                    else
                                    {
                                        _logger.LogError(apiResponse.Message);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                                   $"with status code={response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }
        public ServiceResult VerifyChecksum(object logReport)
        {
            if (logReport == null)
                throw new ArgumentNullException(nameof(logReport));

            try
            {
                var result = PKIMethods.Instance.VerifyChecksum(
                     JsonConvert.SerializeObject(logReport,
                     new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
                if (result == true)
                    return new ServiceResult(true, "Integrity check verified successfully");
                else
                    return new ServiceResult(false, "Failed to verify Integrity check");
            }
            catch (PKIException ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while verifying the checksum");
        }
    }
}

