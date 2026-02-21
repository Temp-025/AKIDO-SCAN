namespace EnterpriseGatewayPortal.Core.Services
{
    //public class SubscriptionVerifyService : ISubscriptionVerifyService
    //{
    //    private readonly HttpClient _client;
    //    private readonly ILogger<SubscriptionVerifyService> _logger;
    //    private readonly IConfiguration _configuration;
    //    public SubscriptionVerifyService(HttpClient httpClient, IConfiguration configuration, ILogger<SubscriptionVerifyService> logger)
    //    {
    //        httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:DocumentVerificationServiceBaseAddress"]);

    //        _logger = logger;
    //        _client = httpClient;
    //        _configuration = configuration;
    //    }

    //     public async Task<ServiceResult> GetAllSubscriptionListAsync()
    //     {
    //         try
    //         {
    //             _logger.LogInformation("Get Subscription Issuer List api call start");
    //             HttpResponseMessage response = await _client.GetAsync($"api/subscription/getallsubscriptionlist");
    //             _logger.LogInformation("Get Subscription Issuer List api call end");
    //             if (response.StatusCode == HttpStatusCode.OK)
    //             {
    //                 APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                 if (apiResponse.Success)
    //                 {
    //                    _logger.LogInformation(apiResponse.Message);
    //                    var result = JsonConvert.DeserializeObject<IEnumerable<SubscriptionAllListDTO>>(apiResponse.Result.ToString());
    //                     return new ServiceResult(true, apiResponse.Message, result);
    //                 }
    //                 else
    //                 {
    //                     _logger.LogError(apiResponse.Message);
    //                    return new ServiceResult(false, apiResponse.Message);
    //                 }
    //             }
    //             else
    //             {
    //                 _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
    //                        $"with status code={response.StatusCode}");
    //             }
    //         }
    //         catch (Exception ex)
    //         {
    //             _logger.LogError(ex, ex.Message);
    //         }

    //         return null;
    //     }

    //    public async Task<ServiceResult> GetSubscriptionDetailByIdAsync(int id)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("Get Subscription Details by id api call start");
    //            HttpResponseMessage response = await _client.GetAsync($"api/subscription/getsubscriptionbyid?id={id}");
    //            _logger.LogInformation("Get Subscription Details by id api call end");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    _logger.LogInformation(apiResponse.Message);
    //                    var result = JsonConvert.DeserializeObject<SubscriptionVerifyListDTO>(apiResponse.Result.ToString());
    //                    return new ServiceResult(true, apiResponse.Message, result);
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

    //    public async Task<ServiceResult> GetAllSubscriptionListByIdAsync(string orgId)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("Get All Subscription Verification List by Org Id api call start");
    //            HttpResponseMessage response = await _client.GetAsync($"api/subscription/getsubscriptionlistbyid?issuerId={orgId}");
    //            _logger.LogInformation("Get All Subscription Verification List by Org Id api call end");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    _logger.LogInformation(apiResponse.Message);
    //                    var result = JsonConvert.DeserializeObject<IEnumerable<SubscriptionVerifyListDTO>>(apiResponse.Result.ToString());
    //                    return new ServiceResult(true, apiResponse.Message, result);
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

    //    public async Task<ServiceResult> GetAllSubscriptionByissuerIdAsync(string orgId)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("Get All Subscription Verification List by Org Id api call start");
    //            HttpResponseMessage response = await _client.GetAsync($"api/subscription/getsubscriptionbyissuerid?uid={orgId}");
    //            _logger.LogInformation("Get All Subscription Verification List by Org Id api call end");
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    _logger.LogInformation(apiResponse.Message);
    //                    var result = JsonConvert.DeserializeObject<SubscriptionVerifyListDTO>(apiResponse.Result.ToString());
    //                    return new ServiceResult(true, apiResponse.Message, result);
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

    //    public async Task<ServiceResult> SaveSubscriptionDetails(SubscriptionModelDTO subscriptionModelDTO)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("Save Subscription Details API call start");
    //            string jsonPayload = JsonConvert.SerializeObject(subscriptionModelDTO);

    //            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    //            HttpResponseMessage response = await _client.PostAsync($"api/subscription/savesubscription", content);

    //            _logger.LogInformation("Save Subscription Details API call end");
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
    //                          $"with status code={response.StatusCode}");
    //                return new ServiceResult("Internal Error");

    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, ex.Message);

    //        }
    //        return new ServiceResult(false, "An error occurred while Subscription. Please try later.");


    //    }

    //    public async Task<ServiceResult> UpdateSubscriptionDetails(SubscriptionUpdateDTO subscriptionUpdateDTO)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("Save Subscription Details API call start");
    //            string jsonPayload = JsonConvert.SerializeObject(subscriptionUpdateDTO);

    //            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    //            HttpResponseMessage response = await _client.PostAsync($"api/subscription/updatesubscription", content);

    //            _logger.LogInformation("Save Subscription Details API call end");
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
    //                          $"with status code={response.StatusCode}");
    //                return new ServiceResult("Internal Error");

    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, ex.Message);

    //        }
    //        return new ServiceResult(false, "An error occurred while Subscription. Please try later.");


    //    }

    //}
}
