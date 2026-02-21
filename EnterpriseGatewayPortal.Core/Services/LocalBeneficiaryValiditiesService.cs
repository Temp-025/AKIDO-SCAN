using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class LocalBeneficiaryValiditiesService : ILocalBeneficiaryValiditiesService
    {
        private readonly ILogger<LocalBeneficiaryValiditiesService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public LocalBeneficiaryValiditiesService(ILogger<LocalBeneficiaryValiditiesService> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> GetAllBeneficiaryValiditiesByBeneficiaryIdListAsync(int beneficiaryId)
        {
            try
            {
                _logger.LogInformation("GetAllBeneficiaryValiditiesByBeneficiaryIdAsync Called");
                var list = await _unitOfWork.BeneficiaryValidities.GetAllBeneficiaryValiditiesByBeneficiaryIdAsync(beneficiaryId);
                if (list == null)
                {
                    _logger.LogError("list is Empty");
                    return new ServiceResult("Beneficiary Validities list is Empty");
                }
                return new ServiceResult(true, "All Beneficiary Validities List recieved successfully", list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllBeneficiaryValiditiesByBeneficiaryIdAsync Error: {ex.Message}");
                return new ServiceResult("Beneficiary Validities list not found");
            }
        }

        public async Task<ServiceResult> RemoveAllBeneficiaryValiditiesByBeneficiaryIdAsync(int beneficiaryId)
        {
            try
            {
                _logger.LogInformation("RemoveAllBeneficiaryValiditiesByBeneficiaryIdAsync Called");
                var value = await _unitOfWork.BeneficiaryValidities.RemoveBeneficiaryValiditiesByBeneficiaryId(beneficiaryId);
                if (!value)
                {
                    _logger.LogError("All Beneficiary Validities by beneficiaryId is Empty");
                    return new ServiceResult("All Beneficiary Validities by beneficiaryId is Empty");
                }
                return new ServiceResult(true, "All Beneficiary Validities by beneficiaryId removed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"RemoveAllBeneficiaryValiditiesByBeneficiaryIdAsync Error: {ex.Message}");
                return new ServiceResult("Beneficiary Validities by beneficiaryId not found");
            }
        }

        public async Task<ServiceResult> AddBeneficiaryValiditiesListAsync(List<BeneficiaryValidity> models)
        {
            try
            {
                _logger.LogInformation("AddBeneficiaryValiditiesOfUsersListAsync Called");

                var validities = await _unitOfWork.BeneficiaryValidities.AddBeneficiaryValiditiesOfUsersListAsync(models);
                if (validities == null)
                {
                    _logger.LogError("Beneficiary Validities not added");
                    return new ServiceResult("Beneficiary Validities not added");
                }
                return new ServiceResult(true, validities.Count > 1
                                            ? "All Beneficiary Validities added successfully"
                                            : "Beneficiary Validities added successfully", validities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddBeneficiaryValiditiesOfUsersListAsync Error: {ex.Message}");
                return new ServiceResult("Beneficiary Validities not added");
            }
        }
    }
}
