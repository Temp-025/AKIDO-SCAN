//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System.Net;
//using System.Net.Http.Headers;
//using System.Text;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class DocumentVerifyIssuerService : IDocumentVerifyIssuerService
//    {
//        private readonly HttpClient _client;
//        private readonly ILogger<DocumentVerifyIssuerService> _logger;
//        private readonly IConfiguration _configuration;
//        public DocumentVerifyIssuerService(HttpClient httpClient, IConfiguration configuration, ILogger<DocumentVerifyIssuerService> logger)
//        {
//            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:DocumentVerificationServiceBaseAddress"]);

//            _logger = logger;
//            _client = httpClient;
//            _configuration = configuration;
//        }

//        public async Task<ServiceResult> GetAllDocumentListAsync()
//        {
//            try
//            {
//                _logger.LogInformation("Get Document Verification Issuer List api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/document/getalldocumentlist");
//                _logger.LogInformation("Get Document Verification Issuer List api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result = JsonConvert.DeserializeObject<IEnumerable<DocumentIssuerListDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, result);
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
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                return new ServiceResult(ex.Message);
//            }

//        }


//        public async Task<ServiceResult> GetVerificationDetailListBySuidAndOrgUidAsync(string orgId, string suid)
//        {
//            try
//            {
//                _logger.LogInformation("Get All Document Verification List api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/verificationdetail/getverificationdetaillistbysuid?orgId={orgId}&suid={suid}");
//                _logger.LogInformation("Get All Document Verification  List api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result = JsonConvert.DeserializeObject<IEnumerable<DocumentVerifyListDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, result);
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
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                return new ServiceResult(ex.Message);
//            }

//        }
//        public async Task<ServiceResult> SaveVerificationDetails(VerificationModelDTO verificationModelDto, IFormFile file)
//        {
//            try
//            {
//                _logger.LogInformation("Save Verification Details API call start");

//                var multipartContent = new MultipartFormDataContent();
//                using (var multipartFormContent = new MultipartFormDataContent())
//                {
//                    string json = JsonConvert.SerializeObject(verificationModelDto);

//                    StringContent content = new(json, Encoding.UTF8, "application/json");
//                    multipartFormContent.Add(content, "model.Model");

//                    StreamContent fileStreamContent = new StreamContent(file.OpenReadStream());
//                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

//                    //Add the file
//                    multipartFormContent.Add(fileStreamContent, name: "model.File", fileName: file.FileName);

//                    HttpResponseMessage response = await _client.PostAsync($"api/verificationdetail/saveverificationdetail", multipartFormContent);

//                    _logger.LogInformation("Save Verification Details API call end");

//                    if (response.StatusCode == HttpStatusCode.OK)
//                    {
//                        APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());

//                        if (apiResponse.Success)
//                        {
//                            _logger.LogInformation(apiResponse.Message);
//                            var result = JsonConvert.DeserializeObject<SaveDocumentVerifyDTO>(apiResponse.Result.ToString());

//                            return new ServiceResult(true, apiResponse.Message, result);
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
//                        return new ServiceResult("Internal Error");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }


//            return new ServiceResult(false, "An error occurred while Verifying the Document. Please try later.");
//        }



//        public async Task<ServiceResult> GetVerificationDetailByIdAsync(int id)
//        {
//            try
//            {
//                _logger.LogInformation("Get Document Verification List by id api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/verificationdetail/getverificationdetailbyid?id={id}");
//                _logger.LogInformation("Get Document Verification  List by id api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);

//                         var verifyListDTO =  JsonConvert.DeserializeObject<DocumentVerifyListDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true,apiResponse.Message, verifyListDTO);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false,apiResponse.Message);

//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                return new ServiceResult(ex.Message);
//            }


//        }

//        public async Task<ServiceResult> GetAllIssuerOrgNamesListAsync()
//        {
//            try
//            {
//                _logger.LogInformation("Get all Issuer Organization Names List api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/document/getissuerorgnamelist");
//                _logger.LogInformation("Get all Issuer Organization Names List api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result = JsonConvert.DeserializeObject<IEnumerable<IssuerOrgNamesDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, result);

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
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                return new ServiceResult(ex.Message);
//            }

//        }

//        public async Task<ServiceResult> GetDocTypeListByIdAsync(string id)
//        {
//            try
//            {
//                _logger.LogInformation("Get Document Type List by id api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/document/getdoctypelist?issuerUid={id}");
//                _logger.LogInformation("Get Document Type List by id api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result = JsonConvert.DeserializeObject<IEnumerable<IssuerOrgNamesDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, result);
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
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                return new ServiceResult(ex.Message);
//            }



//        }

//        public async Task<ServiceResult> GetVerificationMethodListByDocTypeAsync(string docType, string id)
//        {
//            try
//            {
//                _logger.LogInformation("Get Document Type List by id api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/document/getverificationtypebyorguid?orgUid={id}&docType={docType}");
//                _logger.LogInformation("Get Document Type List by id api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        var result =  JsonConvert.DeserializeObject<IEnumerable<VerificationMethodsDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, result);
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
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                return new ServiceResult(ex.Message);
//            }


//        }



//        public async Task<DownloadVerifyDocumentDTO> GetDownloadReportByFileId(string fileId)
//        {
//            try
//            {
//                _logger.LogInformation("Download Report api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/verificationdetail/downloadfile?fileId={fileId}");
//                _logger.LogInformation("Download Report api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);
//                        return JsonConvert.DeserializeObject<DownloadVerifyDocumentDTO>(apiResponse.Result.ToString());
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
//    }

//}
