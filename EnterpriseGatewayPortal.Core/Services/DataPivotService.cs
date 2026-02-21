namespace EnterpriseGatewayPortal.Core.Services
{
    //public class DataPivotService : IDataPivotService
    //{
    //    private readonly HttpClient _client;
    //    private readonly ILogger<DataPivotService> _logger;
    //    private readonly IConfiguration _configuration;
    //    public DataPivotService(HttpClient httpClient, IConfiguration configuration, ILogger<DataPivotService> logger)
    //    {
    //        httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:MultiPivotServiceBaseAddress"]);
    //        _logger = logger;
    //        _client = httpClient;
    //    }

    //    public async Task<IEnumerable<DataPivot>> GetDataPivotListAsync(string orgUid)
    //    {

    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/GetDataPivotList/{orgUid}");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    var pivots = JsonConvert.DeserializeObject<IEnumerable<DataPivot>>(apiResponse.Result.ToString());
    //                    return pivots;
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

    //    public async Task<IEnumerable<Scope>> GetProfileScopeList()
    //    {

    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/GetProfileList");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    var scopes = JsonConvert.DeserializeObject<IEnumerable<Scope>>(apiResponse.Result.ToString());
    //                    return scopes;
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

    //    public async Task<IEnumerable<AuthScheme>> GetAuthSchemesList()
    //    {

    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/GetAuthSchemeList");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    var scopes = JsonConvert.DeserializeObject<IEnumerable<AuthScheme>>(apiResponse.Result.ToString());
    //                    return scopes;
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

    //    public async Task<ServiceResult> CreatePivot(DataPivot dataPivot)
    //    {
    //        try
    //        {

    //            _logger.LogInformation("Create Data Pivot api call start");
    //            string jsonPayload = JsonConvert.SerializeObject(dataPivot,
    //                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

    //            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    //            HttpResponseMessage response = await _client.PostAsync($"api/MultiPivot/AddDataPivot", content);

    //            _logger.LogInformation("Create Data Pivot api call end");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    _logger.LogInformation(apiResponse.Message);

    //                    return new ServiceResult(true, apiResponse.Message);
    //                }
    //                else
    //                {
    //                    _logger.LogError(apiResponse.Message);
    //                    return new ServiceResult(false, apiResponse.Message);

    //                }
    //            }
    //            else
    //            {
    //                _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
    //                       $"with status code={response.StatusCode}");
    //                return new ServiceResult("Internal Error");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, ex.Message);
    //            return new ServiceResult(ex.Message);
    //        }

    //    }

    //    public async Task<ServiceResult> UpdatePivotDataAsync(DataPivot dataPivot)
    //    {
    //        try
    //        {

    //            _logger.LogInformation("update Data Pivot api call start");
    //            string jsonPayload = JsonConvert.SerializeObject(dataPivot,
    //                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

    //            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    //            HttpResponseMessage response = await _client.PostAsync($"api/MultiPivot/UpdateDataPivot", content);

    //            _logger.LogInformation("update Data Pivot api call end");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    _logger.LogInformation(apiResponse.Message);

    //                    return new ServiceResult(true, apiResponse.Message);
    //                }
    //                else
    //                {
    //                    _logger.LogError(apiResponse.Message);
    //                    return new ServiceResult(false, apiResponse.Message);

    //                }
    //            }
    //            else
    //            {
    //                _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
    //                       $"with status code={response.StatusCode}");
    //                return new ServiceResult("Internal Error");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, ex.Message);
    //            return new ServiceResult(ex.Message);
    //        }

    //    }
    //    public async Task<ServiceResult> DeletePivotDataAsync(int id)
    //    {
    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/DeleteDataPivot/{id}");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    _logger.LogInformation(apiResponse.Message);

    //                    return new ServiceResult(true, apiResponse.Message);
    //                }
    //                else
    //                {
    //                    _logger.LogError(apiResponse.Message);
    //                    return new ServiceResult(false, apiResponse.Message);
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

    //    public async Task<DataPivot> GetDataPivotById(int id)
    //    {
    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/EditDataPivot/{id}");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    var datapivot = JsonConvert.DeserializeObject<DataPivot>(apiResponse.Result.ToString());
    //                    return datapivot;
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
