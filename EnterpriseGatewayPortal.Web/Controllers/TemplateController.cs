namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[ServiceFilter(typeof(SessionValidationAttribute))]
    //[Authorize]
    //public class TemplateController : BaseController
    //{
    //    private readonly ITemplateService _templateService;
    //    private readonly IOrgSignatureTemplateService _orgSignatureTemplateService;
    //    private readonly IConfiguration _configuration;
    //    private readonly ISignatureTemplateService _signatureTemplateService;
    //    public TemplateController(IAdminLogService adminLogService,
    //        ITemplateService templateService,
    //        IOrgSignatureTemplateService orgSignatureTemplateService,
    //        IConfiguration configuration,
    //        ISignatureTemplateService signatureTemplateService) : base(adminLogService)
    //    {
    //        _templateService = templateService;
    //        _orgSignatureTemplateService = orgSignatureTemplateService;
    //        _configuration = configuration;
    //        _signatureTemplateService = signatureTemplateService;
    //    }
    //    public async Task<IActionResult> Index()
    //    {
    //        string logMessage;
    //        var orgUID = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);


    //        var templateList = await _signatureTemplateService.GetAllSignatureTemplateListAsync();

    //        var SignatureTemplateList = (IEnumerable<SignatureTemplate>)templateList.Resource;
    //        if (SignatureTemplateList == null)
    //        {
    //            logMessage = $"Failed to get the Signature Template List From Local DB";
    //            SendAdminLog(ModuleNameConstants.SignatureTemplates, ServiceNameConstants.SignatureTemplates,
    //                "Get Signature Template List", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return NotFound();
    //        }

    //        var organizationTemplates = await _orgSignatureTemplateService.GetOrganizationTemplatesDTOByUIdAsync(orgUID);

    //        var templateDto = (OrganizationTemplatesDTO)organizationTemplates.Resource;
    //        if (templateDto == null)
    //        {
    //            logMessage = $"Failed to get the Organization Template From Local DB";
    //            SendAdminLog(ModuleNameConstants.SignatureTemplates, ServiceNameConstants.SignatureTemplates,
    //                "Get Organization Template List", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return NotFound();
    //        }
    //        if (templateDto.signatureTemplateId == 0)
    //        {
    //            templateDto.signatureTemplateId = 1;
    //        }
    //        if (templateDto.esealSignatureTemplateId == 0)
    //        {
    //            templateDto.esealSignatureTemplateId = 5;
    //        }
    //        var model = new UpdateTemplatesViewModel();

    //        model.TemplateList = SignatureTemplateList;

    //        model.SignatureTemplate = templateDto.signatureTemplateId;
    //        model.ESealTemplate = templateDto.esealSignatureTemplateId;

    //        logMessage = $"Successfully Received Signature Template details from Local DB";
    //        SendAdminLog(ModuleNameConstants.SignatureTemplates, ServiceNameConstants.SignatureTemplates,
    //            "Get Signature Template", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //        return View(model);
    //    }
    //    public async Task<IActionResult> Update(UpdateTemplatesViewModel model)
    //    {
    //        string logMessage;
    //        var orgUID = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        OrganizationTemplatesDTO organizationTemplates = new OrganizationTemplatesDTO();
    //        organizationTemplates.esealSignatureTemplateId = model.ESealTemplate;
    //        organizationTemplates.signatureTemplateId = model.SignatureTemplate;
    //        organizationTemplates.organizationUid = orgUID;
    //        var updatetemplateDTO = new UpdateTemplateDTO()
    //        {
    //            SignatureTemplate = model.SignatureTemplate,
    //            TemplateList = model.TemplateList,
    //            ESealTemplate = model.ESealTemplate,
    //        };
    //        var istemplatevalid = await _templateService.IsValid(updatetemplateDTO, orgUID);
    //        if (istemplatevalid == null || !istemplatevalid.Success)
    //        {
    //            AlertViewModel alert = new AlertViewModel { Message = (istemplatevalid == null ? "Internal error please contact to admin" : istemplatevalid.Message) };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);

    //            logMessage = $"Failed to update the Signature Template in server DB";
    //            SendAdminLog(ModuleNameConstants.SignatureTemplates, ServiceNameConstants.SignatureTemplates,
    //                "Update Signature Template", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //            return RedirectToAction("Index");
    //        }
    //        var response = await _templateService.UpdateOrganizationTemplates(organizationTemplates);
    //        if (response == null || !response.Success)
    //        {
    //            AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);

    //            logMessage = $"Failed to update the Signature Template in server DB";
    //            SendAdminLog(ModuleNameConstants.SignatureTemplates, ServiceNameConstants.SignatureTemplates,
    //                "Update Signature Template", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //            return RedirectToAction("Index");
    //        }
    //        var response1 = await _orgSignatureTemplateService.UpdateOrgSignatureTemplateAsync(organizationTemplates);
    //        if (response1 == null || !response1.Success)
    //        {
    //            AlertViewModel alert = new AlertViewModel { Message = (response1 == null ? "Internal error please contact to admin" : response1.Message) };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);

    //            logMessage = $"Failed to update the Signature Template in local DB";
    //            SendAdminLog(ModuleNameConstants.SignatureTemplates, ServiceNameConstants.SignatureTemplates,
    //                "Update Signature Template", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //            return RedirectToAction("Index");
    //        }
    //        else
    //        {
    //            AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response1.Message };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);

    //            logMessage = $"Successfully updated Signature Template in Local DB";
    //            SendAdminLog(ModuleNameConstants.SignatureTemplates, ServiceNameConstants.SignatureTemplates,
    //                "update Signature Template", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //            return RedirectToAction("Index");
    //        }
    //        return View();
    //    }
    //}
}
