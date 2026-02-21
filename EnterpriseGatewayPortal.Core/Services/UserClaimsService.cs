namespace EnterpriseGatewayPortal.Core.Services
{
    //public class UserClaimsService : IUserClaimsService
    //{
    //    private readonly HttpClient _client;
    //    private readonly ILogger<DataPivotService> _logger;
    //    public UserClaimsService(HttpClient httpClient, IConfiguration configuration, ILogger<DataPivotService> logger)
    //    {
    //        httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:MultiPivotServiceBaseAddress"]);
    //        _logger = logger;
    //        _client = httpClient;
    //    }


    //    public async Task<IEnumerable<UserClaimsDTO>> GetUserClaimsListAsync()
    //    {
    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/GetAttributeList");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    var purpose = JsonConvert.DeserializeObject<IEnumerable<UserClaimsDTO>>(apiResponse.Result.ToString());
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

    //    public async Task<ServiceResult> CreateUserClaimsAsync(UserClaimsDTO claims)
    //    {
    //        try
    //        {

    //            _logger.LogInformation("Create claims api call start");
    //            string jsonPayload = JsonConvert.SerializeObject(claims,
    //                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

    //            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    //            HttpResponseMessage response = await _client.PostAsync($"api/MultiPivot/AddAttribute", content);

    //            _logger.LogInformation("Create claims api call end");
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

    //    public async Task<ServiceResult> DeleteUserClaimsAsync(int id)
    //    {
    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/DeleteAttribute/{id}");
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

    //    public async Task<UserClaimsDTO> GetUserClaimsByIdAsync(int id)
    //    {
    //        try
    //        {
    //            HttpResponseMessage response = await _client.GetAsync($"api/MultiPivot/EditAttribute/{id}");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    var purpose = JsonConvert.DeserializeObject<UserClaimsDTO>(apiResponse.Result.ToString());
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

    //    public async Task<ServiceResult> UpdateUserClaimsDataAsync(UserClaimsDTO claims)
    //    {
    //        try
    //        {

    //            _logger.LogInformation("Create claims api call start");
    //            string jsonPayload = JsonConvert.SerializeObject(claims,
    //                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

    //            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    //            HttpResponseMessage response = await _client.PostAsync($"api/MultiPivot/UpdateAttribute", content);

    //            _logger.LogInformation("Create claims api call end");
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
    //}
}
