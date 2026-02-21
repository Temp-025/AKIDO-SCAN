//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;
//using System.Net;
//using System.Text;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using System.Security.Claims;
//using EnterpriseGatewayPortal.Core.Domain.Models;
//using NuGet.Common;
//using static EnterpriseGatewayPortal.Core.Domain.Services.Communication.CommonResponse;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class BulkSignService:IBulkSignService
//    {
//        //private readonly HttpClient _client;
//        private readonly IConfiguration _configuration;
//        private readonly ILogger<BulkSignService> _logger;
//        public BulkSignService(
//            IConfiguration configuration,
//            ILogger<BulkSignService> logger)
//        {
//            _configuration = configuration;
//            _logger = logger;
//        }
//        public async Task<ServiceResult> GetBulkSigDataListAsync(string token)
//        {
//            _logger.LogInformation("GetBulkSigDataListAsync");
//            var agentUrl = _configuration["Agent-Url"];

//            var signingUrl = _configuration["SigningPortalUrl"];

//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);

//            ServiceResult serviceResult = new ServiceResult(null);
//            try
//            {
//                string relativePath = "api/bulksign/get-bulksign-data-list";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}";

//                var response = _client.GetAsync(url).Result;
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        JArray jArray = JArray.FromObject(apiResponse.Result);

//                        var jsonResponseList = JsonConvert.DeserializeObject<List<BulkSignDTO>>(jArray.ToString());
//                        return new ServiceResult(true,apiResponse.Message,jsonResponseList);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false,apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError(response.StatusCode.ToString());
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//                return new ServiceResult(ex.Message);
//            }
//        }
//        public async Task<ServiceResult> GetSentBulkSignListAsync(string token)
//        {
//            _logger.LogInformation("GetSentBulkSignListAsync");
//            HttpClient _client = new HttpClient();
//            _client.Timeout = TimeSpan.FromMinutes(10);
//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            var agentUrl = _configuration["Agent-Url"];

//            var signingUrl = _configuration["SigningPortalUrl"];

//            ServiceResult serviceResult = new ServiceResult(null);
//            try
//            {
//                string relativePath = "api/bulksign/get-sent-bulksign-list";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}";
                
//                var response = _client.GetAsync(url).Result;
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        JArray jArray = JArray.FromObject(apiResponse.Result);

//                        var jsonResponseList = JsonConvert.DeserializeObject<List<BulkSignDTO>>(jArray.ToString());
//                        //var jsonResponseList = JsonConvert.DeserializeObject<BulkSignDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, jsonResponseList);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError(response.StatusCode.ToString());
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//                return new ServiceResult(ex.Message);
//            }
//        }
//        public async Task<ServiceResult> GetReceivedBulkSignList(string token)
//        {
//            _logger.LogInformation("GetReceivedBulkSignList");
//            try
//            {
//                var agentUrl = _configuration["Agent-Url"];

//                var signingUrl = _configuration["SigningPortalUrl"];

//                HttpClient _client = new HttpClient();
//                _client.Timeout = TimeSpan.FromMinutes(10);
//                _client.DefaultRequestHeaders.Add("x-access-token", token);
//                string relativePath = "api/bulksign/get-received-bulksign-list";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}";
                
//                var response = _client.GetAsync(url).Result;
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        JArray jArray = JArray.FromObject(apiResponse.Result);

//                        var jsonResponseList = JsonConvert.DeserializeObject<List<BulkSignDTO>>(jArray.ToString());
//                        //var jsonResponseList = JsonConvert.DeserializeObject<BulkSignDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, jsonResponseList);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//                return new ServiceResult(ex.Message);
//            }
//        }
//        public async Task<ServiceResult> GetDocumentDetails(string correlationId, string token)
//        {
//            _logger.LogInformation("GetDocumentDetails");
//            try
//            {
//                var agentUrl = _configuration["Agent-Url"];

//                var signingUrl = _configuration["SigningPortalUrl"];

//                HttpClient _client = new HttpClient();
//                _client.Timeout = TimeSpan.FromMinutes(10);

//                _client.DefaultRequestHeaders.Add("x-access-token", token);

//                string relativePath = "api/bulksign/get-bulksign-data";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}?corelationId={correlationId}";

//                var response = _client.GetAsync(url).Result;
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var jsonResponseList = JsonConvert.DeserializeObject<DocumentDetailsDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, jsonResponseList);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                return new ServiceResult(ex.Message);
//            }
//        }
//        public async Task<byte[]> DownloadAsync(DocumentDownloadDTO documentDownloadDTO)
//        {
//            _logger.LogInformation("DownloadAsync");
//            try
//            {
//                var agentUrl = _configuration["Agent-Url"];

//                var signingUrl = _configuration["SigningPortalUrl"];
//                HttpClient _client = new HttpClient();
//                _client.Timeout = TimeSpan.FromMinutes(10);

//                string relativePath = "SignatureServiceAgent/api/digital/signature/get/file";

//                Uri fullUrl = new Uri(new Uri(agentUrl), relativePath);

//                string url = $"{fullUrl}";

//                GetfileDTO getfileDTO = new GetfileDTO()
//                {
//                    fileName = documentDownloadDTO.fileName,
//                    destinationPath = documentDownloadDTO.destinationPath
//                };

//                string json = JsonConvert.SerializeObject(getfileDTO);

//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                var response = _client.PostAsync(url, content).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
                    
//                    response.EnsureSuccessStatusCode();

//                    if (response.Content.Headers.ContentType?.MediaType == "application/pdf")
//                    {
//                        return await response.Content.ReadAsByteArrayAsync();
//                    }
//                    else
//                    {
//                        throw new InvalidOperationException("The response is not a PDF file.");
//                    }
//                }
//                else
//                {
//                    throw new InvalidOperationException("The response is not a PDF file.");
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new InvalidOperationException(ex.Message);
//            }
//        }
//        public async Task<ServiceResult> GetBulkSignRequest(string Id, string token)
//        {
//            _logger.LogInformation("GetBulkSignRequest");
//            try
//            {
//                var agentUrl = _configuration["Agent-Url"];

//                var signingUrl = _configuration["SigningPortalUrl"];

//                HttpClient _client = new HttpClient();
//                _client.Timeout = TimeSpan.FromMinutes(10);
//                _client.DefaultRequestHeaders.Add("x-access-token", token);

//                string relativePath = "api/bulksign/prepare-bulksigning-request";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}?id={Id}";

//                var response = _client.PostAsync(url,null).Result;
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var jsonResponseList = JsonConvert.DeserializeObject<PrepareBulksignResponseDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, jsonResponseList);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                return new ServiceResult(ex.Message);
//            }
            

//        }
//        public async Task<ServiceResult> BulkSignAsync(SignDTO signDTO)
//        {
//            _logger.LogInformation("BulkSignAsync");
//            try
//            {
//                var agentUrl = _configuration["Agent-Url"];

//                var signingUrl = _configuration["SigningPortalUrl"];

//                HttpClient _client = new HttpClient();

//                _client.Timeout = TimeSpan.FromMinutes(10);
//                string relativePath = "SignatureServiceAgent/api/digital/signature/bulk/sign";

//                Uri fullUrl = new Uri(new Uri(agentUrl), relativePath);

//                string url = $"{fullUrl}";

//                string json = JsonConvert.SerializeObject(signDTO);

//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                var response = _client.PostAsync(url, content).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
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
//                            return new ServiceResult(false, apiResponse.Message, apiResponse.Result);
//                        }
//                    }
//                    else
//                    {
//                        return new ServiceResult("Internal Error");
//                    }

//                }
//                else
//                {
//                    throw new InvalidOperationException("The response is not a PDF file.");
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new InvalidOperationException(ex.Message);
//            }
//        }
//        public async Task<ServiceResult> SendFiles(List<IFormFile> files,string organizationUid, string uploadKey)
//        {
//            _logger.LogInformation("SendFiles");
//            var agentUrl = _configuration["Agent-Url"];

//            var signingUrl = _configuration["SigningPortalUrl"];
//            HttpClient _client = new HttpClient();
//            _client.Timeout = TimeSpan.FromMinutes(10);

//            string relativePath = "SignatureServiceAgent/api/digital/signature/save/uplod/file";

//            Uri fullUrl = new Uri(new Uri(agentUrl), relativePath);

//            string url = $"{fullUrl}";

//            var content = new MultipartFormDataContent();

//            HttpResponseMessage response = null;

//            content.Add(new StringContent(organizationUid), "orgnizationId");
//            content.Add(new StringContent(uploadKey), "uploadKey");

//            // Add each file to the content with the key "files"
//            //foreach (var file in files)
//            //{
//            //    var fileContent = new StreamContent(file.OpenReadStream());
//            //    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
//            //    {
//            //        Name = "files",
//            //        //FileName = file.FileName
//            //    };
//            //    content.Add(fileContent, "files");
//            //}

            
//            using (var multipartFormContent = new MultipartFormDataContent()) {
                
//                foreach (var file in files)
//                {
//                    StreamContent fileStreamContent = new StreamContent(file.OpenReadStream(), (int)file.Length);

//                    fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

//                    multipartFormContent.Add(fileStreamContent, name: "files", fileName: file.FileName);

//                    //multipartFormContent.Add(fileStreamContent, "files");
//                }
//                multipartFormContent.Add(new StringContent(organizationUid), "orgnizationId");
//                multipartFormContent.Add(new StringContent(uploadKey), "uploadKey");
//                response = await _client.PostAsync(url, multipartFormContent);
//            }
//            //var response = _client.PostAsync(url,content).Result;
//            if (response.StatusCode == HttpStatusCode.OK)
//            {
//                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                if (apiResponse.Success)
//                {
//                    return new ServiceResult(true, apiResponse.Message,apiResponse.Result );
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
//        public async Task<ServiceResult> SaveRequest(string id, string name, string token)
//        {
//            _logger.LogInformation("SaveRequest");
//            try
//            {
//                var agentUrl = _configuration["Agent-Url"];

//                var signingUrl = _configuration["SigningPortalUrl"];
//                HttpClient _client = new HttpClient();
//                _client.Timeout = TimeSpan.FromMinutes(10);
//                _client.DefaultRequestHeaders.Add("x-access-token", token);
//                string relativePath = "api/bulksign/Save-bulksigning-request";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}?id={id}&transactionName={name}";

//                var response = _client.PostAsync(url,null).Result;
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        var jsonResponseList = JsonConvert.DeserializeObject<SaveRequestDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, jsonResponseList);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError(response.StatusCode.ToString());
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//                return new ServiceResult(ex.Message);
//            }
//        }
//        public async Task<ServiceResult> SaveRequestByPreparator(string id, string name,string email, string token)
//        {
//            _logger.LogInformation("SaveRequestByPreparator");
//            try
//            {
//                var agentUrl = _configuration["Agent-Url"];

//                var signingUrl = _configuration["SigningPortalUrl"];
//                HttpClient _client = new HttpClient();
//                _client.Timeout = TimeSpan.FromMinutes(10);
//                _client.DefaultRequestHeaders.Add("x-access-token", token);

//                string relativePath = "api/bulksign/Save-bulksigning-request-by-preparator";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}?id={id}&transactionName={name}&email={email}";

//                var response = _client.PostAsync(url, null).Result;
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        var jsonResponseList = JsonConvert.DeserializeObject<SaveRequestDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, jsonResponseList);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError(response.StatusCode.ToString());
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//                return new ServiceResult(ex.Message);
//            }
//        }
//        public async Task<ServiceResult> ChangePath(UpdatePathDTO pathDTO, string token)
//        {
//            _logger.LogInformation("ChangePath");
//            try
//            {
//                var agentUrl = _configuration["Agent-Url"];

//                var signingUrl = _configuration["SigningPortalUrl"];
//                HttpClient _client = new HttpClient();
//                _client.Timeout = TimeSpan.FromMinutes(10);
//                _client.DefaultRequestHeaders.Add("x-access-token", token);
//                string relativePath = "api/bulksign/update-bulksigning-srcdest";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}";

//                string json = JsonConvert.SerializeObject(pathDTO);

//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                var response = _client.PostAsync(url, content).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        //var jsonResponseList = JsonConvert.DeserializeObject<SaveRequestDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError(response.StatusCode.ToString());
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//                return new ServiceResult(ex.Message);
//            }
//        }
       
//        public async Task<ServiceResult> UpdateStatus(string correlationId,bool forSigner, string token)
//        {
//            _logger.LogInformation("UpdateStatus");
//            try
//            {
//                var agentUrl = _configuration["Agent-Url"];

//                var signingUrl = _configuration["SigningPortalUrl"];

//                HttpClient _client = new HttpClient();

//                _client.Timeout = TimeSpan.FromMinutes(10);

//                _client.DefaultRequestHeaders.Add("x-access-token", token);
//                string relativePath = "api/bulksign/update-bulksigning-status";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}?corelationId={correlationId}&forSigner={forSigner.ToString().ToLower()}";

//                var response = _client.PostAsync(url, null).Result;
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        var jsonResponseList = Convert.ToBoolean(apiResponse.Result);
//                        return new ServiceResult(jsonResponseList, apiResponse.Message, apiResponse.Result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError(response.StatusCode.ToString());
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//                return new ServiceResult(ex.Message);
//            }
//        }

//        public async Task<ServiceResult> UpdateDocumentStatus(string corelationId)
//        {
//            _logger.LogInformation("UpdateDocumentStatus");
//            try
//            {
//                var agentUrl = _configuration["Agent-Url"];

//                var signingUrl = _configuration["SigningPortalUrl"];
//                HttpClient _client = new HttpClient();
//                _client.Timeout = TimeSpan.FromMinutes(10);
//                string relativePath = "SignatureServiceAgent/item/by";

//                Uri fullUrl = new Uri(new Uri(agentUrl), relativePath);

//                string url = $"{fullUrl}/{corelationId}";

//                var response = _client.GetAsync(url).Result;
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    var apiResponse = JsonConvert.DeserializeObject<BulkSignCallBackDTO>(await response.Content.ReadAsStringAsync());
                    
//                    //var jsonResponseList = JsonConvert.DeserializeObject<BulkSignCallBackDTO>(apiResponse.ToString());
//                    return new ServiceResult(true, "Success", apiResponse);
                    
//                }
//                else
//                {
//                    _logger.LogError(response.StatusCode.ToString());
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//                return new ServiceResult(ex.Message);
//            }
//        }

//        public async Task<ServiceResult> getFiles(string Path)
//        {
//            string uploadPath = @Path;

//            // Check if the path is valid
//            if (Directory.Exists(uploadPath))
//            {
                
//                string[] pdfFiles = Directory.GetFiles(uploadPath, "*.pdf");

//                string[] pdfFileNames = new string[pdfFiles.Length];

//                if (pdfFileNames.Length <= 0)
//                {
//                    return new ServiceResult("Folder has no pdf files");
//                }
//                for (int i = 0; i < pdfFiles.Length; i++)
//                {
//                    pdfFileNames[i] = System.IO.Path.GetFileName(pdfFiles[i]);
//                }
//                return new ServiceResult(true, "success", pdfFileNames);
//            }
//            else
//            {
//                return new ServiceResult("Directory not Found");
//            }

//        }

//        public async Task<ServiceResult> getFilesList(FilesPathDTO model)
//        {
//            _logger.LogInformation("getFilesList");
//            var filesDTO=new PathRequestDTO();

//            filesDTO.destinationPath = model.DestinationPath;

//            filesDTO.sourcePath = model.SourcePath;
//            var agentUrl = _configuration["Agent-Url"];

//            var signingUrl = _configuration["SigningPortalUrl"];

//            try
//            {
//                string relativePath = "SignatureServiceAgent/api/digital/signature/get/documents/details";

//                Uri fullUrl = new Uri(new Uri(agentUrl), relativePath);

//                HttpClient _client = new HttpClient();

//                string url = $"{fullUrl}";

//                _client.Timeout = TimeSpan.FromMinutes(10);

//                string json = JsonConvert.SerializeObject(filesDTO);

//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                var response = _client.PostAsync(url, content).Result;

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    if (response.StatusCode == HttpStatusCode.OK)
//                    {
//                        var apiResponse = JsonConvert.DeserializeObject<PathResponseDTO>(await response.Content.ReadAsStringAsync());
//                        if (apiResponse.Success)
//                        {
//                            return new ServiceResult(true, apiResponse.Message, apiResponse.Result);

//                        }
//                        else
//                        {
//                            _logger.LogError(apiResponse.Message);
//                            return new ServiceResult(false, apiResponse.Message, apiResponse.Result);
//                        }
//                    }
//                    else
//                    {
//                        return new ServiceResult("Internal Error");
//                    }

//                }
//                else
//                {
//                    throw new InvalidOperationException("The response is not a PDF file.");
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new InvalidOperationException(ex.Message);
//            }

//        }
//        public async Task<ServiceResult> GetFileConfiguration(string token)
//        {
//            _logger.LogInformation("GetFileConfiguration");
//            try
//            {
//                var agentUrl = _configuration["Agent-Url"];

//                var signingUrl = _configuration["SigningPortalUrl"];

//                HttpClient _client = new HttpClient();
//                _client.Timeout = TimeSpan.FromMinutes(10);

//                _client.DefaultRequestHeaders.Add("x-access-token", token);

//                string relativePath = "api/bulksign/file-config";

//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);

//                string url = $"{fullUrl}";

//                var response = _client.GetAsync(url).Result;
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        var jsonResponseList = JsonConvert.DeserializeObject<FileConfigDTO>(apiResponse.Result.ToString());
//                        return new ServiceResult(true, apiResponse.Message, jsonResponseList);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError(response.StatusCode.ToString());
//                    return new ServiceResult("Internal Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//                return new ServiceResult(ex.Message);
//            }
//        }
//    }
//}
