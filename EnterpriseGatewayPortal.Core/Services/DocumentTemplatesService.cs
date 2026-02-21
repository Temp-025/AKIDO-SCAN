//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using System.Configuration;
//using Newtonsoft.Json.Serialization;
//using NuGet.Common;
//using Newtonsoft.Json.Linq;
//using EnterpriseGatewayPortal.Core.Domain.Models;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class DocumentTemplatesService : IDocumentTemplatesService
//    {
//        private readonly ILogger<OrganizationService> _logger;
//        private readonly IConfiguration _configuration;

//        public DocumentTemplatesService(
//            IConfiguration configuration,
//            ILogger<OrganizationService> logger)
//        {
//            _logger = logger;
//            _configuration = configuration;

//        }



//        public async Task<ServiceResult> GetBulkSignerListAsync(string OrgId,string token)
//        {
//            var signingPortalUrl = _configuration["SigningPortalUrl"];
//            string relativePath = "api/bulksign/get-bulksigner-list";

//            Uri fullUrl = new Uri(new Uri(signingPortalUrl), relativePath);

//            HttpClient _client = new HttpClient();
//            _client.Timeout = TimeSpan.FromMinutes(10);
//            _client.DefaultRequestHeaders.Add("x-access-token", token);

//            if (string.IsNullOrEmpty(OrgId))
//            {
//                return new ServiceResult("Organization id cannot be null");
//            }
//            try
//            {

//                var response = await _client.GetAsync($"{fullUrl}");


//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        BulkSignerListDTO OrgBulkSignerList = JsonConvert.DeserializeObject<BulkSignerListDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, OrgBulkSignerList);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(apiResponse.Message);
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
//                _logger.LogError("FailBulkSigningRequestAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while updating bulksigning request status");
//        }
//        public async Task<ServiceResult> SaveBulksignTemplateAsync(SaveNewTemplateDTO saveNewTemplateDTO,string token)
//        {
//            var signingPortalUrl = _configuration["SigningPortalUrl"];
//            string relativePath = "api/template/savenewtemplate";

//            Uri fullUrl = new Uri(new Uri(signingPortalUrl), relativePath);

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);

//            if (saveNewTemplateDTO.file == null)
//            {
//                return new ServiceResult("File cannot be null");
//            }

//            if (saveNewTemplateDTO.model == null)
//            {
//                return new ServiceResult("Model cannot be null");
//            }

//            try
//            {
//                _logger.LogInformation("SaveBulksignTemplateAsync start");

//                using (var content = new MultipartFormDataContent())
//                using (var stream = saveNewTemplateDTO.file.OpenReadStream())
//                {
//                    // Add the file content
//                    content.Add(new StreamContent(stream), "File", saveNewTemplateDTO.file.FileName);

//                    // Add other form fields
//                    content.Add(new StringContent(saveNewTemplateDTO.model), "Model");

//                    _logger.LogInformation("Save New Template api call start");

//                    //using (var response = await _client.PostAsync("http://localhost:54228/api/template/savenewtemplate", content))
//                    using (var response = await _client.PostAsync(fullUrl.ToString(), content))
//                    {
//                        _logger.LogInformation("Save New Template api call end");

//                        if (response.IsSuccessStatusCode)
//                        {
//                            APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                            if (apiResponse.Success)
//                            {
//                                return new ServiceResult(true, apiResponse.Message,apiResponse.Result);
//                            }
//                            else
//                            {
//                                _logger.LogError(apiResponse.Message);
//                                return new ServiceResult(false, apiResponse.Message);
//                            }
//                        }
//                        else
//                        {
//                            _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                                        $"with status code={response.StatusCode}");
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            _logger.LogInformation("SaveBulksignTemplateAsync end");
//            return new ServiceResult(false, "An error occurred while saving the template. Please try later.");
//        }

//        public async Task<ServiceResult> UpdateBulksignTemplateAsync(SaveNewTemplateDTO saveNewTemplateDTO,string token)
//        {

//            var signingPortalUrl = _configuration["SigningPortalUrl"];
//            string relativePath = "api/template/updatetemplate";
//            Uri fullUrl = new Uri(new Uri(signingPortalUrl), relativePath);

//            HttpClient _client = new HttpClient();
//            _client.Timeout = TimeSpan.FromMinutes(10);
//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            if (saveNewTemplateDTO.file == null)
//            {
//                return new ServiceResult("File cannot be null");
//            }

//            if (saveNewTemplateDTO.model == null)
//            {
//                return new ServiceResult("Model cannot be null");
//            }

//            try
//            {
//                _logger.LogInformation("SaveBulksignTemplateAsync start");

//                using (var content = new MultipartFormDataContent())
//                using (var stream = saveNewTemplateDTO.file.OpenReadStream())
//                {
//                    // Add the file content
//                    content.Add(new StreamContent(stream), "File", saveNewTemplateDTO.file.FileName);

//                    // Add other form fields
//                    content.Add(new StringContent(saveNewTemplateDTO.model), "Model");

//                    _logger.LogInformation("Save New Template api call start");

//                    using (var response = await _client.PostAsync(fullUrl.ToString(), content))
//                    {
//                        {
//                            _logger.LogInformation("Save New Template api call end");

//                            if (response.IsSuccessStatusCode)
//                            {
//                                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                                if (apiResponse.Success)
//                                {
//                                    return new ServiceResult(true, apiResponse.Message);
//                                }
//                                else
//                                {
//                                    _logger.LogError(apiResponse.Message);
//                                    return new ServiceResult(false, apiResponse.Message);
//                                }
//                            }
//                            else
//                            {
//                                _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                                            $"with status code={response.StatusCode}");
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            _logger.LogInformation("SaveBulksignTemplateAsync end");
//            return new ServiceResult(false, "An error occurred while saving the template. Please try later.");
//        }

//        public async Task<IEnumerable<DocumentsTemplatesListDTO>> GetTemplatesListAsync(string token)
//        {

//            var signingPortalUrl = _configuration["SigningPortalUrl"];
//            string relativePath = "api/template/getalltemplatelist";
//            Uri fullUrl = new Uri(new Uri(signingPortalUrl), relativePath);

//            HttpClient _client = new HttpClient();
//            _client.Timeout = TimeSpan.FromMinutes(10);
//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                _logger.LogInformation("Get Template List api call start");
//                HttpResponseMessage response = await _client.GetAsync($"{fullUrl}");
//                _logger.LogInformation("Get Template List api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return JsonConvert.DeserializeObject<IEnumerable<DocumentsTemplatesListDTO>>(apiResponse.Result.ToString());
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

//        public async Task<ServiceResult> GetTemplateDetailsAsync(string templateId,string token)
//        {

//            var signingPortalUrl = _configuration["SigningPortalUrl"];
//            string relativePath = "api/template/gettemplatedeatils";
//            Uri fullUrl = new Uri(new Uri(signingPortalUrl), relativePath);

//            HttpClient _client = new HttpClient();
//            _client.Timeout = TimeSpan.FromMinutes(10);
//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            if (string.IsNullOrEmpty(templateId))
//            {
//                return new ServiceResult("Organization id cannot be null");
//            }
//            try
//            {
//                var response = await _client.GetAsync($"{fullUrl}?templateId={templateId}");

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        PreviewDTO previewDto = JsonConvert.DeserializeObject<PreviewDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, previewDto);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(apiResponse.Message);
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
//                _logger.LogError("FailBulkSigningRequestAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while updating bulksigning request status");
//        }

//        public async Task<ServiceResult> GetPreviewTemplateAsync(int edmsid,string token)
//        {
//             var signingPortalUrl = _configuration["SigningPortalUrl"];
//            string relativePath = "api/downloaddoc";
//            Uri fullUrl = new Uri(new Uri(signingPortalUrl), relativePath);

//            HttpClient _client = new HttpClient();
//            _client.Timeout = TimeSpan.FromMinutes(10);
//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                var response = await _client.GetAsync($"{fullUrl}/{edmsid}");

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

//        public async Task<ServiceResult> GetTemplateListByOrgUid(string token)
//        {
//            var signingPortalUrl = _configuration["SigningPortalUrl"];
//            string relativePath = "api/template/get-templatelist-by-orgid";
//            Uri fullUrl = new Uri(new Uri(signingPortalUrl), relativePath);

//            HttpClient _client = new HttpClient();
//            _client.Timeout = TimeSpan.FromMinutes(10);
//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                var response = await _client.GetAsync($"{fullUrl}");

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        JArray jArray=JArray.Parse(apiResponse.Result.ToString());
//                        List<Template> templates = new List<Template>();
//                        List<Subscriberorgtemplate> subscriberOrgTemplates = new List<Subscriberorgtemplate>();

//                        foreach (var item in jArray)
//                        {
//                            var template = new Template
//                            {
//                                Templateid = item["templateId"]?.ToString(),
//                                Createdat = item["createdAt"]?.ToObject<DateTime?>(),
//                                Updatedat = item["updatedAt"]?.ToObject<DateTime?>(),
//                                Templatefile = Convert.FromBase64String(item["templateDetails"]?[0]?["templateName"]?.ToString()),
//                                Documentname = item["templateDetails"]?[0]?["documentName"]?.ToString(),
//                                Annotations = string.IsNullOrEmpty(item["templateDetails"]?[0]?["annotations"]?.ToString())
//                                   ? null
//                                   : item["templateDetails"]?[0]?["annotations"]?.ToString(),
//                                Esealannotations = string.IsNullOrEmpty(item["templateDetails"]?[0]?["esealAnnotations"]?.ToString())
//                                   ? null
//                                   : item["templateDetails"]?[0]?["esealAnnotations"]?.ToString(),
//                                Qrcodeannotations = string.IsNullOrEmpty(item["templateDetails"]?[0]?["qrCodeAnnotations"]?.ToString())
//                                    ? null
//                                    : item["templateDetails"]?[0]?["qrCodeAnnotations"]?.ToString(),
//                                Qrcoderequired = (bool?)item["templateDetails"]?[0]?["qrCodeRequired"],
//                                Settingconfig = item["templateDetails"]?[0]?["settingConfig"]?.ToString(),
//                                Signaturetemplate = (int?)item["templateDetails"]?[0]?["signatureTemplate"],
//                                Esealsignaturetemplate = (int?)item["templateDetails"]?[0]?["esealSignatureTemplate"],
//                                Status = item["templateDetails"]?[0]?["status"]?.ToString(),
//                                Edmsid = item["templateDetails"]?[0]?["edmsId"]?.ToString(),
//                                Createdby = item["templateDetails"]?[0]?["createdBy"]?.ToString(),
//                                Updatedby = item["templateDetails"]?[0]?["updatedBy"]?.ToString(),
//                                Rolelist = item["templateDetails"]?[0]?["roleList"]?.ToString(),
//                                Emaillist = item["templateDetails"]?[0]?["emailList"]?.ToString(),
//                            };

//                            templates.Add(template);

//                            var subscriberOrgTemplate = new Subscriberorgtemplate
//                            {
//                                Createdat = item["createdAt"]?.ToObject<DateTime?>(),
//                                Updatedat = item["updatedAt"]?.ToObject<DateTime?>(),
//                                Suid = item["suid"]?.ToString(),
//                                Organizationid = item["organizationId"]?.ToString(),
//                                Templateid = item["templateId"]?.ToString()
//                            };

//                            subscriberOrgTemplates.Add(subscriberOrgTemplate);
//                        }
//                        GetDocumentTemplatesListDTO documentTemplatesListDTO = new GetDocumentTemplatesListDTO()
//                        {
//                            templates= templates,
//                            subscriberOrgTemplates= subscriberOrgTemplates
//                        };
//                        return new ServiceResult(true,apiResponse.Message, documentTemplatesListDTO);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(apiResponse.Message);
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
//                _logger.LogError("FailBulkSigningRequestAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while updating bulksigning request status");
//        }

//        public async Task<ServiceResult> CheckUserWithSignatureTemplate(DTVerifyOrganizationUserDTO dTVerifyOrganizationUserDTO, string token)
//        {
//            var signingPortalUrl = _configuration["SigningPortalUrl"];
//            string relativePath = "api/template/verifyorganizationuser";

//            Uri fullUrl = new Uri(new Uri(signingPortalUrl), relativePath);

//            HttpClient _client = new HttpClient();
//            _client.Timeout = TimeSpan.FromMinutes(10);
//            _client.DefaultRequestHeaders.Add("x-access-token", token);

//            try
//            {

//                string json = JsonConvert.SerializeObject(dTVerifyOrganizationUserDTO,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                _logger.LogInformation("verify user api call start");
//                HttpResponseMessage response = await _client.PostAsync(fullUrl.ToString(), content);
//                _logger.LogInformation("verify user api call end");
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
//                               $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("CheckUserWithSignatureTemplate Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while verifying user for signature template");
//        }

//    }
//}
