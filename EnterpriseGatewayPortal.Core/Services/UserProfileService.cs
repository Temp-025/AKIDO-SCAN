using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ILogger<UserProfileService> _logger;
        private IConfiguration _configuration;
        public UserProfileService(ILogger<UserProfileService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<ServiceResult> GetSocialBenefitCardDetails
            (string userId)
        {
            try
            {
                HttpClient _client = new HttpClient();

                var url = _configuration["BeneficiaryCardUrl"] + userId;

                HttpResponseMessage result;

                result = await _client.GetAsync(url);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }
                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }
                var Data = new Dictionary<string, object>();
                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(apiResponse.Result));

                foreach (var property in jsonObject.Properties())
                {
                    Data[property.Name] = property.Value.Type switch
                    {
                        JTokenType.Object => property.Value.ToObject<Dictionary<string, object>>(),
                        JTokenType.Array => property.Value.ToObject<List<object>>(),
                        _ => property.Value.ToObject<object>()
                    };
                }
                return new ServiceResult(true, apiResponse.Message, Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetMdlProfile(string userId)
        {
            try
            {
                HttpClient _client = new HttpClient();

                var url = _configuration["MdlUrl"] + userId;

                HttpResponseMessage result;

                result = await _client.GetAsync(url);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }
                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }
                var Data = new Dictionary<string, object>();
                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(apiResponse.Result));

                foreach (var property in jsonObject.Properties())
                {
                    Data[property.Name] = property.Value.Type switch
                    {
                        JTokenType.Object => property.Value.ToObject<Dictionary<string, object>>(),
                        JTokenType.Array => property.Value.ToObject<List<object>>(),
                        _ => property.Value.ToObject<object>()
                    };
                }
                return new ServiceResult(true, apiResponse.Message, Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetPidProfile(string userId)
        {
            try
            {
                HttpClient _client = new HttpClient();

                var url = _configuration["PidUrl"] + userId;

                HttpResponseMessage result;

                result = await _client.GetAsync(url);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }
                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }
                var Data = new Dictionary<string, object>();

                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(apiResponse.Result));

                foreach (var property in jsonObject.Properties())
                {
                    Data[property.Name] = property.Value.Type switch
                    {
                        JTokenType.Object => property.Value.ToObject<Dictionary<string, object>>(),
                        JTokenType.Array => property.Value.ToObject<List<object>>(),
                        _ => property.Value.ToObject<object>()
                    };
                }
                return new ServiceResult(true, apiResponse.Message, Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetPOAProfile(string userId)
        {
            try
            {
                HttpClient _client = new HttpClient();

                var url = _configuration["PoaUrl"] + userId;

                HttpResponseMessage result;

                result = await _client.GetAsync(url);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }
                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }
                var Data = new Dictionary<string, object>();

                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(apiResponse.Result));

                foreach (var property in jsonObject.Properties())
                {
                    Data[property.Name] = property.Value.Type switch
                    {
                        JTokenType.Object => property.Value.ToObject<Dictionary<string, object>>(),
                        JTokenType.Array => property.Value.ToObject<List<object>>(),
                        _ => property.Value.ToObject<object>()
                    };
                }
                return new ServiceResult(true, apiResponse.Message, Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetPOAProfileRequest(string userId)
        {
            try
            {
                HttpClient _client = new HttpClient();

                var url = _configuration["PoaRequestUrl"] + userId;

                HttpResponseMessage result;

                result = await _client.GetAsync(url);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }
                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }
                var Data = new Dictionary<string, object>();

                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(apiResponse.Result));

                foreach (var property in jsonObject.Properties())
                {
                    Data[property.Name] = property.Value.Type switch
                    {
                        JTokenType.Object => property.Value.ToObject<Dictionary<string, object>>(),
                        JTokenType.Array => property.Value.ToObject<List<object>>(),
                        _ => property.Value.ToObject<object>()
                    };
                }
                return new ServiceResult(true, apiResponse.Message, Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetUaeidProfile(string userId)
        {
            try
            {
                HttpClient _client = new HttpClient();

                var url = _configuration["UaeIdUrl"] + userId;

                HttpResponseMessage result;

                result = await _client.GetAsync(url);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }
                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }
                var Data = new Dictionary<string, object>();

                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(apiResponse.Result));

                foreach (var property in jsonObject.Properties())
                {
                    Data[property.Name] = property.Value.Type switch
                    {
                        JTokenType.Object => property.Value.ToObject<Dictionary<string, object>>(),
                        JTokenType.Array => property.Value.ToObject<List<object>>(),
                        _ => property.Value.ToObject<object>()
                    };
                }
                return new ServiceResult(true, apiResponse.Message, Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetEmiratesIdProfile(string userId)
        {
            try
            {
                HttpClient _client = new HttpClient();

                var url = _configuration["EmiratesIdUrl"] + userId;

                HttpResponseMessage result;

                result = await _client.GetAsync(url);

                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }

                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }

                var Data = new Dictionary<string, object>();

                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(apiResponse.Result));

                foreach (var property in jsonObject.Properties())
                {
                    Data[property.Name] = property.Value.Type switch
                    {
                        JTokenType.Object => property.Value.ToObject<Dictionary<string, object>>(),
                        JTokenType.Array => property.Value.ToObject<List<object>>(),
                        _ => property.Value.ToObject<object>()
                    };
                }

                return new ServiceResult(true, apiResponse.Message, Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetPassportProfile(string userId)
        {
            try
            {
                HttpClient _client = new HttpClient();
                var url = _configuration["PassportUrl"] + userId;

                HttpResponseMessage result;

                result = await _client.GetAsync(url);

                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }

                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }

                var Data = new Dictionary<string, object>();

                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(apiResponse.Result));

                foreach (var property in jsonObject.Properties())
                {
                    Data[property.Name] = property.Value.Type switch
                    {
                        JTokenType.Object => property.Value.ToObject<Dictionary<string, object>>(),
                        JTokenType.Array => property.Value.ToObject<List<object>>(),
                        _ => property.Value.ToObject<object>()
                    };
                }

                return new ServiceResult(true, apiResponse.Message, Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetUaeidAuthProfile(string userId)
        {
            try
            {
                HttpClient _client = new HttpClient();
                var url = _configuration["UaeIdAuthUrl"] + userId;
                HttpResponseMessage result;
                result = await _client.GetAsync(url);

                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }

                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }

                var Data = new Dictionary<string, object>();

                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(apiResponse.Result));

                foreach (var property in jsonObject.Properties())
                {
                    Data[property.Name] = property.Value.Type switch
                    {
                        JTokenType.Object => property.Value.ToObject<Dictionary<string, object>>(),
                        JTokenType.Array => property.Value.ToObject<List<object>>(),
                        _ => property.Value.ToObject<object>()
                    };
                }

                return new ServiceResult(true, apiResponse.Message, Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }
    }

}