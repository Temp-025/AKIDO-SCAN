namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[Authorize]
    //public class KYCDashboardController : BaseController
    //{

    //    private readonly ILogger<KYCDashboardController> _logger;
    //    private readonly IKYCLogReportsService _kycLogReportsService;
    //    private readonly IHttpClientFactory _httpClientFactory;
    //    private readonly IConfiguration _configuration;
    //    private readonly DataExportService _dataExportService;
    //    private readonly IRazorRendererHelper _razorRendererHelper;



    //    public KYCDashboardController(ILogger<KYCDashboardController> logger, DataExportService dataExportService, IKYCLogReportsService kycLogReportService, IRazorRendererHelper razorRendererHelper, IAdminLogService adminLogService, IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(adminLogService)

    //    {
    //        _logger = logger;
    //        _kycLogReportsService = kycLogReportService;
    //        _httpClientFactory = httpClientFactory;
    //        _configuration = configuration;
    //        _razorRendererHelper = razorRendererHelper;
    //        _dataExportService = dataExportService;
    //    }


    //    public async Task<IActionResult> Index(string startDate, string endDate, int page = 1, int perPage = 10)
    //    {
    //        _logger.LogInformation("KYC Dashboard - Request received. StartDate: {StartDate}, EndDate: {EndDate}, Page: {Page}, PerPage: {PerPage}", startDate, endDate, page, perPage);
    //        try
    //        {
    //            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
    //            {
    //                var today = DateTime.Today;
    //                startDate = new DateTime(today.Year, today.Month, 1).ToString("yyyy-MM-dd");
    //                endDate = today.ToString("yyyy-MM-dd");
    //            }

    //            var orgId = _configuration["KycOrganizationUid"];

    //            if (string.IsNullOrEmpty(orgId))
    //            {
    //                _logger.LogError("Organization ID not found in Configuration");
    //                return BadRequest("Organization ID not configured");
    //            }

    //            _logger.LogInformation("Fetching KYC reports for Organization: {OrgId}", orgId);

    //            PaginatedList<KYCValidationResponseDTO> report = null;
    //            OrgKycSummaryDTO orgReport = null;
    //            int noOfKycDevices = 0;

    //            try
    //            {
    //                var reportTask = _kycLogReportsService.GetKYCValidationReportAsync("", "", orgId, null, "", [], 1);
    //                var orgReportTask = _kycLogReportsService.GetOrganizationIdValidationSummaryAsync(orgId);
    //                var devicesResultTask = _kycLogReportsService.GetKycDevicesCountOfOrg(orgId);

    //                await Task.WhenAll(reportTask, orgReportTask, devicesResultTask);

    //                report = await reportTask ?? new PaginatedList<KYCValidationResponseDTO>(new List<KYCValidationResponseDTO>(), 1, 1, 1, 0);
    //                orgReport = await orgReportTask ?? new OrgKycSummaryDTO();

    //                var devicesResult = await devicesResultTask;
    //                if (devicesResult != null && devicesResult.Success && devicesResult.Resource != null)
    //                    noOfKycDevices = Convert.ToInt32(devicesResult.Resource);
    //                else
    //                    noOfKycDevices = 0;
    //            }
    //            catch (Exception ex)
    //            {
    //                _logger.LogError(ex, "Error fetching KYC reports or summary for OrgId: {OrgId}", orgId);
    //                report ??= new PaginatedList<KYCValidationResponseDTO>(new List<KYCValidationResponseDTO>(), 1, 1, 1, 0);
    //                orgReport ??= new OrgKycSummaryDTO();
    //                noOfKycDevices = 0;
    //            }

    //            _logger.LogInformation("All kyc api sucess start.");

    //            ViewBag.StartDate = startDate;
    //            ViewBag.EndDate = endDate;

    //            var viewmodel = new KYCDashboardViewModel
    //            {
    //                Report = report,
    //                orgReport = orgReport,
    //                NoOfKycDevices = noOfKycDevices,
    //                OrgID = orgId
    //            };
    //            _logger.LogInformation("Kyc dashboard sucessfully loaded.");
    //            return View(viewmodel);
    //        }
    //        catch(Exception ex)
    //        {
    //            _logger.LogError(ex, "Error occured while loading KYC Dashboard. StartDate: {StartDate}, EndDate: {EndDate}, Page: {Page}, PerPage: {PerPage}", startDate, endDate, page, perPage);
    //            return View("Error");
    //        }
    //    }


    //    [HttpPost("GetBatchPaginatedKYCLogReport")]
    //    public async Task<IActionResult> GetBatchPaginatedKYCLogReport()
    //    {
    //        var pageNumber = Convert.ToInt32(Request.Form["page"].FirstOrDefault());
    //        var status = Request.Form["status"].FirstOrDefault();
    //        var selectedDate = Request.Form["selectedDate"].FirstOrDefault();
    //        var searchValue = Request.Form["searchValue"].FirstOrDefault();
    //        var id = Request.Form["orgId"].FirstOrDefault();
    //        var perpage = 10;
    //        var fromDate = string.Empty;
    //        var toDate = string.Empty;
    //        if (!string.IsNullOrEmpty(selectedDate))
    //        {
    //            fromDate = selectedDate + " 00:00:00";
    //            toDate = selectedDate + " 23:59:59";
    //        }

    //        var reports = await _kycLogReportsService.GetKYCValidationReportAsync(searchValue, status, id, fromDate, toDate, ["BATCH_CARD_STATUS"], pageNumber + 1, perpage);

    //        if (reports == null || !reports.Any())
    //        {
    //            return Json(new { totalCount = 0, records = new List<KYCValidationResponseDTO>() });
    //        }


    //        return Json(new
    //        {
    //            totalCount = reports.TotalCount,
    //            data = reports
    //        });


    //    }

    //    [HttpPost("UpdateServiceProviderPage")]
    //    public async Task<IActionResult> UpdateServiceProviderPage(string id, int page, int perpage)
    //    {
    //        var orgReport = await _kycLogReportsService.GetOrganizationIdValidationSummaryAsync(id);

    //        var kycDevicesList = await _kycLogReportsService.GetKycDevicesOfOrg(id);
    //        if (kycDevicesList != null && kycDevicesList.Resource is List<string> deviceList)
    //        {
    //            orgReport.KycDevices = deviceList;
    //        }
    //        else
    //        {
    //            orgReport.KycDevices = new List<string>();
    //        }

    //        var viewModel = new ServiceProviderPageViewModel
    //        {
    //            OrgData = orgReport,
    //        };

    //        return Json(viewModel);
    //    }


    //    [HttpPost("GetPaginatedIdUsers")]
    //    public async Task<IActionResult> GetPaginatedIdUsers()
    //    {
    //        var pageNumber = Convert.ToInt32(Request.Form["pageNumber"].FirstOrDefault());
    //        var status = Request.Form["status"].FirstOrDefault();
    //        var fromDate = Request.Form["fromDate"].FirstOrDefault();
    //        var toDate = Request.Form["toDate"].FirstOrDefault();
    //        var searchValue = Request.Form["searchValue"].FirstOrDefault();
    //        var id = Request.Form["id"].FirstOrDefault();
    //        var perpage = 10;

    //        List<string> services = [
    //            "CARD_STATUS_WITH_EID_READER", 
    //            "CARD_STATUS_WITH_OCR", 
    //            "CARD_AND_FACE_STATUS_WITH_OCR",
    //            "CARD_AND_FACE_VERIFY_WITH_OCR",
    //            "CARD_AND_FINGERPRINT_VERIFICATION_WITH_EID_READER_AND_BIOMETRIC_SENSOR",
    //            "CARD_STATUS_WITH_MANUAL_ENTRY",
    //            "CARD_AND_FACE_STATUS_REMOTE_VERIFICATION",
    //            "CARD_AND_FACE_AUTH_WITH_MANUAL_ENTRY",
    //            "CARD_AND_FACE_VERIFY_WITH_MANUAL_ENTRY",
    //            "CARD_AND_FINGERPRINT_STATUS_WITH_MANUAL_ENTRY",
    //            "PASSPORT_STATUS_WITH_MANUAL_ENTRY",
    //            "PASSPORT_STATUS_WITH_OCR",
    //            "PASSPORT_AND_FACE_VERIFY_OCR",
    //            "PASSPORT_AND_FACE_VERIFY_MANUAL_ENTRY"
    //        ];


    //        var reports = await _kycLogReportsService.GetKYCValidationReportAsync(searchValue, status, id, fromDate, toDate, services, pageNumber + 1, perpage);

    //        if (reports == null || !reports.Any())
    //        {
    //            return Json(new { totalCount = 0, records = new List<KYCValidationResponseDTO>() });
    //        }


    //        return Json(new
    //        {
    //            totalCount = reports.TotalCount,
    //            data = reports
    //        });


    //    }

    //    [HttpPost("PrintIdValidationData")]
    //    public IActionResult PrintIdValidationData(string SignedData, string VerificationResponse)
    //    {
    //        // Deserialize JSON back to objects
    //        var verificationResponse = JsonConvert.DeserializeObject<KYCValidationResponseDTO>(VerificationResponse);
    //        var signedData = JsonConvert.DeserializeObject<VerifiedIdValidationResponseDTO>(SignedData);

    //        TimeZoneInfo gstZone = TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time"); // GST = UTC+4
    //        DateTime gstTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gstZone);

    //        string timeStamp = gstTime.ToString("dd MMMM yyyy, HH:mm:ss") + " GST";


    //        var viewModel = new PrintViewModel
    //        {
    //            Name = string.IsNullOrWhiteSpace(signedData?.name) ? "N/A" : signedData.name,
    //            EmirateId = string.IsNullOrWhiteSpace(signedData?.idNumber) ? "N/A" : signedData.idNumber,
    //            ServiceProvider = string.IsNullOrWhiteSpace(verificationResponse?.orgName) ? "N/A" : verificationResponse.orgName,
    //            VerificationMethod = string.IsNullOrWhiteSpace(verificationResponse?.kycMethod) ? "N/A" : verificationResponse.kycMethod,
    //            RequestDate = verificationResponse?.validationDateTime?.ToString() ?? "N/A",
    //            ProcessingTime = "TBD",
    //            ICPReference = "TBD",
    //            VerificationResult = string.IsNullOrWhiteSpace(verificationResponse?.status) ? "N/A" : verificationResponse.status,
    //            OverallConfidence = "TBD",
    //            VerificationCost = "TBD",
    //            ConsentObtained = "Yes",
    //            DataRetention = "As per UAE regulations",
    //            Status = "Signed & Verified",
    //            CertificateAuthority = "ICP Root CA",
    //            Signatory = "ID Validation Platform",
    //            TimeStamp = string.IsNullOrWhiteSpace(timeStamp?.ToString()) ? "N/A" : timeStamp.ToString(),
    //            ReportId = string.IsNullOrWhiteSpace(verificationResponse?.identifier) ? "N/A" : verificationResponse.identifier
    //        };

    //        return View("PrintIdValidationData", viewModel);
    //    }

    //    [HttpPost]
    //    [Route("[action]")]
    //    public JsonResult DownloadPdf([FromBody] PrintViewModel viewModel)
    //   // public async Task<IActionResult> DownloadPdf(PrintViewModel viewModel)
    //    {
    //        var partialName = "/Views/KYCDashboard/DownloadValidationData.cshtml";
    //        var htmlContent = _razorRendererHelper.RenderPartialToString(partialName, viewModel);
    //        byte[] pdfBytes = _dataExportService.GeneratePdf(htmlContent);

    //       // return File(pdfBytes, "application/pdf", "Report.pdf");
    //        return Json(new { Status = "Success", Title = "Generate PDF", Message = "Successfully Generated PDF bytes", Result = pdfBytes });

    //    }

    //    [HttpPost]
    //    public IActionResult DownloadPrint([FromBody] PrintViewModel viewModel)
    //    {
    //        // model will now contain all the data sent from JS
    //        return PartialView("DownloadValidationData", viewModel); // render download.cshtml with data
    //    }

    //    [HttpPost]
    //    public IActionResult PrintIdValidationReportDirect(PrintViewModel viewModel)
    //    {
    //        var partialName = "/Views/KycDashboard/DownloadValidationData.cshtml";
    //        var htmlContent = _razorRendererHelper.RenderPartialToString(partialName, viewModel);

    //        // html → pdf
    //        var pdfBytes = _dataExportService.GeneratePdf(htmlContent);
    //        return File(pdfBytes, "application/pdf");
    //    }

    //}
}
