//using EnterpriseGatewayPortal.Core.Domain.Models;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using EnterpriseGatewayPortal.Core.Services;
//using EnterpriseGatewayPortal.Core.Utilities;
//using EnterpriseGatewayPortal.Web.Attribute;
//using EnterpriseGatewayPortal.Web.ViewModel.Documents;
//using EnterpriseGatewayPortal.Web.ViewModel.DocumentTemplates;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;
//using Roles = EnterpriseGatewayPortal.Web.ViewModel.Documents.Roles;
//using EnterpriseGatewayPortal.Web.Models.Users;
//using DocumentFormat.OpenXml.Wordprocessing;
//using Document = EnterpriseGatewayPortal.Core.Domain.Models.Document;
//using EnterpriseGatewayPortal.Web.Constants;
//using EnterpriseGatewayPortal.Web.Enums;
//using EnterpriseGatewayPortal.Web.ExtensionMethods;
//using DocumentFormat.OpenXml.Office2010.Excel;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using EnterpriseGatewayPortal.Web.Models;
//using DocumentFormat.OpenXml.Drawing.Charts;

//namespace EnterpriseGatewayPortal.Web.Controllers
//{
//    [ServiceFilter(typeof(SessionValidationAttribute))]
//    [Authorize]
//    public class DocumentsController : BaseController
//    {

//        private readonly IConfiguration _configuration;
//        private readonly IDocumentSigningService _documentSigningService;
//        private readonly ILocalBusinessUsersService _localBusinessUsersService;
//        private readonly IDocumentService _documentService;
//        private readonly ITemplateService _templateService;


//        public DocumentsController(IAdminLogService adminLogService, ITemplateService templateService, IDocumentSigningService documentSigningService, ILocalBusinessUsersService localBusinessUsersService, IDocumentService documentService,
//            IConfiguration configuration

//           ) : base(adminLogService)
//        {
//            _documentService = documentService;
//            _documentSigningService = documentSigningService;
//            _localBusinessUsersService = localBusinessUsersService;
//            _configuration = configuration;
//            _templateService = templateService;


//        }
//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            string logMessage;

//            var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
//            var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);

//            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);

//            var response = await _documentService.GetDraftDocumentListAsync(userDTO);
//            if (response == null || !response.Success)
//            {
//                logMessage = $"Failed to get Documnts";
//                SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                    "Document Signing", LogMessageType.FAILURE.GetValue(), logMessage, UUID, Email, FullName);
//                return NotFound();
//            }
//            var model = (List<Core.Domain.Models.Document>)response.Resource;
//            if (model == null)
//            {
//                logMessage = $"Failed to get Documents";
//                SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                    "Document Signing", LogMessageType.FAILURE.GetValue(), logMessage, UUID, Email, FullName);
//                return NotFound();

//            }
//            logMessage = $"Successfully received all the documents";
//            SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                "Document Signing", LogMessageType.SUCCESS.GetValue(), logMessage, UUID, Email, FullName);

//            return View(model);

//        }

//        [HttpGet]
//        public async Task<IActionResult> DocumentStatus(string id)
//        {
//            string logMessage;
//            var response = await _documentService.GetDocumentDetaildByIdAsync(id);
//            if (response == null || !response.Success)
//            {
//                logMessage = $"Failed to get Documnt status";
//                SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                    "Document Signing", LogMessageType.FAILURE.GetValue(), logMessage, UUID, Email, FullName);
//                return NotFound();
//            }
//            var model = (Document)response.Resource;
//            if (model == null)
//            {
//                logMessage = $"Failed to get Document status";
//                SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                    "Document Signing", LogMessageType.FAILURE.GetValue(), logMessage, UUID, Email, FullName);
//                return NotFound();

//            }
//            logMessage = $"Successfully received document status";
//            SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                "Document Signing", LogMessageType.SUCCESS.GetValue(), logMessage, UUID, Email, FullName);
//            return View(model);
//        }


//        [HttpGet]
//        public async Task<IActionResult> CreateDocument()
//        {
//            var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
//            var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);

//            var signature = await _templateService.GetSignaturePreviewAsync(userDTO);
//            if (signature == null || !signature.Success)
//            {
//                AlertViewModel alert = new AlertViewModel { IsSuccess = false, Message = signature.Message };
//                TempData["Alert"] = JsonConvert.SerializeObject(alert);
//                return RedirectToAction("Index");

//            }
//            var preview = (string)signature.Resource;
//            var previewDTO = JsonConvert.DeserializeObject<PreviewImageDTO>(preview);

//            var response = await _localBusinessUsersService.GetBusinessUserByEmailAsync(Email);
//            if (response == null || !response.Success)
//            {
//                AlertViewModel alert = new AlertViewModel { IsSuccess = false, Message = response.Message };
//                TempData["Alert"] = JsonConvert.SerializeObject(alert);
//                return RedirectToAction("Index");
//            }
//            var data = (OrgSubscriberEmail)response.Resource;
//            var view = new DocumentCreateViewModel();
//            ViewBag.SignatureImage = previewDTO.signatureImage;
//            ViewBag.EsealImage = previewDTO.esealImage;
//            ViewBag.InitialBaseImage = data.ShortSignature;
//            return View(view);
//        }

//        [HttpGet]
//        public async Task<IActionResult> isEsealAffix(string Email)
//        {
//            var response = await _localBusinessUsersService.GetBusinessUserByEmployeeEmailAsync(Email);
//            if (response == null || !response.Success)
//            {
//                return Ok(response);
//            }
//            var businessuser = (OrgSubscriberEmail)response.Resource;
//            if (businessuser.IsEsealSignatory == true)
//            {
//                return Ok(new ServiceResult(true, "This user has Eseal permission"));
//            }
//            return Ok(new ServiceResult(false, "Sorry you dont have eseal permission"));
//        }

//        [HttpPost]
//        public async Task<IActionResult> SaveNewDocument(DocumentCreateViewModel documentCreateViewModel)
//        {
//            string logMessage;

//            string pageNumber = "";
//            string posX = "";
//            string posY = "";
//            string width = "";
//            string height = "";
//            string esealpageNumber = "";
//            string esealposX = "";
//            string esealposY = "";
//            string esealwidth = "";
//            string esealheight = "";
//            string qrpageNumber = "";
//            string qrposX = "";
//            string qrposY = "";
//            string qrwidth = "";
//            string qrheight = "";

//            try
//            {

//                var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);

//                var accessToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "AccessToken").Value);

//                JObject configObject = JObject.Parse(documentCreateViewModel.Config);

//                CoordinatesData signCords = ExtractCoordinates(configObject["Signature"], documentCreateViewModel.Signatory);
//                CoordinatesData qrCords = ExtractCoordinates(configObject["Qrcode"], documentCreateViewModel.Signatory);
//                CoordinatesDataEseal esealCords = ExtractCoordinatesForEseal(configObject["Eseal"], documentCreateViewModel.Signatory);

//                if (configObject["Signature"][documentCreateViewModel.Signatory] is JObject emailData)
//                {
//                    pageNumber = emailData["PageNumber"]?.ToString();
//                    posX = emailData["posX"]?.ToString();
//                    posY = emailData["posY"]?.ToString();
//                    width = emailData["width"]?.ToString();
//                    height = emailData["height"]?.ToString();

//                }
//                if (esealCords != null)
//                {
//                    if (configObject["Eseal"][documentCreateViewModel.Signatory] is JObject esealData)
//                    {
//                        esealpageNumber = esealData["PageNumber"]?.ToString();
//                        esealposX = esealData["posX"]?.ToString();
//                        esealposY = esealData["posY"]?.ToString();
//                        esealwidth = esealData["width"]?.ToString();
//                        esealheight = esealData["height"]?.ToString();

//                    }
//                }

//                if (qrCords != null)
//                {
//                    if (configObject["Qrcode"][documentCreateViewModel.Signatory] is JObject qrData)
//                    {
//                        qrpageNumber = qrData["PageNumber"]?.ToString();
//                        qrposX = qrData["posX"]?.ToString();
//                        qrposY = qrData["posY"]?.ToString();
//                        qrwidth = qrData["width"]?.ToString();
//                        qrheight = qrData["height"]?.ToString();

//                    }
//                }


//                var pathobj = new PathsdataDTO
//                {
//                    inputpath = "",
//                    outputpath = ""
//                };

//                var pathobject = JsonConvert.SerializeObject(pathobj);
//                var emailList = documentCreateViewModel.Signatory?.Split(',').Select(email => email.Trim()).ToList() ?? new List<string>();
//                var role = new Roles
//                {
//                    Order = 0,
//                    Role = "sesdd",
//                    Eseal = true
//                };
//                var roleList = new List<Roles>();

//                roleList.Add(role);
//                var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
//                var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
//                userDTO.Email = Email;
//                Receps receps = new Receps()
//                {
//                    index = null,
//                    order = 0,
//                    email = userDTO.Email,
//                    suid = userDTO.Suid,
//                    name = userDTO.Name,
//                    eseal = true,
//                    orgUID = userDTO.OrganizationId,
//                    orgName = userDTO.OrganizationName
//                };
//                List<Receps> receps1 = new List<Receps> { receps };
//                Docdetails docdetails = new Docdetails()
//                {
//                    ownerName = userDTO.Name,
//                    receps = receps1,
//                    tempname = documentCreateViewModel.DocumentName,
//                    daysToComplete = "2",
//                    annotations = "",
//                    orgn_name = userDTO.OrganizationName,
//                    watermark = null,
//                    expiredate = DateTime.Now.AddDays(2),

//                };

//                var data = new SaveDocumentViewModel()
//                {
//                    docdetails = docdetails,
//                    docData = "",
//                    fileName = documentCreateViewModel.DocumentName,
//                    signCords = signCords == null ? "" : new Dictionary<string, CoordinatesData> { { userDTO.Suid, signCords } },
//                    qrCords = qrCords == null ? "" : new Dictionary<string, CoordinatesData> { { userDTO.Suid, qrCords } },
//                    esealCords = esealCords == null ? "" : new Dictionary<string, CoordinatesDataEseal> { { userDTO.Suid, esealCords } },
//                    actoken = apiToken,
//                    qrCodeRequired = documentCreateViewModel.qrCodeRequired,
//                    htmlSchema = documentCreateViewModel.htmlSchema,
//                };


//                var dataJson = JsonConvert.SerializeObject(data);
//                SaveNewDocumentDTO saveNewDocumentDTO = new SaveNewDocumentDTO()
//                {
//                    file = documentCreateViewModel.File,
//                    model = dataJson,
//                };
//                var response = await _documentService.SaveNewDocumentAsync(saveNewDocumentDTO, userDTO);
//                if (response.Success)
//                {
//                    var sigRequestData = new SigningRequestModel()
//                    {
//                        tempid = response.Resource.ToString(),
//                        userName = userDTO.Name,
//                        userEmail = userDTO.Email,
//                        signature = "",
//                        actoken = apiToken,
//                        suid = userDTO.Suid,
//                        organizationID = userDTO.OrganizationId,
//                        signVisible = 1,

//                        pageNumber = string.IsNullOrEmpty(pageNumber) ? null : Convert.ToInt32(pageNumber), //new code --trying
//                        posX = string.IsNullOrEmpty(posX) ? 0 : Convert.ToDouble(posX),
//                        posY = string.IsNullOrEmpty(posY) ? 0 : Convert.ToDouble(posY),
//                        width = string.IsNullOrEmpty(width) ? 0 : Convert.ToDouble(width),
//                        height = string.IsNullOrEmpty(height) ? 0 : Convert.ToDouble(height),
//                        EsealPageNumber = string.IsNullOrEmpty(esealpageNumber) ? 0 : Convert.ToInt32(esealpageNumber),
//                        EsealPosX = string.IsNullOrEmpty(esealposX) ? 0 : Convert.ToDouble(esealposX),
//                        EsealPosY = string.IsNullOrEmpty(esealposY) ? 0 : Convert.ToDouble(esealposY),
//                        EsealWidth = string.IsNullOrEmpty(esealwidth) ? 0 : Convert.ToDouble(esealwidth),
//                        EsealHeight = string.IsNullOrEmpty(esealheight) ? 0 : Convert.ToDouble(esealheight),
//                        QrPageNumber = string.IsNullOrEmpty(qrpageNumber) ? 0 : Convert.ToInt32(qrpageNumber),
//                        QrHeight = string.IsNullOrEmpty(qrheight) ? 0 : Convert.ToDouble(qrheight),
//                        QrPosX = string.IsNullOrEmpty(qrposX) ? 0 : Convert.ToDouble(qrposX),
//                        QrPosY = string.IsNullOrEmpty(qrposY) ? 0 : Convert.ToDouble(qrposY),
//                        QrWidth = string.IsNullOrEmpty(qrwidth) ? 0 : Convert.ToDouble(qrwidth),
//                        docSerialNo = documentCreateViewModel.docSerialNo,
//                        entityName = documentCreateViewModel.entityName,
//                        faceRequired = documentCreateViewModel.faceRequired,

//                    };


//                    if (signCords == null && esealCords == null && qrCords != null)
//                    {
//                        sigRequestData.signVisible = 0;
//                    }
//                    else if (signCords == null && esealCords == null && qrCords == null)
//                    {
//                        sigRequestData.signVisible = 0;
//                    }

//                    var signData = JsonConvert.SerializeObject(sigRequestData);
//                    SigningRequestDTO signRequestDTO = new SigningRequestDTO()
//                    {
//                        signfile = documentCreateViewModel.File,
//                        model = signData,
//                    };
//                    var signResponse = await _documentService.SendSigningRequestAsync(signRequestDTO, userDTO, accessToken);
//                    if (signResponse.Success)
//                    {
//                        logMessage = $"Successfully signed the Document";
//                        SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                            "Document Signing", LogMessageType.SUCCESS.GetValue(), logMessage, UUID, Email, FullName);
//                        return Json(new { Status = "Success", Title = "Sign New Document", Message = signResponse.Message });
//                    }
//                    else
//                    {
//                        logMessage = $"Failed signed the Document";
//                        SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                            "Document Signing", LogMessageType.FAILURE.GetValue(), logMessage, UUID, Email, FullName);

//                        return Json(new { Status = "Failed", Title = "Sign New Document", Message = signResponse.Message, Result = signRequestDTO });
//                    }
//                }
//                else
//                {
//                    return Json(new { Status = "Failed", Title = "Save New Document", Message = response.Message });
//                }
//            }
//            catch (Exception ex)
//            {
//                return NotFound();
//            }

//        }

//        [HttpGet]
//        public async Task<IActionResult> DownloadSignedDocument(string fileID, string fileName)
//        {
//            try
//            {
//                var result = await _documentService.DownloadSignedDocumentAsync(fileID);

//                if (!result.Success)
//                {
//                    return NotFound(result.Message);
//                }

//                var fileData = (Filestorage)result.Resource;
//                var documentBytes = fileData.File;
//                byte[] pdfBytes = (byte[])documentBytes;
//                if (documentBytes == null || documentBytes.Length == 0)
//                {
//                    return NotFound("Downloaded document is empty or null.");
//                }


//                return File(pdfBytes, "application/pdf", fileName);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, "Internal Server Error");
//            }
//        }

//        private CoordinatesData ExtractCoordinates(JToken token, string email)
//        {
//            CoordinatesData coordinates = null;

//            if (token != null && token.Type == JTokenType.Object)
//            {
//                JObject tokenObject = (JObject)token;

//                if (tokenObject[email] != null)
//                {
//                    coordinates = tokenObject[email].ToObject<CoordinatesData>();
//                }
//            }

//            return coordinates;
//        }

//        private CoordinatesDataEseal ExtractCoordinatesForEseal(JToken token, string email)
//        {
//            CoordinatesDataEseal coordinates = null;

//            if (token != null && token.Type == JTokenType.Object)
//            {
//                JObject tokenObject = (JObject)token;

//                if (tokenObject[email] != null)
//                {
//                    coordinates = tokenObject[email].ToObject<CoordinatesDataEseal>();
//                }
//            }

//            return coordinates;
//        }

//        [HttpPost]
//        public async Task<IActionResult> SendSigningRequest([FromForm] DocumentRetryViewModel documentRetryViewModel)
//        {
//            var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user")?.Value;
//            var accessToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "AccessToken").Value);
//            var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
//            userDTO.Email = Email;

//            var signRequestDTO = new SigningRequestDTO()
//            {
//                signfile = documentRetryViewModel.File,
//                model = documentRetryViewModel.Config,
//            };
//            var signResponse = await _documentService.SendSigningRequestAsync(signRequestDTO, userDTO, accessToken);
//            if (signResponse.Success)
//            {
//                string logMessage = $"Successfully signed the Document";
//                SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                    "Document Signing", LogMessageType.SUCCESS.GetValue(), logMessage, UUID, Email, FullName);
//                return Json(new { Status = "Success", Title = "Sign New Document", Message = signResponse.Message });
//            }
//            else
//            {
//                string logMessage = $"Failed signed the Document";
//                SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                    "Document Signing", LogMessageType.FAILURE.GetValue(), logMessage, UUID, Email, FullName);
//                return Json(new { Status = "Failed", Title = "Sign New Document", Message = signResponse.Message, Result = signRequestDTO });
//            }

//        }


//        public async Task<IActionResult> SignActionConfigByDocId(string Id)
//        {

//            try
//            {

//                var previewTemplate = await _documentService.GetDocumentDetaildByIdAsync(Id);
//                var preview = (Document)previewTemplate.Resource;

//                if (preview == null)
//                {
//                    AlertViewModel alert = new AlertViewModel { IsSuccess = false, Message = previewTemplate.Message };
//                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
//                    return RedirectToAction("Index");
//                }

//                var viewModel = new Document
//                {
//                    Id = preview.Id,
//                    DocumentId = preview.DocumentId,
//                    DocumentName = preview.DocumentName,
//                    Annotations = preview.Annotations,
//                    EsealAnnotations = preview.EsealAnnotations,
//                    QrcodeAnnotations = preview.QrcodeAnnotations,
//                    Status = preview.Status,
//                    EdmsId = preview.EdmsId,
//                    AccountType = preview.AccountType,
//                    OrganizationId = preview.OrganizationId,
//                    OrganizationName = preview.OrganizationName,
//                    SignaturesRequiredCount = preview.SignaturesRequiredCount,
//                    RecepientCount = preview.RecepientCount,
//                    CompleteSignList = preview.CompleteSignList,
//                    PendingSignList = preview.PendingSignList,
//                    //MultiSign = preview.MultiSign,
//                    MultiSign = false,
//                    AllowToAssignSomeone = preview.AllowToAssignSomeone,
//                    DisableOrder = preview.DisableOrder,
//                    DocumentBlockedTime = preview.DocumentBlockedTime,
//                    IsDocumentBlocked = preview.IsDocumentBlocked,
//                    Watermark = preview.Watermark,
//                    // Recepients = preview.Recepients,                   
//                    //Recepients = preview.Recepients?.ToList(),
//                    ExpireDate = preview.ExpireDate,
//                    CompleteTime = preview.CompleteTime,
//                    CreateTime = preview.CreateTime,
//                    RemindEvery = preview.RemindEvery,
//                    AutoReminders = preview.AutoReminders,
//                    DaysToComplete = preview.DaysToComplete,
//                    OwnerName = preview.OwnerName,
//                    OwnerEmail = preview.OwnerEmail,
//                    OwnerId = preview.OwnerId,
//                    FileId = preview.FileId,
//                    HtmlSchema = preview.HtmlSchema,

//                };



//                return View(viewModel);
//            }
//            catch (Exception e)
//            {

//                return RedirectToAction("Index");
//            }
//        }

//        [HttpPost]
//        public async Task<IActionResult> SignDocument(SignDocumentViewModel signDocumentViewModel)
//        {
//            string pageNumber = "";
//            string posX = "";
//            string posY = "";
//            string width = "";
//            string height = "";
//            string esealpageNumber = "";
//            string esealposX = "";
//            string esealposY = "";
//            string esealwidth = "";
//            string esealheight = "";
//            string organizationId = "";
//            string qrcodepageNumber = "";
//            string qrcodeposX = "";
//            string qrcodeposY = "";
//            string qrcodewidth = "";
//            string qrcodeheight = "";
//            try
//            {


//                var apiToken = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken")?.Value;
//                var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user")?.Value;
//                var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
//                userDTO.Email = Email;
//                var accessToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "AccessToken").Value);

//                var idToken = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "ID_Token")?.Value;
//                var configObject = JObject.Parse(signDocumentViewModel.Config);

//                CoordinatesData signCords = ExtractCoordinatesForNormalSignAndQuick(configObject["Signature"], userDTO.Suid);
//                CoordinatesDataEseal esealCords = ExtractCoordinatesForEsealForNormalSignAndQuick(configObject["Eseal"], userDTO.Suid);
//                CoordinatesData qrCords = ExtractCoordinatesForNormalSignAndQuick(configObject["Qrcode"], userDTO.Suid);



//                if (configObject["Signature"][userDTO.Suid] is JObject emailData)
//                {
//                    pageNumber = emailData["PageNumber"]?.ToString();
//                    posX = emailData["posX"]?.ToString();
//                    posY = emailData["posY"]?.ToString();
//                    width = emailData["width"]?.ToString();
//                    height = emailData["height"]?.ToString();

//                }
//                if (esealCords != null)
//                {
//                    if (configObject["Eseal"][userDTO.Suid] is JObject esealData)
//                    {
//                        esealpageNumber = esealData["PageNumber"]?.ToString();
//                        esealposX = esealData["posX"]?.ToString();
//                        esealposY = esealData["posY"]?.ToString();
//                        esealwidth = esealData["width"]?.ToString();
//                        esealheight = esealData["height"]?.ToString();
//                        organizationId = esealData["organizationID"]?.ToString();

//                    }
//                }
//                if (qrCords != null)
//                {
//                    if (configObject["Qrcode"][userDTO.Suid] is JObject qrData)
//                    {
//                        qrcodepageNumber = qrData["PageNumber"]?.ToString();
//                        qrcodeposX = qrData["posX"]?.ToString();
//                        qrcodeposY = qrData["posY"]?.ToString();
//                        qrcodewidth = qrData["width"]?.ToString();
//                        qrcodeheight = qrData["height"]?.ToString();

//                    }
//                }

//                var sigRequestData = new SigningRequestModel()
//                {
//                    tempid = signDocumentViewModel.DocId,
//                    userName = userDTO.Name,
//                    userEmail = userDTO.Email,
//                    signature = "",
//                    actoken = apiToken,
//                    suid = userDTO.Suid,
//                    //signVisible = 1,
//                    pageNumber = string.IsNullOrEmpty(pageNumber) ? 0 : Convert.ToInt32(pageNumber),
//                    posX = string.IsNullOrEmpty(posX) ? 0 : Convert.ToDouble(posX),
//                    posY = string.IsNullOrEmpty(posY) ? 0 : Convert.ToDouble(posY),
//                    width = string.IsNullOrEmpty(width) ? 0 : Convert.ToDouble(width),
//                    height = string.IsNullOrEmpty(height) ? 0 : Convert.ToDouble(height),
//                    EsealPageNumber = string.IsNullOrEmpty(esealpageNumber) ? 0 : Convert.ToInt32(esealpageNumber),
//                    EsealPosX = string.IsNullOrEmpty(esealposX) ? 0 : Convert.ToDouble(esealposX),
//                    EsealPosY = string.IsNullOrEmpty(esealposY) ? 0 : Convert.ToDouble(esealposY),
//                    EsealWidth = string.IsNullOrEmpty(esealwidth) ? 0 : Convert.ToDouble(esealwidth),
//                    EsealHeight = string.IsNullOrEmpty(esealheight) ? 0 : Convert.ToDouble(esealheight),
//                    QrPageNumber = string.IsNullOrEmpty(qrcodepageNumber) ? 0 : Convert.ToInt32(qrcodepageNumber),
//                    QrHeight = string.IsNullOrEmpty(qrcodeheight) ? 0 : Convert.ToDouble(qrcodeheight),
//                    QrPosX = string.IsNullOrEmpty(qrcodeposX) ? 0 : Convert.ToDouble(qrcodeposX),
//                    QrPosY = string.IsNullOrEmpty(qrcodeposY) ? 0 : Convert.ToDouble(qrcodeposY),
//                    QrWidth = string.IsNullOrEmpty(qrcodewidth) ? 0 : Convert.ToDouble(qrcodewidth),
//                };
//                if (sigRequestData.pageNumber == 0)
//                {
//                    sigRequestData.pageNumber = null;

//                }
//                if (esealCords == null)
//                {
//                    sigRequestData.organizationID = null;
//                }
//                else
//                {
//                    sigRequestData.organizationID = userDTO.OrganizationId;
//                }
//                if (signCords == null && esealCords == null)
//                {
//                    sigRequestData.signVisible = 0;

//                }
//                else if (signCords == null && esealCords != null)
//                {
//                    sigRequestData.signVisible = 1;

//                }
//                else
//                {
//                    sigRequestData.signVisible = 1;
//                }

//                var signData = JsonConvert.SerializeObject(sigRequestData);
//                var signRequestDTO = new SigningRequestDTO()
//                {
//                    signfile = signDocumentViewModel.File,
//                    model = signData,
//                };

//                var signResponse = await _documentService.SendSigningRequestAsync(signRequestDTO, userDTO, accessToken);
//                if (signResponse.Success)
//                {
//                    string logMessage = $"Successfully signed the Document";
//                    SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                        "Document Signing", LogMessageType.SUCCESS.GetValue(), logMessage, UUID, Email, FullName);
//                    return Json(new { Status = "Success", Title = "Sign New Document", Message = signResponse.Message });
//                }
//                else if (signResponse.Resource != null)
//                {
//                    string logMessage = $"Failed signed the Document";
//                    SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                        "Document Signing", LogMessageType.FAILURE.GetValue(), logMessage, UUID, Email, FullName);
//                    return Json(new { Status = "Failed", Title = "Sign New Document", Message = signResponse.Message, Result = signRequestDTO, DocumtStatus = signResponse.Resource });
//                }
//                else
//                {
//                    string logMessage = $"Failed signed the Document";
//                    SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                        "Document Signing", LogMessageType.FAILURE.GetValue(), logMessage, UUID, Email, FullName);
//                    return Json(new { Status = "Failed", Title = "Sign New Document", Message = signResponse.Message, Result = signRequestDTO, DocumtStatus = "" });
//                }
//            }
//            catch (Exception ex)
//            {
//                return Json(new { Status = "Failed", Title = "Sign New Document", Message = "Signing Failed" });
//            }

//        }


//        [HttpGet]
//        public async Task<IActionResult> GetPreviewConfig(string id)
//        {
//            var result = await _documentService.DownloadSignedDocumentAsync(id);

//            if (!result.Success)
//            {
//                return NotFound(result.Message);
//            }
//            var fileData = (Filestorage)result.Resource;
//            var documentBytes = fileData.File;
//            byte[] pdfBytes = (byte[])documentBytes;
//            if (documentBytes == null || documentBytes.Length == 0)
//            {
//                return NotFound("Downloaded document is empty or null.");
//            }
//            string base64Document = Convert.ToBase64String(documentBytes);
//            return Json(new
//            {
//                success = true,
//                result = base64Document, // Your Base64 string
//                message = "Document loaded successfully"
//            });

//        }

//        public async Task<IActionResult> RejectSigning(RejectSigningViewModel rejectSigningViewModel)
//        {

//            var signResponse = await _documentService.DeclineDocumentSigningAsync(rejectSigningViewModel.DocId, rejectSigningViewModel.Suid, rejectSigningViewModel.Comment);
//            if (signResponse.Success)
//            {
//                string logMessage = $"Successfully rejected the Document";
//                SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                    "Document Signing", LogMessageType.SUCCESS.GetValue(), logMessage, UUID, Email, FullName);
//                return Json(new { Status = "Success", Title = "Reject New Document", Message = signResponse.Message });
//            }
//            else
//            {
//                string logMessage = $"Failed to reject the Document";
//                SendAdminLog(ModuleNameConstants.Documents, ServiceNameConstants.Documents,
//                    "Document Signing", LogMessageType.FAILURE.GetValue(), logMessage, UUID, Email, FullName);
//                return Json(new { Status = "Failed", Title = "Reject New Document", Message = signResponse.Message });
//            }


//        }

//        [HttpPut]
//        public async Task<IActionResult> Recall(string documentid)
//        {
//            var response = await _documentService.RecallDocumentToSignAsync(documentid);
//            if (!response.Success)
//            {
//                return Ok(response);
//            }
//            return Ok(response);
//        }

//        private CoordinatesData ExtractCoordinatesForNormalSignAndQuick(JToken token, string email)
//        {
//            CoordinatesData coordinates = null;

//            if (token != null && token.Type == JTokenType.Object)
//            {
//                JObject tokenObject = (JObject)token;

//                if (tokenObject[email] != null)
//                {
//                    coordinates = tokenObject[email].ToObject<CoordinatesData>();
//                }
//            }

//            return coordinates;
//        }

//        private CoordinatesDataEseal ExtractCoordinatesForEsealForNormalSignAndQuick(JToken token, string email)
//        {
//            CoordinatesDataEseal coordinates = null;

//            if (token != null && token.Type == JTokenType.Object)
//            {
//                JObject tokenObject = (JObject)token;

//                if (tokenObject[email] != null)
//                {
//                    coordinates = tokenObject[email].ToObject<CoordinatesDataEseal>();
//                }
//            }

//            return coordinates;
//        }
//    }
//}
