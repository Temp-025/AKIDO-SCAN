namespace EnterpriseGatewayPortal.Core.Services
{
    //public class DocumentSigningService : IDocumentSigningService
    //{
    //    private readonly ILogger<OrganizationService> _logger;
    //    private readonly IConfiguration _configuration;

    //    public DocumentSigningService(
    //        IConfiguration configuration,
    //        ILogger<OrganizationService> logger)
    //    {

    //        _logger = logger;
    //        _configuration = configuration;

    //    }
    //    public async Task<ServiceResult> GetDocumentDataMyListAsync(string token)
    //    {
    //        _logger.LogInformation("GetDocumentDataMyListAsync");


    //        var signingUrl = _configuration["SigningPortalUrl"];

    //        HttpClient _client = new HttpClient();

    //        _client.Timeout = TimeSpan.FromMinutes(10);

    //        _client.DefaultRequestHeaders.Add("x-access-token", token);

    //        ServiceResult serviceResult = new ServiceResult(null);
    //        try
    //        {
    //            string relativePath = "api/documents/getdraftdocumentlist";

    //            Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

    //            string url = $"{fullUrl}";

    //            var response = _client.GetAsync(url).Result;
    //            if (response.StatusCode == HttpStatusCode.OK)
    //            {
    //                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
    //                if (apiResponse.Success)
    //                {
    //                    JArray jArray = JArray.FromObject(apiResponse.Result);

    //                    var jsonResponseList = JsonConvert.DeserializeObject<List<DocumentSigningMyListDTO>>(jArray.ToString());
    //                    return new ServiceResult(true, apiResponse.Message, jsonResponseList);
    //                }
    //                else
    //                {
    //                    _logger.LogError(apiResponse.Message);
    //                    return new ServiceResult(false, apiResponse.Message);
    //                }
    //            }
    //            else
    //            {
    //                _logger.LogError(response.StatusCode.ToString());
    //                return new ServiceResult("Internal Error");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex.Message);
    //            return new ServiceResult(ex.Message);
    //        }
    //    }
    //}
}
