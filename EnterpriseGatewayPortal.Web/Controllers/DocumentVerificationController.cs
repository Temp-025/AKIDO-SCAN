namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[ServiceFilter(typeof(SessionValidationAttribute))]
    //[Authorize]
    //public class DocumentVerificationController : BaseController
    //{
    //    private readonly IConfiguration _configuration;
    //    private readonly IDocumentVerifyIssuerService _documentVerifyIssuerService;
    //    private readonly IVerificationRequestsService _verificationRequestsService;

    //    public DocumentVerificationController(IAdminLogService adminLogService, IConfiguration configuration, IVerificationRequestsService verificationRequestsService, IDocumentVerifyIssuerService documentVerifyIssuerService) : base(adminLogService)
    //    {
    //        _configuration = configuration;
    //        _documentVerifyIssuerService = documentVerifyIssuerService;
    //        _verificationRequestsService = verificationRequestsService;
    //    }
    //    [HttpGet]
    //    public async Task<IActionResult> Index()
    //    {
    //        string logMessage;
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
    //        var suid_id = userDTO.Suid;

    //        var response = await _documentVerifyIssuerService.GetVerificationDetailListBySuidAndOrgUidAsync(organizationUid, suid_id);
    //        var documentVerifyDetailsEnumerable = (IEnumerable<DocumentVerifyListDTO>)response.Resource;

    //        if (documentVerifyDetailsEnumerable == null)
    //        {
    //            logMessage = $"Failed to get the Verification Users List";
    //            SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                "Get Verification users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return NotFound();
    //        }

    //        DocumentVerificationListViewModelcs viewModelcs = new DocumentVerificationListViewModelcs()
    //        {
    //            DocumentVerifyLists = documentVerifyDetailsEnumerable
    //        };

    //        logMessage = $"Successfully received Verification Users List";
    //        SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //            "Get Verification users details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //        return View(viewModelcs);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> Create()
    //    {
    //        try
    //        {
    //            string logMessage;

    //            var response2 = await _documentVerifyIssuerService.GetAllIssuerOrgNamesListAsync();
    //            var issuerOrgNamesEnumerable = (IEnumerable<IssuerOrgNamesDTO>)response2.Resource;
    //            List<IssuerOrgNamesDTO> issuerOrgNamesdetails = issuerOrgNamesEnumerable.ToList();
    //            if (issuerOrgNamesdetails == null)
    //            {
    //                logMessage = $"Failed to Create New Verification Request ";
    //                SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                    "Get Verification Request", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //                return NotFound();
    //            }

    //            CreateVerificationViewModelcs createVerificationViewModelcs = new CreateVerificationViewModelcs
    //            {

    //                IssuerName = "",
    //                DocumentType = "",
    //                VerificationMethod = "",
    //                IssuerOrgNameDetails = issuerOrgNamesdetails

    //            };
    //            return View(createVerificationViewModelcs);

    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex.Message);
    //        }
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Add(CreateVerificationViewModelcs model)
    //    {
    //        try
    //        {
    //            string logMessage;

    //            var res = await _documentVerifyIssuerService.GetAllDocumentListAsync();
    //            var documentDetailsEnumerable = (IEnumerable<DocumentIssuerListDTO>)res.Resource;
    //            List<DocumentIssuerListDTO> documentDetails = documentDetailsEnumerable.ToList();

    //            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //            string issuerName = model.IssuerName;
    //            string issuerUid = model.IssuerUid;
    //            string documentType = model.DocumentType;
    //            string verificationMethod = model.VerificationMethod;
    //            IFormFile uploadDocument = model.File;
    //            var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
    //            var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);


    //            DocumentIssuerListDTO relevantDocument = documentDetails.FirstOrDefault(d => d.DocumentName == documentType);

    //            if (relevantDocument != null)
    //            {

    //                var verificationModelDto = new VerificationModelDTO
    //                {
    //                    DocumentID = relevantDocument.Id,
    //                    RelyingpartyUid = userDTO.OrganizationId,
    //                    IssuerUid = issuerUid,
    //                    IssuerOrgName = issuerName,
    //                    RequestedBy = FullName,
    //                    CreatedBy = FullName,
    //                    UpdatedBy = FullName,
    //                    Status = DocumentStatusConstants.New,
    //                    VerificationType = model.VerificationMethod,
    //                    DocumentType = documentType,
    //                    SUID = userDTO.Suid,
    //                    RelyingpartyType = "RelyingParty",
    //                    RelyingpartyName = userDTO.OrganizationName,
    //                    RelyingpartyEmail = userDTO.Email
    //                };

    //                var response = await _documentVerifyIssuerService.SaveVerificationDetails(verificationModelDto, uploadDocument);
    //                var docDetails = (SaveDocumentVerifyDTO)response.Resource;

    //                if (response.Success)
    //                {
    //                    logMessage = $"Successfully Added Verification Request";
    //                    SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                        "Get Verification Request", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //                    return Json(new { success = true, response.Message, docDetails.FileId });
    //                }
    //                else
    //                {
    //                    logMessage = $"Failed to Add the Verification Request ";
    //                    SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                        "Get Verification Request", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //                    return Json(new { success = false, response.Message });
    //                }
    //            }
    //            else
    //            {
    //                logMessage = $"Failed to Add the Verification Request ";
    //                SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                    "Get Verification Request", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //                return Json(new { success = false, message = "Details not found from The Api." });
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return Json(new { success = false, message = "Error processing the data." });
    //        }
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> ViewDetails(int id)
    //    {
    //        string logMessage;
    //        var response = await _documentVerifyIssuerService.GetVerificationDetailByIdAsync(id);
    //        var documentDetails = (DocumentVerifyListDTO)response.Resource;
    //        if (documentDetails == null)
    //        {
    //            logMessage = $"Failed to get the Verification request details";
    //            SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //                "Get Verification users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return RedirectToAction("List");

    //        }

    //        DocumentVerifyDetailsViewModel model = new DocumentVerifyDetailsViewModel
    //        {
    //            Id = documentDetails.Id,
    //            IssuerOrgName = string.IsNullOrEmpty(documentDetails.IssuerOrgName) ? "N/A" : documentDetails.IssuerOrgName,
    //            VerifiedBy = string.IsNullOrEmpty(documentDetails.VerifiedBy) ? "N/A" : documentDetails.VerifiedBy,
    //            RequestedBy = string.IsNullOrEmpty(documentDetails.RequestedBy) ? "N/A" : documentDetails.RequestedBy,
    //            CreatedAt = documentDetails.CreatedAt.HasValue ? documentDetails.CreatedAt.ToString() : "N/A",
    //            UpdatedAt = documentDetails.UpdatedAt.HasValue ? documentDetails.UpdatedAt.ToString() : "N/A",
    //            CreatedBy = string.IsNullOrEmpty(documentDetails.CreatedBy) ? "N/A" : documentDetails.CreatedBy,
    //            UpdatedBy = string.IsNullOrEmpty(documentDetails.UpdatedBy) ? "N/A" : documentDetails.UpdatedBy,
    //            VerificationType = string.IsNullOrEmpty(documentDetails.VerificationType) ? "N/A" : documentDetails.VerificationType,
    //            DocumentType = string.IsNullOrEmpty(documentDetails.DocumentType) ? "N/A" : documentDetails.DocumentType,
    //            CompletedAt = documentDetails.CompletedAt.HasValue ? documentDetails.CompletedAt.ToString() : "N/A",
    //            Status = string.IsNullOrEmpty(documentDetails.Status) ? "N/A" : documentDetails.Status,
    //            FileId = documentDetails.FileId
    //        };

    //        logMessage = $"Successfully viewed Verification request details";
    //        SendAdminLog(ModuleNameConstants.Verification, ServiceNameConstants.Verification,
    //            "Get Verification users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //        return View(model);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> GetDocumentType(string issuerUid)
    //    {

    //        var response = await _documentVerifyIssuerService.GetDocTypeListByIdAsync(issuerUid);
    //        if (!response.Success)
    //        {
    //            return Json(new { success = false, message = response.Message});
    //        }
    //        var documentTypeEnumerable = (IEnumerable<IssuerOrgNamesDTO>)response.Resource;
    //        List<IssuerOrgNamesDTO> documentTypeDetails = documentTypeEnumerable.ToList();

    //        return Json(new { success = true, message = response.Message, result = documentTypeDetails });
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> GetVerificationMethods(string docType, string issuerUid)
    //    {

    //        var response = await _documentVerifyIssuerService.GetVerificationMethodListByDocTypeAsync(docType, issuerUid);
    //        if (!response.Success)
    //        {
    //            return Json(new { success = false, message = response.Message });
    //        }
    //        var verificationTypeEnumerable = (IEnumerable<VerificationMethodsDTO>)response.Resource;
    //        List<VerificationMethodsDTO> verificationMethodDetails = verificationTypeEnumerable
    //          .Where(vm => vm.verificationType != null).ToList();

    //        return Json(new { success = true, message = response.Message, result = verificationMethodDetails });
    //    }

    //    public async Task<IActionResult> DownloadReport(string fileId)
    //    {
    //        try
    //        {
    //            var downloadReportDetails = await _documentVerifyIssuerService.GetDownloadReportByFileId(fileId);
    //            if (downloadReportDetails == null)
    //            {
    //                return NotFound();
    //            }

    //            return File(downloadReportDetails.VerificationReport, "application/pdf", downloadReportDetails.FileName);

    //        }
    //        catch (InvalidOperationException ex)
    //        {
    //            return NotFound(ex.Message);
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, "An error occurred while processing the request.");
    //        }

    //    }

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

    //                    // Add the second PDF (True Copy) to the zip archive
    //                    var fileName = fileData.FileName;
    //                    var entry2 = archive.CreateEntry(fileName.Replace(".pdf", "") + "_TrueCopy.pdf");
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
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, "Internal Server Error");
    //        }
    //    }

    //}
}
