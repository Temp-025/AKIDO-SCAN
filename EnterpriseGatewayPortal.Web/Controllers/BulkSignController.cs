namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[ServiceFilter(typeof(SessionValidationAttribute))]
    //[Authorize]
    //public class BulkSignController : BaseController
    //{
    //    private readonly IBulkSignService _bulkSignService;
    //    private readonly IDocumentTemplatesService _documentTemplatesService;
    //    private readonly IConfiguration _configuration;
    //    private readonly ILocalBulkSignService _localBulkSignService;
    //    private readonly ILocalTemplateService _localTemplateService;
    //    private readonly ILocalBusinessUsersService _localBusinessUsersService;
    //    private readonly IOrganizationService _organizationService;
    //    private readonly ISubscriberOrgTemplateService _subscriberOrgTemplateService;
    //    public BulkSignController(IAdminLogService adminLogService, IBulkSignService bulkSignService,
    //        IDocumentTemplatesService documentTemplatesService,
    //        IConfiguration configuration,
    //        ILocalBulkSignService localBulkSignService,
    //        ISubscriberOrgTemplateService subscriberOrgTemplateService,
    //        ILocalTemplateService localTemplateService, IOrganizationService organizationService, ILocalBusinessUsersService localBusinessUsersService) : base(adminLogService)
    //    {
    //        _bulkSignService = bulkSignService;
    //        _documentTemplatesService = documentTemplatesService;
    //        _configuration = configuration;
    //        _localBulkSignService = localBulkSignService;
    //        _localTemplateService = localTemplateService;
    //        _subscriberOrgTemplateService = subscriberOrgTemplateService;
    //        _organizationService = organizationService;
    //        _localBusinessUsersService = localBusinessUsersService;

    //    }
    //    public async Task<IActionResult> GetBulkSigDataList()
    //    {

    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);

    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);

    //        var result = await _localBulkSignService.GetBulkSigDataListAsync(userDTO);
    //        var model= (List<Core.Domain.Models.Bulksign>)result.Resource;
    //        SendAdminLog(ModuleNameConstants.BulkSign, ServiceNameConstants.BulkSign, "Bulk Sign", LogMessageType.SUCCESS.ToString(), "Get BulkSign List Success", UUID, Email, null, FullName);
    //        return View(model);
    //    }
    //    public async Task<IActionResult> GetReceivedBulkSignList()
    //    {

    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);

    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);

    //        var result = await _localBulkSignService.GetReceivedBulkSignListAsync(userDTO);
    //        var model = (List<Core.Domain.Models.Bulksign>)result.Resource;
    //        SendAdminLog(ModuleNameConstants.BulkSign, ServiceNameConstants.BulkSign, "Bulk Sign", LogMessageType.SUCCESS.ToString(), "Get BulkSign Received List Success", UUID, Email, null, FullName);
    //        return View(model);
    //    }
    //    public async Task<IActionResult> GetSentBulkSignList()
    //    {

    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);

    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);

    //        var result = await _localBulkSignService.GetSentBulkSignListAsync(userDTO);
    //        var model = (List<Core.Domain.Models.Bulksign>)result.Resource;
    //        SendAdminLog(ModuleNameConstants.BulkSign, ServiceNameConstants.BulkSign, "Bulk Sign", LogMessageType.SUCCESS.ToString(), "Get BulkSign Sent List Success", UUID, Email, null, FullName);
    //        return View(model);
    //    }
    //    public async Task<IActionResult> DocumentDetails(string correlationId)
    //    {
    //        var Viewmodel = new DocumentDetailsDTO();

    //        Viewmodel.result = new Result1();

    //        var result = await _localBulkSignService.GetBulkSigDataAsync(correlationId);

    //        if (result == null || !result.Success)
    //        {
    //            return NotFound();
    //        }
    //        List<Core.DTOs.FileResult> fileResult = new List<Core.DTOs.FileResult>();

    //        var model = (Core.Domain.Models.Bulksign)result.Resource;

    //        BulkSignCallBackDTO bulkSignCallBackDTO = new BulkSignCallBackDTO();

    //        if (model.Result != null)
    //        {
    //            bulkSignCallBackDTO = JsonConvert.DeserializeObject<BulkSignCallBackDTO>(model.Result);
    //            Viewmodel.result.totalFileCount = bulkSignCallBackDTO.Result.TotalFileCount;
    //            Viewmodel.result.failedFileCount = bulkSignCallBackDTO.Result.FailedFileCount;
    //            Viewmodel.result.successFileCount = bulkSignCallBackDTO.Result.SuccessFileCount;
    //            if (bulkSignCallBackDTO.Result.FileArray.Length > 0)
    //            {
    //                foreach (var item in bulkSignCallBackDTO.Result.FileArray)
    //                {
    //                    fileResult.Add(new Core.DTOs.FileResult() { fileName = item.FileName, status = item.Status });
    //                }
    //            }
    //            Viewmodel.result.fileArray = fileResult;
    //        }
    //        else
    //        {
    //            Viewmodel.result = new Result1();
    //            Viewmodel.result.fileArray = fileResult;
    //        }
    //        Viewmodel.corelationId = model.CorelationId;
    //        Viewmodel.signedPath = model.SignedPath;
    //        Viewmodel.status = model.Status;
    //        Viewmodel.sourcePath = model.SourcePath;
    //        return View(Viewmodel);
    //    }



    //    [HttpGet]
    //    public async Task<IActionResult> DocumentResultDetails(string correlationId)
    //    {
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);

    //        var result = await _localBulkSignService.GetBulkSigDataAsync(correlationId);
    //        if (result == null || !result.Success)
    //        {
    //            return NotFound();
    //        }
    //        var model = (Core.Domain.Models.Bulksign)result.Resource;

    //        BulkSignCallBackDTO bulkSignCallBackDTO = new BulkSignCallBackDTO();

    //        if (model.Result != null)
    //        {
    //            bulkSignCallBackDTO = JsonConvert.DeserializeObject<BulkSignCallBackDTO>(model.Result);
    //        }
    //        var response = bulkSignCallBackDTO.Result;
    //        if (response == null)
    //        {
    //            response.FileArray = new Filearray[0]; ;
    //        }
    //        return Ok(response);
    //    }
    //    //path based download functionality
    //    public async Task<IActionResult> Download([FromBody] DocumentDownloadDTO documentDownloadDTO)
    //    {
    //        try
    //        {
    //            //string filePath = @"C:\Users\saika\OneDrive\Documents\destinationpdfs\YaminiResume.pdf";


    //            string filePath = documentDownloadDTO.destinationPath + documentDownloadDTO.fileName.Trim();

    //            if (!System.IO.File.Exists(filePath))
    //            {
    //                return NotFound("File not found");
    //            }
    //            var fileBytes = System.IO.File.ReadAllBytes(filePath);
    //            return File(fileBytes, "application/pdf", documentDownloadDTO.fileName);
    //        }
    //        catch (InvalidOperationException ex)
    //        {
    //            // Handle the case where the response is not a PDF file
    //            return NotFound(ex.Message);
    //        }
    //        catch (Exception ex)
    //        {
    //            // Handle other exceptions
    //            return StatusCode(500, "An error occurred while processing the request.");
    //        }
    //    }
    //    public async Task<IActionResult> DownloadOld([FromBody] DocumentDownloadDTO documentDownloadDTO)
    //    {
    //        try
    //        {
    //            byte[] pdfBytes = await _bulkSignService.DownloadAsync(documentDownloadDTO);
    //            return File(pdfBytes, "application/pdf", documentDownloadDTO.fileName);
    //        }
    //        catch (InvalidOperationException ex)
    //        {
    //            // Handle the case where the response is not a PDF file
    //            return NotFound(ex.Message);
    //        }
    //        catch (Exception ex)
    //        {
    //            // Handle other exceptions
    //            return StatusCode(500, "An error occurred while processing the request.");
    //        }
    //    }

    //    public async Task<IActionResult> BulkSign(string Id)
    //    {
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //        var result = await _bulkSignService.GetDocumentDetails(Id, apiToken);
    //        if (result == null || !result.Success)
    //        {
    //            return NotFound();
    //        }
    //        var model = (DocumentDetailsDTO)result.Resource;
    //        string id = model.templateId;
    //        var response = await _bulkSignService.GetBulkSignRequest(id, apiToken);
    //        if (response == null || !response.Success)
    //        {
    //            return NotFound();
    //        }
    //        var bulksignobject = (PrepareBulksignResponseDTO)response.Resource;
    //        var signObject = new SignDTO();
    //        signObject.correlationId = Id;
    //        signObject.organizationId = bulksignobject.OrganizationId;
    //        signObject.callBackUrl = bulksignobject.CallBackUrl;
    //        signObject.sourcePath = model.sourcePath;
    //        signObject.destinationPath = model.signedPath;
    //        signObject.suid = bulksignobject.Suid;
    //        signObject.qrCodeRequired = bulksignobject.QrCodeRequired;
    //        signObject.signatureTemplateId = bulksignobject.SignatureTemplateId;
    //        signObject.esealSignatureTemplateId = bulksignobject.EsealSignatureTemplateId;
    //        signObject.esealPlaceHolderCoordinates = bulksignobject.EsealPlaceHolderCoordinates;
    //        signObject.placeHolderCoordinates = bulksignobject.PlaceHolderCoordinates;
    //        signObject.qrcodePlaceHolderCoordinates = bulksignobject.QrCodePlaceHolderCoordinates;

    //        var response1 = await _bulkSignService.BulkSignAsync(signObject);
    //        if (response1 == null || !response1.Success)
    //        {
    //            return NotFound();
    //        }
    //        var response2 = await _bulkSignService.UpdateStatus(Id, false, apiToken);
    //        if (response2 == null || !response2.Success)
    //        {
    //            return NotFound();
    //        }
    //        return Ok(response1);
    //    }


    //    public async Task<IActionResult> UploadFiles() //on-going code
    //    {
    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);

    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
    //        var documentsList = await _subscriberOrgTemplateService.GetSubscriberOrgTemplateAsync(userDTO);

    //        var Templates = (IEnumerable<Subscriberorgtemplate>)documentsList.Resource;

    //        IList<DocumentsTemplatesListDTO> documentTemplates = new List<DocumentsTemplatesListDTO>();

    //        foreach (var item in Templates)
    //        {
    //            DocumentsTemplatesListDTO documentsTemplateDTO = new DocumentsTemplatesListDTO()
    //            {
    //                TemplateName = item.Template.Templatename,
    //                TemplateId = item.Templateid,
    //                _id = item.Templateid,
    //                DocumentName = item.Template.Documentname,
    //                SettingConfig = item.Template.Settingconfig,
    //                SignCords = item.Template.Annotations,
    //                EsealCords = item.Template.Esealannotations,
    //                QrCords = item.Template.Qrcodeannotations,
    //                QrCodeRequired = (bool)item.Template.Qrcoderequired,
    //                RoleList = string.IsNullOrEmpty(item.Template.Rolelist) ? new List<Roles>() : JsonConvert.DeserializeObject<IList<Roles>>(item.Template.Rolelist),
    //                EmailList = string.IsNullOrEmpty(item.Template.Emaillist) ? new List<string>() : JsonConvert.DeserializeObject<IList<string>>(item.Template.Emaillist),
    //                status = item.Template.Status,
    //                edmsId = item.Template.Edmsid,
    //                createdBy = item.Template.Createdby,
    //                SignatureTemplate = (int)item.Template.Signaturetemplate,
    //                EsealSignatureTemplate = (int)(item.Template?.Esealsignaturetemplate),
    //                Annotations = item.Template.Annotations,
    //                EsealAnnotations = item.Template.Esealannotations,
    //                QrCodeAnnotations = item.Template.Qrcodeannotations,
    //            };
    //            documentTemplates.Add(documentsTemplateDTO);
    //        }
    //        return View(documentTemplates);

    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> SendFiles([FromForm] List<IFormFile> files, string displayName, string id)
    //    {
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //        if (files != null && files.Count > 0)
    //        {
    //            var preparator = false;
    //            var requestdto = new PrepareBulksignResponseDTO();

    //            var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);

    //            var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);

    //            var templatedetails = await _localTemplateService.GetLocalTemplateByIdAsync(id);
    //            var preview = (Template)templatedetails.Resource;

    //            var emailList = JsonConvert.DeserializeObject<IList<string>>(preview.Emaillist);
    //            var email = emailList[0];

    //            if (email != EmployeeEmail)
    //            {

    //                var request1 = await _localBulkSignService.SaveBulkSigningRequestAsync(id, displayName, userDTO, email);
    //                if (request1 == null || !request1.Success)
    //                {
    //                    return Json(new { success = false, message = request1.Message });
    //                }
    //                requestdto = (PrepareBulksignResponseDTO)request1.Resource;
    //                preparator = true;
    //            }
    //            else
    //            {

    //                var request = await _localBulkSignService.SaveBulkSigningRequestAsync(id, displayName, userDTO);
    //                if (request == null || !request.Success)
    //                {
    //                    return Json(new { success = false, message = request.Message });
    //                }
    //                requestdto = (PrepareBulksignResponseDTO)request.Resource;
    //                preparator = false;
    //            }
    //            var uploadKey = userDTO.OrganizationId+userDTO.Suid;
    //            var response = await _bulkSignService.SendFiles(files, requestdto.CorelationId, uploadKey);

    //            if (response == null || !response.Success)
    //            {
    //                return Json(new { success = false, message = response.Message });
    //            }
    //            var destinationPath = response.Resource.ToString();

    //            UpdatePathDTO pathDTO = new UpdatePathDTO();
    //            pathDTO.CorelationId = requestdto.CorelationId;
    //            pathDTO.Source = destinationPath;
    //            pathDTO.Destination = destinationPath + "_Signed";

    //            var pathResponse = await _localBulkSignService.UpdateBulkSigningSourceDestinationAsync(pathDTO);
    //            if (pathResponse == null || !pathResponse.Success)
    //            {
    //                return Json(new { success = false, message = pathResponse.Message });
    //            }
    //            SendAdminLog(ModuleNameConstants.BulkSign, ServiceNameConstants.BulkSign, "Bulk Sign", LogMessageType.SUCCESS.ToString(), "Send Files Success", UUID, Email, null, FullName);
    //            return Json(new { success = response.Success, message = response.Message, correlationid = requestdto.CorelationId, ispreparator = preparator });
    //        }
    //        else
    //        {
    //            return Json(new { success = false, message = "No files received." });
    //        }
    //    }



    //    [HttpPost]
    //    public async Task<IActionResult> PerformBulkSign([FromBody] string correlationId) // path based flow
    //    {
    //        var accessToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "AccessToken").Value);

    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);

    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);

    //        var result = await _localBulkSignService.GetBulkSigDataAsync(correlationId);

    //        if (result == null || !result.Success)
    //        {
    //            return Json(new { success = false, message = result.Message });
    //        }
    //        var model = (Core.Domain.Models.Bulksign)result.Resource;

    //        string id = model.TemplateId;

    //        var response = await _localBulkSignService.PrepareBulkSigningRequestAsync(id, userDTO);

    //        if (response == null || !response.Success)
    //        {
    //            return Json(new { success = false, message = response.Message });
    //        }

    //        var bulksignobject = (PrepareBulksignResponseDTO)response.Resource;

    //        var sendBulkSignObject = new SendBulkSignDTO();
    //        sendBulkSignObject.UgpassEmailId = bulksignobject.Email;
    //        sendBulkSignObject.CorrelationId = correlationId;
    //        sendBulkSignObject.SourcePath = model.SourcePath;
    //        sendBulkSignObject.DestinationPath = model.SignedPath;

    //        if (bulksignobject.PlaceHolderCoordinates != null)
    //        {
    //            var placeHolderCoordinatesObj = new PlaceHolderCoordinates()
    //            {
    //                pageNumber = bulksignobject.PlaceHolderCoordinates.pageNumber,
    //                signatureXaxis = bulksignobject.PlaceHolderCoordinates.signatureXaxis,
    //                signatureYaxis = bulksignobject.PlaceHolderCoordinates.signatureYaxis,
    //            };
    //            sendBulkSignObject.placeHolderCoordinates = placeHolderCoordinatesObj;
    //        }


    //        if (bulksignobject.EsealPlaceHolderCoordinates != null) {
    //            var esealPlaceHolderCoordinatesObj = new PlaceHolderCoordinates()
    //            {
    //                pageNumber = bulksignobject.EsealPlaceHolderCoordinates.pageNumber,
    //                signatureXaxis = bulksignobject.EsealPlaceHolderCoordinates.signatureXaxis,
    //                signatureYaxis = bulksignobject.EsealPlaceHolderCoordinates.signatureYaxis,
    //            };
    //            sendBulkSignObject.esealPlaceHolderCoordinates = esealPlaceHolderCoordinatesObj;
    //        }
    //        sendBulkSignObject.AccessToken = accessToken;

    //        var response1 = await _localBulkSignService.SendBulkSignRequestAsync(sendBulkSignObject);

    //        if (response1 == null || !response1.Success)
    //        {
    //            return Json(new { success = false, message = response1.Message });
    //        }

    //        var response2 = await _localBulkSignService.UpdateBulkSigningStatusAsync(correlationId, false);
    //        if (response2 == null || !response2.Success)
    //        {
    //            return Json(new { success = false, message = response2.Message });
    //        }


    //        SendAdminLog(ModuleNameConstants.BulkSign, ServiceNameConstants.BulkSign, "Bulk Sign", LogMessageType.SUCCESS.ToString(), "Perform Bulk sign Success", UUID, Email, null, FullName);
    //        return Ok(response1);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> SendRequest([FromBody] string correlationId)
    //    {

    //        var response = await _localBulkSignService.UpdateBulkSigningStatusAsync(correlationId, true);
    //        if (response == null || !response.Success)
    //        {
    //            return NotFound();
    //        }
    //        return Ok(response);
    //    }
    //    [HttpGet]
    //    public async Task<IActionResult> UpdateDocumentDetails(string correlationId)
    //    {
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);

    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);

    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);

    //        var uploadKey = userDTO.OrganizationId+userDTO.Suid;

    //        var response = await _bulkSignService.UpdateDocumentStatus(uploadKey);

    //        if (response == null || !response.Success)
    //        {
    //            return NotFound();
    //        }
    //        var bulksigndto = (BulkSignCallBackDTO)response.Resource;
    //        var result = bulksigndto.Result;
    //        return Ok(result);
    //    }
    //    [HttpGet]
    //    public async Task<IActionResult> UpdateDocumentDetails1(string correlationId)
    //    {
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);

    //        var response = await _bulkSignService.UpdateDocumentStatus(correlationId);

    //        if (response == null || !response.Success)
    //        {
    //            return NotFound();
    //        }
    //        var bulksigndto = (BulkSignCallBackDTO)response.Resource;
    //        var result = bulksigndto.Result;
    //        return Ok(result);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> UpdateDocumentDetailsOfAllFiles(string correlationId)
    //    {
    //        try
    //        {
    //            var apiToken = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken")?.Value;
    //            var accessToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "AccessToken").Value);

    //            var response = await _localBulkSignService.BulkSignStatusAsync(correlationId, accessToken);

    //            if (response == null || !response.Success)
    //            {
    //                return NotFound();
    //            }

    //            var bulksigndto = ((JObject)response.Resource).ToObject<ResultDTO>();


    //            var bulkSignCallBackDTO1 = new BulkSignCallBackDTO
    //            {
    //                CorrelationId = correlationId,
    //                Result = new Result
    //                {
    //                    TotalFileCount = bulksigndto.TotalFileCount,
    //                    FailedFileCount = bulksigndto.FailedFileCount,
    //                    SuccessFileCount = bulksigndto.SuccessFileCount,
    //                    FileArray = bulksigndto.FileSigningStatus.Select(f => new Filearray
    //                    {
    //                        FileName = f.FileName,
    //                        Status = f.IsSuccess ? "success" : "failed"
    //                    }).ToArray()
    //                }
    //            };

    //            var res = await _localBulkSignService.UpdateBulkSignResultAsync(bulkSignCallBackDTO1);
    //            if (res == null || !res.Success)
    //            {
    //                return NotFound();
    //            }


    //            var result = bulkSignCallBackDTO1.Result;
    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            return NotFound(ex.Message);
    //        }
    //    }


    //    public async Task<IActionResult> SendRequestbyPreparator(string Id)
    //    {
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);

    //        var response = await _localBulkSignService.UpdateBulkSigningStatusAsync(Id, true);
    //        if (response == null || !response.Success)
    //        {
    //            return NotFound();
    //        }
    //        return Ok(response);
    //    }
    //    [HttpGet]
    //    public async Task<IActionResult> DocumentStatusDetails(string correlationId)
    //    {

    //        var result = await _localBulkSignService.GetBulkSigDataAsync(correlationId);
    //        if (result == null || !result.Success)
    //        {
    //            return NotFound();
    //        }
    //        var model = (Core.Domain.Models.Bulksign)result.Resource;
    //        var result1 = model.Status;
    //        return Ok(result1);
    //    }
    //    [HttpPost]
    //    public async Task<IActionResult> SaveRequestByPath([FromBody] FilesPathDTO model)
    //    {
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);

    //        var prepator = false;

    //        var requestdto = new SaveRequestDTO();

    //        var templatedetails = await _documentTemplatesService.GetTemplateDetailsAsync(model.TemplateId, apiToken);

    //        var preview = (PreviewDTO)templatedetails.Resource;

    //        var email = preview.emailList[0];

    //        if (email == EmployeeEmail)
    //        {
    //            var request = await _bulkSignService.SaveRequest(model.TemplateId, model.TransactionName, apiToken);
    //            if (request == null || !request.Success)
    //            {
    //                return Json(new { success = false, message = request.Message });
    //            }
    //            requestdto = (SaveRequestDTO)request.Resource;
    //        }
    //        else
    //        {
    //            var request1 = await _bulkSignService.SaveRequestByPreparator(model.TemplateId, model.TransactionName, email, apiToken);

    //            if (request1 == null || !request1.Success)
    //            {
    //                return NotFound();
    //            }
    //            requestdto = (SaveRequestDTO)request1.Resource;
    //            prepator = true;
    //        }

    //        UpdatePathDTO pathDTO = new UpdatePathDTO();
    //        pathDTO.CorelationId = requestdto.corelationId;
    //        pathDTO.Source = model.SourcePath;
    //        pathDTO.Destination = model.DestinationPath;

    //        var pathResponse = await _bulkSignService.ChangePath(pathDTO, apiToken);
    //        if (pathResponse == null || !pathResponse.Success)
    //        {
    //            return Json(new { success = false, message = pathResponse.Message });
    //        }
    //        SendAdminLog(ModuleNameConstants.BulkSign, ServiceNameConstants.BulkSign, "Bulk Sign", LogMessageType.SUCCESS.ToString(), "Save BulkSign Request Success", UUID, Email, null, FullName);
    //        return Json(new { success = pathResponse.Success, message = pathResponse.Message, correlationid = requestdto.corelationId, ispreparator = false });
    //    }


    //    public async Task<IActionResult> getFiles([FromBody] FilesPathDTO model)
    //    {
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);

    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
    //        var prepator = false;

    //        var requestdto = new PrepareBulksignResponseDTO();

    //        var result = await _localBulkSignService.GetFilesListFromPath(model);


    //        if (result == null || !result.Success)
    //        {
    //            return Json(new { success = false, message = result.Message });
    //        }
    //        var filesList = (List<string>)result.Resource;

    //        var templateDetailsLocal = await _localTemplateService.GetLocalTemplateByIdAsync(model.TemplateId);
    //        var details = (Template)templateDetailsLocal.Resource;
    //        if (details == null)
    //        {
    //            return NotFound();
    //        }

    //        var cleanedEmailList = details.Emaillist.Replace("[", "").Replace("]", "");

    //        var emailList = cleanedEmailList.Split(',')
    //                             .Select(email => email.Trim().Trim('"'))
    //                             .Where(email => !string.IsNullOrEmpty(email))
    //                             .ToList();

    //        // Get the first email
    //        var email = emailList[0];

    //        if (email == EmployeeEmail)
    //        {
    //            var request = await _localBulkSignService.SaveBulkSigningRequestAsync(model.TemplateId, model.TransactionName, userDTO);
    //            if (request == null || !request.Success)
    //            {
    //                return Json(new { success = false, message = request.Message });
    //            }
    //            requestdto = (PrepareBulksignResponseDTO)request.Resource;
    //        }
    //        else
    //        {
    //            var request1 = await _localBulkSignService.SaveBulkSigningRequestAsync(model.TemplateId, model.TransactionName, userDTO, email);
    //            if (request1 == null || !request1.Success)
    //            {
    //                return Json(new { success = false, message = request1.Message });
    //            }
    //            requestdto = (PrepareBulksignResponseDTO)request1.Resource;
    //            prepator = true;
    //        }

    //        UpdatePathDTO pathDTO = new UpdatePathDTO();
    //        pathDTO.CorelationId = requestdto.CorelationId;
    //        pathDTO.Source = model.SourcePath;
    //        pathDTO.Destination = model.DestinationPath;

    //        var pathResponse = await _localBulkSignService.UpdateBulkSigningSourceDestinationAsync(pathDTO);
    //        if (pathResponse == null || !pathResponse.Success)
    //        {
    //            return Json(new { success = false, message = pathResponse.Message });
    //        }
    //        SendAdminLog(ModuleNameConstants.BulkSign, ServiceNameConstants.BulkSign, "Bulk Sign", LogMessageType.SUCCESS.ToString(), "Get BulkSign Files Success", UUID, Email, null, FullName);
    //        return Json(new { success = pathResponse.Success, message = pathResponse.Message, correlationid = requestdto.CorelationId, ispreparator = prepator, result = filesList });

    //    }
    //    public async Task<IActionResult> GetFileConfiguration()
    //    {

    //        int numberOfFiles = _configuration.GetValue<int>("FileConfiguration:NumberOfFiles");
    //        int eachFileSize = _configuration.GetValue<int>("FileConfiguration:EachFileSize");
    //        int allFileSize = _configuration.GetValue<int>("FileConfiguration:AllFileSize");
    //        FileConfigDTO model = new FileConfigDTO()
    //        {
    //            EachFileSize = eachFileSize,
    //            NumberOfFiles = numberOfFiles,
    //            AllFileSize = allFileSize
    //        };
    //        return Ok(model);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetOrganizationStatus()
    //    {
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var response = await _organizationService.GetOrganizationCertificateDetailstAsync(organizationUid);
    //        if (!response.Success)
    //        {
    //            return Ok(response);
    //        }
    //        return Ok(response);
    //    }

    //    public async Task<IActionResult> UpdateBulkSignStatusToCompleted(string correlationId)
    //    {
    //        try
    //        {
    //            var accessToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "AccessToken").Value);
    //            var response = await _localBulkSignService.BulkSignStatusAsync(correlationId, accessToken);

    //            if (response == null || !response.Success)
    //            {
    //                return NotFound();
    //            }
    //            var bulksigndto = ((JObject)response.Resource).ToObject<ResultDTO>();


    //            var bulkSignCallBackDTO1 = new BulkSignCallBackDTO
    //            {
    //                CorrelationId = correlationId,
    //                Result = new Result
    //                {
    //                    TotalFileCount = bulksigndto.TotalFileCount,
    //                    FailedFileCount = bulksigndto.FailedFileCount,
    //                    SuccessFileCount = bulksigndto.SuccessFileCount,
    //                    FileArray = bulksigndto.FileSigningStatus.Select(f => new Filearray
    //                    {
    //                        FileName = f.FileName,
    //                        Status = f.IsSuccess ? "success" : "failed"
    //                    }).ToArray()
    //                }
    //            };

    //            var response2 = await _localBulkSignService.CompletedBulkSigningRequestAsync(correlationId, bulkSignCallBackDTO1);

    //            if (response2 == null || !response2.Success)
    //            {
    //                return NotFound();
    //            }

    //            return Json(new { success = response2.Success, message = response2.Message });
    //        }
    //        catch (Exception ex)
    //        {
    //            return NotFound(ex.Message);
    //        }
    //    }

    //    public async Task<IActionResult> GetFilesFromSourcePath(string sourcePath)
    //    {
    //        FilesPathDTO model = new FilesPathDTO();
    //        model.SourcePath = sourcePath;

    //        var result = await _localBulkSignService.GetFilesListFromPath(model);

    //        if (result == null || !result.Success)
    //        {
    //            return Json(new { success = false, message = result.Message });
    //        }
    //        var filesList = (List<string>)result.Resource;

    //        Result result2 = new Result
    //        {
    //            TotalFileCount = filesList.Count,
    //            FailedFileCount = 0,
    //            SuccessFileCount = 0,
    //            FileArray = filesList.Select(f => new Filearray
    //            {
    //                FileName = f,
    //                Status = "pending"
    //            }).ToArray()
    //        };
    //        return Ok(result2);
    //    }
    //}
}
