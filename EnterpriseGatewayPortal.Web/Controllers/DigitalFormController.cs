namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[ServiceFilter(typeof(SessionValidationAttribute))]
    //[Authorize]
    //public class DigitalFormController : BaseController
    //{
    //    private readonly IDigitalFormService _digitalFormService;
    //    private readonly IConfiguration _configuration;

    //    public DigitalFormController(IAdminLogService adminLogService, IDigitalFormService digitalFormService,
    //        IConfiguration configuration) : base(adminLogService)
    //    {
    //        _digitalFormService = digitalFormService;
    //        _configuration = configuration;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> Index()
    //    {
    //        try
    //        {
    //            string logMessage;

    //            var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
    //            var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = await _digitalFormService.GetDocumentTemplateListAsync(apiToken);
    //            if (response == null)
    //            {
    //                Models.AlertViewModel alert = new Models.AlertViewModel { IsSuccess = false, Message = "Error while fetching the data" };
    //                TempData["Alert"] = JsonConvert.SerializeObject(alert);
    //                //return NotFound();
    //                return RedirectToAction("Index", "Dashboard");
    //            }
    //            var model = (IList<DocumentTemplateDTO>)response.Resource;

    //            var viewModelList = new List<DocumentTemplateViewModel>();

    //            foreach (var item in model)
    //            {
    //                var response_1 = await _digitalFormService.GetResponseListCount(item._id, apiToken);
    //                if (response_1.Resource == null)
    //                {
    //                    Alert alert = new Alert { IsSuccess = true, Message = response_1.Message };
    //                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
    //                }
    //                else
    //                {
    //                    var list = (IList<DigitalFormResponseDTO>)response_1.Resource;
    //                    var viewModel = new DocumentTemplateViewModel
    //                    {
    //                        TemplateName = item.TemplateName,
    //                        OrganizationUid = item.OrganizationUid,
    //                        Email = item.Email,
    //                        Suid = item.Suid,
    //                        Status = item.Status,
    //                        EdmsId = item.EdmsId,
    //                        DocumentName = item.DocumentName,
    //                        AdvancedSettings = item.AdvancedSettings,
    //                        DaysToComplete = item.DaysToComplete,
    //                        NumberOfSignatures = item.NumberOfSignatures,
    //                        AllSigRequired = item.AllSigRequired,
    //                        PublishGlobally = item.PublishGlobally,
    //                        SequentialSigning = item.SequentialSigning,
    //                        CreatedBy = item.CreatedBy,
    //                        UpdatedBy = item.UpdatedBy,
    //                        Model = item.Model,
    //                        _id = item._id,
    //                        CreatedAt = item.CreatedAt,
    //                        UpdatedAt = item.UpdatedAt,
    //                        Count = list.Count()  // Assigning the count of the entire list to each item in the view model
    //                    };

    //                    viewModelList.Add(viewModel);
    //                }
    //            }

    //            logMessage = $"Get Digital Forms List";
    //            SendAdminLog(ModuleNameConstants.DigitalForm, ServiceNameConstants.DigitalForm,
    //                "Successfully Received Digital Forms List", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
    //            return View(viewModelList);

    //        }
    //        catch (Exception ex)
    //        {

    //           return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> publishedForms()
    //    {
    //        try
    //        {
    //            string logMessage;

    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = _digitalFormService.GetDocumentTemplatePublishListAsync(apiToken);
    //            if (response.Result == null)
    //            {
    //                return NotFound();
    //            }
    //            var model = (List<DocTemplateResponseDTO>)response.Result.Resource;
    //            ViewBag.PublishForm = "publishForm";

    //            logMessage = $"Get Published Forms List";
    //            SendAdminLog(ModuleNameConstants.DigitalForm, ServiceNameConstants.DigitalForm,
    //                "Successfully Received Published Forms List", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //            return View(model);

    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> standardForms()
    //    {
    //        try
    //        {
    //            string logMessage;

    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = _digitalFormService.GetDocumentTemplatePublishGlobalListAsync(apiToken);
    //            if (response.Result == null)
    //            {
    //                return NotFound();
    //            }
    //            var model = (List<DocTemplateResponseDTO>)response.Result.Resource;

    //            logMessage = $"Get Standard Forms List";
    //            SendAdminLog(ModuleNameConstants.DigitalForm, ServiceNameConstants.DigitalForm,
    //                "Successfully Received Standard Forms List", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //            return View(model);

    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex.Message);
    //        }

    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> createForms()
    //    {
    //        try
    //        {
    //            string logMessage;
    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = _digitalFormService.GetSignatureTemplateList(apiToken);
    //            if (response.Result == null)
    //            {
    //                return NotFound();
    //            }
    //            var SignatureTemplateList = (IList<SignatureTemplatesDTO>)response.Result.Resource;
    //            if (SignatureTemplateList == null)
    //            {
    //                return NotFound();
    //            }

    //            SignatureTemplateViewModel model = new SignatureTemplateViewModel
    //            {
    //                Templates = SignatureTemplateList
    //            };

    //            return View(model);

    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> Responses(string templateId)
    //    {
    //        try
    //        {
    //            string logMessage;
    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = _digitalFormService.GetResponseListCount(templateId, apiToken);
    //            if (response.Result == null)
    //            {
    //                return NotFound();
    //            }
    //            var model = (List<DigitalFormResponseDTO>)response.Result.Resource;

    //            logMessage = $"Get Response List";
    //            SendAdminLog(moduleName: ModuleNameConstants.DigitalForm, ServiceNameConstants.DigitalForm,
    //                "Successfully Received Response List", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //            return View(model);

    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> downloadCSV(string templateId,string FormName)
    //    {
    //        try
    //        {
    //            string logMessage;
    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = _digitalFormService.GetCsvResponseSheet(templateId, apiToken);
    //            if (response.Result == null)
    //            {
    //                return NotFound();
    //            }
    //            var model = (SaveResponseSheetDTO)response.Result.Resource;

    //            byte[] fileBytes = Convert.FromBase64String(model.fileContents);

    //            logMessage = $"Get CSV File";
    //            SendAdminLog(ModuleNameConstants.DigitalForm, ServiceNameConstants.DigitalForm,
    //                "Successfully Received CSV File", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //            return File(fileBytes, model.contentType, FormName + ".csv");

    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetPreviewConfig(string id)
    //    {
    //        try
    //        {
    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = await _digitalFormService.GetPreviewTemplateAsync(id, apiToken);
    //            if (response == null)
    //            {
    //                return NotFound();
    //            }
    //            return Ok(response);
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> previewForm(string edmsid,string documentName)
    //    {
    //        try
    //        {                
    //            DocumentTemplateViewModel model = new DocumentTemplateViewModel
    //            {
    //                EdmsId = edmsid,
    //                DocumentName = documentName,
    //            };

    //            return View(model);

    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex.Message);
    //        }
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> FormFillData(string Suid, string templateId, string edmsId, string digitalName)
    //    {

    //        try
    //        {

    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = _digitalFormService.GetDigitalFormFilldataAsync(Suid, apiToken);
    //            if (response.Result == null)
    //            {
    //                return NotFound();
    //            }

    //            FillFormDetailsDTO model = (FillFormDetailsDTO)response.Result.Resource;

    //            var viewModel = new DigitalFormFillViewModel
    //            {
    //                TemplateId= templateId,
    //                EdmsId = edmsId,
    //                Nationality = model.SubscriberData.Nationality,
    //                Gender = model.SubscriberData.Gender,
    //                DateOfBirth = model.SubscriberData.DateOfBirth,
    //                PrimaryIdentifier = model.SubscriberData.PrimaryIdentifier,
    //                SecondaryIdentifier = model.SubscriberData.SecondaryIdentifier,  
    //                DigitalFormName = digitalName,
    //            };

    //            return View(viewModel);

    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> EditForms(string templateId)
    //    {
    //        try
    //        {
    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);

    //            var response = await _digitalFormService.GetSignatureTemplateList(apiToken);
    //            if (response.Resource == null)
    //            {
    //                return NotFound();
    //            }

    //            var SignatureTemplateList = (IList<SignatureTemplatesDTO>)response.Resource;

    //            var response1 = _digitalFormService.GetDocTemplateById(templateId, apiToken);
    //            if (response1.Result == null)
    //            {
    //                return NotFound();
    //            }
    //            DocumentTemplateDTO model = (DocumentTemplateDTO)response1.Result.Resource;

    //            var templateProp = JsonConvert.DeserializeObject<AdvancedSettingsViewModel>(model.AdvancedSettings);


    //            var viewModel = new EditDigitalFormViewModel
    //            {
    //                Templates = SignatureTemplateList,
    //                TemplateName = model.TemplateName,
    //                documentName = model.DocumentName,
    //                EdmsId = model.EdmsId,
    //                allSigRequired = model.AllSigRequired,
    //                publishGlobally = model.PublishGlobally,
    //                advancedSettings = templateProp,
    //                sequentialSigning = model.SequentialSigning,
    //                daysToComplete = model.DaysToComplete,
    //                Model = model.Model,
    //                TemplateId = templateId,
    //            };

    //            return View(viewModel);
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> changeStatus(string templateId, string action)
    //    {

    //        try
    //        {
    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = _digitalFormService.UpdateTemplateStatusAsync(templateId, action, apiToken);

    //            if (!response.Result.Success)
    //            {
    //                return Json(new { Status = "Failed", Title = "Status", Message = response.Result.Message });
    //            }
    //            else
    //            {
    //                return Json(new { Status = "Success", Title = "Status", Message = response.Result.Message });
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            return Json(new { Error = ex.Message });
    //        }
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> saveForm(SignatureTemplateViewModel signatureTemplateViewModel)
    //    {
    //        try
    //        {
    //            string logMessage;

    //            JObject configObject = JObject.Parse(signatureTemplateViewModel.rolesConfig);

    //            JObject signCordinates = (JObject)configObject["signCordinates"];
    //            var signCord = new placeHolderCoordinates()
    //            {
    //                signatureXaxis = (string)signCordinates["posX"],
    //                signatureYaxis = (string)signCordinates["posY"],
    //                pageNumber = (string)signCordinates["PageNumber"],
    //                imgWidth = "126",
    //                imgHeight = "30.09"
    //            };

    //            JObject esealCordinates = (JObject)configObject["esealCordinates"];
    //            var esealCord = new esealplaceHolderCoordinates()
    //            {
    //                signatureXaxis = (string)esealCordinates["posX"],
    //                signatureYaxis = (string)esealCordinates["posY"],
    //                pageNumber = (string)esealCordinates["PageNumber"],
    //                imgWidth = "70",
    //                imgHeight = "70"
    //            };

    //            List<RoleDetails> rolesConfig = new List<RoleDetails>();
    //            var rolesCon = new RoleDetails
    //            {
    //                esealPlaceHolderCoordinates = esealCord,
    //                placeHolderCoordinates = signCord,
    //            };
    //            rolesConfig.Add(rolesCon);

    //            var dataConfig = JsonConvert.SerializeObject(rolesConfig);

    //            var docConfig = new DocumentTemplateModel
    //            {
    //                documentName = signatureTemplateViewModel.documentName,
    //                name = signatureTemplateViewModel.name,
    //                daysToComplete = signatureTemplateViewModel.daysToComplete,
    //                numberOfSignatures = signatureTemplateViewModel.numberOfSignatures,
    //                allSigRequired = signatureTemplateViewModel.allSigRequired,
    //                publishGlobally = signatureTemplateViewModel.publishGlobally,
    //                sequentialSigning = signatureTemplateViewModel.sequentialSigning,
    //                advancedSettings = signatureTemplateViewModel.advancedSettings,
    //                docType = "PDF",
    //            };

    //            JObject rolesObject = JObject.Parse(signatureTemplateViewModel.roles);

    //            List<TemplateRole> roles = new List<TemplateRole>();

    //            TemplateRole role = new TemplateRole
    //            {
    //                name = (string)rolesObject["name"],
    //                email = (string)rolesObject["email"],
    //                description = (string)rolesObject["description"]
    //            };

    //            roles.Add(role);


    //            var model = new TemplateModelDTO
    //            {
    //                docConfig = docConfig,
    //                roles = roles,
    //                rolesConfig = dataConfig
    //            };

    //            var dataJson = JsonConvert.SerializeObject(model);

    //            SaveNewDocumentTemplateDTO SaveNewDocumentTemplateDTO = new SaveNewDocumentTemplateDTO()
    //            {
    //                File = signatureTemplateViewModel.File,
    //                Model = dataJson
    //            };

    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = await _digitalFormService.SaveNewDocTemplate(SaveNewDocumentTemplateDTO, apiToken);
    //            if (!response.Success)
    //            {

    //                logMessage = $"Failed to Save Digital Form";
    //                SendAdminLog(ModuleNameConstants.DigitalForm, ServiceNameConstants.DigitalForm,
    //                    "Failed to Created Digital Forms", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //                return Json(new { Status = "Failed", Title = "Status", Message = response.Message });
    //            }
    //            else
    //            {

    //                logMessage = $"Save Digital Form";
    //                SendAdminLog(ModuleNameConstants.DigitalForm, ServiceNameConstants.DigitalForm,
    //                    "Successfully Created Digital Forms", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
    //                return Json(new { Status = "Success", Title = "Status", Message = response.Message });
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> updateForm(EditDigitalFormViewModel editDigitalFormViewModel)
    //    {
    //        try
    //        {
    //            string logMessage;

    //            JObject configObject = JObject.Parse(editDigitalFormViewModel.rolesConfig);

    //            JObject signCordinates = (JObject)configObject["signCordinates"];
    //            var signCord = new placeHolderCoordinates()
    //            {
    //                signatureXaxis = (string)signCordinates["posX"],
    //                signatureYaxis = (string)signCordinates["posY"],
    //                pageNumber = (string)signCordinates["PageNumber"]
    //            };

    //            JObject esealCordinates = (JObject)configObject["esealCordinates"];
    //            var esealCord = new esealplaceHolderCoordinates()
    //            {
    //                signatureXaxis = (string)esealCordinates["posX"],
    //                signatureYaxis = (string)esealCordinates["posY"],
    //                pageNumber = (string)esealCordinates["PageNumber"]
    //            };

    //            List<RoleDetails> rolesConfig = new List<RoleDetails>();
    //            var rolesCon = new RoleDetails
    //            {
    //                esealPlaceHolderCoordinates = esealCord,
    //                placeHolderCoordinates = signCord,
    //            };
    //            rolesConfig.Add(rolesCon);

    //            var dataConfig = JsonConvert.SerializeObject(rolesConfig);

    //            var docConfig = new DocumentTemplateModel
    //            {
    //                documentName = editDigitalFormViewModel.documentName,
    //                name = editDigitalFormViewModel.TemplateName,
    //                daysToComplete = editDigitalFormViewModel.daysToComplete,
    //                numberOfSignatures = editDigitalFormViewModel.numberOfSignatures,
    //                allSigRequired = editDigitalFormViewModel.allSigRequired,
    //                publishGlobally = editDigitalFormViewModel.publishGlobally,
    //                sequentialSigning = editDigitalFormViewModel.sequentialSigning,
    //                advancedSettings = editDigitalFormViewModel.advancedSettings2,
    //            };

    //            JObject rolesObject = JObject.Parse(editDigitalFormViewModel.roles);

    //            List<TemplateRole> roles = new List<TemplateRole>();

    //            TemplateRole role = new TemplateRole
    //            {
    //                name = (string)rolesObject["name"],
    //                email = (string)rolesObject["email"],
    //                description = (string)rolesObject["description"]
    //            };
    //            roles.Add(role);

    //            var model = new TemplateModelDTO
    //            {
    //                docConfig = docConfig,
    //                roles = roles,
    //                rolesConfig = dataConfig
    //            };

    //            var dataJson = JsonConvert.SerializeObject(model);

    //            UpdateDocumentTemplateDTO updateDocumentTemplateDTO = new UpdateDocumentTemplateDTO()
    //            {
    //                File = editDigitalFormViewModel.File,
    //                Model = dataJson,
    //                TemplateId = editDigitalFormViewModel.TemplateId,
    //            };

    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var response = await _digitalFormService.UpdateDocTemplate(updateDocumentTemplateDTO, apiToken);

    //            if (!response.Success)
    //            {

    //                logMessage = $"Failed To Update Digital Form";
    //                SendAdminLog(ModuleNameConstants.DigitalForm, ServiceNameConstants.DigitalForm,
    //                    "Failed To Update Digital Forms", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //                return Json(new { Status = "Failed", Title = "Status", Message = response.Message });
    //            }
    //            else
    //            {
    //                logMessage = $"Successfully Updated Digital Form";
    //                SendAdminLog(ModuleNameConstants.DigitalForm, ServiceNameConstants.DigitalForm,
    //                    "Successfully Updated Digital Form", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
    //                return Json(new { Status = "Success", Title = "Status", Message = response.Message });
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }

    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> SaveandSingFormData(SigningViewModel signingViewModel)
    //    {
    //        try
    //        {
    //            string logMessage;
    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var Idp_Token = AccessToken;
    //            var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
    //            var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
    //            var OrgId = userDTO.OrganizationId;

    //            JObject rolesObject = JObject.Parse(signingViewModel.FormFieldData);
    //            var data = JsonConvert.SerializeObject(rolesObject);

    //            SigningDigitalFormDTO dto = new SigningDigitalFormDTO
    //            {
    //                File = signingViewModel.File,
    //                TemplateId = signingViewModel.TemplateId,
    //                AccToken = apiToken,
    //                Idp_Token = Idp_Token,
    //                OrgUId = signingViewModel.isEseal ? OrgId : string.Empty,
    //                FormFieldData = data
    //            };

    //            var response = await _digitalFormService.SaveNewDigitalFormRespons(dto, apiToken);
    //            if (!response.Success)
    //            {
    //                logMessage = $"Failed To Sign Digital Form";
    //                SendAdminLog(ModuleNameConstants.DigitalForm, ServiceNameConstants.DigitalForm,
    //                    "Failed To Sign Digital Form", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //                return Json(new { Status = "Failed", Title = "Status", Message = response.Message });
    //            }
    //            else
    //            {
    //                logMessage = $"Successfully Signed Digital Form";
    //                SendAdminLog(ModuleNameConstants.DigitalForm, ServiceNameConstants.DigitalForm,
    //                    "Successfully Signed Digital Form", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
    //                return Json(new { Status = "Success", Title = "Status", Message = response.Message });
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }

    //    }

    //}
}
