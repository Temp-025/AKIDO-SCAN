//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class VerificationRequestService : IVerificationRequestsService
//    {
//        private readonly ILogger<DocumentIssuerService> _logger;
//        private readonly IConfiguration _configuration;
//        private readonly HttpClient _client;

//        public VerificationRequestService(HttpClient httpClient,
//            IConfiguration configuration,
//            ILogger<DocumentIssuerService> logger)
//        {
//            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:DocumentVerificationServiceBaseAddress"]);
//            _client = httpClient;
//            _logger = logger;
//            _configuration = configuration;

//        }
//        public async Task<IEnumerable<VerificationDetailDTO>> GetVerificationRequestListAsync(string orgId, string email)
//        {
//            try
//            {

//                HttpResponseMessage response = await _client.GetAsync($"api/verificationdetail/getverificationrequestlist?orgId={orgId}&email={email}");

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return JsonConvert.DeserializeObject<IEnumerable<VerificationDetailDTO>>(apiResponse.Result.ToString());
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<VerificationDetailDTO> GetDocverifyRequestDetailsByIdAsync(int docVerificationId)
//        {
//            try
//            {
//                _logger.LogInformation("Get Document details by id api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/verificationdetail/getverificationdetailbyid?id={docVerificationId}");
//                _logger.LogInformation("Get Document details id api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return JsonConvert.DeserializeObject<VerificationDetailDTO>(apiResponse.Result.ToString());



//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);

//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<ServiceResult> Download(string fileId)
//        {

//                try
//                {


//                    HttpResponseMessage response = await _client.GetAsync($"api/verificationdetail/downloadfile?fileId={fileId}");
//                    if (response.StatusCode == HttpStatusCode.OK)
//                    {
//                        APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                        if (apiResponse.Success)
//                        {
//                            return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
//                        }
//                        else
//                        {
//                            _logger.LogError(apiResponse.Message);
//                            return new ServiceResult(false, apiResponse.Message);
//                        }
//                    }
//                    else
//                    {
//                        _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}");
//                    }
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError(ex, ex.Message);
//                }

//                return new ServiceResult(false, "An error occurred while Downloading the file. Please try later.");

//        }

//        public async Task<ServiceResult> GetPreviewDocAsync(string id)
//        {

//            try
//            {
//                var response = await _client.GetAsync($"api/verificationdetail/downloaddoc/{id}");

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

//                    return new ServiceResult(true, "File downloaded successfully.", fileBytes);
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}");
//                    return new ServiceResult($"Failed to download the document. Status code: {response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("GetDocumentAsync Exception: {0}", ex.Message);
//                return new ServiceResult($"An error occurred while downloading the document. {ex.Message}");
//            }
//        }

//        public async Task<VerificationDetailDTO> GetDocverifyRequestDetailsByFileIdAsync(string fileid)
//        {
//            try
//            {
//                _logger.LogInformation("Get Document details by fileid api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/verificationdetail/getverificationdetailbyfileid/{fileid}");
//                _logger.LogInformation("Get Document details fileid api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return JsonConvert.DeserializeObject<VerificationDetailDTO>(apiResponse.Result.ToString());



//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);

//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<ServiceResult> StatusChangeToInProgress(int requestid)
//        {
//            try
//            {
//                _logger.LogInformation("Change status to InProgress api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/verificationdetail/updateverificationstatusinprogress/{requestid}");
//                _logger.LogInformation("Change status to InProgress api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return new ServiceResult(true, apiResponse.Message);



//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);

//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<ServiceResult> SignVerificationRequestAsync(VerificationRequestTrueCopySignDTO document)
//        {
//            try
//            {

//                string json = JsonConvert.SerializeObject(document,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = await _client.PostAsync("api/verificationdetail/verifytruecopyrequest", content);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return new ServiceResult(true, apiResponse.Message);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return new ServiceResult(false, "An error occurred while Signing Document. Please try later.");
//        }
//    }
//}
