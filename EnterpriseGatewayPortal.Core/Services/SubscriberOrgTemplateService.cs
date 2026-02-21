using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class SubscriberOrgTemplateService : ISubscriberOrgTemplateService
    {
        private readonly ILogger<SubscriberOrgTemplateService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public SubscriberOrgTemplateService(ILogger<SubscriberOrgTemplateService> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> GetSubscriberOrgTemplateAsync(UserDTO userDTO)
        {
            try
            {
                _logger.LogInformation("GetSubscriberOrgTemplateAsync Called");
                var subscriberOrgTemplates = await _unitOfWork.SubscriberOrgTemplate.GetTemplateListBySuidAndOrgId(userDTO.Suid, userDTO.OrganizationId);
                if (subscriberOrgTemplates == null)
                {
                    _logger.LogError("Template not found");
                    return new ServiceResult("Template not found");
                }
                return new ServiceResult(true, "Template recieved successfully", subscriberOrgTemplates);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetSubscriberOrgTemplateAsync Error: {ex.Message}");
                return new ServiceResult("Template not found");
            }
        }

        public async Task<ServiceResult> GetTemplateListByOrgId(string orgID)
        {
            try
            {
                _logger.LogInformation("GetTemplateListByOrgId Called");
                var subscriberOrgTemplates = await _unitOfWork.SubscriberOrgTemplate.GetTemplateListByOrgId(orgID);
                if (subscriberOrgTemplates == null)
                {
                    _logger.LogError("Template not found");
                    return new ServiceResult("Template not found");
                }
                return new ServiceResult(true, "Template recieved successfully", subscriberOrgTemplates);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetTemplateListByOrgId Error: {ex.Message}");
                return new ServiceResult("Template not found");
            }
        }

        public async Task<ServiceResult> AddSubscriberOrgTemplateAsync(Subscriberorgtemplate model)
        {
            try
            {
                _logger.LogInformation("AddSubscriberOrgTemplateAsync Called");

                var template = await _unitOfWork.SubscriberOrgTemplate.SaveSubscriberOrgTemplate(model);
                if (template == null)
                {
                    _logger.LogError("Template not added");
                    return new ServiceResult("Template not added");
                }
                return new ServiceResult(true, "Template added successfully", template);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddSubscriberOrgTemplateAsync Error: {ex.Message}");
                return new ServiceResult("Template not added");
            }
        }

        public async Task<ServiceResult> DeleteSubscriberOrgTemplateAsync(string tempID, UserDTO user)
        {
            try
            {
                _logger.LogInformation("DeleteSubscriberOrgTemplateAsync Called");

                var organization = await _unitOfWork.SubscriberOrgTemplate.DeleteSubscriberOrgTemplate(tempID, user);
                if (!organization)
                {
                    _logger.LogError("SubscriberOrgTemplate not deleted");
                    return new ServiceResult("SubscriberOrgTemplate not deleted");
                }
                return new ServiceResult(true, "SubscriberOrgTemplate deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteSubscriberOrgTemplateAsync Error: {ex.Message}");
                return new ServiceResult("Failed to delete SubscriberOrgTemplate");
            }
        }

    }
}
