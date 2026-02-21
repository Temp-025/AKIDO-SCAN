//using EnterpriseGatewayPortal.Core.Constants;
//using EnterpriseGatewayPortal.Core.Domain.Models;
//using EnterpriseGatewayPortal.Core.Domain.Repositories;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication.Documents;
//using EnterpriseGatewayPortal.Core.DTOs;
//using EnterpriseGatewayPortal.Core.Persistance.Repositories;
//using EnterpriseGatewayPortal.Core.Utilities;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json.Serialization;
//using NuGet.Common;
//using System.Net;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Xml.Linq;
//using static EnterpriseGatewayPortal.Core.Domain.Services.Communication.CommonResponse;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class DocumentService : IDocumentService
//    {
//        private readonly ILogger<DocumentService> _logger;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IPaymentService _paymentService;
//        private readonly IConfiguration _configuration;
//        private readonly HttpClient _client;
//        public DocumentService(ILogger<DocumentService> logger,
//                                    IUnitOfWork unitOfWork,
//                                    IPaymentService paymentService,
//                                    IConfiguration configuration,
//                                    HttpClient httpClient
//                                    )
//        {
//            _logger = logger;
//            _unitOfWork = unitOfWork;
//            _paymentService = paymentService;
//            _configuration = configuration;
//            _client = httpClient;
//        }

//        public async Task<ServiceResult> GetDocumentDetaildByIdAsync(string id)
//        {
//            if (id == null)
//            {
//                return new ServiceResult("Id cannot be null");
//            }

//            try
//            {
//                var response = await _unitOfWork.Document.GetDocumentById(id);
//                if (response == null)
//                {
//                    return new ServiceResult("Document not found");
//                }

//                return new ServiceResult(true, "Successfully received documents", response);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("GetDocumentDetaildByIdAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while receiving document");
//        }

//        public async Task<ServiceResult> GetDraftDocumentListAsync(UserDTO userDTO)
//        {
//            if (userDTO == null)
//            {
//                return new ServiceResult("Email cannot be null");
//            }

//            try
//            {
//                var documents = await _unitOfWork.Document.GetDocumentListAsync(userDTO.Suid, userDTO.AccountType, userDTO.OrganizationId);
//                if (documents == null)
//                {
//                    _logger.LogError("Failed to receive draft document list.");
//                    return new ServiceResult("Failed to receive draft document list.");
//                }
//                return new ServiceResult(true, "Successfully received draft document list.", documents);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("GetDraftDocumentListAsync Exception :  {0}", ex.Message);
//            }
//            return new ServiceResult("GetDraftDocumentListAsync Exception");
//        }

//        public async Task<ServiceResult> GetDocumentDisplayDetaildByIdAsync(string id)
//        {
//            if (id == null)
//            {
//                _logger.LogInformation("Id cannot be null");
//                return new ServiceResult("Id cannot be null");
//            }

//            try
//            {
//                var response = await _unitOfWork.Document.GetDocumentById(id);
//                if (response == null)
//                {
//                    _logger.LogInformation("Document not found");
//                    return new ServiceResult("Document not found");
//                }

//                var serviceResult = await _unitOfWork.FileStorage.GetFileStorageById(response.FileId);
//                if (serviceResult == null)
//                {
//                    _logger.LogInformation("File not Found");
//                    return new ServiceResult("File not Found");
//                }

//                DocDisplayDTO? docDisplay = new()
//                {
//                    DocumentName = response.DocumentName,
//                    OwnerName = response.OwnerName,
//                    OwnerEmail = response.OwnerEmail,
//                    Status = response.Status,
//                    SignatoryCount = (int)response.RecepientCount,
//                    Document = Convert.ToBase64String(serviceResult.File)
//                };

//                var verifySignedDocumentDTO = new VerifySignedDocumentDTO()
//                {
//                    docData = docDisplay.Document,
//                    suid = response.OwnerId,
//                    email = response.OwnerEmail,
//                };

//                var verify = await VerifySignedDocumentAsync(verifySignedDocumentDTO);
//                if (verify == null)
//                {
//                    _logger.LogInformation(verify.Message);
//                    return new ServiceResult(verify.Message);
//                }

//                docDisplay.VerificationDetails = (VerifySigningRequestResponse)verify.Resource;

//                return new ServiceResult(true, "Successfully received documents", docDisplay);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("GetDocumentDetaildByIdAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while receiving document");
//        }

//        private async Task<ServiceResult> VerifySignedDocumentAsync(VerifySignedDocumentDTO signedDocument)
//        {
//            if (signedDocument == null)
//            {
//                return new ServiceResult("Signed document cannot be null");
//            }

//            try
//            {
//                var data = new VerifySigningDTO
//                {
//                    documentType = "PADES",
//                    docData = signedDocument.docData,
//                    signature = null,
//                    subscriberUid = signedDocument.suid,
//                    //remove blow object for new api
//                    digitalSignatureParams = new Digitalsignatureparams
//                    {
//                        verificationContext = new Verificationcontext
//                        {
//                            reportType = 2
//                        }
//                    }
//                };

//                string json = JsonConvert.SerializeObject(data);
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                var response = await _client.PostAsync(_configuration.GetValue<string>("Config:SignVerifyUrl"), content);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse? apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        VerifySignedDocumentApiResponse? responseObj = JsonConvert.DeserializeObject<VerifySignedDocumentApiResponse>(apiResponse.Result.ToString());
//                        VerifySigningRequestResponse verifysigningResponse = new VerifySigningRequestResponse
//                        {
//                            recpList = responseObj.signatureVerificationDetails,
//                            signCount = responseObj.signatureVerificationDetails.Count
//                        };
//                        return new ServiceResult(true, "Successfully verified signing request", verifysigningResponse);
//                    }
//                    else
//                    {
//                        _logger.LogError("VerifySignedDocumentAsync : {0} ", apiResponse.Message);
//                        return new ServiceResult(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}" + $" error = {response.ReasonPhrase}");
//                    return new ServiceResult("Failed to verify signing request");
//                }

//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("VerifySignedDocumentAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("Failed to verify signed document");
//        }

//        public async Task<ServiceResult> DeleteDocumentByIdListAsync(List<string> idList)
//        {
//            if (idList == null)
//            {
//                return new ServiceResult("List of id cannot be empty");
//            }

//            try
//            {
//                await _unitOfWork.Document.DeleteDocumentsByIdsAsync(idList);
//                await _unitOfWork.Recepient.DeleteRecepientsByTempId(idList);

//                return new ServiceResult(true, "Documents deleted successfully");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("DeleteDocumentByIdListAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while deleting document");
//        }

//        public async Task<ServiceResult> DownloadSignedDocumentAsync(string fileID)
//        {
//            if (fileID == null)
//            {
//                return new ServiceResult("File Id cannot be null");
//            }

//            try
//            {
//                var document = await _unitOfWork.FileStorage.GetFileStorageById(fileID);
//                if (document == null)
//                {
//                    _logger.LogError("Signed Document not found.");
//                    return new ServiceResult("Signed Document not found.");
//                }
//                return new ServiceResult(true, "Successfully recieved signed document.", document);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("DownloadSignedDocumentAsync Exception :  {0}", ex.Message);
//            }
//            return new ServiceResult("DownloadSignedDocumentAsync Exception");
//        }

//        public async Task<ServiceResult> SaveNewDocumentAsync(SaveNewDocumentDTO document, UserDTO userDetails)
//        {
//            // Get Start Time
//            DateTime startTime = DateTime.Now;

//            if (document.file == null)
//            {
//                return new ServiceResult("File cannot be null");
//            }

//            if (document.model == null)
//            {
//                return new ServiceResult("Model cannot be null");
//            }

//            try
//            {

//                JObject esealCoordinatesObj = null;
//                string qrCords = string.Empty;
//                var tempId = "";
//                Model modelObj = JsonConvert.DeserializeObject<Model>(document.model);
//                JObject signCoordinatesObj = JObject.Parse(document.model);

//                var signCords = signCoordinatesObj["signCords"].ToString();
//                var esealCords = signCoordinatesObj["esealCords"].ToString();
//                if (modelObj.qrCodeRequired)
//                    qrCords = signCoordinatesObj["qrCords"].ToString();

//                if (!string.IsNullOrEmpty(signCords))
//                {
//                    modelObj.signCords.coordinates = JsonConvert.SerializeObject(signCoordinatesObj["signCords"]);
//                    //modelObj.esealCords.coordinates
//                }

//                if (!string.IsNullOrEmpty(esealCords))
//                {
//                    modelObj.esealCords.coordinates = JsonConvert.SerializeObject(signCoordinatesObj["esealCords"]);

//                    esealCoordinatesObj = JObject.Parse(signCoordinatesObj["esealCords"].ToString());
//                }

//                if (!string.IsNullOrEmpty(qrCords))
//                {
//                    modelObj.QrCords.coordinates = JsonConvert.SerializeObject(signCoordinatesObj["qrCords"]);
//                }

//                //============================================

//                var isEsealPresent = false;
//                StringComparison comp = StringComparison.OrdinalIgnoreCase;
//                if (!string.IsNullOrEmpty(esealCords) &&
//                    modelObj.docdetails.receps[0].eseal &&
//                    signCoordinatesObj["esealCords"].ToString().Contains(userDetails.Suid, comp))
//                {
//                    isEsealPresent = true;
//                }

//                var isSignaturePresent = false;
//                StringComparison comp1 = StringComparison.OrdinalIgnoreCase;
//                if (!string.IsNullOrEmpty(signCords) &&
//                    signCoordinatesObj["signCords"].ToString().Contains(userDetails.Suid, comp1))
//                {
//                    isSignaturePresent = true;
//                }

//                if (!isSignaturePresent && !isEsealPresent) //if both flags false then it is quick sign
//                {
//                    isSignaturePresent = true;
//                }

//                //check credit available or not
//                var res = await _paymentService.IsCreditAvailable(userDetails, isEsealPresent, isSignaturePresent);
//                if (res.Success && !(bool)res.Resource)
//                {
//                    return new ServiceResult(res.Message);
//                }

//                //============================================

//                var documentData = new Document
//                {
//                    DocumentId = Guid.NewGuid().ToString(),
//                    DocumentName = modelObj.docdetails.tempname,
//                    OwnerId = userDetails.Suid,
//                    OwnerEmail = userDetails.Email,
//                    OwnerName = modelObj.docdetails.ownerName,
//                    DaysToComplete = modelObj.docdetails.daysToComplete,
//                    Status = DocumentStatusConstants.InProgress,
//                    Annotations = modelObj?.signCords?.coordinates,
//                    EsealAnnotations = modelObj?.esealCords?.coordinates,
//                    QrcodeAnnotations = modelObj?.QrCords?.coordinates,
//                    ExpireDate = modelObj.docdetails.expiredate,
//                    RecepientCount = modelObj.docdetails.receps.Count,
//                    IsDocumentBlocked = false,
//                    CreateTime = DateTime.Now,
//                    CreatedAt = DateTime.Now,
//                    UpdatedAt = DateTime.Now,
//                    OrganizationId = userDetails.OrganizationId,
//                    OrganizationName = userDetails.OrganizationName,
//                    AccountType = userDetails.AccountType,
//                    HtmlSchema = modelObj.htmlSchema,
//                };

//                Filestorage fileStorage = new() { Fileid = Guid.NewGuid().ToString() };

//                using (var stream = document.file.OpenReadStream())
//                using (var memoryStream = new MemoryStream())
//                {
//                    await stream.CopyToAsync(memoryStream);
//                    fileStorage.File = memoryStream.ToArray();
//                    // Store pdfBytes in the database
//                }

//                var storedFile = await _unitOfWork.FileStorage.SaveFileStorage(fileStorage);
//                if (storedFile == null)
//                {
//                    return new ServiceResult("Error occured while saving the file");
//                }

//                documentData.FileId = storedFile.Fileid;

//                var receps = modelObj.docdetails.receps;
//                IList<PendingSignListDTO> pendingSigns = new List<PendingSignListDTO>();
//                foreach (var recep in receps)
//                {
//                    pendingSigns.Add(new PendingSignListDTO()
//                    {
//                        email = recep.email.ToLower(),
//                        suid = recep.suid
//                    });
//                }

//                documentData.PendingSignList = JsonConvert.SerializeObject(pendingSigns);

//                _logger.LogInformation("documentData : " + JsonConvert.SerializeObject(documentData));

//                var savedDocument = await _unitOfWork.Document.SaveDocument(documentData);



//                IList<Recepient> recepientsList = new List<Recepient>();
//                Recepient recepients;

//                foreach (var recep in receps)
//                {
//                    recepients = new Recepient
//                    {
//                        Recepientid = Guid.NewGuid().ToString(),
//                        Name = recep.name,
//                        Suid = recep.suid,
//                        Email = recep.email.ToLower(),
//                        Order = recep.order,
//                        Tempid = savedDocument.DocumentId,
//                        Createdat = DateTime.Now,
//                        Updatedat = DateTime.Now,
//                        Organizationid = recep.orgUID,
//                        Organizationname = recep.orgName,
//                        Status = RecepientStatus.NeedToSign,
//                    };

//                    if (string.IsNullOrEmpty(recep.orgUID.Trim()))
//                    {
//                        recepients.Accounttype = AccountTypeConstants.Self;
//                    }
//                    else
//                    {
//                        recepients.Accounttype = AccountTypeConstants.Organization;
//                    }

//                    StringComparison comp2 = StringComparison.OrdinalIgnoreCase;
//                    if (!string.IsNullOrEmpty(esealCords) && recep.eseal && signCoordinatesObj["esealCords"].ToString().Contains(recep.suid, comp2))
//                    {
//                        var obj = esealCoordinatesObj[recep.suid.ToLower()].ToString();

//                        if (!string.IsNullOrEmpty(obj))
//                        {
//                            JObject currentRecepientEsealObj = JObject.Parse(obj);
//                            recepients.Esealorgid = currentRecepientEsealObj["organizationID"].ToString();
//                        }
//                    }

//                    recepientsList.Add(recepients);

//                }

//                var savedRecepients = await _unitOfWork.Recepient.SaveRecepientsAsync(recepientsList);


//                tempId = savedDocument.DocumentId;

//                _logger.LogInformation("SaveNewDocumentAsync function time execution: {0}" + DateTime.Now.Subtract(startTime).TotalSeconds);

//                return new ServiceResult(true, "Successfully saved document", tempId);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("SaveNewDocumentAsync Exception :  {0}", ex.Message);
//            }

//            _logger.LogInformation("SaveNewDocumentAsync function time execution: {0}" + DateTime.Now.Subtract(startTime).TotalSeconds);
//            return new ServiceResult("An error occurred while saving document");
//        }

//        public async Task<ServiceResult> SendSigningRequestAsync(SigningRequestDTO signingRequest, UserDTO userDTO, string accessToken)
//        {
//            bool isEsealPresent = false;
//            bool isSignaturePresent = false;
//            SigningRequestModel modelObj = null;
//            bool unBlockDoc = false;
//            HttpResponseMessage response;


//            if (signingRequest == null)
//            {
//                return new ServiceResult("Signing request data cannot be null");
//            }
//            if (signingRequest.signfile == null)
//            {
//                return new ServiceResult("File cannot be null");
//            }

//            if (signingRequest.model == null)
//            {
//                return new ServiceResult("Model cannot be null");
//            }

//            try
//            {
//                _logger.LogInformation($"Signing Request Object : {signingRequest.model}");

//                modelObj = JsonConvert.DeserializeObject<SigningRequestModel>(signingRequest.model);

//                if (string.IsNullOrEmpty(modelObj.actoken))
//                {
//                    return new ServiceResult("Access token cannot be null");
//                }

//                var fileName = signingRequest.signfile.FileName;

//                SignatureDTO data = new SignatureDTO()
//                {
//                    id = userDTO.Email,
//                    documentType = "PADES"

//                };

//                if (modelObj.pageNumber != null && modelObj.posX != 0 && modelObj.posY != 0)
//                {
//                    data.placeHolderCoordinates = new placeHolderCoordinates
//                    {
//                        pageNumber = modelObj.pageNumber?.ToString(),
//                        signatureXaxis = modelObj.posX?.ToString(),
//                        signatureYaxis = modelObj.posY?.ToString()
//                    };
//                }

//                if (modelObj.signVisible == 0)
//                {
//                    data.placeHolderCoordinates = new placeHolderCoordinates
//                    {
//                        pageNumber = modelObj.pageNumber?.ToString(),
//                        signatureXaxis = modelObj.posX?.ToString(),
//                        signatureYaxis = modelObj.posY?.ToString()
//                    };
//                }

//                var documentData = await _unitOfWork.Document.GetDocumentById(modelObj.tempid);
//                if (documentData == null)
//                {
//                    return new ServiceResult("Document Record not Found");
//                }
//                var fileStore = await _unitOfWork.FileStorage.GetFileStorageById(documentData.FileId);
//                if (fileStore == null)
//                {
//                    return new ServiceResult("File not found");
//                }

//                if (modelObj.signVisible > 0)
//                {
//                    if (modelObj.pageNumber != null && modelObj.posX != null && modelObj.posY != null)
//                    {
//                        isSignaturePresent = true;
//                    }

//                    if (modelObj.EsealPageNumber != null && modelObj.EsealPosX != null && modelObj.EsealPosY != null &&
//                        modelObj.EsealPageNumber > 0 && modelObj.EsealPosX > 0 && modelObj.EsealPosY > 0)
//                    {
//                        data.esealPlaceHolderCoordinates = new esealplaceHolderCoordinates
//                        {
//                            pageNumber = modelObj.EsealPageNumber?.ToString(),
//                            signatureXaxis = modelObj.EsealPosX?.ToString(),
//                            signatureYaxis = modelObj.EsealPosY?.ToString()
//                        };
//                        isEsealPresent = true;
//                    }
//                }
//                else
//                {
//                    isSignaturePresent = true;
//                }

//                //check credit available or not
//                var res = await _paymentService.IsCreditAvailable(userDTO, isEsealPresent, isSignaturePresent);
//                if (res.Success && !(bool)res.Resource)
//                {
//                    _logger.LogInformation("Credits not available for " + userDTO.Email);
//                    return new ServiceResult(res.Message);
//                }

//                var recepient = await _unitOfWork.Recepient.GetRecepientsBySuidAndTempId(modelObj.suid, modelObj.tempid);
//                if (recepient.Takenaction == true)
//                {
//                    _logger.LogInformation("Document Id : " + modelObj.tempid);
//                    _logger.LogInformation("Document has signed already.");
//                    return new ServiceResult("Document has signed already.");
//                }

//                _client.DefaultRequestHeaders.Add("UgPassAuthorization", "Bearer " + accessToken);




//                if (modelObj.QrPageNumber != null && modelObj.QrPosX != null && modelObj.QrPosY != null &&
//                    modelObj.QrPageNumber > 0 && modelObj.QrPosX > 0 && modelObj.QrPosY > 0)
//                {

//                    CriticalData criticalData = new()
//                    {
//                        entityName = string.IsNullOrEmpty(modelObj.entityName) ? string.Empty : modelObj.entityName,
//                        docSerialNo = string.IsNullOrEmpty(modelObj.docSerialNo) ? string.Empty : modelObj.docSerialNo,
//                        faceRequired = modelObj.faceRequired
//                    };

//                    var qrCodeData = new
//                    {
//                        document_id = modelObj.tempid,
//                        critical_data = criticalData // Directly assign the object
//                    };

//                    var signatories = documentData.Recepients.Select(recep => new Signatories
//                    {
//                        name = recep.Name,
//                        orgname = string.IsNullOrEmpty(recep.Organizationname) ? "" : recep.Organizationname
//                    }).ToList();

//                    if (isEsealPresent)
//                    {
//                        string organizationName = string.IsNullOrEmpty(userDTO.OrganizationName) ? "" : userDTO.OrganizationName;

//                        signatories.Add(new Signatories { name = organizationName, orgname = organizationName });
//                    }




//                    var publicDataList = new List<string>();

//                    foreach (var signatory in signatories)
//                    {
//                        if (!string.IsNullOrWhiteSpace(signatory.name))
//                            publicDataList.Add(signatory.name);

//                        if (!string.IsNullOrWhiteSpace(signatory.orgname))
//                            publicDataList.Add(signatory.orgname);
//                    }

//                    var privateDataList = new List<string>();
//                    privateDataList.Add(modelObj.tempid);
//                    privateDataList.Add(string.IsNullOrEmpty(modelObj.entityName) ? string.Empty : modelObj.entityName);
//                    privateDataList.Add(string.IsNullOrEmpty(modelObj.docSerialNo) ? string.Empty : modelObj.docSerialNo);

//                    string base64File;
//                    using (var memoryStream = new MemoryStream())
//                    {
//                        await signingRequest.signfile.CopyToAsync(memoryStream);
//                        byte[] fileBytes = memoryStream.ToArray();
//                        base64File = Convert.ToBase64String(fileBytes);
//                    }
//                    //var credentialId = _configuration.GetValue<string>("QRCredentialId");
//                    //if (_configuration.GetValue<bool>("EncryptionEnabled"))
//                    //{
//                    //    credentialId = PKIMethods.Instance.
//                    //            PKIDecryptSecureWireData(credentialId);
//                    //};
//                    var qrDto = new QRCodeSigningSendDTO
//                    {
//                        // publicData = publicDataList,
//                        // privateData = privateDataList,
//                        publicData = publicDataList,
//                        privateData = privateDataList,
//                        document = base64File,
//                        //credentialId = credentialId,
//                        credentialId = _configuration.GetValue<string>("QRCredentialId"),
//                        qrPlaceHolderCoordinates = new QrCodeCoordinates
//                        {
//                            x = modelObj.QrPosX.ToString(),
//                            y = modelObj.QrPosY.ToString(),
//                            pageNumber = modelObj.QrPageNumber.ToString()
//                        }
//                    };

//                    string jsonBody = JsonConvert.SerializeObject(qrDto, Formatting.Indented);

//                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

//                    var qrUrl = _configuration.GetValue<string>("Config:QrGeneratorUrl");

//                    DateTime startTimeForAPI = DateTime.Now;
//                    _logger.LogInformation("Qr code generator call start : " + startTimeForAPI);

//                    HttpResponseMessage qrResponse = await _client.PostAsync(qrUrl, content);

//                    DateTime endTimeForAPI = DateTime.Now;
//                    _logger.LogInformation("Qr code generator call end : " + endTimeForAPI);
//                    _logger.LogInformation("Total time taken to update SignService call in total seconds : {0} ",
//                        (endTimeForAPI - startTimeForAPI).TotalSeconds);

//                    if (qrResponse.StatusCode == HttpStatusCode.OK)
//                    {
//                        APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await qrResponse.Content.ReadAsStringAsync());
//                        if (apiResponse.Success)
//                        {
//                            signingRequest.signfile = ConvertBase64ToIFormFile(apiResponse.Result.ToString(), signingRequest.signfile.FileName);
//                        }
//                        else
//                        {
//                            documentData.UpdatedAt = DateTime.Now;

//                            documentData.CompleteTime = DateTime.Now;

//                            if (apiResponse.Message == "Request time out")
//                            {
//                                documentData.Status = DocumentStatusConstants.InProgress;
//                            }
//                            else
//                            {
//                                documentData.Status = DocumentStatusConstants.Failed;
//                            }
//                            var updatedDoc = await _unitOfWork.Document.UpdateDocumentStatusById(documentData);
//                            if (!updatedDoc)
//                            {
//                                _logger.LogError("Failed to update document");
//                            }

//                            recepient.Name = modelObj.userName;
//                            recepient.Updatedat = DateTime.Now;
//                            recepient.Accesstoken = modelObj.actoken;
//                            recepient.Signedby = userDTO.Email;
//                            recepient.Organizationname = userDTO.OrganizationName;
//                            recepient.Organizationid = userDTO.OrganizationId;
//                            recepient.Accounttype = userDTO.AccountType;

//                            var updateRecepient = await _unitOfWork.Recepient.UpdateRecepientsById(recepient);
//                            if (updateRecepient == false)
//                            {
//                                _logger.LogError("Failed to update recepient");
//                            }

//                            unBlockDoc = true;

//                            _logger.LogInformation("Document Id : " + modelObj.tempid);
//                            _logger.LogError("Signing request response : " + apiResponse.Message);
//                            _logger.LogError("SendSigningRequestAsync response");

//                            return new ServiceResult(apiResponse.Message);
//                        }
//                    }
//                }


//                ////////////////////QR generator End/////////////////////


//                if ((bool)documentData.IsDocumentBlocked)
//                {
//                    _logger.LogInformation("Document Id : " + modelObj.tempid);
//                    _logger.LogInformation("Document is blocked.");
//                    return new ServiceResult("Document signing is in progress please retry after 10 minutes");
//                }
//                else
//                {
//                    var update = await _unitOfWork.Document.UpdateDocumentBlockedStatusAsync(modelObj.tempid, true);
//                    if (!update)
//                    {
//                        _logger.LogError("Failed to update document blocked status");
//                    }
//                }

//                if (!fileName.Contains(".pdf"))
//                {
//                    fileName += ".pdf";
//                }



//                var url = _configuration.GetValue<string>("Config:SignService");

//                using (var multipartFormContent = new MultipartFormDataContent())
//                {
//                    StreamContent fileStreamContent = new StreamContent(signingRequest.signfile.OpenReadStream());
//                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

//                    //Add the file
//                    multipartFormContent.Add(fileStreamContent, name: "multipartFile", fileName: fileName + ".pdf");
//                    multipartFormContent.Add(new StringContent(JsonConvert.SerializeObject(data)), "model");
//                    var jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
//                    Console.WriteLine(jsonString);

//                    _logger.LogInformation("actoken : " + modelObj.actoken);
//                    _logger.LogInformation("Document Id : " + modelObj.tempid);

//                    DateTime startTimeForAPI = DateTime.Now;
//                    _logger.LogInformation("Signing service call start : " + startTimeForAPI);
//                    response = await _client.PostAsync(url, multipartFormContent);
//                    DateTime endTimeForAPI = DateTime.Now;
//                    TimeSpan diff = endTimeForAPI - startTimeForAPI;
//                    _logger.LogInformation("Signing service call end : " + endTimeForAPI);
//                    _logger.LogInformation("Total time taken to update SignService call in total seconds : {0} ", diff.TotalSeconds);
//                    _logger.LogInformation("Document Id : " + modelObj.tempid);
//                }
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {

//                        _logger.LogInformation("Document Id : " + modelObj.tempid);
//                        // _logger.LogInformation("Signed request Corelation Id :{0} ", apiResponse.Result.ToString());
//                        _logger.LogInformation("Signed request Recepient Email Id :{0} ", modelObj.userEmail.ToLower());
//                        _logger.LogInformation("Signed request Recepient Tempid :{0} ", modelObj.tempid);

//                        fileStore.File = Convert.FromBase64String(apiResponse.Result.ToString());
//                        documentData.UpdatedAt = DateTime.Now;
//                        documentData.Status = DocumentStatusConstants.Completed;
//                        documentData.CompleteTime = DateTime.Now;
//                        documentData.CompleteSignList = documentData.PendingSignList;
//                        documentData.PendingSignList = "[]";


//                        var updatedFile = await _unitOfWork.FileStorage.UpdateFileStorageByFileId(fileStore);
//                        if (!updatedFile)
//                        {
//                            _logger.LogError("Failed to update file");
//                        }

//                        var updatedDoc = await _unitOfWork.Document.UpdateDocumentStatusById(documentData);
//                        if (!updatedDoc)
//                        {
//                            _logger.LogError("Failed to update document");
//                        }

//                        //recepient.Suid = modelObj.suid;
//                        recepient.Name = modelObj.userName;
//                        recepient.Updatedat = DateTime.Now;
//                        recepient.Accesstoken = modelObj.actoken;
//                        recepient.Signedby = userDTO.Email;
//                        recepient.Organizationname = userDTO.OrganizationName;
//                        recepient.Organizationid = userDTO.OrganizationId;
//                        recepient.Accounttype = userDTO.AccountType;

//                        var updateRecepient = await _unitOfWork.Recepient.UpdateRecepientsById(recepient);
//                        if (updateRecepient == false)
//                        {
//                            _logger.LogError("Failed to update recepient");
//                        }

//                        await _unitOfWork.Recepient.UpdateTakenActionOfRecepientById(recepient.Recepientid);

//                        return new ServiceResult(true, "Document signed successfully");
//                    }
//                    else
//                    {
//                        ///FAIL CASE

//                        documentData.UpdatedAt = DateTime.Now;

//                        documentData.CompleteTime = DateTime.Now;


//                        documentData.Status = DocumentStatusConstants.InProgress;

//                        var updatedDoc = await _unitOfWork.Document.UpdateDocumentStatusById(documentData);
//                        if (!updatedDoc)
//                        {
//                            _logger.LogError("Failed to update document");
//                        }

//                        recepient.Name = modelObj.userName;
//                        recepient.Updatedat = DateTime.Now;
//                        recepient.Accesstoken = modelObj.actoken;
//                        recepient.Signedby = userDTO.Email;
//                        recepient.Organizationname = userDTO.OrganizationName;
//                        recepient.Organizationid = userDTO.OrganizationId;
//                        recepient.Accounttype = userDTO.AccountType;

//                        var updateRecepient = await _unitOfWork.Recepient.UpdateRecepientsById(recepient);
//                        if (updateRecepient == false)
//                        {
//                            _logger.LogError("Failed to update recepient");
//                        }

//                        unBlockDoc = true;

//                        _logger.LogInformation("Document Id : " + modelObj.tempid);
//                        _logger.LogError("Signing request response : " + apiResponse.Message);
//                        _logger.LogError("SendSigningRequestAsync response");

//                        return new ServiceResult(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    ///FAIL CASE


//                    documentData.UpdatedAt = DateTime.Now;
//                    documentData.Status = DocumentStatusConstants.InProgress;
//                    documentData.CompleteTime = DateTime.Now;

//                    var updatedDoc = await _unitOfWork.Document.UpdateDocumentStatusById(documentData);
//                    if (!updatedDoc)
//                    {
//                        _logger.LogError("Failed to update document");
//                    }

//                    recepient.Name = modelObj.userName;
//                    recepient.Updatedat = DateTime.Now;
//                    recepient.Accesstoken = modelObj.actoken;
//                    recepient.Signedby = userDTO.Email;
//                    recepient.Organizationname = userDTO.OrganizationName;
//                    recepient.Organizationid = userDTO.OrganizationId;
//                    recepient.Accounttype = userDTO.AccountType;

//                    var updateRecepient = await _unitOfWork.Recepient.UpdateRecepientsById(recepient);
//                    if (updateRecepient == false)
//                    {
//                        _logger.LogError("Failed to update recepient");
//                    }

//                    unBlockDoc = true;

//                    _logger.LogInformation("Document Id : " + modelObj.tempid);
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}" + $" error = {response.ReasonPhrase}");
//                    return new ServiceResult("Signing failed");
//                }
//            }
//            catch (Exception ex)
//            {
//                unBlockDoc = true;

//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("SendSigningRequestAsync Exception :  {0}", ex.Message);
//            }
//            finally
//            {

//                if (unBlockDoc)
//                {
//                    var update = await _unitOfWork.Document.UpdateDocumentBlockedStatusAsync(modelObj.tempid, false);
//                    if (!update)
//                    {
//                        _logger.LogError("Failed to update document blocked status");
//                    }
//                }

//            }

//            return new ServiceResult("Signing request failed");
//        }
//        public async Task<ServiceResult> GetInitialPreviewImgAsync(string token)
//        {
//            var signingUrl = _configuration["SigningPortalUrl"];
//            HttpClient _client = new HttpClient();

//            _client.Timeout = TimeSpan.FromMinutes(10);

//            _client.DefaultRequestHeaders.Add("x-access-token", token);
//            try
//            {
//                string relativePath = "api/template/getsignaturepreviewimage";
//                Uri fullUrl = new Uri(new Uri(signingUrl), relativePath);
//                string url = $"{fullUrl}";

//                HttpResponseMessage response = await _client.PostAsync(url, null);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
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
//                           $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return new ServiceResult(false, "An error occurred while receiving initial preview image. Please try later.");
//        }

//        public async Task<ServiceResult> GetSignaturePreviewAsync(UserDTO user)
//        {
//            try
//            {
//                _logger.LogInformation("GetSignaturePreviewAsync start");
//                _client.BaseAddress = new Uri(_configuration["APIServiceLocations:SignatureTemplates"]);

//                JObject keyValuePairs = new JObject();
//                keyValuePairs.Add("sUid", user.Suid);
//                keyValuePairs.Add("oUid", user.OrganizationId);
//                keyValuePairs.Add("accountType", user.AccountType.ToLower());

//                string json = JsonConvert.SerializeObject(keyValuePairs,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                _logger.LogInformation("GetSignaturePreviewAsync api call start");
//                HttpResponseMessage response = await _client.PostAsync($"api/digital/signature/post/preview", content);
//                _logger.LogInformation("GetSignaturePreviewAsync api call end");

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation("GetSignaturePreviewAsync end");
//                        return new ServiceResult(true, "Successfully received signature preview image", apiResponse.Result.ToString().Replace("\r\n", ""));
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        _logger.LogInformation("GetSignaturePreviewAsync end");
//                        return new ServiceResult(apiResponse.Message);
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

//            _logger.LogInformation("GetSignaturePreviewAsync end");
//            return new ServiceResult("Failed to receive signature preview image");
//        }

//        public async Task<ServiceResult> DeclineDocumentSigningAsync(string tempId, string suid, string comment)
//        {
//            try
//            {
//                var updateRecepient = await _unitOfWork.Recepient.DeclineSigningAsync(tempId, suid, comment);
//                if (updateRecepient == false)
//                {
//                    _logger.LogError("Failed to update recepient");
//                    return new ServiceResult(false, "Document is not declined");
//                }

//                var documentData = await _unitOfWork.Document.GetDocumentById(tempId);
//                if (documentData == null)
//                {
//                    return new ServiceResult(false, "Document Record not Found");
//                }
//                documentData.UpdatedAt = DateTime.Now;
//                documentData.Status = DocumentStatusConstants.Declined;
//                documentData.CompleteTime = DateTime.Now;
//                var updatedDoc = await _unitOfWork.Document.UpdateDocumentStatusById(documentData);
//                if (!updatedDoc)
//                {
//                    _logger.LogError("Failed to update document status");
//                }

//                return new ServiceResult(true, "Successfully declined signing");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("DeclineDocumentSigningAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while decline signing");
//        }

//        public async Task<ServiceResult> RecallDocumentToSignAsync(string docId)
//        {
//            try
//            {

//                var documentData = await _unitOfWork.Document.GetDocumentById(docId);
//                if (documentData == null)
//                {
//                    return new ServiceResult(false, "Document Record not Found");
//                }
//                documentData.UpdatedAt = DateTime.Now;
//                documentData.Status = DocumentStatusConstants.Recalled;
//                documentData.CompleteTime = DateTime.Now;
//                var updatedDoc = await _unitOfWork.Document.UpdateDocumentStatusById(documentData);
//                if (!updatedDoc)
//                {
//                    _logger.LogError("Failed to update document status to recalled");
//                }

//                return new ServiceResult(true, "Successfully recalled document to sign");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//                _logger.LogError("RecallDocumentToSignAsync Exception :  {0}", ex.Message);
//            }

//            return new ServiceResult("An error occurred while recalling the document");
//        }

//        public static IFormFile ConvertBase64ToIFormFile(string base64String, string fileName, string contentType = "application/octet-stream")
//        {
//            var base64Data = base64String.Contains(",") ? base64String.Substring(base64String.IndexOf(",") + 1) : base64String;

//            byte[] fileBytes = Convert.FromBase64String(base64Data);

//            var stream = new MemoryStream(fileBytes);

//            IFormFile file = new FormFile(stream, 0, fileBytes.Length, "file", fileName)
//            {
//                Headers = new HeaderDictionary(),
//                ContentType = contentType
//            };

//            return file;
//        }

//    }
//}
