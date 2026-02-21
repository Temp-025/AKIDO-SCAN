//using EnterpriseGatewayPortal.Core.Domain.Models;
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
//    public class DocumentIssuerService : IDocumentIssuerService
//    {
//        private readonly ILogger<DocumentIssuerService> _logger;
//        private readonly IConfiguration _configuration;
//        private readonly HttpClient _client;

//        public DocumentIssuerService(HttpClient httpClient,
//            IConfiguration configuration,
//            ILogger<DocumentIssuerService> logger)
//        {
//            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:DocumentVerificationServiceBaseAddress"]);
//            _client = httpClient;
//            _logger = logger;
//            _configuration = configuration;

//        }
//        public async Task<IEnumerable<SaveDocumentIssuerDTO>> GetAllDocIssuerListAsync()
//        {
//            try
//            {
//                HttpResponseMessage response = await _client.GetAsync($"api/document/getalldocumentlist");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return JsonConvert.DeserializeObject<IEnumerable<SaveDocumentIssuerDTO>>(apiResponse.Result.ToString());
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

//        public async Task<ServiceResult> AddDocumentissuerAsync(SaveDocumentIssuerDTO document)
//        {
//            try
//            {
//                _logger.LogInformation(document.ToString());
//                string json = JsonConvert.SerializeObject(document,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
//                _logger.LogInformation(json);
//                HttpResponseMessage response = await _client.PostAsync("api/document/savedocument", content);
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

//            return new ServiceResult(false, "An error occurred while Adding document Issuer. Please try later.");
//        }

//        public async Task<ServiceResult> UpdateDocumentissuerAsync(SaveDocumentIssuerDTO document)
//        {
//            try
//            {

//                string json = JsonConvert.SerializeObject(document,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = await _client.PostAsync("api/document/updatedocument", content);
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

//            return new ServiceResult(false, "An error occurred while Adding document Issuer. Please try later.");
//        }

//        public async Task<ServiceResult> UpdateDocuemntIssuerStatusAsync(UpdatedocumentIssuerStatusDTO updatedocumentIssuerStatus)
//        {

//            try
//            {

//                string json = JsonConvert.SerializeObject(updatedocumentIssuerStatus,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = await _client.PostAsync("api/document/updatedocumentstatus", content);
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

//            return new ServiceResult(false, "An error occurred while Updating Issuer Status. Please try later.");
//        }

//        public async Task<SaveDocumentIssuerDTO> GetDocIssuerDetailsByIdAsync(int docIssuerId)
//        {
//            try
//            {
//                _logger.LogInformation("Get Document Issuer details by id api call start");
//                HttpResponseMessage response = await _client.GetAsync($"api/document/getdocumentbyid?id={docIssuerId}");
//                _logger.LogInformation("Get Document Issuer details id api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return JsonConvert.DeserializeObject<SaveDocumentIssuerDTO>(apiResponse.Result.ToString());



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
