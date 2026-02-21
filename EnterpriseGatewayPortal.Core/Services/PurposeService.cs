namespace EnterpriseGatewayPortal.Core.Services
{
    //public class PurposeService : IPurposeService
    //{
    //    private readonly HttpClient _client;
    //    private readonly ILogger<DataPivotService> _logger;
    //    private readonly IConfiguration _configuration;
    //    public PurposeService(HttpClient httpClient, IConfiguration configuration, ILogger<DataPivotService> logger)
    //    {
    //        _logger = logger;
    //        _client = httpClient;
    //    }
    //    public async Task<IEnumerable<PurposesDTO>> GetPurposesListAsync()
    //    {

    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"http://localhost:52373/api/MultiPivot/GetPurposeList");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    var purpose = JsonConvert.DeserializeObject<IEnumerable<PurposesDTO>>(apiResponse.Result.ToString());
    //                    return purpose;
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


    //    public async Task<ServiceResult> DeletePurposeAsync(int id)
    //    {
    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"http://localhost:52373/api/MultiPivot/DeletePurpose/{id}");
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


    //    public async Task<ServiceResult> UpdatePurposeDataAsync(PurposesDTO purposes)
    //    {
    //        try
    //        {

    //            _logger.LogInformation("Create Purpose api call start");
    //            string jsonPayload = JsonConvert.SerializeObject(purposes,
    //                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

    //            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    //            HttpResponseMessage response = await _client.PostAsync($"http://localhost:52373/api/MultiPivot/UpdatePurpose", content);

    //            _logger.LogInformation("Create Purpose api call end");
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

    //    public async Task<ServiceResult> CreatePurposeAsync(PurposesDTO purposes)
    //    {
    //        try
    //        {

    //            _logger.LogInformation("Create Purpose api call start");
    //            string jsonPayload = JsonConvert.SerializeObject(purposes,
    //                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

    //            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    //            HttpResponseMessage response = await _client.PostAsync($"http://localhost:52373/api/MultiPivot/AddPurpose", content);

    //            _logger.LogInformation("Create Purpose api call end");
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

    //    public async Task<PurposesDTO> GetPurposeByIdAsync(int id)
    //    {
    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"http://localhost:52373/api/MultiPivot/EditPurpose/{id}");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    var purpose = JsonConvert.DeserializeObject<PurposesDTO>(apiResponse.Result.ToString());
    //                    return purpose;
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
