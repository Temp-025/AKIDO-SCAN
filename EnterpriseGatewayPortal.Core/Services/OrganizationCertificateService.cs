using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class OrganizationCertificateService : IOrganizationCertificateService
    {
        private readonly ILogger<OrganizationCertificateService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationCertificateService(ILogger<OrganizationCertificateService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> GetAllOrganizationCertificateListAsync()
        {
            try
            {
                _logger.LogInformation("GetAllOrganizationCertificateList Called");
                var list = await _unitOfWork.OrganizationCertificate.GetAllOrganizationCertificateAsync();
                if (list == null)
                {
                    _logger.LogError("Organization Certificate list is Empty");
                    return new ServiceResult("Organization Certificate list is Empty");
                }
                return new ServiceResult(true, "All Organization Certificate List recieved successfully", list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllOrganizationCertificateList Error: {ex.Message}");
                return new ServiceResult("Organization Certificate list not found");
            }
        }

        public async Task<ServiceResult> GetOrganizationCertificateByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("GetOrganizationCertificateByIdAsync Called");
                var organization = await _unitOfWork.OrganizationCertificate.GetOrganizationCertificateByIdAsync(id);
                if (organization == null)
                {
                    _logger.LogError("Organization Certificate not found");
                    return new ServiceResult("Organization Certificate not found");
                }
                return new ServiceResult(true, "Organization Certificate recieved successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetOrganizationCertificateByIdAsync Error: {ex.Message}");
                return new ServiceResult("Organization Certificate not found");
            }
        }

        public async Task<ServiceResult> GetOrganizationCertificateStatusByUIdAsync(string uid)
        {
            try
            {
                _logger.LogInformation("GetOrganizationCertificateStatusByUIdAsync Called");
                var organization = await _unitOfWork.OrganizationCertificate.GetOrganizationCertificateByUIDAsync(uid);
                if (organization == null)
                {
                    _logger.LogError("Organization Certificate not found");
                    return new ServiceResult("Organization Certificate not found");
                }
                EsealCertificateStatusDTO esealCetificateStatus = new()
                {
                    certificateStatus = organization.CertificateStatus.ToString(),
                    certificateStartDate = organization.CertificateIssueDate.ToString(),
                    certificateEndDate = organization.CerificateExpiryDate.ToString()
                };

                return new ServiceResult(true, "Organization Certificate Status recieved successfully", esealCetificateStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetOrganizationCertificateStatusByUIdAsync Error: {ex.Message}");
                return new ServiceResult("Organization Certificate status not found");
            }
        }

        public async Task<ServiceResult> AddOrganizationCertificateAsync(OrganizationCertificate model)
        {
            try
            {
                _logger.LogInformation("AddOrganizationCertificateAsync Called");

                var isOrganizationCertificateNameExist = await _unitOfWork.OrganizationCertificate.IsOrganizationCertificateExistsWithUIDAsync(model.OrganizationUid);
                if (isOrganizationCertificateNameExist)
                {
                    _logger.LogError("Organization Certificate Already Exists");
                    return new ServiceResult("Organization Certificate Already Exists");
                }

                var organization = await _unitOfWork.OrganizationCertificate.AddOrganizationCertificateAsync(model);
                if (organization == null)
                {
                    _logger.LogError("Organization Certificate not added");
                    return new ServiceResult("Organization Certificate not added");
                }
                return new ServiceResult(true, "Organization Certificate added successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddOrganizationCertificateAsync Error: {ex.Message}");
                return new ServiceResult("Organization Certificate not added");
            }
        }

        public async Task<ServiceResult> UpdateOrganizationCertificateAsync(OrganizationCertificate model)
        {
            try
            {
                _logger.LogInformation("UpdateOrganizationCertificateAsync Called");

                var organization = await _unitOfWork.OrganizationCertificate.UpdateOrganizationCertificate(model);
                if (organization == null)
                {
                    _logger.LogError("Organization Certificate not updated");
                    return new ServiceResult("Organization Certificate not updated");
                }
                return new ServiceResult(true, "Organization Certificate updated successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateOrganizationCertificate Error: {ex.Message}");
                return new ServiceResult("Organization Certificate not updated");
            }
        }

        public Task<ServiceResult> DeleteOrganizationCertificateAsync(OrganizationCertificate model)
        {
            try
            {
                _logger.LogInformation("DeleteOrganizationCertificateAsync Called");
                var organization = _unitOfWork.OrganizationCertificate.RemoveOrganizationCertificate(model);
                if (organization)
                {
                    _logger.LogError("organization not deleted");
                    return Task.FromResult(new ServiceResult("Organization Certificate not deleted"));
                }
                return Task.FromResult(new ServiceResult(true, "Organization Certificate deleted successfully", organization));
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteOrganizationCertificateAsync Error: {ex.Message}");
                return Task.FromResult(new ServiceResult("Organization Certificate not deleted"));
            }
        }

        public async Task<ServiceResult> DeleteOrganizationCertificateByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("DeleteOrganizationCertificateAsync Called");
                var organization = await _unitOfWork.OrganizationCertificate.RemoveOrganizationCertificateById(id);
                if (!organization)
                {
                    _logger.LogError("organization not deleted");
                    return new ServiceResult("Organization Certificate not deleted");
                }
                return new ServiceResult(true, "Organization Certificate deleted successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteOrganizationCertificateAsync Error: {ex.Message}");
                return new ServiceResult("Organization Certificate not deleted");
            }
        }

        public async Task<ServiceResult> GetOrganizationActiveCertificateByAsync()
        {
            try
            {
                _logger.LogInformation("GetOrganizationCertificateByIdAsync Called");
                var organization = await _unitOfWork.OrganizationCertificate.GetOrganizationActiveCertificateByUIDAsync();
                if (organization == null)
                {
                    _logger.LogError("Organization Certificate not found");
                    return new ServiceResult("Organization Certificate not found");
                }
                return new ServiceResult(true, "Organization Certificate recieved successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetOrganizationCertificateByIdAsync Error: {ex.Message}");
                return new ServiceResult("Organization Certificate not found");
            }
        }

    }
}
