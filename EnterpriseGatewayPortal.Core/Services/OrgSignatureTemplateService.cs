namespace EnterpriseGatewayPortal.Core.Services
{
    //public class OrgSignatureTemplateService : IOrgSignatureTemplateService
    //{
    //    private readonly ILogger<OrgSignatureTemplateService> _logger;
    //    private readonly IUnitOfWork _unitOfWork;

    //    public OrgSignatureTemplateService(ILogger<OrgSignatureTemplateService> logger, IUnitOfWork unitOfWork)
    //    {
    //        _logger = logger;
    //        _unitOfWork = unitOfWork;
    //    }

    //    public async Task<ServiceResult> GetAllOrgSignatureTemplateListAsync()
    //    {
    //        try
    //        {
    //            _logger.LogInformation("GetAllOrgSignatureTemplateList Called");
    //            var list = await _unitOfWork.OrgSignatureTemplate.GetAllOrgSignatureTemplateAsync();
    //            if (list == null)
    //            {
    //                _logger.LogError("list is Empty");
    //                return new ServiceResult("list is Empty");
    //            }
    //            return new ServiceResult(true, "All Organization Signature Template List recieved successfully", list);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"GetAllOrgSignatureTemplateList Error: {ex.Message}");
    //            return new ServiceResult("Organization Signature Template list not found");
    //        }
    //    }

    //    public async Task<ServiceResult> GetOrgSignatureTemplateByIdAsync(int id)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("GetOrgSignatureTemplateByIdAsync Called");
    //            var organization = await _unitOfWork.OrgSignatureTemplate.GetOrgSignatureTemplateByIdAsync(id);
    //            if (organization == null)
    //            {
    //                _logger.LogError("OrgSignatureTemplate not found");
    //                return new ServiceResult("Organization Signature Template not found");
    //            }
    //            return new ServiceResult(true, "Organization Signature Template recieved successfully", organization);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"GetOrgSignatureTemplateByIdAsync Error: {ex.Message}");
    //            return new ServiceResult("Organization Signature Template not found");
    //        }
    //    }

    //    public async Task<ServiceResult> GetOrganizationTemplatesDTOByUIdAsync(string uid)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("GetOrganizationTemplatesDTOByUIdAsync Called");

    //            var templates = await _unitOfWork.OrgSignatureTemplate.GetAllOrgSignatureTemplateByUIDAsync(uid);
    //            if (templates == null)
    //            {
    //                _logger.LogError("OrgSignatureTemplate not found");
    //                return new ServiceResult("Organization Signature Template not found");
    //            }

    //            var templateObject = new OrganizationTemplatesDTO()
    //            {
    //                organizationUid = uid
    //            };

    //            foreach (var template in templates)
    //            {
    //                if (template.Type == "SIGN")
    //                {
    //                    templateObject.signatureTemplateId = (int)template.TemplateId!;
    //                }
    //                else if (template.Type == "ESEAL")
    //                {
    //                    templateObject.esealSignatureTemplateId = (int)template.TemplateId!;
    //                }
    //            }

    //            return new ServiceResult(true, "Organization Signature Template recieved successfully", templateObject);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"GetOrganizationTemplatesDTOByUIdAsync Error: {ex.Message}");
    //            return new ServiceResult("Organization Signature Template not found");
    //        }
    //    }

    //    public async Task<ServiceResult> AddOrgSignatureTemplateAsync(OrganizationTemplatesDTO model)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("AddOrgSignatureTemplateAsync Called");

    //            var eSeal = await _unitOfWork.SignatureTemplate.GetSignatureTemplateByIdAsync(model.esealSignatureTemplateId);
    //            if (eSeal == null)
    //            {
    //                _logger.LogError("OrgSignatureTemplate not added");
    //                return new ServiceResult("Organization Signature Template not added");
    //            }
    //            OrgSignatureTemplate newESealTemplate = new()
    //            {
    //                OrganizationUid = model.organizationUid,
    //                TemplateId = eSeal.Id,
    //                Type = eSeal.Type
    //            };

    //            var eSealTemplate = await _unitOfWork.OrgSignatureTemplate.AddOrgSignatureTemplateAsync(newESealTemplate);
    //            if (eSealTemplate == null)
    //            {
    //                _logger.LogError("OrgSignatureTemplate not added");
    //                return new ServiceResult("Organization Signature Template not added");
    //            }

    //            var sign = await _unitOfWork.SignatureTemplate.GetSignatureTemplateByIdAsync(model.signatureTemplateId);
    //            if (sign == null)
    //            {
    //                _logger.LogError("OrgSignatureTemplate not added");
    //                return new ServiceResult("Organization Signature Template not added");
    //            }
    //            OrgSignatureTemplate newSignTemplate = new()
    //            {
    //                OrganizationUid = model.organizationUid,
    //                TemplateId = sign.Id,
    //                Type = sign.Type
    //            };

    //            var signTemplate = await _unitOfWork.OrgSignatureTemplate.AddOrgSignatureTemplateAsync(newSignTemplate);
    //            if (signTemplate == null)
    //            {
    //                _logger.LogError("OrgSignatureTemplate not added");
    //                return new ServiceResult("Organization Signature Template not added");
    //            }

    //            var newTemplate = new
    //            {
    //                eSealTemplate,
    //                signTemplate
    //            };
    //            return new ServiceResult(true, "Organization Signature Template added successfully", newTemplate);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"AddOrgSignatureTemplateAsync Error: {ex.Message}");
    //            return new ServiceResult("Organization Signature Template not added");
    //        }
    //    }

    //    public async Task<ServiceResult> UpdateOrgSignatureTemplateAsync(OrganizationTemplatesDTO model)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("UpdateOrgSignatureTemplateAsync Called");

    //            var eSealTemplate = new OrgSignatureTemplate();
    //            var signTemplate = new OrgSignatureTemplate();

    //            bool eSealCount = false;
    //            bool eSignCount = false;
    //            string returnmsg = string.Empty;

    //            var templates = await _unitOfWork.OrgSignatureTemplate.GetAllOrgSignatureTemplateByUIDAsync(model.organizationUid);
    //            if (templates.Count() == 0)
    //            {
    //                ServiceResult addResult = await AddOrgSignatureTemplateAsync(model);
    //                if (addResult != null && !addResult.Success)
    //                {
    //                    return new ServiceResult("Failed to update templates");
    //                }
    //                return new ServiceResult(true, "Templates Updated Successfully", null);
    //            }
    //            foreach (var template in templates)
    //            {
    //                OrgSignatureTemplate newTemplate = new()
    //                {
    //                    Id = template.Id,
    //                    OrganizationUid = template.OrganizationUid,
    //                    Type = template.Type
    //                };

    //                if (template.Type == "SIGN")
    //                {
    //                    newTemplate.TemplateId = model.signatureTemplateId;
    //                    if (newTemplate.TemplateId != template.TemplateId)
    //                    {
    //                        signTemplate = await _unitOfWork.OrgSignatureTemplate.UpdateOrgSignatureTemplate(newTemplate);
    //                        if (signTemplate == null)
    //                        {
    //                            _logger.LogError("OrgSignatureTemplate not updated");
    //                            return new ServiceResult("SIGN Organization Signature Template not updated");
    //                        }
    //                        eSignCount = true;
    //                    }
    //                    else
    //                        signTemplate = template;
    //                }
    //                else if (template.Type == "ESEAL")
    //                {
    //                    newTemplate.TemplateId = model.esealSignatureTemplateId;
    //                    if (newTemplate.TemplateId != template.TemplateId)
    //                    {
    //                        eSealTemplate = await _unitOfWork.OrgSignatureTemplate.UpdateOrgSignatureTemplate(newTemplate);
    //                        if (eSealTemplate == null)
    //                        {
    //                            _logger.LogError("OrgSignatureTemplate not updated");
    //                            return new ServiceResult("ESEAL Organization Signature Template not updated");
    //                        }
    //                        eSealCount = true;
    //                    }
    //                    else
    //                        eSealTemplate = template;
    //                }
    //            }

    //            var updatedTemplate = new
    //            {
    //                signTemplate,
    //                eSealTemplate
    //            };

    //            if (eSealCount && eSignCount)
    //            {
    //                returnmsg = "Signature Template and E-Seal Template updated successfully";
    //            }
    //            else if (eSignCount)
    //            {
    //                returnmsg = "Signature Template updated successfully";
    //            }
    //            else if (eSealCount)
    //            {
    //                returnmsg = "E-Seal Template updated successfully";
    //            }
    //            else
    //            {
    //                returnmsg = "Templates updated Successfully";
    //            }

    //            return new ServiceResult(true, returnmsg, updatedTemplate);

    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"UpdateOrgSignatureTemplate Error: {ex.Message}");
    //            return new ServiceResult("Organization Signature Template not updated");
    //        }
    //    }

    //    public async Task<ServiceResult> DeleteOrgSignatureTemplateAsync(OrgSignatureTemplate model)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("DeleteOrgSignatureTemplateAsync Called");
    //            var organization = _unitOfWork.OrgSignatureTemplate.RemoveOrgSignatureTemplate(model);
    //            if (organization)
    //            {
    //                _logger.LogError("Organization Signature Template not deleted");
    //                return new ServiceResult("Organization Signature Template not deleted");
    //            }
    //            return new ServiceResult(true, "Organization Signature Template deleted successfully", organization);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"DeleteOrgSignatureTemplateAsync Error: {ex.Message}");
    //            return new ServiceResult("Organization Signature Template not deleted");
    //        }
    //    }

    //    public async Task<ServiceResult> DeleteOrgSignatureTemplateByIdAsync(int id)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("DeleteOrgSignatureTemplateAsync Called");
    //            var organization = await _unitOfWork.OrgSignatureTemplate.RemoveOrgSignatureTemplateById(id);
    //            if (!organization)
    //            {
    //                _logger.LogError("Organization Signature Template not deleted");
    //                return new ServiceResult("Organization Signature Template not deleted");
    //            }
    //            return new ServiceResult(true, "Organization Signature Template deleted successfully", organization);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"DeleteOrgSignatureTemplateAsync Error: {ex.Message}");
    //            return new ServiceResult("Organization Signature Template not deleted");
    //        }
    //    }

    //}
}
