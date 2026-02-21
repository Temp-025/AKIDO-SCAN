using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class OrgEmailDomainService : IOrgEmailDomainService
    {
        private readonly ILogger<OrgEmailDomainService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public OrgEmailDomainService(ILogger<OrgEmailDomainService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> GetAllOrgEmailDomainListAsync()
        {
            try
            {
                _logger.LogInformation("GetAllOrgEmailDomainList Called");
                var list = await _unitOfWork.OrgEmailDomain.GetAllOrgEmailDomainAsync();
                if (list == null)
                {
                    _logger.LogError("list is Empty");
                    return new ServiceResult("Organization email domain list is Empty");
                }
                return new ServiceResult(true, "All Organization email domain List recieved successfully", list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllOrgEmailDomainList Error: {ex.Message}");
                return new ServiceResult("Organization email domain list not found");
            }
        }

        public async Task<ServiceResult> GetOrgEmailDomainByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("GetOrgEmailDomainByIdAsync Called");
                var organization = await _unitOfWork.OrgEmailDomain.GetOrgEmailDomainByIdAsync(id);
                if (organization == null)
                {
                    _logger.LogError("Organization email domain not found");
                    return new ServiceResult("Organization email domain not found");
                }
                return new ServiceResult(true, "Organization email domain recieved successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetOrgEmailDomainByIdAsync Error: {ex.Message}");
                return new ServiceResult("Organization email domain not found");
            }
        }

        public async Task<ServiceResult> GetOrgEmailDomainByOrgUidAsync(string uid)
        {
            try
            {
                _logger.LogInformation("GetOrgEmailDomainByUIdAsync Called");
                var organization = await _unitOfWork.OrgEmailDomain.GetOrgEmailDomainByOrgUidAsync(uid);
                if (organization == null)
                {
                    _logger.LogError("Organization email domain not found");
                    return new ServiceResult("Organization email domain not found");
                }
                return new ServiceResult(true, "Organization email domain recieved successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetOrgEmailDomainByUIdAsync Error: {ex.Message}");
                return new ServiceResult("Organization email domain not found");
            }
        }

        public async Task<ServiceResult> AddOrgEmailDomainAsync(OrgEmailDomain model)
        {
            try
            {
                _logger.LogInformation("AddOrgEmailDomainAsync Called");

                var isOrgEmailDomainNameExist = await _unitOfWork.OrgEmailDomain.IsOrgEmailDomainExistsWithUIDAsync(model.OrganizationUid);
                if (isOrgEmailDomainNameExist)
                {
                    _logger.LogError("Organization email domain Already Exists");
                    return new ServiceResult("Organization email domain Already Exists");
                }

                var organization = await _unitOfWork.OrgEmailDomain.AddOrgEmailDomainAsync(model);
                if (organization == null)
                {
                    _logger.LogError("OrgEmailDomain not added");
                    return new ServiceResult("Organization email domain not added");
                }
                return new ServiceResult(true, "Organization email domain added successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddOrgEmailDomainAsync Error: {ex.Message}");
                return new ServiceResult("Organization email domain not added");
            }
        }

        public async Task<ServiceResult> UpdateOrgEmailDomainAsync(OrgEmailDomain model)
        {
            try
            {
                _logger.LogInformation("UpdateOrgEmailDomainAsync Called");

                var organization = await _unitOfWork.OrgEmailDomain.UpdateOrgEmailDomain(model);
                if (organization == null)
                {
                    _logger.LogError("Organization email domain not updated");
                    return new ServiceResult("Organization email domain not updated");
                }
                return new ServiceResult(true, "Organization email domain updated successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateOrgEmailDomain Error: {ex.Message}");
                return new ServiceResult("Organization email domain not updated");
            }
        }

        public Task<ServiceResult> DeleteOrgEmailDomainAsync(OrgEmailDomain model)
        {
            try
            {
                _logger.LogInformation("DeleteOrgEmailDomainAsync Called");
                var organization = _unitOfWork.OrgEmailDomain.RemoveOrgEmailDomain(model);
                if (organization)
                {
                    _logger.LogError("organization not deleted");
                    return Task.FromResult(new ServiceResult("Organization email domain not deleted"));
                }
                return Task.FromResult(new ServiceResult(true, "Organization email domain deleted successfully", organization));
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteOrgEmailDomainAsync Error: {ex.Message}");
                return Task.FromResult(new ServiceResult("Organization email domain not deleted"));
            }
        }

        public async Task<ServiceResult> DeleteOrgEmailDomainByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("DeleteOrgEmailDomainAsync Called");
                var organization = await _unitOfWork.OrgEmailDomain.RemoveOrgEmailDomainById(id);
                if (!organization)
                {
                    _logger.LogError("Organization email domain not deleted");
                    return new ServiceResult("Organization email domain not deleted");
                }
                return new ServiceResult(true, "Organization email domain deleted successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteOrgEmailDomainAsync Error: {ex.Message}");
                return new ServiceResult("Organization email domain not deleted");
            }
        }

    }
}
