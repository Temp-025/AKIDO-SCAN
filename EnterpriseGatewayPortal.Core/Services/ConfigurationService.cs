using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class ConfigurationService: IConfigurationService
    {
        private readonly ILogger<ConfigurationService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ConfigurationService(ILogger<ConfigurationService> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResult> GetAllConfiguration()
        {
            try
            {
                _logger.LogInformation("Get all configurationList Called");
                var list = await _unitOfWork.Configurations.GetAll();
                if (list == null)
                {
                    _logger.LogError("list is Empty");
                    return new ServiceResult("Configuration list is Empty");
                }
                return new ServiceResult(true, "All Configuration List recieved successfully", list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get all configurationList Error: {ex.Message}");
                return new ServiceResult("All Configuration list not found");
            }
        }
        public async Task<ServiceResult> AddConfigurationAsync(Configuration model)
        {
            try
            {
                _logger.LogInformation("AddConfigurationAsync Called");

                var list = await _unitOfWork.Configurations.GetAll();

                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item.Name == model.Name)
                        {
                            return new ServiceResult("Configuration Already exist");
                        }
                    }
                }
                try
                {
                    await _unitOfWork.Configurations.AddConfigurationAsync(model);
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    _logger.LogError("DatabaseExceptionAddingConfiguration" + ex.Message);
                    return new ServiceResult("Failed to add");
                }
                return new ServiceResult(true, "Configuration added successfully", null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddConfigurationAsync Error: {ex.Message}");
                return new ServiceResult("Configuration not added");
            }
        }
    }
}
