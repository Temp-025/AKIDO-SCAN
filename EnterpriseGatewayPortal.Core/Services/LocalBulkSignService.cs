//using EnterpriseGatewayPortal.Core.Constants;
//using EnterpriseGatewayPortal.Core.Domain.Models;
//using EnterpriseGatewayPortal.Core.Domain.Repositories;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json.Serialization;
//using System.Net;
//using System.Net.Http;
////using System.Security.Policy;
//using System.Text;
////using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class LocalBulkSignService : ILocalBulkSignService
//    {
//        private readonly ILogger<LocalBulkSignService> _logger;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IConfiguration _configuration;
//        private readonly HttpClient _client;
//        public LocalBulkSignService(
//                                    ILogger<LocalBulkSignService> logger,
//                                    IUnitOfWork unitOfWork,
//                                    IConfiguration configuration,
//                                    HttpClient httpClient
//                                    )
//        {
//            _logger = logger;
//            _unitOfWork = unitOfWork;
//            _configuration = configuration;
//            _client = httpClient;
//        }

//        public async Task<ServiceResult> GetBulkSigDataListAsync(UserDTO userDTO)
//        {
//            try
//            {
//                _logger.LogCritical("GetBulkSigDataListAsync strart");
//                _logger.LogCritical("organizaion id: " + userDTO.OrganizationId);
//                _logger.LogCritical("suid: " + userDTO.Suid);
//                var list = await _unitOfWork.BulkSign.GetBulkSigDataList(userDTO.OrganizationId, userDTO.Suid);
//                if (list == null)
//                {
//                    _logger.LogInformation("No records found");
//                    return new ServiceResult(true, "No records found.", new List<Bulksign>());
//                }
//                foreach (var item in list)
//                {
//                    _logger.LogInformation("Transaction : " + item.Transaction + " Status: " + item.Status);
//                }
//                _logger.LogCritical("GetBulkSigDataListAsync end");

//                return new ServiceResult(true, "Successfully received bulksign data list", list);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("GetBulkSigDataListAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while receiving bulksign data list");
//        }

//        public async Task<ServiceResult> GetReceivedBulkSignListAsync(UserDTO user)
//        {
//            try
//            {
//                _logger.LogCritical("GetReceivedBulkSignListAsync strart");
//                _logger.LogCritical("organizaion id: " + user.OrganizationId);
//                _logger.LogCritical("suid: " + user.Suid);
//                var list = await _unitOfWork.BulkSign.GetReceivedBulkSignDataList(user.OrganizationId, user.Email);
//                if (list == null)
//                {
//                    _logger.LogInformation("No records found");
//                    return new ServiceResult(true, "No records found.", new List<Bulksign>());
//                }
//                _logger.LogCritical("GetReceivedBulkSignListAsync end");

//                return new ServiceResult(true, "Successfully received bulksign list", list);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("GetReceivedBulkSignListAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while receiving bulksign list");
//        }

//        public async Task<ServiceResult> GetSentBulkSignListAsync(UserDTO user)
//        {
//            try
//            {
//                _logger.LogCritical("GetSentBulkSignListAsync strart");
//                _logger.LogCritical("organizaion id: " + user.OrganizationId);
//                _logger.LogCritical("suid: " + user.Suid);
//                var list = await _unitOfWork.BulkSign.GetSentBulkSignDataList(user.OrganizationId, user.Suid);
//                if (list == null || list.Count == 0)
//                {
//                    _logger.LogInformation("No records found");
//                    return new ServiceResult(true, "No records found.", new List<Bulksign>());
//                }
//                _logger.LogCritical("GetSentBulkSignListAsync end");

//                return new ServiceResult(true, "Successfully received bulksign list", list);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("GetSentBulkSignListAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while receiving bulksign list");
//        }

//        public async Task<ServiceResult> GetBulkSigDataListByTemplateIdAsync(string templateID)
//        {
//            try
//            {
//                var bulkSignDataList = await _unitOfWork.BulkSign.GetBulkSigDataListByTemplateId(templateID);
//                if (bulkSignDataList == null)
//                {
//                    _logger.LogInformation("No details found");
//                    return new ServiceResult(true, "No details found.");
//                }

//                return new ServiceResult(true, "Successfully received bulksign data list", bulkSignDataList);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("GetBulkSigDataListByTemplateIdAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while receiving bulksign data list");
//        }

//        public async Task<ServiceResult> GetBulkSigDataAsync(string corelationId)
//        {
//            try
//            {
//                var bulkSignData = await _unitOfWork.BulkSign.GetBulkSignDataByCorelationId(corelationId);
//                if (bulkSignData == null)
//                {
//                    _logger.LogInformation("No details found");
//                    return new ServiceResult(true, "No details found.");
//                }

//                return new ServiceResult(true, "Successfully received bulksign data", bulkSignData);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("GetBulkSigDataAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while receiving bulksign data");
//        }

//        public async Task<ServiceResult> PrepareBulkSigningRequestAsync(string templateId, UserDTO userDTO)
//        {

//            if (templateId == null)
//            {
//                return new ServiceResult("Template id cannot be null");
//            }

//            try
//            {
//                var templateDetails = (Template)(await _unitOfWork.Template.GetTemplateAsync(templateId));
//                if (templateDetails == null)
//                {
//                    return new ServiceResult("Failed to get template details");
//                }

//                JObject Settings = JObject.Parse(templateDetails.Settingconfig);

//                //var agentResponce = await GetAgentUrlAsync(userDTO.OrganizationId);
//                //if (!agentResponce.Success)
//                //{
//                //    return new ServiceResult(agentResponce.Message);
//                //}

//                PrepareBulksignResponseDTO prepareBulksignResponse = new PrepareBulksignResponseDTO()
//                {
//                    Suid = userDTO.Suid,
//                    Email = userDTO.Email,
//                    OrganizationId = userDTO.OrganizationId,
//                    SourcePath = Settings["inputpath"].ToString(),
//                    DestinationPath = Settings["outputpath"].ToString(),
//                    //AgentUrl = agentResponce.Resource.ToString(),
//                    SignatureTemplateId = (int)templateDetails.Signaturetemplate,
//                    EsealSignatureTemplateId = (int)templateDetails.Esealsignaturetemplate,
//                    QrCodeRequired = (bool)templateDetails.Qrcoderequired
//                };

//                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//                var isDevelopment = environment == Environments.Development;
//                if (isDevelopment)
//                {
//                    prepareBulksignResponse.CallBackUrl = _configuration.GetValue<string>("Config:CallBackUrl");
//                }
//                else
//                {
//                    prepareBulksignResponse.CallBackUrl = _configuration.GetValue<string>("Config:BACKEND_URL") + "/api/bulksign/bulksigned-document";
//                }

//                if (!string.IsNullOrEmpty(templateDetails.Annotations))
//                {
//                    JObject signCoordinatesObj = JObject.Parse(templateDetails.Annotations);

//                    //var signCords = signCoordinatesObj[templateDetails.RoleList[0].Role];
//                    var signCords = signCoordinatesObj[userDTO.Email];

//                    prepareBulksignResponse.PlaceHolderCoordinates = new placeHolderCoordinates
//                    {
//                        pageNumber = signCords["PageNumber"].ToString(),
//                        signatureXaxis = signCords["posX"].ToString(),
//                        signatureYaxis = signCords["posY"].ToString()
//                    };
//                }

//                if (templateDetails.Esealannotations != null)
//                {
//                    JObject esealCoordinatesObj = JObject.Parse(templateDetails.Esealannotations);
//                    var esealCords = esealCoordinatesObj[userDTO.Email];
//                    //var esealCords = esealCoordinatesObj[templateDetails.RoleList[0].Role];

//                    prepareBulksignResponse.EsealPlaceHolderCoordinates = new esealplaceHolderCoordinates
//                    {
//                        pageNumber = esealCords["PageNumber"].ToString(),
//                        signatureXaxis = esealCords["posX"].ToString(),
//                        signatureYaxis = esealCords["posY"].ToString()
//                    };
//                }

//                if (templateDetails.Qrcodeannotations != null)
//                {
//                    JObject qrCodeCoordinatesObj = JObject.Parse(templateDetails.Qrcodeannotations);
//                    var esealCords = qrCodeCoordinatesObj[userDTO.Email];
//                    //var esealCords = esealCoordinatesObj[templateDetails.RoleList[0].Role];

//                    prepareBulksignResponse.QrCodePlaceHolderCoordinates = new QrCodePlaceHolderCoordinates
//                    {
//                        pageNumber = esealCords["PageNumber"].ToString(),
//                        signatureXaxis = esealCords["posX"].ToString(),
//                        signatureYaxis = esealCords["posY"].ToString()
//                    };
//                }

//                // prepareBulksignResponse.CorelationId = $@"{Guid.NewGuid()}";

//                return new ServiceResult(true, "Successfully received preparing bulksigning request", prepareBulksignResponse);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("PrepareBulkSigningRequestAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while preparing bulksigning request");
//        }


//        public async Task<ServiceResult> SaveBulkSigningRequestAsync(string templateId, string transactionName, UserDTO userDTO, string? signerEmail = null)
//        {
//            if (templateId == null)
//            {
//                return new ServiceResult("Template id cannot be null");
//            }

//            try
//            {
//                string email = !string.IsNullOrEmpty(signerEmail) ? signerEmail : userDTO.Email;

//                if (string.IsNullOrWhiteSpace(transactionName))
//                {
//                    return new ServiceResult("Transaction name cannot be empty or blank spaces");
//                }
//                else
//                {
//                    bool IsTransactionNameExist = await _unitOfWork.BulkSign.IsBulkSigningTransactionNameExists(transactionName);
//                    if (IsTransactionNameExist)
//                    {
//                        return new ServiceResult("Transaction name already exists");
//                    }
//                }

//                var templateDetails = (Template)(await _unitOfWork.Template.GetTemplateAsync(templateId));
//                if (templateDetails == null)
//                {
//                    return new ServiceResult("Failed to get template details");
//                }

//                JObject Settings = JObject.Parse(templateDetails.Settingconfig);



//                PrepareBulksignResponseDTO prepareBulksignResponse = new()
//                {
//                    Suid = userDTO.Suid,
//                    Email = userDTO.Email,
//                    OrganizationId = userDTO.OrganizationId,
//                    SourcePath = Settings["inputpath"].ToString(),
//                    DestinationPath = Settings["outputpath"].ToString(),
//                    SignatureTemplateId = (int)templateDetails.Signaturetemplate,
//                    EsealSignatureTemplateId = (int)templateDetails.Esealsignaturetemplate,
//                    QrCodeRequired = (bool)templateDetails.Qrcoderequired
//                };

//                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//                var isDevelopment = environment == Environments.Development;
//                if (isDevelopment)
//                {
//                    prepareBulksignResponse.CallBackUrl = _configuration.GetValue<string>("CallBackUrl");
//                }
//                else
//                {
//                    prepareBulksignResponse.CallBackUrl = _configuration.GetValue<string>("Config:Signing_portal:BACKEND_URL") + "/api/bulksigne/bulksigned-document";
//                }

//                if (!string.IsNullOrEmpty(templateDetails.Annotations))
//                {
//                    JObject signCoordinatesObj = JObject.Parse(templateDetails.Annotations);

//                    var signCords = signCoordinatesObj[email];

//                    prepareBulksignResponse.PlaceHolderCoordinates = new placeHolderCoordinates
//                    {
//                        pageNumber = signCords["PageNumber"].ToString(),
//                        signatureXaxis = signCords["posX"].ToString(),
//                        signatureYaxis = signCords["posY"].ToString()
//                    };
//                }

//                if (templateDetails.Esealannotations != null)
//                {
//                    JObject esealCoordinatesObj = JObject.Parse(templateDetails.Esealannotations);
//                    var esealCords = esealCoordinatesObj[email];

//                    prepareBulksignResponse.EsealPlaceHolderCoordinates = new esealplaceHolderCoordinates
//                    {
//                        pageNumber = esealCords["PageNumber"].ToString(),
//                        signatureXaxis = esealCords["posX"].ToString(),
//                        signatureYaxis = esealCords["posY"].ToString()
//                    };
//                }

//                if (templateDetails.Qrcodeannotations != null)
//                {
//                    JObject qrCodeCoordinatesObj = JObject.Parse(templateDetails.Qrcodeannotations);
//                    var esealCords = qrCodeCoordinatesObj[email];

//                    prepareBulksignResponse.QrCodePlaceHolderCoordinates = new QrCodePlaceHolderCoordinates
//                    {
//                        pageNumber = esealCords["PageNumber"].ToString(),
//                        signatureXaxis = esealCords["posX"].ToString(),
//                        signatureYaxis = esealCords["posY"].ToString()
//                    };
//                }

//                prepareBulksignResponse.CorelationId = $@"{Guid.NewGuid()}";

//                Bulksign bulksignobj = new()
//                {
//                    CorelationId = prepareBulksignResponse.CorelationId,
//                    TemplateId = templateId,
//                    TemplateName = templateDetails.Templatename,
//                    OrganizationId = userDTO.OrganizationId,
//                    Suid = userDTO.Suid,
//                    Transaction = transactionName,
//                    SourcePath = prepareBulksignResponse.SourcePath,
//                    SignedPath = prepareBulksignResponse.DestinationPath,
//                    Status = DocumentStatusConstants.Draft,
//                    CreatedAt = DateTime.Now,
//                    OwnerName = userDTO.Name,
//                    OwnerEmail = userDTO.Email,
//                    SignerEmail = string.IsNullOrEmpty(signerEmail) ? null : signerEmail
//                };

//                if (!string.IsNullOrEmpty(templateDetails.Annotations))
//                {
//                    List<Roles> roles = JsonConvert.DeserializeObject<List<Roles>>(templateDetails.Rolelist);

//                    Roles? firstRole = roles.Count > 0 ? roles[0] : null;

//                    if (firstRole != null)
//                    {
//                        templateDetails.Annotations = templateDetails.Annotations.Replace(firstRole.role, email);
//                    }

//                    bulksignobj.SignatureAnnotations = templateDetails.Annotations;
//                }

//                if (templateDetails.Esealannotations != null)
//                {
//                    List<Roles> esealRoles = JsonConvert.DeserializeObject<List<Roles>>(templateDetails.Rolelist);

//                    Roles? firstEsealRole = esealRoles.Count > 0 ? esealRoles[0] : null;

//                    if (firstEsealRole != null)
//                    {
//                        templateDetails.Esealannotations = templateDetails.Esealannotations.Replace(firstEsealRole.role, email);
//                    }

//                    bulksignobj.EsealAnnotations = templateDetails.Esealannotations;
//                }

//                if (!string.IsNullOrEmpty(templateDetails.Qrcodeannotations))
//                {
//                    List<Roles> QrRoles = JsonConvert.DeserializeObject<List<Roles>>(templateDetails.Rolelist);

//                    Roles? firstQrRole = QrRoles.Count > 0 ? QrRoles[0] : null;

//                    if (firstQrRole != null)
//                    {
//                        templateDetails.Qrcodeannotations = templateDetails.Qrcodeannotations.Replace(firstQrRole.role, email);
//                    }

//                    bulksignobj.QrcodeAnnotations = templateDetails.Qrcodeannotations;
//                }

//                var saveBulkSignData = await _unitOfWork.BulkSign.SaveBulkSignData(bulksignobj);

//                return new ServiceResult(true, "Successfully saved bulksign data", prepareBulksignResponse);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("SaveBulkSigningRequestByPreparatorAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while saving bulksign data");
//        }

//        public async Task<ServiceResult> UpdateBulkSigningStatusAsync(string corelationId, bool forSigner)
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(corelationId))
//                {
//                    return new ServiceResult("Corelation id cannot be null");
//                }

//                var record = await _unitOfWork.BulkSign.GetBulkSignDataByCorelationId(corelationId);
//                if (record == null)
//                {
//                    return new ServiceResult("Bulk Sign data not found");
//                }

//                if (record.Status == DocumentStatusConstants.Completed || record.Status == DocumentStatusConstants.Failed)
//                {
//                    return new ServiceResult(true, "Status updated successfully", record);
//                }

//                Bulksign bulkSign = new()
//                {
//                    CorelationId = corelationId
//                };

//                if (forSigner)
//                {
//                    bulkSign.Status = DocumentStatusConstants.Pending;
//                }
//                else
//                {
//                    bulkSign.Status = DocumentStatusConstants.InProgress;
//                }

//                var updateBulkSignData = await _unitOfWork.BulkSign.UpdateBulkSignData(bulkSign);
//                if (!updateBulkSignData)
//                {
//                    return new ServiceResult("Failed to update bulk sign status");
//                }

//                return new ServiceResult(updateBulkSignData, "Status updated successfully");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("UpdateBulkSigningStatusAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while updating bulksign status");
//        }

//        public async Task<ServiceResult> UpdateBulkSigningSourceDestinationAsync(UpdatePathDTO dto)
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(dto.CorelationId))
//                {
//                    return new ServiceResult("Corelation id cannot be null");
//                }

//                Bulksign bulkSign = new Bulksign
//                {
//                    CorelationId = dto.CorelationId,
//                    SourcePath = dto.Source,
//                    SignedPath = dto.Destination,
//                };

//                var updateBulkSignData = await _unitOfWork.BulkSign.UpdateBulkSignSrcDestData(bulkSign);
//                if (!updateBulkSignData)
//                {
//                    return new ServiceResult("Failed to update bulksign data");
//                }

//                return new ServiceResult(updateBulkSignData, "Source and Destination updated successfully");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("UpdateBulkSigningSourceDestinationAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while updating bulksign source and destination");
//        }

//        public async Task<ServiceResult> FailBulkSigningRequestAsync(string CorrelationId)
//        {
//            if (string.IsNullOrEmpty(CorrelationId))
//            {
//                return new ServiceResult("Corelation id cannot be null");
//            }

//            try
//            {

//                var bulkSignData = await _unitOfWork.BulkSign.GetBulkSignDataByCorelationId(CorrelationId);
//                if (bulkSignData == null)
//                {
//                    _logger.LogInformation("No details found");
//                    return new ServiceResult(true, "No details found.");
//                }

//                Bulksign bulkSign = new Bulksign
//                {
//                    CorelationId = CorrelationId,
//                    Result = new Result().ToString(),
//                    Status = DocumentStatusConstants.Failed
//                };

//                var updateBulksignData = await _unitOfWork.BulkSign.UpdateBulkSignData(bulkSign);
//                if (!updateBulksignData)
//                {
//                    return new ServiceResult("Failed to update bulksign data");
//                }

//                return new ServiceResult(true, "Successfully update bulksigning request status");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("FailBulkSigningRequestAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while updating bulksigning request status");
//        }

//        public async Task<ServiceResult> GetBulkSignerListAsync(string OrgId)
//        {

//            if (string.IsNullOrEmpty(OrgId))
//            {
//                return new ServiceResult("Organization id cannot be null");
//            }

//            try
//            {

//                var response = await _client.GetAsync(_configuration.GetValue<string>("Config:OrganizationBulkSignerEmailListUrl") + OrgId);

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

//        public async Task<ServiceResult> DownloadSignedDocumentAsync(string fileName, string corelationId)
//        {
//            try
//            {
//                if (String.IsNullOrEmpty(fileName))
//                {
//                    return new ServiceResult("Filename cannot be null.");
//                }

//                if (String.IsNullOrEmpty(corelationId))
//                {
//                    return new ServiceResult("Corelation Id cannot be null.");
//                }

//                var bulkSignData = await _unitOfWork.BulkSign.GetBulkSignDataByCorelationId(corelationId);
//                if (bulkSignData == null)
//                {
//                    _logger.LogInformation("No details found");
//                    return new ServiceResult(true, "No details found.");
//                }

//                DocumentDownloadDTO downloadSignedDocument = new DocumentDownloadDTO
//                {
//                    fileName = fileName,
//                    destinationPath = bulkSignData.SignedPath
//                };

//                string json = JsonConvert.SerializeObject(downloadSignedDocument,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                DateTime startTime = DateTime.Now;
//                _logger.LogInformation("DownloadSignedDocumentAsync Time : {0}", startTime);

//                HttpResponseMessage response = await _client.PostAsync($"api/digital/signature/get/file", content);

//                _logger.LogInformation("Download document total time : {0}", DateTime.Now.Subtract(startTime));

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
//                else if (response.StatusCode == HttpStatusCode.NoContent)
//                {
//                    return new ServiceResult("This Document No Longer Exists");
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}");

//                    return new ServiceResult("Failed to receive document : " + response.ReasonPhrase);
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("DownloadSignedDocumentAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while fetching document");
//        }

//        public async Task<ServiceResult> GetAgentUrlAsync(string OrgId)
//        {
//            _logger.LogInformation("GetAgentUrlAsync Start");

//            if (string.IsNullOrEmpty(OrgId))
//            {
//                return new ServiceResult("Organization id cannot be null");
//            }

//            try
//            {
//                _logger.LogInformation("GetOrganizationAgentUrl API call start");

//                var response = await _client.GetAsync(_configuration.GetValue<string>("Config:GetOrganizationAgentUrl") + OrgId);

//                _logger.LogInformation("GetOrganizationAgentUrl API call end");

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation("GetAgentUrlAsync End");
//                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message, apiResponse);
//                        _logger.LogInformation("GetAgentUrlAsync End");
//                        return new ServiceResult(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError(response.Content.ToString());
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("FailBulkSigningRequestAsync Exception :  {0}", ex.Message);
//            }
//            _logger.LogInformation("GetAgentUrlAsync End");
//            return new ServiceResult("An error occurred while updating bulksigning request status");
//        }

//        public async Task<ServiceResult> BulkSignCallBackAsync(BulkSignCallBackDTO bulkSignCallBackDTO)
//        {
//            if (string.IsNullOrEmpty(bulkSignCallBackDTO.CorrelationId))
//            {
//                return new ServiceResult("Corelation id cannot be null");
//            }

//            try
//            {
//                var bulkSignData = await _unitOfWork.BulkSign.GetBulkSignDataByCorelationId(bulkSignCallBackDTO.CorrelationId);
//                if (bulkSignData == null)
//                {
//                    _logger.LogInformation("No details found");
//                    return new ServiceResult(true, "No details found.");
//                }
//                var result = JsonConvert.SerializeObject(bulkSignCallBackDTO);
//                Bulksign bulkSign = new()
//                {
//                    CorelationId = bulkSignCallBackDTO.CorrelationId,
//                    Result = result,
//                    Status = DocumentStatusConstants.Completed,
//                    CompletedAt = DateTime.Now
//                };

//                var updateBulksignData = await _unitOfWork.BulkSign.UpdateBulkSignData(bulkSign);
//                if (!updateBulksignData)
//                {
//                    return new ServiceResult("Failed to update bulksign data");
//                }

//                return new ServiceResult(true, "Successfully received preparing bulksigning request");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("BulkSignCallBackAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while updating bulksigning request");
//        }

//        public async Task<ServiceResult> BulkSignStatusAsync(string correlationId, string accessToken)
//        {

//            _logger.LogInformation("BulkSignStatusAsync start");

//            if (string.IsNullOrEmpty(correlationId))
//            {
//                return new ServiceResult("Correlation id cannot be null");
//            }

//            try
//            {
//                _logger.LogInformation("BulkSignStatusAsync API call start");

//                _client.DefaultRequestHeaders.Add("UgPassAuthorization", "Bearer " + accessToken);
//                _client.BaseAddress = new Uri(_configuration.GetValue<string>("Config:BulkSignStatusUrl"));

//                var response = await _client.GetAsync("api/digital/signature/bulk/sign/status/" + correlationId);


//                _logger.LogInformation("BulkSignStatusAsync API call end");

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation("BulkSignStatusAsync End");
//                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message, apiResponse);
//                        _logger.LogInformation("BulkSignStatusAsync End");
//                        return new ServiceResult(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError(response.Content.ToString());
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}");
//                }
//            }

//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("BulkSignStatusAsync Exception :  {0}", ex.Message);
//            }

//            _logger.LogInformation("BulkSignStatusAsync End");
//            return new ServiceResult("An error occurred while getting bulksigning status");
//        }

//        public async Task<ServiceResult> SendBulkSignRequestAsync(SendBulkSignDTO signDTO)
//        {

//            _logger.LogInformation("SendBulkSignRequestAsync start");

//            try
//            {
//                var model = new SignatureRequestModel
//                {
//                    SourcePath = signDTO.SourcePath,
//                    DestinationPath = signDTO.DestinationPath,
//                    Id = signDTO.UgpassEmailId,
//                    CorrelationId = signDTO.CorrelationId,
//                    PlaceHolderCoordinates = signDTO.placeHolderCoordinates,
//                    EsealPlaceHolderCoordinates = signDTO.esealPlaceHolderCoordinates
//                };



//                var url = _configuration.GetValue<string>("Config:BulkSignBaseUrl");
//                HttpResponseMessage response;
//                _client.DefaultRequestHeaders.Add("UgPassAuthorization", "Bearer " + signDTO.AccessToken);
//                using (var multipartFormContent = new MultipartFormDataContent())
//                {

//                    multipartFormContent.Add(new StringContent(JsonConvert.SerializeObject(model)), "model");

//                    DateTime startTimeForAPI = DateTime.Now;
//                    _logger.LogInformation(" bulk Signing service call start : " + startTimeForAPI);
//                    response = await _client.PostAsync(url, multipartFormContent);
//                    DateTime endTimeForAPI = DateTime.Now;
//                    TimeSpan diff = endTimeForAPI - startTimeForAPI;
//                    _logger.LogInformation("bulk Signing service call end : " + endTimeForAPI);
//                    _logger.LogInformation("Total time taken to update bulk SignService call in total seconds : {0} ", diff.TotalSeconds);
//                }

//                _logger.LogInformation("Bulksign API call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());

//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation(apiResponse.Message);

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
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                              $"with status code={response.StatusCode}");
//                    return new ServiceResult("Internal Error");

//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("SendBulkSignRequestAsync Exception :  {0}", ex.Message);
//            }

//            _logger.LogInformation("SendBulkSignRequestAsync End");
//            return new ServiceResult("An error occurred while getting bulksigning status");
//        }

//        public async Task<ServiceResult> GetFilesListFromPath(FilesPathDTO model)
//        {
//            _logger.LogInformation("GetFilesListFromPath started");

//            try
//            {
//                string sourcePath = model.SourcePath;

//                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//                var isDevelopment = environment == Environments.Development;
//                if (isDevelopment)
//                {
//                    sourcePath = @"C:\Users\dtuser7\Downloads\BulkSign\"; //hardcode the path to system directory to test locallly 
//                }
//                else
//                {
//                    sourcePath = model.SourcePath;
//                }

//                _logger.LogInformation($"Entered Source path is: {sourcePath}");

//                // Check if the source directory exists
//                if (!Directory.Exists(sourcePath))
//                {
//                    _logger.LogError($"Source directory does not exist: {sourcePath}");
//                    return new ServiceResult(false, $"Directory not found: {sourcePath}");
//                }

//                // Get the list of files in the directory
//                var files = Directory.GetFiles(sourcePath).Select(Path.GetFileName).ToList();

//                if (files.Count == 0)
//                {
//                    return new ServiceResult(false, "No files found in the directory.");
//                }

//                _logger.LogInformation($"Found {files.Count} files in {sourcePath}");

//                return new ServiceResult(true, "Files retrieved successfully", files);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Error accessing files: {ex.Message}");
//                return new ServiceResult(false, "An error occurred while retrieving files.");
//            }
//        }

//        public async Task<ServiceResult> UpdateBulkSignResultAsync(BulkSignCallBackDTO bulkSignCallBackDTO)
//        {
//            if (string.IsNullOrEmpty(bulkSignCallBackDTO.CorrelationId))
//            {
//                return new ServiceResult("Corelation id cannot be null");
//            }

//            try
//            {
//                var bulkSignData = await _unitOfWork.BulkSign.GetBulkSignDataByCorelationId(bulkSignCallBackDTO.CorrelationId);
//                if (bulkSignData == null)
//                {
//                    _logger.LogInformation("No details found");
//                    return new ServiceResult(true, "No details found.");
//                }
//                var result = JsonConvert.SerializeObject(bulkSignCallBackDTO);
//                Bulksign bulkSign = new()
//                {
//                    CorelationId = bulkSignCallBackDTO.CorrelationId,
//                    Result = result,
//                    Status = DocumentStatusConstants.InProgress,
//                    CompletedAt = DateTime.Now
//                };

//                var updateBulksignData = await _unitOfWork.BulkSign.UpdateBulkSignData(bulkSign);
//                if (!updateBulksignData)
//                {
//                    return new ServiceResult("Failed to update bulksign data");
//                }

//                return new ServiceResult(true, "Successfully received preparing bulksigning request");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("UpdateBulkSignResultAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while updating bulksigning request");
//        }

//        public async Task<ServiceResult> CompletedBulkSigningRequestAsync(string CorrelationId, BulkSignCallBackDTO bulkSignCallBackDTO)
//        {
//            if (string.IsNullOrEmpty(CorrelationId))
//            {
//                return new ServiceResult("Corelation id cannot be null");
//            }

//            try
//            {

//                var bulkSignData = await _unitOfWork.BulkSign.GetBulkSignDataByCorelationId(CorrelationId);
//                if (bulkSignData == null)
//                {
//                    _logger.LogInformation("No details found");
//                    return new ServiceResult(true, "No details found.");
//                }
//                var result = JsonConvert.SerializeObject(bulkSignCallBackDTO);
//                Bulksign bulkSign = new Bulksign
//                {
//                    CorelationId = CorrelationId,
//                    Result = result,
//                    Status = DocumentStatusConstants.Completed,
//                };

//                var updateBulksignData = await _unitOfWork.BulkSign.UpdateBulkSignData(bulkSign);
//                if (!updateBulksignData)
//                {
//                    return new ServiceResult("Failed to update bulksign data");
//                }

//                return new ServiceResult(true, "Successfully update bulksigning request status");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("FailBulkSigningRequestAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while updating bulksigning request status");
//        }

//    }
//}
