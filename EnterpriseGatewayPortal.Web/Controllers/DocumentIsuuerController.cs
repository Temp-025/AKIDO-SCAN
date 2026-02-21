namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[ServiceFilter(typeof(SessionValidationAttribute))]
    //[Authorize]
    //public class DocumentIsuuerController : BaseController
    //{
    //    private readonly IConfiguration _configuration;
    //    private readonly IDocumentIssuerService _documentIssuerService;
    //    private readonly ILocalBusinessUsersService _localBusinessUsersService;
    //    private readonly IOrganizationService _organizationService;

    //    public DocumentIsuuerController(IAdminLogService adminLogService, ILocalBusinessUsersService localBusinessUsersService, IDocumentIssuerService documentIssuerService,IOrganizationService organizationService,
    //        IConfiguration configuration

    //       ) : base(adminLogService)
    //    {
    //        _documentIssuerService = documentIssuerService;
    //        _localBusinessUsersService = localBusinessUsersService;
    //        _configuration = configuration;
    //        _organizationService = organizationService;

    //    }
    //    [HttpGet]
    //    public async Task<IActionResult> List()
    //    {
    //        string logMessage;
    //        var response = await _documentIssuerService.GetAllDocIssuerListAsync();
    //        if (response == null)
    //        {
    //            logMessage = $"Failed to get document issuer list";
    //            SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //                 "Get document issuer list", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return View(response);
    //        }

    //        return View(response);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> CreateDocumentIssuer()
    //    {
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var businessUserList = await _localBusinessUsersService.GetAllBusinessUsersByOrgUidAsync(organizationUid);
    //        if (businessUserList == null)
    //        {
    //            return RedirectToAction("List");
    //        }
    //        var businessUser = (IEnumerable<OrgSubscriberEmail>)businessUserList.Resource;
    //        var viewModel = new CreateDocIssuerViewModel();
    //        viewModel.businessUsers = businessUser;
    //        return View(viewModel);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetDocIssuerDetails(int docIssuerId)
    //    {
    //        string logMessage;
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var businessUserList = await _localBusinessUsersService.GetAllBusinessUsersByOrgUidAsync(organizationUid);
    //        if (businessUserList == null)
    //        {
    //            return RedirectToAction("List");
    //        }
    //        var businessUser = (IEnumerable<OrgSubscriberEmail>)businessUserList.Resource;
    //        var documentDetails = await _documentIssuerService.GetDocIssuerDetailsByIdAsync(docIssuerId);
    //        if (documentDetails == null)
    //        {
    //            logMessage = $"Failed to get document issuer details by id";
    //            SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //                 "Get document issuer by id", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //            return RedirectToAction("List");
    //        }

    //        var viewModel = new CreateDocIssuerViewModel()
    //        {
    //            id = documentDetails.Id,
    //            documentName = documentDetails.DocumentName,

    //            businessUsers = businessUser,
    //            allowedIssuers = documentDetails.AllowedIssuers
    //        };
    //        if (documentDetails.Technical != null)
    //        {
    //            var technicalMethod = JsonConvert.DeserializeObject<Verification>(documentDetails.Technical.ToString());
    //            if (technicalMethod.VerificationType == VerificationTypeConstants.Technical)
    //            {
    //                viewModel.techincal = true;
    //            }
    //            else
    //            {
    //                viewModel.techincal = false;
    //            }
    //            viewModel.daysForTechincal = technicalMethod.Duration;
    //            viewModel.priceForTechincal = technicalMethod.Price;
    //        }
    //        if (documentDetails.Qr != null)
    //        {
    //            var qrMethod = JsonConvert.DeserializeObject<Verification>(documentDetails.Qr.ToString());
    //            if (qrMethod.VerificationType == VerificationTypeConstants.Qr)
    //            {
    //                viewModel.qrcode = true;
    //            }
    //            else
    //            {
    //                viewModel.qrcode = false;
    //            }
    //            viewModel.daysForQrcode = qrMethod.Duration;
    //            viewModel.priceForQrcode = qrMethod.Price;
    //        }
    //        if (documentDetails.Truecopy != null)
    //        {
    //            var trueCopyMethod = JsonConvert.DeserializeObject<Verification>(documentDetails.Truecopy.ToString());
    //            if (trueCopyMethod.VerificationType == VerificationTypeConstants.TrueCopy)
    //            {
    //                viewModel.trueCopyVerification = true;
    //            }
    //            else
    //            {
    //                viewModel.trueCopyVerification = false;
    //            }
    //            viewModel.daysForTrueCopyVerification = trueCopyMethod.Duration;
    //            viewModel.priceForTrueCopyVerification = trueCopyMethod.Price;
    //        }

    //        var allowedConsumersArray = documentDetails.AllowedConsumers.Split(',').Select(s => s.Trim()).ToArray();

    //        bool relayingParty = allowedConsumersArray.Contains("Relaying Party");
    //        if (relayingParty == true)
    //        {
    //            viewModel.relayingParty = true;
    //        }
    //        else
    //        {
    //            viewModel.relayingParty = false;
    //        }


    //        bool individual = allowedConsumersArray.Contains("Individual");
    //        if (individual == true)
    //        {
    //            viewModel.individual = true;
    //        }
    //        else
    //        {
    //            viewModel.individual = false;
    //        }


    //        bool professional = allowedConsumersArray.Contains("Professional");

    //        if (professional == true)
    //        {
    //            viewModel.professional = true;
    //        }
    //        else
    //        {
    //            viewModel.professional = false;
    //        }

    //        if (documentDetails.SubscriptionFee != null)
    //        {
    //            viewModel.subscriptionFee = documentDetails.SubscriptionFee.Value;
    //        }

    //        logMessage = $"Successfully received document issuer details";
    //        SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //             "Get document issuer by id", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //        return View(viewModel);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> ViewDocIssuerDetails(int docIssuerId)
    //    {
    //        string logMessage;
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var businessUserList = await _localBusinessUsersService.GetAllBusinessUsersByOrgUidAsync(organizationUid);
    //        if (businessUserList == null)
    //        {
    //            return RedirectToAction("List");
    //        }
    //        var businessUser = (IEnumerable<OrgSubscriberEmail>)businessUserList.Resource;
    //        var documentDetails = await _documentIssuerService.GetDocIssuerDetailsByIdAsync(docIssuerId);
    //        if (documentDetails == null)
    //        {
    //            logMessage = $"Failed to get document issuer details by id";
    //            SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //                 "Get document issuer by id", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //            return RedirectToAction("List");
    //        }
    //        var viewModel = new CreateDocIssuerViewModel()
    //        {
    //            id = documentDetails.Id,
    //            documentName = documentDetails.DocumentName,

    //            businessUsers = businessUser,
    //            allowedIssuers = documentDetails.AllowedIssuers
    //        };
    //        if (documentDetails.Technical != null)
    //        {
    //            var technicalMethod = JsonConvert.DeserializeObject<Verification>(documentDetails.Technical.ToString());
    //            if (technicalMethod.VerificationType == VerificationTypeConstants.Technical)
    //            {
    //                viewModel.techincal = true;
    //            }
    //            else
    //            {
    //                viewModel.techincal = false;
    //            }
    //            viewModel.daysForTechincal = technicalMethod.Duration;
    //            viewModel.priceForTechincal = technicalMethod.Price;
    //        }
    //        if (documentDetails.Qr != null)
    //        {
    //            var qrMethod = JsonConvert.DeserializeObject<Verification>(documentDetails.Qr.ToString());
    //            if (qrMethod.VerificationType == VerificationTypeConstants.Qr)
    //            {
    //                viewModel.qrcode = true;
    //            }
    //            else
    //            {
    //                viewModel.qrcode = false;
    //            }
    //            viewModel.daysForQrcode = qrMethod.Duration;
    //            viewModel.priceForQrcode = qrMethod.Price;
    //        }
    //        if (documentDetails.Truecopy != null)
    //        {
    //            var trueCopyMethod = JsonConvert.DeserializeObject<Verification>(documentDetails.Truecopy.ToString());
    //            if (trueCopyMethod.VerificationType == VerificationTypeConstants.TrueCopy)
    //            {
    //                viewModel.trueCopyVerification = true;
    //            }
    //            else
    //            {
    //                viewModel.trueCopyVerification = false;
    //            }
    //            viewModel.daysForTrueCopyVerification = trueCopyMethod.Duration;
    //            viewModel.priceForTrueCopyVerification = trueCopyMethod.Price;
    //        }

    //        var allowedConsumersArray = documentDetails.AllowedConsumers.Split(',').Select(s => s.Trim()).ToArray();

    //        bool relayingParty = allowedConsumersArray.Contains("Relaying Party");
    //        if (relayingParty == true)
    //        {
    //            viewModel.relayingParty = true;
    //        }
    //        else
    //        {
    //            viewModel.relayingParty = false;
    //        }


    //        bool individual = allowedConsumersArray.Contains("Individual");
    //        if (individual == true)
    //        {
    //            viewModel.individual = true;
    //        }
    //        else
    //        {
    //            viewModel.individual = false;
    //        }


    //        bool professional = allowedConsumersArray.Contains("Professional");

    //        if (professional == true)
    //        {
    //            viewModel.professional = true;
    //        }
    //        else
    //        {
    //            viewModel.professional = false;
    //        }

    //        if (documentDetails.SubscriptionFee != null)
    //        {
    //            viewModel.subscriptionFee = documentDetails.SubscriptionFee.Value;
    //        }

    //        logMessage = $"Successfully received document issuer details";
    //        SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //             "Get document issuer by id", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //        return View(viewModel);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> SaveCreateDocumentIssure([FromBody] CreateDocIssuerViewModel createDocIssuerViewModel)
    //    {
    //        string logMessage;
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);

    //        Verification technicalVerification = new Verification();
    //        if (createDocIssuerViewModel.techincal == true)
    //        {
    //            technicalVerification.VerificationType = VerificationTypeConstants.Technical;
    //        }
    //        else
    //        {
    //            technicalVerification.VerificationType = null;
    //        }

    //        technicalVerification.Duration = createDocIssuerViewModel.daysForTechincal;
    //        technicalVerification.Price = createDocIssuerViewModel.priceForTechincal;

    //        Verification QrVerification = new Verification();
    //        if (createDocIssuerViewModel.qrcode == true)
    //        {
    //            QrVerification.VerificationType = VerificationTypeConstants.Qr;
    //        }
    //        else
    //        {
    //            QrVerification.VerificationType = null;
    //        }
    //        QrVerification.Duration = createDocIssuerViewModel.daysForQrcode;
    //        QrVerification.Price = createDocIssuerViewModel.priceForQrcode;

    //        Verification trueCopyVerification = new Verification();
    //        if (createDocIssuerViewModel.trueCopyVerification == true)
    //        {
    //            trueCopyVerification.VerificationType = VerificationTypeConstants.TrueCopy;
    //        }
    //        else
    //        {
    //            trueCopyVerification.VerificationType = null;
    //        }
    //        trueCopyVerification.Duration = createDocIssuerViewModel.daysForTrueCopyVerification;
    //        trueCopyVerification.Price = createDocIssuerViewModel.priceForTrueCopyVerification;

    //        string techniacal = JsonConvert.SerializeObject(technicalVerification);
    //        string qrCode = JsonConvert.SerializeObject(QrVerification);
    //        string trueCopy = JsonConvert.SerializeObject(trueCopyVerification);

    //        List<string> selectedNames = new List<string>();

    //        // Check conditions and add names to the list
    //        if (createDocIssuerViewModel.relayingParty == true)
    //        {
    //            selectedNames.Add("Relaying Party");
    //        }

    //        if (createDocIssuerViewModel.individual == true)
    //        {
    //            selectedNames.Add("Individual");
    //        }

    //        if (createDocIssuerViewModel.professional == true)
    //        {
    //            selectedNames.Add("Professional");
    //        }

    //        // Create a comma-separated string from the list elements
    //        string consumers = string.Join(", ", selectedNames);

    //        SaveDocumentIssuerDTO saveDocumentIssuerDTO = new SaveDocumentIssuerDTO()
    //        {
    //            DocumentName = createDocIssuerViewModel.documentName,
    //            CreatedAt = DateTime.Now,
    //            UpdatedAt = DateTime.Now,
    //            CreatedBy = FullName,
    //            UpdatedBy = FullName,
    //            AllowedConsumers = consumers,
    //            AllowedIssuers = createDocIssuerViewModel.allowedIssuers,
    //            Technical = techniacal,
    //            Qr = qrCode,
    //            Truecopy = trueCopy,
    //            IssuerUid = userDTO.OrganizationId,
    //            IssuerOrgName = userDTO.OrganizationName,
    //            Status = DocumentStatusConstants.Unpublish,
    //            SubscriptionFee = createDocIssuerViewModel.subscriptionFee

    //        };
    //        var response = await _documentIssuerService.AddDocumentissuerAsync(saveDocumentIssuerDTO);
    //        if (response.Success)
    //        {
    //            logMessage = $"Successfully added document issuer details";
    //            SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //                 "add document issuer", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return Json(new { Status = "Success", Title = "Save Document Issure", Message = response.Message });
    //        }
    //        else
    //        {
    //            logMessage = $"Failed to add document issuer details";
    //            SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //                 "add document issuer", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return Json(new { Status = "Failed", Title = "Save Document Issuer", Message = response.Message });
    //        }
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> UpdateDocumentIssure([FromBody] CreateDocIssuerViewModel updateDocIssuer)
    //    {
    //        string logMessage;
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);

    //        Verification technicalVerification = new Verification();
    //        if (updateDocIssuer.techincal == true)
    //        {
    //            technicalVerification.VerificationType = VerificationTypeConstants.Technical;
    //        }
    //        else
    //        {
    //            technicalVerification.VerificationType = null;
    //        }

    //        technicalVerification.Duration = updateDocIssuer.daysForTechincal;
    //        technicalVerification.Price = updateDocIssuer.priceForTechincal;

    //        Verification QrVerification = new Verification();
    //        if (updateDocIssuer.qrcode == true)
    //        {
    //            QrVerification.VerificationType = VerificationTypeConstants.Qr;
    //        }
    //        else
    //        {
    //            QrVerification.VerificationType = null;
    //        }
    //        QrVerification.Duration = updateDocIssuer.daysForQrcode;
    //        QrVerification.Price = updateDocIssuer.priceForQrcode;

    //        Verification trueCopyVerification = new Verification();
    //        if (updateDocIssuer.trueCopyVerification == true)
    //        {
    //            trueCopyVerification.VerificationType = VerificationTypeConstants.TrueCopy;
    //        }
    //        else
    //        {
    //            trueCopyVerification.VerificationType = null;
    //        }
    //        trueCopyVerification.Duration = updateDocIssuer.daysForTrueCopyVerification;
    //        trueCopyVerification.Price = updateDocIssuer.priceForTrueCopyVerification;

    //        string techniacal = JsonConvert.SerializeObject(technicalVerification);
    //        string qrCode = JsonConvert.SerializeObject(QrVerification);
    //        string trueCopy = JsonConvert.SerializeObject(trueCopyVerification);

    //        List<string> selectedNames = new List<string>();

    //        // Check conditions and add names to the list
    //        if (updateDocIssuer.relayingParty == true)
    //        {
    //            selectedNames.Add("Relaying Party");
    //        }

    //        if (updateDocIssuer.individual == true)
    //        {
    //            selectedNames.Add("Individual");
    //        }

    //        if (updateDocIssuer.professional == true)
    //        {
    //            selectedNames.Add("Professional");
    //        }

    //        // Create a comma-separated string from the list elements
    //        string consumers = string.Join(", ", selectedNames);
    //        SaveDocumentIssuerDTO updateDocumentIssuerDTO = new SaveDocumentIssuerDTO()
    //        {
    //            Id = updateDocIssuer.id,
    //            DocumentName = updateDocIssuer.documentName,
    //            CreatedAt = DateTime.Now,
    //            UpdatedAt = DateTime.Now,
    //            CreatedBy = FullName,
    //            UpdatedBy = FullName,
    //            AllowedConsumers = consumers,
    //            AllowedIssuers = updateDocIssuer.allowedIssuers,
    //            Technical = techniacal,
    //            Qr = qrCode,
    //            Truecopy = trueCopy,
    //            IssuerUid = userDTO.OrganizationId,
    //            IssuerOrgName = userDTO.OrganizationName,
    //            Status = DocumentStatusConstants.Unpublish,
    //            SubscriptionFee = updateDocIssuer.subscriptionFee
    //        };
    //        var response = await _documentIssuerService.UpdateDocumentissuerAsync(updateDocumentIssuerDTO);
    //        if (response.Success)
    //        {
    //            logMessage = $"Successfully updated document issuer details";
    //            SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //                 "add document issuer", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return Json(new { Status = "Success", Title = "Save Document Issure", Message = response.Message });
    //        }
    //        else
    //        {
    //            logMessage = $"Failed to update document issuer details";
    //            SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //                 "add document issuer", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return Json(new { Status = "Failed", Title = "Save Document Issuer", Message = response.Message });
    //        }
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> UpdateIssuerStatus([FromBody] UpdateIssuerStatusViewModel updateIssuerStatus)
    //    {
    //        UpdatedocumentIssuerStatusDTO updatedocumentIssuerStatus = new UpdatedocumentIssuerStatusDTO()
    //        {
    //            DocId = updateIssuerStatus.docId,
    //            Action = updateIssuerStatus.action,
    //        };

    //        var response = await _documentIssuerService.UpdateDocuemntIssuerStatusAsync(updatedocumentIssuerStatus);
    //        if (response.Success)
    //        {
    //            return Json(new { Status = "Success", Title = "Save Document Issure", Message = response.Message });
    //        }
    //        else
    //        {
    //            return Json(new { Status = "Failed", Title = "Save Document Issuer", Message = response.Message });
    //        }
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> DocumentIssuerPrivilage()
    //    {
    //        string logMessage;
    //        var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
    //        var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
    //        var response = await _organizationService.GetPrevilagesAsync(userDTO.OrganizationId);
    //        if (response == null)
    //        {
    //            return Json(new { Status = "Failed", Title = "Save Document Issuer", Result = response });
    //        }
    //        else
    //        {
    //            return Json(new { Status = "Success", Title = "Save Document Issure", Result = response });
    //        }
    //    }


    //}
}
