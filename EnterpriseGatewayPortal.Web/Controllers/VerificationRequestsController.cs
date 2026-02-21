namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[ServiceFilter(typeof(SessionValidationAttribute))]
    //[Authorize]
    //public class VerificationRequestsController : BaseController
    //{
    //    private readonly IConfiguration _configuration;
    //    private readonly IVerificationRequestsService _verificationRequestsService;
    //    private readonly IOrgSignatureTemplateService _orgSignatureTemplateService;
    //    private readonly IDocumentService _documentService;
    //    public VerificationRequestsController(IAdminLogService adminLogService, IVerificationRequestsService verificationRequestsService, IOrgSignatureTemplateService orgSignatureTemplateService,
    //        IConfiguration configuration, IDocumentService documentService

    //       ) : base(adminLogService)
    //    {
    //        _documentService = documentService;
    //        _verificationRequestsService = verificationRequestsService;
    //        _configuration = configuration;
    //        _orgSignatureTemplateService = orgSignatureTemplateService;
    //    }
    //    [HttpGet]
    //    public async Task<IActionResult> List()
    //    {
    //        string logMessage;
    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var response = await _verificationRequestsService.GetVerificationRequestListAsync(organizationUid, userDTO.Email);
    //        if (response == null)
    //        {
    //            logMessage = $"Failed to get the Verification request List";
    //            SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                "Get Verification users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //            AlertViewModel alert = new AlertViewModel { IsSuccess = false, Message = "Error while getting list" };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);
    //            return RedirectToAction("Index", "Dashboard");
    //        }
    //        return View(response);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetVerificationRequestDetails(int requestverificationId)
    //    {
    //        string logMessage;
    //        var documentDetails = await _verificationRequestsService.GetDocverifyRequestDetailsByIdAsync(requestverificationId);
    //        if (documentDetails == null)
    //        {
    //            logMessage = $"Failed to get the Verification request details";
    //            SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                "Get Verification users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return RedirectToAction("List");

    //        }
    //        var viewModel = new ViewVerificationRequestViewModel()
    //        {
    //            Id = documentDetails.Id,
    //            IssuerOrgName = documentDetails.IssuerOrgName,
    //            IssuerUid = documentDetails.IssuerUid,
    //            CompletedAt = documentDetails.CompletedAt,
    //            UpdatedAt = documentDetails.UpdatedAt,
    //            CreatedAt = documentDetails.CreatedAt,
    //            UpdatedBy = documentDetails.UpdatedBy,
    //            DocumentId = documentDetails.DocumentId,
    //            DocumentType = documentDetails.DocumentType,
    //            RelyingpartyUid = documentDetails.RelyingpartyUid,
    //            RequestedBy = documentDetails.RequestedBy,
    //            FileId = documentDetails.FileId,
    //            VerificationType = documentDetails.VerificationType,
    //            VerifiedBy = documentDetails.VerifiedBy,
    //            Status = documentDetails.Status,
    //        };
    //        logMessage = $"Successfully viewed Verification request details";
    //        SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //            "Get Verification users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //        return View(viewModel);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> DownloadCertificateForTechnical(string fileId)
    //    {

    //        try
    //        {
    //            var result = await _verificationRequestsService.Download(fileId);

    //            if (!result.Success)
    //            {
    //                return NotFound(result.Message);
    //            }

    //            var fileDataJson = result.Resource as JObject;

    //            if (fileDataJson == null)
    //            {
    //                return BadRequest("Invalid data format received");
    //            }

    //            var fileData = fileDataJson.ToObject<DownloadViewModel>();

    //            if (fileData == null)
    //            {
    //                return BadRequest("Failed to deserialize data to DownloadViewModel");
    //            }

    //            var documentBytes = fileData.VerificationReport;
    //            if (documentBytes == null || documentBytes.Length == 0)
    //            {
    //                return NotFound("Downloaded document is empty or null.");
    //            }
    //            return File(documentBytes, "application/pdf", "TechnicalVerificationReport.pdf");
    //        }
    //        catch (Exception)
    //        {
    //            return StatusCode(500, "Internal Server Error");
    //        }

    //    }
    //    [HttpGet]


    //    public async Task<IActionResult> DownloadCertificatesForTrueCopy(string fileId)
    //    {
    //        try
    //        {
    //            // Download the PDF
    //            var result = await _verificationRequestsService.Download(fileId);
    //            if (!result.Success)
    //            {
    //                return NotFound(result.Message);
    //            }

    //            // Convert the JSON data to DownloadViewModel for the PDF
    //            var fileDataJson = result.Resource as JObject;
    //            var fileData = fileDataJson?.ToObject<DownloadViewModel>();

    //            if (fileData == null)
    //            {
    //                return BadRequest("Failed to deserialize data to DownloadViewModel");
    //            }

    //            // Create a memory stream for the zip file
    //            using (var memoryStream = new MemoryStream())
    //            {
    //                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
    //                {
    //                    // Add the first PDF (Verification Report) to the zip archive
    //                    var entry1 = archive.CreateEntry("TechnicalVerificationReport.pdf");
    //                    using (var entryStream1 = entry1.Open())
    //                    {
    //                        await entryStream1.WriteAsync(fileData.VerificationReport, 0, fileData.VerificationReport.Length);
    //                    }
    //                    var fileName = fileData.FileName;
    //                    // Add the second PDF (True Copy) to the zip archive

    //                    var entry2 = archive.CreateEntry(fileName.Replace(".pdf", "") + "_TrueCopy.pdf");
    //                    // var entry2 = archive.CreateEntry("TrueCopy.pdf");
    //                    using (var entryStream2 = entry2.Open())
    //                    {
    //                        await entryStream2.WriteAsync(fileData.File, 0, fileData.File.Length);
    //                    }
    //                }

    //                // Set the position of the memory stream to the beginning
    //                memoryStream.Seek(0, SeekOrigin.Begin);

    //                // Return the zip file to the user for download
    //                return File(memoryStream.ToArray(), "application/zip", "VerificationReportAndTrueCopy.zip");
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            return StatusCode(500, "Internal Server Error");
    //        }
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> DownloadVerificationCertificate(string fileId)
    //    {
    //        try
    //        {
    //            var result = await _verificationRequestsService.Download(fileId);

    //            if (!result.Success)
    //            {
    //                return NotFound(result.Message);
    //            }

    //            var fileDataJson = result.Resource as JObject;

    //            if (fileDataJson == null)
    //            {
    //                return BadRequest("Invalid data format received");
    //            }

    //            var fileData = fileDataJson.ToObject<DownloadViewModel>();

    //            if (fileData == null)
    //            {
    //                return BadRequest("Failed to deserialize data to DownloadViewModel");
    //            }

    //            var documentBytes = fileData.VerificationReport;
    //            if (documentBytes == null || documentBytes.Length == 0)
    //            {
    //                return NotFound("Downloaded document is empty or null.");
    //            }
    //            ViewBag.FileId = fileId;
    //            return View(fileData);
    //        }
    //        catch (Exception)
    //        {
    //            return StatusCode(500, "Internal Server Error");
    //        }
    //    }
    //    [HttpGet]
    //    public async Task<IActionResult> GetSaveDocConfig(string id)
    //    {
    //        string logMessage;
    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
    //        var result = await _verificationRequestsService.Download(id);
    //        if (!result.Success)
    //        {
    //            return NotFound(result.Message);
    //        }

    //        var fileDataJson = result.Resource as JObject;

    //        if (fileDataJson == null)
    //        {
    //            return BadRequest("Invalid data format received");
    //        }

    //        var fileData = fileDataJson.ToObject<DownloadDocViewModel>();

    //        if (fileData == null)
    //        {
    //            return BadRequest("Failed to deserialize data to DownloadViewModel");
    //        }

    //        var documentBytes = fileData.File;
    //        if (documentBytes == null || documentBytes.Length == 0)
    //        {
    //            return NotFound("Downloaded document is empty or null.");
    //        }
    //        var documentDetailsByFileIDd = await _verificationRequestsService.GetDocverifyRequestDetailsByFileIdAsync(id);
    //        if (documentDetailsByFileIDd == null)
    //        {
    //            logMessage = $"Failed to get the Verification request details file";
    //            SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                "Get Verification users details by fileId", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return RedirectToAction("List");
    //        }
    //        var viewModel = new DownloadDocViewModel
    //        {
    //            FileName = fileData.FileName,
    //            File = documentBytes,
    //            Signatory = userDTO.Email,
    //            RequestId = documentDetailsByFileIDd.Id,
    //        };
    //        ViewBag.FileId = id;

    //        logMessage = $"Successfullt recived Verification request details file";
    //        SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //            "Get Verification users details by fileId", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //        return View(viewModel);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetPreviewDocConfig(string id)
    //    {

    //        var response = await _verificationRequestsService.GetPreviewDocAsync(id);
    //        return Ok(response);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> SignVerificationRequestDocument(DownloadDocViewModel downloadDocViewModel)
    //    {
    //        string logMessage;

    //        string esealpageNumber = "";
    //        string esealposX = "";
    //        string esealposY = "";
    //        string esealwidth = "";
    //        string esealheight = "";

    //        string signpageNumber = "";
    //        string signposX = "";
    //        string signposY = "";
    //        string signwidth = "";
    //        string signheight = "";

    //        string truecopypageNumber = "";
    //        string truecopyposX = "";
    //        string truecopyposY = "";
    //        string truecopywidth = "";
    //        string truecopyheight = "";

    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
    //        JObject configObject = JObject.Parse(downloadDocViewModel.Config);

    //        // TrueCopy Data
    //        CoordinatesData truecopyCords = ExtractCoordinates(configObject["Truecopy"], downloadDocViewModel.Signatory);
    //        if (truecopyCords != null)
    //        {
    //            if (configObject["Truecopy"][downloadDocViewModel.Signatory] is JObject trueCopyData)
    //            {
    //                truecopypageNumber = trueCopyData["PageNumber"]?.ToString();
    //                truecopyposX = trueCopyData["posX"]?.ToString();
    //                truecopyposY = trueCopyData["posY"]?.ToString();

    //                truecopywidth = trueCopyData["width"] != null ? "110" : null;
    //                truecopyheight = trueCopyData["height"] != null ? "70" : null;

    //            }
    //        }
    //        var trueCopyCoordinates = new TrueCopyCoordinatesdata()
    //        {
    //            pageNumber = truecopypageNumber,
    //            signatureXaxis = truecopyposX,
    //            signatureYaxis = truecopyposY,

    //            imgWidth = truecopywidth,
    //            imgHeight = truecopyheight
    //        };
    //        var trueCopy = JsonConvert.SerializeObject(trueCopyCoordinates);

    //        // Eseal Data
    //        CoordinatesData esealCords = ExtractCoordinates(configObject["Eseal"], downloadDocViewModel.Signatory);
    //        if (esealCords != null)
    //        {
    //            if (configObject["Eseal"][downloadDocViewModel.Signatory] is JObject esealData)
    //            {

    //                esealpageNumber = esealData["PageNumber"]?.ToString();
    //                esealposX = esealData["posX"]?.ToString();
    //                esealposY = esealData["posY"]?.ToString();
    //                esealwidth = esealData["width"]?.ToString();
    //                esealheight = esealData["height"]?.ToString();


    //                if (downloadDocViewModel.rotation == 90)
    //                {
    //                    var yy = esealposY;
    //                    esealposY = esealposX;
    //                    int esealposX_int = downloadDocViewModel.pageHeight - int.Parse(yy) - int.Parse(esealheight);
    //                    esealposX = esealposX_int.ToString();
    //                }
    //                if (downloadDocViewModel.rotation == 180)
    //                {
    //                    var yy = esealposY;
    //                    int esealposY_int = downloadDocViewModel.pageHeight - int.Parse(esealposY) - int.Parse(esealheight);
    //                    esealposY = esealposY_int.ToString();
    //                    int esealposX_int = downloadDocViewModel.pageWidth - int.Parse(esealposX) - int.Parse(esealwidth);
    //                    esealposX = esealposX_int.ToString();

    //                }
    //                if (downloadDocViewModel.rotation == 270)
    //                {
    //                    var yy = esealposY;
    //                    int esealposY_int = downloadDocViewModel.pageWidth - int.Parse(esealposX) - int.Parse(esealwidth);
    //                    esealposY = esealposY_int.ToString();
    //                    int esealposX_int = int.Parse(yy);
    //                    esealposX = esealposX_int.ToString();
    //                }

    //            }
    //        }


    //        var esealCoordinates = new EsealCoordinatesdata()
    //        {
    //            pageNumber = esealpageNumber,
    //            signatureXaxis = esealposX,
    //            signatureYaxis = esealposY,

    //            imgWidth = esealwidth,
    //            imgHeight = esealheight
    //        };
    //        var eseal = JsonConvert.SerializeObject(esealCoordinates);

    //        // Sign Data
    //        CoordinatesData signCords = ExtractCoordinates(configObject["Signature"], downloadDocViewModel.Signatory);
    //        if (signCords != null)
    //        {
    //            if (configObject["Signature"][downloadDocViewModel.Signatory] is JObject signData)
    //            {
    //                signpageNumber = signData["PageNumber"]?.ToString();
    //                signposX = signData["posX"]?.ToString();
    //                signposY = signData["posY"]?.ToString();
    //                signwidth = signData["width"]?.ToString();
    //                signheight = signData["height"]?.ToString();

    //                if (downloadDocViewModel.rotation == 90)
    //                {
    //                    var y = signposY;
    //                    signposY = signposX;
    //                    int signposX_int = downloadDocViewModel.pageHeight - int.Parse(y) - int.Parse(signheight);
    //                    signposX = signposX_int.ToString();
    //                }
    //                if (downloadDocViewModel.rotation == 180)
    //                {
    //                    var y = signposY;
    //                    int signposY_int = downloadDocViewModel.pageHeight - int.Parse(signposY) - int.Parse(signheight);
    //                    signposY = signposY_int.ToString();
    //                    int signposX_int = downloadDocViewModel.pageWidth - int.Parse(signposX) - int.Parse(signwidth);
    //                    signposX = signposX_int.ToString();


    //                }
    //                if (downloadDocViewModel.rotation == 270)
    //                {
    //                    var y = signposY;
    //                    int signposY_int = downloadDocViewModel.pageWidth - int.Parse(signposX) - int.Parse(signwidth);
    //                    signposY = signposY_int.ToString();
    //                    signposX = y;

    //                }

    //            }
    //        }
    //        var signCoordinates = new SignatoryCoordinatesdata()
    //        {
    //            pageNumber = signpageNumber,
    //            signatureXaxis = signposX,
    //            signatureYaxis = signposY,
    //            imgWidth = signwidth,
    //            imgHeight = signheight
    //        };
    //        var signatory = JsonConvert.SerializeObject(signCoordinates);


    //        var verificationRequestTrueCopySign = new VerificationRequestTrueCopySignDTO()
    //        {
    //            RequestId = downloadDocViewModel.RequestId,
    //            OrgId = userDTO.OrganizationId,
    //            OrgName = userDTO.OrganizationName,
    //            VerifierName = FullName,
    //            Suid = userDTO.Suid,
    //            Email = downloadDocViewModel.Signatory,
    //            Annotations = (signCords == null) ? null : signatory,
    //            ESealAnnotation = (esealCords == null) ? null : eseal,
    //            StampAnnotation = (truecopyCords == null) ? null : trueCopy,

    //            FileName = downloadDocViewModel.FileName,

    //        };
    //        var statusChange = await _verificationRequestsService.StatusChangeToInProgress(downloadDocViewModel.RequestId);
    //        if (statusChange.Success)
    //        {
    //            var response = await _verificationRequestsService.SignVerificationRequestAsync(verificationRequestTrueCopySign);
    //            if (response.Success)
    //            {
    //                logMessage = $"Sucessfully signed Verification request document";
    //                SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                    "Save Verification request sign", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //                return Json(new { Status = "Success", Title = "Sign New Document", Message = response.Message });
    //            }
    //            else
    //            {
    //                logMessage = $"Failed to sign Verification request document";
    //                SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                    "Save Verification request sign", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //                return Json(new { Status = "Failed", Title = "Sign New Document", Message = response.Message, Result = verificationRequestTrueCopySign });
    //            }
    //        }
    //        else
    //        {
    //            logMessage = $"Failed to sign Verification request document";
    //            SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                "Save Verification request sign", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return Json(new { Status = "Failed", Title = "Failed To Change The Status", Message = statusChange.Message });
    //        }


    //    }

    //    private CoordinatesData ExtractCoordinates(JToken token, string email)
    //    {
    //        CoordinatesData coordinates = null;

    //        if (token != null && token.Type == JTokenType.Object)
    //        {
    //            JObject tokenObject = (JObject)token;

    //            if (tokenObject[email] != null)
    //            {
    //                coordinates = tokenObject[email].ToObject<CoordinatesData>();
    //            }
    //        }

    //        return coordinates;
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> SignVerificationRequest([FromForm] DocumentRetryViewModel documentRetryViewModel)
    //    {
    //        string logMessage;
    //        var verificationRequestTrueCopySign = JsonConvert.DeserializeObject<VerificationRequestTrueCopySignDTO>(documentRetryViewModel.Config);

    //        var statusChange = await _verificationRequestsService.StatusChangeToInProgress(verificationRequestTrueCopySign.RequestId);
    //        if (statusChange.Success)
    //        {
    //            var response = await _verificationRequestsService.SignVerificationRequestAsync(verificationRequestTrueCopySign);
    //            if (response.Success)
    //            {
    //                logMessage = $"Sucessfully signed Verification request document";
    //                SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                    "Save Verification request sign", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //                return Json(new { Status = "Success", Title = "Sign New Document", Message = response.Message });
    //            }
    //            else
    //            {
    //                logMessage = $"Failed to sign Verification request document";
    //                SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                    "Save Verification request sign", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //                return Json(new { Status = "Failed", Title = "Sign New Document", Message = response.Message, Result = verificationRequestTrueCopySign });
    //            }
    //        }
    //        else
    //        {
    //            return Json(new { Status = "Failed", Title = "Failed To Change The Status", Message = statusChange.Message });
    //        }

    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetPreviewimages()
    //    {
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //        var response = await _documentService.GetInitialPreviewImgAsync(apiToken);
    //        if (!response.Success)
    //        {
    //            return Json(new { success = false, message = response.Message });
    //        }
    //        var json = (string)response.Resource;
    //        if (json == null)
    //        {
    //            return Json(new { success = false, message = response.Message });
    //        }
    //        var previewDTO = JsonConvert.DeserializeObject<PreviewImagesDTO>(json);
    //        var view = new DocumentCreateViewModel();

    //        return Json(new { success = true, data = previewDTO });

    //    }

    //}
}
