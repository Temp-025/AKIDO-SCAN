using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class OrganizationDetailService : IOrganizationDetailService
    {
        private readonly ILogger<OrganizationDetailService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationDetailService(ILogger<OrganizationDetailService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> GetAllOrganizationDetailListAsync()
        {
            try
            {
                _logger.LogInformation("GetAllOrganizationDetailList Called");
                var list = await _unitOfWork.OrganizationDetail.GetAllOrganizationDetailAsync();
                if (list == null)
                {
                    _logger.LogError("list is Empty");
                    return new ServiceResult("Organization detail list is Empty");
                }
                return new ServiceResult(true, "All Organization detail List recieved successfully", list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllOrganizationDetailList Error: {ex.Message}");
                return new ServiceResult("Organization detail list not found");
            }
        }

        public async Task<ServiceResult> GetOrganizationDetailByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("GetOrganizationDetailByIdAsync Called");
                var organization = await _unitOfWork.OrganizationDetail.GetOrganizationDetailByIdAsync(id);
                if (organization == null)
                {
                    _logger.LogError("Organization detail not found");
                    return new ServiceResult("Organization detail not found");
                }
                return new ServiceResult(true, "Organization detail recieved successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetOrganizationDetailByIdAsync Error: {ex.Message}");
                return new ServiceResult("Organization detail not found");
            }
        }

        public async Task<ServiceResult> GetOrganizationDetailByUIdAsync(string uid)
        {
            try
            {
                _logger.LogInformation("GetOrganizationDetailByUIdAsync Called");
                var organization = await _unitOfWork.OrganizationDetail.GetOrganizationDetailByUIDAsync(uid);
                if (organization == null)
                {
                    _logger.LogError("Organization detail not found");
                    return new ServiceResult("Organization detail not found");
                }
                return new ServiceResult(true, "Organization detail recieved successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetOrganizationDetailByUIdAsync Error: {ex.Message}");
                return new ServiceResult("Organization detail not found");
            }
        }

        public async Task<ServiceResult> AddOrganizationDetailAsync(OrganizationDetail model)
        {
            try
            {
                _logger.LogInformation("AddOrganizationDetailAsync Called");

                var isOrganizationDetailNameExist = await _unitOfWork.OrganizationDetail.IsOrganizationDetailExistsWithUIDAsync(model.OrganizationUid);
                if (isOrganizationDetailNameExist)
                {
                    _logger.LogError("Organization detail Already Exists");
                    return new ServiceResult("Organization detail Already Exists");
                }

                var organization = await _unitOfWork.OrganizationDetail.AddOrganizationDetailAsync(model);
                if (organization == null)
                {
                    _logger.LogError("OrganizationDetail not added");
                    return new ServiceResult("Organization detail not added");
                }
                return new ServiceResult(true, "Organization detail added successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddOrganizationDetailAsync Error: {ex.Message}");
                return new ServiceResult("Organization detail not added");
            }
        }

        public async Task<ServiceResult> UpdateOrganizationDetailAsync(OrganizationDetail model)
        {
            try
            {
                _logger.LogInformation("UpdateOrganizationDetailAsync Called");

                var organization = await _unitOfWork.OrganizationDetail.UpdateOrganizationDetail(model);
                if (organization == null)
                {
                    _logger.LogError("Organization detail not updated");
                    return new ServiceResult("Organization detail not updated");
                }
                return new ServiceResult(true, "Organization detail updated successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateOrganizationDetail Error: {ex.Message}");
                return new ServiceResult("Organization detail not updated");
            }
        }

        public async Task<ServiceResult> UpdateESealImageAsync(ESealImageUpdateDTO model)
        {
            try
            {
                _logger.LogInformation("UpdateESealImageAsync Called");
                if (string.IsNullOrWhiteSpace(model.orgUid))
                {
                    return new ServiceResult("Organization Uid is required");
                }

                var organization = await _unitOfWork.OrganizationDetail.GetOrganizationDetailByUIDAsync(model.orgUid);
                if (organization == null)
                {
                    _logger.LogError("Organization detail not updated");
                    return new ServiceResult("Organization detail not found");
                }

                organization.ESealImage = model.eSealImage;

                var updatedOrganization = await _unitOfWork.OrganizationDetail.UpdateOrganizationDetail(organization);
                if (organization == null)
                {
                    _logger.LogError("ESeal Image not updated");
                    return new ServiceResult("ESeal Image not updated");
                }

                return new ServiceResult(true, "ESeal Image updated successfully", updatedOrganization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateESealImageAsync Error: {ex.Message}");
                return new ServiceResult("ESeal Image not updated");
            }
        }

        public async Task<ServiceResult> UpdateAgentUrlAndSpocEmailAsync(AgentUrlAndSpocUpdateDTO model)
        {
            try
            {
                _logger.LogInformation("UpdateAgentUrlAndSpocEmailAsync Called");
                if (string.IsNullOrWhiteSpace(model.OrganizationUid))
                {
                    return new ServiceResult("Organization Uid is required");
                }

                var organization = await _unitOfWork.OrganizationDetail.GetOrganizationDetailByUIDAsync(model.OrganizationUid);
                if (organization == null)
                {
                    _logger.LogError("Organization detail not found");
                    return new ServiceResult("Organization detail not found");
                }

                organization.SpocUgpassEmail = model.SpocUgpassEmail;
                organization.AgentUrl = model.AgentUrl;

                var updatedOrganization = await _unitOfWork.OrganizationDetail.UpdateOrganizationDetail(organization);
                if (organization == null)
                {
                    _logger.LogError("Organization detail not updated");
                    return new ServiceResult("Organization detail not updated");
                }

                return new ServiceResult(true, "Organization detail updated successfully", updatedOrganization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateAgentUrlAndSpocEmailAsync Error: {ex.Message}");
                return new ServiceResult("Organization detail not updated");
            }
        }

        public Task<ServiceResult> DeleteOrganizationDetailAsync(OrganizationDetail model)
        {
            try
            {
                _logger.LogInformation("DeleteOrganizationDetailAsync Called");
                var organization = _unitOfWork.OrganizationDetail.RemoveOrganizationDetail(model);
                if (!organization)
                {
                    _logger.LogError("Organization detail not deleted");
                    return Task.FromResult(new ServiceResult("Organization detail not deleted"));
                }
                return Task.FromResult(new ServiceResult(true, "Organization detail deleted successfully", organization));
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteOrganizationDetailAsync Error: {ex.Message}");
                return Task.FromResult(new ServiceResult("Organization detail not deleted"));
            }
        }

        public async Task<ServiceResult> DeleteOrganizationDetailByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("DeleteOrganizationDetailAsync Called");
                var organization = await _unitOfWork.OrganizationDetail.RemoveOrganizationDetailById(id);
                if (!organization)
                {
                    _logger.LogError("Organization detail not deleted");
                    return new ServiceResult("Organization detail not deleted");
                }
                return new ServiceResult(true, "Organization detail deleted successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteOrganizationDetailAsync Error: {ex.Message}");
                return new ServiceResult("Organization detail not deleted");
            }
        }

    }
}
