//using EnterpriseGatewayPortal.Core.Domain.Models;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using NuGet.Common;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Reflection.Metadata;
//using System.Text;
//using System.Threading.Tasks;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class DigitalFormService : IDigitalFormService
//    {

//        private readonly ILogger<DigitalFormService> _logger;
//        private readonly IConfiguration _configuration;
//        public DigitalFormService(IConfiguration configuration,
//            ILogger<DigitalFormService> logger)
//        {

//            _configuration = configuration;
//            _logger = logger;
//        }
//        public async Task<ServiceResult> GetDocumentTemplateListAsync(string token)
//        {
//            _logger.LogInformation("GetDocumentTemplateListAsync");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                _logger.LogInformation("access_token:", token);
//				string relativePath = "api/formtemplate/get-form-template-list";

//				Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}";

//                var response = _client.GetAsync(url).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var list = JsonConvert.DeserializeObject<IList<DocumentTemplateDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, "Successfully received signature template list", list);

//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                       $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null; 
//        }

//        public async Task<ServiceResult> GetDocumentTemplatePublishListAsync(string token)
//        {
//            _logger.LogInformation("GetDocumentTemplatePublishListAsync");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                string relativePath = "api/formtemplate/get-form-template-publish-list";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}";

//                var response = _client.GetAsync(url).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var list = JsonConvert.DeserializeObject<IList<DocTemplateResponseDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, "Successfully received signature template list", list);

//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                       $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<ServiceResult> GetDocumentTemplatePublishGlobalListAsync(string token)
//        {
//            _logger.LogInformation("GetDocumentTemplatePublishGlobalListAsync");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {

//                string relativePath = "api/formtemplate/get-form-template-publish-global-list";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}";

//                var response = _client.GetAsync(url).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var list = JsonConvert.DeserializeObject<IList<DocTemplateResponseDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, "Successfully received signature template list", list);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                       $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }
//        public async Task<ServiceResult> GetSignatureTemplateList(string token)
//        {
//            _logger.LogInformation("GetSignatureTemplateList");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                string relativePath = "api/template/getsignaturetemplatelist";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}";

//                var response = _client.GetAsync(url).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {

//                        var list = JsonConvert.DeserializeObject<IList<SignatureTemplatesDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, "Successfully received signature template list", list);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                       $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<ServiceResult> GetResponseListCount(string templateId, string token)
//        {

//            _logger.LogInformation("GetResponseListCount");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                //string relativePath = "api/digitalformresponse/get-response-list";
//                  string relativePath = "api/digitalformresponse/get-response-list";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}?templateId={templateId}";

//                var response = _client.GetAsync(url).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var list = JsonConvert.DeserializeObject<List<DigitalFormResponseDTO>>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, "Successfully received signature template list", list);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                       $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }



//        public async Task<ServiceResult> GetCsvResponseSheet(string templateId, string token)
//        {
//            _logger.LogInformation("GetCsvResponseSheet");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                string relativePath = "api/digitalformresponse/get-csv-response-sheet";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}?templateId={templateId}";

//                var response = _client.GetAsync(url).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {                        
//                        var list = JsonConvert.DeserializeObject<SaveResponseSheetDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, "Successfully received CSV Response Sheet", list);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                       $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }


//        public async Task<ServiceResult> GetPreviewTemplateAsync(string edmsId, string token)
//        {
//            _logger.LogInformation("GetPreviewTemplateAsync");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                string relativePath = "api/downloaddoc";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}/{edmsId}";

//                var response = _client.GetAsync(url).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    byte[] bytes = await response.Content.ReadAsByteArrayAsync();
//                    if (bytes == null || bytes.Length == 0)
//                    {
//                        return new ServiceResult("Document not found");
//                    }
//                    else
//                    {
//                        return new ServiceResult(true, "Document received successfully", bytes);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                       $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<ServiceResult> GetDigitalFormFilldataAsync(string Suid, string token)
//        {
//            _logger.LogInformation("GetDigitalFormFilldataAsync");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                string relativePath = "api/digitalformresponse/get-digital-form-filldata";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}/{Suid}";

//                var response = _client.GetAsync(url).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var list = JsonConvert.DeserializeObject<FillFormDetailsDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, "Successfully received signature template list", list);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                       $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<ServiceResult> UpdateTemplateStatusAsync(string templateId, string action, string token)
//        {
//            _logger.LogInformation("UpdateTemplateStatusAsync");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                string relativePath = "api/formtemplate/update-template-status";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}?templateId={templateId}&action={action}";

//                var response = await _client.PostAsync(url, null);

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
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                       $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<ServiceResult> GetDocTemplateById(string templateId, string token)
//        {
//            _logger.LogInformation("GetDocTemplateById");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                string relativePath = "api/formtemplate/get-form-template-by-id";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}?templateId={templateId}";

//                var response = _client.GetAsync(url).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var list = JsonConvert.DeserializeObject<DocumentTemplateDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, "Successfully received signature template list", list);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                       $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<ServiceResult> SaveNewDocTemplate(SaveNewDocumentTemplateDTO SaveNewDocumentTemplateDTO, string token)
//        {
//            _logger.LogInformation("SaveNewDocTemplate");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                string relativePath = "api/formtemplate/save-newform-template";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);
//                string url = $"{fullUrl}";

//                if (SaveNewDocumentTemplateDTO.File == null)
//                {
//                    return new ServiceResult("File cannot be null");
//                }

//                if (SaveNewDocumentTemplateDTO.Model == null)
//                {
//                    return new ServiceResult("Model cannot be null");
//                }


//                using (var content = new MultipartFormDataContent())
//                using (var stream = SaveNewDocumentTemplateDTO.File.OpenReadStream())
//                {
//                    // Add the file content
//                    content.Add(new StreamContent(stream), "File", SaveNewDocumentTemplateDTO.File.FileName);

//                    // Add other form fields
//                    content.Add(new StringContent(SaveNewDocumentTemplateDTO.Model), "Model");

//                    var response = await _client.PostAsync(url, content);

//                    if (response.StatusCode == HttpStatusCode.OK)
//                    {
//                        APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                        if (apiResponse.Success)
//                        {
//                            return new ServiceResult(true, apiResponse.Message);
//                        }
//                        else
//                        {
//                            _logger.LogError(apiResponse.Message);
//                            return new ServiceResult(false, apiResponse.Message);
//                        }
//                    }
//                    else
//                    {
//                        _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                    }

//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<ServiceResult> UpdateDocTemplate(UpdateDocumentTemplateDTO updateDocumentTemplateDTO, string token)
//        {
//            _logger.LogInformation("UpdateDocTemplate");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                string relativePath = "api/formtemplate/update-form-template";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);
//                string url = $"{fullUrl}";

//                if (updateDocumentTemplateDTO.File == null)
//                {
//                    return new ServiceResult("File cannot be null");
//                }

//                if (updateDocumentTemplateDTO.Model == null)
//                {
//                    return new ServiceResult("Model cannot be null");
//                }

//                if (updateDocumentTemplateDTO.TemplateId == null)
//                {
//                    return new ServiceResult("TemplateId cannot be null");
//                }
//                using (var content = new MultipartFormDataContent())
//                using (var stream = updateDocumentTemplateDTO.File.OpenReadStream())
//                {
//                    // Add the file content
//                    content.Add(new StreamContent(stream), "File", updateDocumentTemplateDTO.File.FileName);

//                    // Add other form fields
//                    content.Add(new StringContent(updateDocumentTemplateDTO.Model), "Model");

//                    content.Add(new StringContent(updateDocumentTemplateDTO.TemplateId), "TemplateId");

//                    var response = await _client.PostAsync(url, content);

//                    if (response.StatusCode == HttpStatusCode.OK)
//                    {
//                        APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                        if (apiResponse.Success)
//                        {
//                            return new ServiceResult(true, apiResponse.Message);
//                        }
//                        else
//                        {
//                            _logger.LogError(apiResponse.Message);
//                            return new ServiceResult(false, apiResponse.Message);
//                        }
//                    }
//                    else
//                    {
//                        _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                    }

//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<ServiceResult> SaveNewDigitalFormRespons(SigningDigitalFormDTO signingDigitalFormDTO, string token)
//        {
//            _logger.LogInformation("SaveNewDigitalFormRespons");
//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                string relativePath = "api/digitalformresponse/new-save-newdigitalform-response";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);
//                string url = $"{fullUrl}";

//                using (var content = new MultipartFormDataContent())
//                using (var stream = signingDigitalFormDTO.File.OpenReadStream())
//                {
//                    // Add the file content
//                    content.Add(new StreamContent(stream), "File", signingDigitalFormDTO.File.FileName);
//                    content.Add(new StringContent(signingDigitalFormDTO.FormFieldData), "FormFieldData");
//                    content.Add(new StringContent(signingDigitalFormDTO.Idp_Token), "IdpToken");
//                    content.Add(new StringContent(signingDigitalFormDTO.AccToken), "AccessToken");
//                    content.Add(new StringContent(signingDigitalFormDTO.OrgUId), "OrganizationId");
//                    content.Add(new StringContent(signingDigitalFormDTO.TemplateId), "FormId");

//                    var response = await _client.PostAsync(url, content);

//                    if (response.StatusCode == HttpStatusCode.OK)
//                    {
//                        APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                        if (apiResponse.Success)
//                        {
//                            return new ServiceResult(true, apiResponse.Message);
//                        }
//                        else
//                        {
//                            _logger.LogError(apiResponse.Message);
//                            return new ServiceResult(false, apiResponse.Message);
//                        }
//                    }
//                    else
//                    {
//                        _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                    }

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
