namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[ServiceFilter(typeof(SessionValidationAttribute))]
    //[Authorize]
    //public class DocumentTemplatesController : BaseController
    //{
    //    private readonly ITemplateService _templateService;
    //    private readonly IOrgSignatureTemplateService _orgSignatureTemplateService;
    //    private readonly IConfiguration _configuration;
    //    private readonly ISignatureTemplateService _signatureTemplateService;
    //    private readonly IBussinessUserService _bussinessUserService;
    //    private readonly IDocumentTemplatesService _documentTemplatesService;
    //    private readonly ILocalTemplateService _localTemplateService;
    //    private readonly ISubscriberOrgTemplateService _subscriberOrgTemplateService;
    //    private readonly ILocalBusinessUsersService _localBusinessUsersService;
    //    public DocumentTemplatesController(IAdminLogService adminLogService, ITemplateService templateService, ILocalBusinessUsersService localBusinessUsersService,
    //        IOrgSignatureTemplateService orgSignatureTemplateService,
    //        IConfiguration configuration,
    //        ISignatureTemplateService signatureTemplateService,
    //        IDocumentTemplatesService documentTemplatesService,
    //        ILocalTemplateService localTemplateService,
    //        ISubscriberOrgTemplateService subscriberOrgTemplateService,
    //        IBussinessUserService bussinessUserService
    //       ) : base(adminLogService)
    //    {
    //        _templateService = templateService;
    //        _orgSignatureTemplateService = orgSignatureTemplateService;
    //        _configuration = configuration;
    //        _documentTemplatesService = documentTemplatesService;
    //        _signatureTemplateService = signatureTemplateService;
    //        _localTemplateService = localTemplateService;
    //        _subscriberOrgTemplateService = subscriberOrgTemplateService;
    //        _localBusinessUsersService = localBusinessUsersService;
    //        _bussinessUserService = bussinessUserService;
    //    }
    //    [HttpGet]
    //    public async Task<IActionResult> Index()
    //    {

    //        var viewModel = new DocumentTemplatesListViewModel();



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
    //                _id = item.Templateid,
    //                DocumentName = item.Template.Documentname,
    //                status = item.Template.Status,
    //                edmsId = item.Template.Edmsid,
    //                createdBy = item.Template.Createdby
    //            };
    //            documentTemplates.Add(documentsTemplateDTO);
    //        }

    //        viewModel.Documents = documentTemplates;
    //        return View(viewModel);
    //    }


    //    [HttpGet]
    //    public async Task<IActionResult> Create() 
    //    {
    //        string logMessage;
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //        var templateList = await _signatureTemplateService.GetAllSignatureTemplateListAsync();

    //        var SignatureTemplateList = (IEnumerable<SignatureTemplate>)templateList.Resource;
    //        if (SignatureTemplateList == null)
    //        {
    //            logMessage = $"Failed to get the Signature Template List From Local DB";
    //            SendAdminLog(ModuleNameConstants.SignatureTemplates, ServiceNameConstants.SignatureTemplates,
    //                "Get Signature Template List", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return NotFound();
    //        }
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);

    //        var bulkSignerListDetails = await _localBusinessUsersService.GetBulkSignerListAsync();
    //        var org = (IEnumerable<OrgSubscriberEmail>)bulkSignerListDetails.Resource;
    //        var list = new List<string>();
    //        if (org == null)
    //        {
    //            logMessage = $"Failed to get Bulk Signer list with OragnizationUid {organizationUid}";
    //            SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
    //                "Get Bulk Signer list", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return NotFound();
    //        }


    //        list.AddRange(org.Select(o => o.EmployeeEmail).Where(email => !string.IsNullOrEmpty(email)).Distinct());

    //        BulkSignerListViewModel bulkSignerList = new BulkSignerListViewModel
    //        {
    //            bulkSignerList = list,


    //        };
    //        CreateViewModel viewModel = new CreateViewModel
    //        {
    //            TemplateList = SignatureTemplateList,
    //            BulkSignerEmails = bulkSignerList

    //        };
    //        var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user")?.Value;
    //        if (!string.IsNullOrEmpty(user))
    //        {
    //            var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
    //            var signatureResponse = await _templateService.GetSignaturePreviewAsync(userDTO);

    //            if (signatureResponse.Success && signatureResponse.Resource != null)
    //            {
    //                var preview = (string)signatureResponse.Resource;
    //                var previewDTO = JsonConvert.DeserializeObject<PreviewImageDTO>(preview);

    //                if (previewDTO != null)
    //                {
    //                    ViewBag.SignatureImage = previewDTO.signatureImage;
    //                    ViewBag.EsealImage = previewDTO.esealImage;
    //                }
    //                else
    //                {
    //                    TempData["Alert"] = JsonConvert.SerializeObject(new AlertViewModel
    //                    {
    //                        IsSuccess = false,
    //                        Message = "Error parsing signature preview data"
    //                    });
    //                }
    //            }
    //            else
    //            {
    //                AlertViewModel alert = new AlertViewModel { IsSuccess = false, Message = (signatureResponse == null ? "Internal error please contact to admin" : signatureResponse.Message) };
    //                TempData["Alert"] = JsonConvert.SerializeObject(alert);

    //                return RedirectToAction("Index");
    //            }
    //        }


    //        return View(viewModel);
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> SaveTemplate(CreateViewModel viewModel) 
    //    {
    //        try
    //        {
    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);
    //            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);

    //            JObject configObject = JObject.Parse(viewModel.Config);

    //            CoordinatesData signCords = ExtractCoordinates(configObject["Signature"], viewModel.Signatory);
    //            CoordinatesData qrCords = ExtractCoordinates(configObject["Qrcode"], viewModel.Signatory);
    //            CoordinatesData esealCords = ExtractCoordinates(configObject["Eseal"], viewModel.Signatory);

    //            var pathobj = new PathsdataDTO
    //            {
    //                inputpath = "",
    //                outputpath = ""
    //            };

    //            var pathobject = JsonConvert.SerializeObject(pathobj);
    //            var emailList = viewModel.Signatory?.Split(',').Select(email => email.Trim()).ToList() ?? new List<string>();
    //            var role = new Roles
    //            {
    //                Order = 0,
    //                Role = "sesdd",
    //                Eseal = viewModel.esealRequired
    //            };
    //            var roleList = new List<Roles>();

    //            roleList.Add(role);

    //            byte[] fileData;

    //            using (var stream = viewModel.File.OpenReadStream())
    //            {
    //                using (var memoryStream = new MemoryStream())
    //                {
    //                    await stream.CopyToAsync(memoryStream);
    //                    fileData = memoryStream.ToArray();
    //                }
    //            }

    //            Template template = new Template()
    //            {
    //                // TemplateId = subscriberOrgTemplate.TemplateId,
    //                Templateid = Guid.NewGuid().ToString(),
    //                Templatename = viewModel.TemplateName,
    //                Documentname = viewModel.DocumentName,
    //                Annotations = signCords == null ? null : JsonConvert.SerializeObject(new Dictionary<string, CoordinatesData> { { viewModel.Signatory, signCords } }),
    //                Esealannotations = esealCords == null ? null : JsonConvert.SerializeObject(new Dictionary<string, CoordinatesData> { { viewModel.Signatory, esealCords } }),
    //                Qrcodeannotations = qrCords == null ? null : JsonConvert.SerializeObject(new Dictionary<string, CoordinatesData> { { viewModel.Signatory, qrCords } }),
    //                Qrcoderequired = viewModel.qrCodeRequired,
    //                Htmlschema = viewModel.htmlSchema,
    //                Settingconfig = pathobject,
    //                Signaturetemplate = viewModel.signatureTemplate,
    //                Esealsignaturetemplate = viewModel.esealSignatureTemplate,
    //                //EdmsId = preview.edmsId,
    //                Status = "ACTIVE",
    //                Rolelist = JsonConvert.SerializeObject(roleList),
    //                Emaillist = JsonConvert.SerializeObject(emailList),
    //                Createdat = DateTime.Now,
    //                Updatedat = DateTime.Now,
    //                Createdby = FullName,
    //                Updatedby = FullName,
    //                Templatefile = fileData
    //            };

    //            Subscriberorgtemplate OrgTemplate = new Subscriberorgtemplate()
    //            {
    //                Templateid = template.Templateid,
    //                Suid = Suid,
    //                Createdat = DateTime.Now,
    //                Updatedat = DateTime.Now,
    //                Organizationid = organizationUid
    //            };
    //            var templateResult = await _localTemplateService.AddLocalTemplateAsync(template);

    //            if (!templateResult.Success)
    //            {
    //                return Json(new { Status = "Failed", Title = "Save New Template", Message = templateResult.Message });
    //            }
    //            var subscriberTempResult = await _subscriberOrgTemplateService.AddSubscriberOrgTemplateAsync(OrgTemplate);
    //            if (!subscriberTempResult.Success)
    //            {
    //                return Json(new { Status = "Failed", Title = "Save New Template", Message = templateResult.Message });
    //            }
    //            else
    //            {
    //                return Json(new { Status = "Success", Title = "Save New Template", Message = templateResult.Message });
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> UpdateTemplate(CreateViewModel viewModel) 
    //    {
    //        try
    //        {
    //            var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);

    //            JObject configObject = JObject.Parse(viewModel.Config);

    //            CoordinatesData signCords = ExtractCoordinates(configObject["Signature"], viewModel.Signatory);
    //            CoordinatesData qrCords = ExtractCoordinates(configObject["Qrcode"], viewModel.Signatory);
    //            CoordinatesData esealCords = ExtractCoordinates(configObject["Eseal"], viewModel.Signatory);

    //            var emailList = viewModel.Signatory?.Split(',').Select(email => email.Trim()).ToList() ?? new List<string>();

    //            byte[] fileData;

    //            using (var stream = viewModel.File.OpenReadStream())
    //            {
    //                using (var memoryStream = new MemoryStream())
    //                {
    //                    await stream.CopyToAsync(memoryStream);
    //                    fileData = memoryStream.ToArray();
    //                }
    //            }
    //            var getTemplate = await _localTemplateService.GetLocalTemplateByIdAsync(viewModel._id);
    //            var Template = (Template)getTemplate.Resource;
    //            Template.Templatename = viewModel.TemplateName;
    //            Template.Documentname = viewModel.DocumentName;
    //            Template.Annotations = signCords == null ? null : JsonConvert.SerializeObject(new Dictionary<string, CoordinatesData> { { viewModel.Signatory, signCords } });
    //            Template.Esealannotations = esealCords == null ? null : JsonConvert.SerializeObject(new Dictionary<string, CoordinatesData> { { viewModel.Signatory, esealCords } });
    //            Template.Qrcodeannotations = qrCords == null ? null : JsonConvert.SerializeObject(new Dictionary<string, CoordinatesData> { { viewModel.Signatory, qrCords } });
    //            Template.Qrcoderequired = viewModel.qrCodeRequired;
    //            Template.Htmlschema = viewModel.htmlSchema;
    //            Template.Signaturetemplate = viewModel.signatureTemplate;
    //            Template.Esealsignaturetemplate = viewModel.esealSignatureTemplate;
    //            Template.Edmsid = Template.Edmsid;
    //            Template.Emaillist = JsonConvert.SerializeObject(emailList);
    //            Template.Updatedat = DateTime.Now;
    //            Template.Updatedby = FullName;
    //            Template.Templatefile = fileData;
    //            var templateResult = await _localTemplateService.UpdateLocalTemplateAsync(Template);
    //            if (!templateResult.Success)
    //            {
    //                return Json(new { Status = "Failed", Title = "Save New Template", Message = templateResult.Message });
    //            }
    //            else
    //            {
    //                return Json(new { Status = "Success", Title = "Save New Template", Message = templateResult.Message });
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"An error occurred: {ex.Message}");
    //        }
    //    }


    //    [HttpGet]
    //    public async Task<IActionResult> GetPreviewConfig(string id) 
    //    {
    //        var templateId = id.ToString();
    //        var getTemplate = await _localTemplateService.GetLocalTemplateByIdAsync(id);
    //        var Template = (Template)getTemplate.Resource;

    //        return Json(new { success = true, message = "File downloaded successfully.", resource = Template.Templatefile });
    //    }



    //    public async Task<IActionResult> Preview(string templateId)
    //    {

    //        try
    //        {
    //            var getTemplate = await _localTemplateService.GetLocalTemplateByIdAsync(templateId);
    //            var Template = (Template)getTemplate.Resource;

    //            var viewModel = new PreviewConfigViewModel
    //            {
    //                _id = templateId,
    //                templateName = Template.Templatename,
    //                documentName = Template.Documentname,
    //                annotations = Template.Annotations,
    //                esealAnnotations = Template.Esealannotations,
    //                qrCodeAnnotations = Template.Qrcodeannotations,
    //                qrCodeRequired = (bool)Template.Qrcoderequired,
    //                settingConfig = Template.Settingconfig,
    //                //roleList = (IList<ViewModel.DocumentTemplates.Roles>)preview.roleList,
    //                emailList = JsonConvert.DeserializeObject<IList<string>>(Template.Emaillist),
    //                //signatureTemplate = Template.signatureTemplate,
    //                //esealSignatureTemplate = Template.esealSignatureTemplate,
    //                status = Template.Status,
    //                //rotation = Template.Rotation,
    //                edmsId = Template.Edmsid,
    //                createdBy = Template.Createdby,
    //                updatedBy = Template.Updatedby,
    //                htmlSchema = Template.Htmlschema,

    //            };
    //            return View(viewModel);
    //        }
    //        catch (Exception e)
    //        {

    //            return RedirectToAction("Index");
    //        }
    //    }



    //    public async Task<IActionResult> GetTemplateDetails(string templateId) 
    //    {
    //        string logMessage;
    //        var apiToken = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "apiToken").Value);

    //        var templateDetails = await _localTemplateService.GetLocalTemplateByIdAsync(templateId);

    //        var details = (Template)templateDetails.Resource;
    //        if (details == null)
    //        {
    //            return NotFound();
    //        }

    //        var templateList = await _signatureTemplateService.GetAllSignatureTemplateListAsync();

    //        var SignatureTemplateList = (IEnumerable<SignatureTemplate>)templateList.Resource;
    //        if (SignatureTemplateList == null)
    //        {
    //            logMessage = $"Failed to get the Signature Template List From Local DB";
    //            SendAdminLog(ModuleNameConstants.SignatureTemplates, ServiceNameConstants.SignatureTemplates,
    //                "Get Signature Template List", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return NotFound();
    //        }
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var bulkSignerListDetails = await _localBusinessUsersService.GetBulkSignerListAsync();
    //        var org = (IEnumerable<OrgSubscriberEmail>)bulkSignerListDetails.Resource;
    //        var list = new List<string>();
    //        if (org == null)
    //        {
    //            logMessage = $"Failed to get Bulk Signer list with OragnizationUid {organizationUid}";
    //            SendAdminLog(ModuleNameConstants.Organization, ServiceNameConstants.Organization,
    //                "Get Bulk Signer list", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return NotFound();
    //        }


    //        list.AddRange(org.Select(o => o.EmployeeEmail).Where(email => !string.IsNullOrEmpty(email)).Distinct());

    //        BulkSignerListViewModel bulkSignerList = new BulkSignerListViewModel
    //        {
    //            bulkSignerList = list,
    //        };
    //        var viewModel = new CreateViewModel
    //        {
    //            _id = details.Templateid,
    //            DocumentName = details.Documentname,
    //            TemplateName = details.Templatename,
    //            TemplateList = SignatureTemplateList,
    //            BulkSignerEmails = bulkSignerList,
    //            annotations = details.Annotations,
    //            esealAnnotations = details.Esealannotations,
    //            qrCodeAnnotations = details.Qrcodeannotations,
    //            qrCodeRequired = (bool)details.Qrcoderequired,
    //            settingConfig = details.Settingconfig,
    //            emailList = JsonConvert.DeserializeObject<IList<string>>(details.Emaillist),
    //            signatureTemplate = (int)details.Signaturetemplate,
    //            esealSignatureTemplate = (int)details.Esealsignaturetemplate,
    //            status = details.Status,
    //            //rotation = details.,
    //            edmsId = details.Edmsid,
    //            createdBy = details.Createdby,
    //            updatedBy = details.Updatedby,
    //            htmlSchema = details.Htmlschema,
    //            fileKb = details.Templatefile != null
    //                ? $"{Math.Round((double)details.Templatefile.Length / 1024, 2)}"
    //                : "0"
    //        };
    //        if (details.Rolelist != null)
    //        {
    //            List<Roles> roleList = JsonConvert.DeserializeObject<List<Roles>>(details.Rolelist);

    //            // Accessing values
    //            Roles firstRole = roleList[0];

    //            // Access the Eseal property and assign it to viewModel.esealRequired
    //            viewModel.esealRequired = firstRole.Eseal;

    //        }
    //        if (viewModel.esealSignatureTemplate == 0)
    //        {
    //            viewModel.esealSignatureTemplate = 5;
    //        }
    //        if (viewModel.signatureTemplate == 0)
    //        {
    //            viewModel.signatureTemplate = 1;
    //        }
    //        var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user")?.Value;
    //        if (!string.IsNullOrEmpty(user))
    //        {
    //            var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
    //            var signatureResponse = await _templateService.GetSignaturePreviewAsync(userDTO);

    //            if (signatureResponse.Success && signatureResponse.Resource != null)
    //            {
    //                var preview = (string)signatureResponse.Resource;
    //                var previewDTO = JsonConvert.DeserializeObject<PreviewImageDTO>(preview);

    //                if (previewDTO != null)
    //                {
    //                    ViewBag.SignatureImage = previewDTO.signatureImage;
    //                    ViewBag.EsealImage = previewDTO.esealImage;
    //                }
    //                else
    //                {
    //                    TempData["Alert"] = JsonConvert.SerializeObject(new AlertViewModel
    //                    {
    //                        IsSuccess = false,
    //                        Message = "Error parsing signature preview data"
    //                    });
    //                }
    //            }
    //            else
    //            {
    //                AlertViewModel alert = new AlertViewModel { IsSuccess = false, Message = (signatureResponse == null ? "Internal error please contact to admin" : signatureResponse.Message) };
    //                TempData["Alert"] = JsonConvert.SerializeObject(alert);

    //                return RedirectToAction("Index");
    //            }
    //        }

    //        return View("CreateEdit", viewModel);
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
    //    public async Task<IActionResult> VerifyUserForST([FromBody] SignatureTemplateVerifyViewModel viewModel)
    //    {
    //        string logMessage;
    //        try
    //        {
    //            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //            GetTemplateDTO orgUser = new GetTemplateDTO()
    //            {
    //                TemplateId = viewModel.TemplateId,
    //                Email = viewModel.Email,
    //                OrgId = organizationUid
    //            };

    //            var response = await _templateService.CheckOrgUserWithSignatureTemplate(orgUser);

    //            if (!response.Success)
    //            {
    //                return Json(new { Status = "Failed", Title = "Save New Template", Message = response.Message });

    //            }
    //            else
    //            {
    //                return Json(new { Status = "Success", Title = "Save New Template", Message = response.Message });

    //            }


    //        }
    //        catch (Exception e)
    //        {

    //            return RedirectToAction("List");
    //        }
    //    }

    //}
}
