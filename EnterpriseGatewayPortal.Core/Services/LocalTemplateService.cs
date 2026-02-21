namespace EnterpriseGatewayPortal.Core.Services
{
    //public class LocalTemplateService : ILocalTemplateService
    //{
    //    private readonly ILogger<LocalTemplateService> _logger;
    //    private readonly IUnitOfWork _unitOfWork;
    //    public LocalTemplateService(ILogger<LocalTemplateService> logger,
    //        IUnitOfWork unitOfWork)
    //    {
    //        _logger = logger;
    //        _unitOfWork = unitOfWork;
    //    }

    //    public async Task<ServiceResult> GetAllLocalTemplatesByIdAsync(string name)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("GetAllLocalTemplatesByIdAsync Called");
    //            var user = await _unitOfWork.Template.GetAllTemplatesByIdAsync(name);
    //            if (user == null)
    //            {
    //                _logger.LogError("Template not found");
    //                return new ServiceResult("Template not found");
    //            }
    //            return new ServiceResult(true, "Template recieved successfully", user);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"GetAllLocalTemplatesByIdAsync Error: {ex.Message}");
    //            return new ServiceResult("Template not found");
    //        }
    //    }

    //    public async Task<ServiceResult> GetLocalTemplateByIdAsync(string tempId)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("GetLocalTemplateByIdAsync Called");
    //            var user = await _unitOfWork.Template.GetTemplateAsync(tempId);
    //            if (user == null)
    //            {
    //                _logger.LogError("Template not found");
    //                return new ServiceResult("Template not found");
    //            }
    //            return new ServiceResult(true, "Template recieved successfully", user);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"GetLocalTemplateByIdAsync Error: {ex.Message}");
    //            return new ServiceResult("Template not found");
    //        }
    //    }

    //    public async Task<ServiceResult> GetLocalTemplateEmailByIdAsync(string tempId)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("GetLocalTemplateEmailByIdAsync Called");
    //            var user = await _unitOfWork.Template.GetTemplateAsync(tempId);
    //            if (user == null)
    //            {
    //                _logger.LogError("Template not found");
    //                return new ServiceResult("Template not found");
    //            }

    //            List<string>? list = JsonConvert.DeserializeObject<List<string>>(user.Emaillist);

    //            return new ServiceResult(true, "Email from template recieved successfully", list[0]);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"GetLocalTemplateEmailByIdAsync Error: {ex.Message}");
    //            return new ServiceResult("Email not found");
    //        }
    //    }


    //    public async Task<ServiceResult> AddLocalTemplateAsync(Template model)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("AddLocalTemplateAsync Called");

    //            var template = await _unitOfWork.Template.SaveTemplateAsync(model);
    //            if (template == null)
    //            {
    //                _logger.LogError("Template not added");
    //                return new ServiceResult("Template not added");
    //            }
    //            return new ServiceResult(true, "Successfully saved template", template);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"AddLocalTemplateAsync Error: {ex.Message}");
    //            return new ServiceResult("Template not added");
    //        }
    //    }

    //    public async Task<ServiceResult> UpdateLocalTemplateAsync(Template model)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("UpdateLocalTemplateAsync Called");

    //            _unitOfWork.Template.Update(model);
    //            await _unitOfWork.SaveAsync();

    //            return new ServiceResult(true, "Successfully updated template");
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"UpdateLocalTemplateAsync Error: {ex.Message}");
    //            return new ServiceResult("Template not updated");
    //        }
    //    }

    //    public async Task<ServiceResult> DeleteLocalTemplateAsync(string tempID)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("DeleteLocalTemplateAsync Called");

    //            var organization = await _unitOfWork.Template.DeleteTemplateAsync(tempID);
    //            if (!organization)
    //            {
    //                _logger.LogError("Local Template not deleted");
    //                return new ServiceResult("Local Template not deleted");
    //            }
    //            return new ServiceResult(true, "Local Template deleted successfully", null);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"DeleteLocalTemplateAsync Error: {ex.Message}");
    //            return new ServiceResult("Failed to delete Local Template");
    //        }
    //    }


    //}
}
