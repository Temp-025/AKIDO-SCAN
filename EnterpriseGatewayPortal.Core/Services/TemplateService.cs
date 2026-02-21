namespace EnterpriseGatewayPortal.Core.Services
{
    //public class TemplateService : ITemplateService
    //{
    //    private readonly HttpClient _client;
    //    private readonly IConfiguration _configuration;
    //    private readonly ILocalBusinessUsersService _localBusinessUsersService;
    //    private readonly ILogger<TemplateService> _logger;
    //    public TemplateService(HttpClient httpClient,
    //        IConfiguration configuration,
    //        ILocalBusinessUsersService localBusinessUsersService,
    //        ILogger<TemplateService> logger)
    //    {

    //        httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:OrganizationOnboardingServiceBaseAddress"]);
    //        _client = httpClient;
    //        _configuration = configuration;
    //        _client.Timeout = TimeSpan.FromMinutes(10);
    //        _logger = logger;
    //        _localBusinessUsersService = localBusinessUsersService;
    //    }
    //    public async Task<IList<SignatureTemplatesDTO>> GetSignatureTemplateListAsync()
    //    {
    //        try
    //        {
    //            string url = "api/get/all/templates";
    //            HttpResponseMessage response = await _client.GetAsync(url);
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    var result = JsonConvert.DeserializeObject<IList<SignatureTemplatesDTO>>(apiResponse.Result.ToString());
    //                    return result;
    //                }
    //                else
    //                {
    //                    _logger.LogError(apiResponse.Message);
    //                    return null;
    //                }
    //            }
    //            else
    //            {
    //                _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
    //                   $"with status code={response.StatusCode}");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }

    //        return null;
    //    }
    //    public async Task<OrganizationTemplatesDTO> GetOrganizationTemplates(string organizationUid)
    //    {
    //        try
    //        {
    //            var organizationid = organizationUid;
    //            string url = $"api/get/signature-templates/by/id/{organizationid}";
    //            HttpResponseMessage response = await _client.GetAsync(url);
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    var result = JsonConvert.DeserializeObject<OrganizationTemplatesDTO>(apiResponse.Result.ToString());
    //                    return result;
    //                }
    //                else
    //                {
    //                    _logger.LogError(apiResponse.Message);
    //                    return null;
    //                }
    //            }
    //            else
    //            {
    //                _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
    //                $"with status code={response.StatusCode}");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, ex.Message);
    //            return null;
    //        }

    //        return null;
    //    }
    //    public async Task<APIResponse> UpdateOrganizationTemplates(OrganizationTemplatesDTO model)
    //    {
    //        try
    //        {
    //            string url = "api/post/update/siganture-templates/by/id";
    //            string json = JsonConvert.SerializeObject(model);

    //            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

    //            var response = _client.PostAsync(url, content).Result;

    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    return apiResponse;
    //                }
    //                else
    //                {
    //                    _logger.LogError(apiResponse.Message);
    //                    return apiResponse;
    //                }
    //            }
    //            else
    //            {
    //                _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
    //                $"with status code={response.StatusCode}");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, ex.Message);
    //            return null;
    //        }

    //        return null;
    //    }

    //    public async Task<ServiceResult> IsValid(UpdateTemplateDTO model, string orgUID)
    //    {
    //        var organizationUid = orgUID;
    //        var businessUserList = await _localBusinessUsersService.GetAllBusinessUsersByOrgUidAsync(organizationUid);
    //        var businessUser = (IEnumerable<OrgSubscriberEmail>)businessUserList.Resource;

    //        if (model.SignatureTemplate == 2 || model.SignatureTemplate == 4)
    //        {
    //            foreach (var item in businessUser)
    //            {
    //                if (string.IsNullOrEmpty(item.Designation))
    //                {
    //                    return new ServiceResult("some of the business users does not have designation");
    //                }
    //            }
    //        }
    //        if (model.SignatureTemplate == 3 || model.SignatureTemplate == 4)
    //        {
    //            foreach (var item in businessUser)
    //            {
    //                if (string.IsNullOrEmpty(item.SignaturePhoto))
    //                {
    //                    return new ServiceResult("some of the business users does not have handwritten signature");
    //                }
    //            }
    //        }
    //        return new ServiceResult(true, "success", null);
    //    }

    //    public async Task<ServiceResult> CheckOrgUserWithSignatureTemplate(GetTemplateDTO orgUser)
    //    {
    //        try
    //        {

    //            _logger.LogInformation("CheckOrgUserWithSignatureTemplate api call start");
    //            string url = "api/get/user/template/details";

    //            string json = JsonConvert.SerializeObject(
    //                orgUser, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
    //            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


    //            var response = _client.PostAsync(url, content).Result;

    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
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
    //                _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
    //                $"with status code={response.StatusCode}");
    //                return new ServiceResult(false, "Error while checking");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, ex.Message);
    //            return new ServiceResult(false, ex.Message);
    //        }


    //    }

    //    public async Task<ServiceResult> GetSignaturePreviewAsync(UserDTO user)
    //    {
    //        try
    //        {
    //            HttpClient client = new HttpClient();
    //            _logger.LogInformation("GetSignaturePreviewAsync start");
    //            client.BaseAddress = new Uri(_configuration["Config:SignatureTemplates"]);

    //            JObject keyValuePairs = new JObject();
    //            keyValuePairs.Add("sUid", user.Suid);
    //            keyValuePairs.Add("oUid", user.OrganizationId);
    //            keyValuePairs.Add("accountType", user.AccountType.ToLower());

    //            string json = JsonConvert.SerializeObject(keyValuePairs,
    //                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
    //            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

    //            _logger.LogInformation("GetSignaturePreviewAsync api call start");
    //            HttpResponseMessage response = await client.PostAsync($"api/digital/signature/post/preview", content);
    //            _logger.LogInformation("GetSignaturePreviewAsync api call end");

    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    _logger.LogInformation("GetSignaturePreviewAsync end");
    //                    return new ServiceResult(true, "Successfully received signature preview image", apiResponse.Result.ToString().Replace("\r\n", ""));
    //                }
    //                else
    //                {
    //                    _logger.LogError(apiResponse.Message);
    //                    _logger.LogInformation("GetSignaturePreviewAsync end");
    //                    return new ServiceResult(apiResponse.Message);
    //                }
    //            }
    //            else
    //            {
    //                _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
    //                   $"with status code={response.StatusCode}");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, ex.Message);
    //        }

    //        _logger.LogInformation("GetSignaturePreviewAsync end");
    //        return new ServiceResult("Failed to receive signature preview image");
    //    }
    //}
}
