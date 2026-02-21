namespace EnterpriseGatewayPortal.Core.Services
{

    //public class PaymentHistoryService : IPaymentHistoryService
    //{
    //private readonly HttpClient _client;
    //private readonly ILogger<PaymentHistoryService> _logger;
    //private readonly IConfiguration _configuration;
    //public PaymentHistoryService(HttpClient httpClient, IConfiguration configuration, ILogger<PaymentHistoryService> logger)
    //{
    //    httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:PriceModelServiceBaseAddress"]);

    //    _logger = logger;
    //    _client = httpClient;
    //    _configuration = configuration;
    //}

    //public async Task<IEnumerable<OrganizationPaymentHistoryDTO>> GetOrganizationPaymentHistoryAsync(string orgUid)
    //{

    //    try
    //    {
    //        HttpResponseMessage response = await _client.GetAsync($"api/get/payment-history?orgUID={orgUid}");
    //        if (response.StatusCode == HttpStatusCode.OK)
    //        {
    //            APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //            if (apiResponse.Success)
    //            {
    //                var paymentHistory = JsonConvert.DeserializeObject<IEnumerable<OrganizationPaymentHistoryDTO>>(apiResponse.Result.ToString());
    //                return paymentHistory;
    //            }
    //            else
    //            {
    //                _logger.LogError(apiResponse.Message);
    //            }
    //        }
    //        else
    //        {
    //            _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
    //                   $"with status code={response.StatusCode}");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, ex.Message);
    //    }
    //    return null;
    //}

    //public async Task<IEnumerable<ServiceDefinitionDTO>> GetServiceDefinitionsAsync()
    //{
    //    try
    //    {
    //        HttpResponseMessage response = await _client.GetAsync($"api/get-all-services");
    //        if (response.StatusCode == HttpStatusCode.OK)
    //        {
    //            APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //            if (apiResponse.Success)
    //            {
    //                var serviceDefinitions = JsonConvert.DeserializeObject<IEnumerable<ServiceDefinitionDTO>>(apiResponse.Result.ToString());
    //                return serviceDefinitions.Where(x => x.Status.ToLower() == "active");
    //            }
    //            else
    //            {
    //                _logger.LogError(apiResponse.Message);
    //            }
    //        }
    //        else
    //        {
    //            _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
    //                       $"with status code={response.StatusCode}");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, ex.Message);
    //    }

    //    return null;
    //}

    //public async Task<ServiceResult> GetPaymentInfoByTransactionRefId(string Id)
    //{
    //    try
    //    {

    //        _logger.LogInformation("Get Payment Info by Transaction ack Id api call start");
    //        HttpResponseMessage response = await _client.GetAsync($"api/get/payment-record-by-transaction_reference_id/{Id}");
    //        _logger.LogInformation("Get Payment Info by Transaction ack Id api call end");
    //        if (response.StatusCode == HttpStatusCode.OK)
    //        {
    //            APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //            if (apiResponse.Success)
    //            {
    //                _logger.LogInformation(apiResponse.Message);
    //                var result = JsonConvert.DeserializeObject<IEnumerable<OrganizationPaymentHistoryDTO>>(apiResponse.Result.ToString());
    //                return new ServiceResult(true, apiResponse.Message, result);
    //            }
    //            else
    //            {
    //                _logger.LogError(apiResponse.Message);
    //                return new ServiceResult(false, apiResponse.Message);

    //            }
    //        }
    //        else
    //        {
    //            _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
    //                   $"with status code={response.StatusCode}");
    //            return new ServiceResult("Internal Error");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, ex.Message);
    //        return new ServiceResult(ex.Message);
    //    }

    //}

    //}
}
