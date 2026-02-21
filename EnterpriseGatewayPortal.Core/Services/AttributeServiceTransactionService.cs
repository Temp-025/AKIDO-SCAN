namespace EnterpriseGatewayPortal.Core.Services
{
    //public class AttributeServiceTransactionService : IAttributeServiceTransactionsService
    //{
    //    private readonly HttpClient _client;
    //    private readonly ILogger<DataPivotService> _logger;
    //    private readonly IConfiguration _configuration;
    //    public AttributeServiceTransactionService(HttpClient httpClient, IConfiguration configuration, ILogger<DataPivotService> logger)
    //    {
    //        httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:MultiPivotServiceBaseAddress"]);
    //        _logger = logger;
    //        _client = httpClient;
    //    }
    //    public async Task<IEnumerable<AttributeServiceTransactionListDTO>> GetTransactionsListAsync(string orgUid)
    //    {
    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/GetTransactionList/{orgUid}");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                   var transactions = JsonConvert.DeserializeObject<IEnumerable<AttributeServiceTransactionListDTO>>(apiResponse.Result.ToString());
    //                    return transactions;
    //                }
    //                else
    //                {
    //                    _logger.LogError(apiResponse.Message);
    //                }
    //            }
    //            else
    //            {
    //                _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
    //                       $"with status code={response.StatusCode}");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, ex.Message);
    //        }
    //        return null;
    //    }

    //    public async Task<AttributeServiceTransactionsDTO> GetTransactionsByIdAsync(int id)
    //    {
    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/GetTransactionDetails/{id}");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    var transactions = JsonConvert.DeserializeObject<AttributeServiceTransactionsDTO>(apiResponse.Result.ToString());
    //                    return transactions;
    //                }
    //                else
    //                {
    //                    _logger.LogError(apiResponse.Message);
    //                }
    //            }
    //            else
    //            {
    //                _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
    //                       $"with status code={response.StatusCode}");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, ex.Message);
    //        }
    //        return null;
    //    }
    //}
}
