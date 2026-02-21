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
    public class LocalPrivilegesService : ILocalPrivilegesService
    {
        private readonly ILogger<LocalPrivilegesService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public LocalPrivilegesService(ILogger<LocalPrivilegesService> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> GetAllPrivilegesListAsync()
        {
            try
            {
                _logger.LogInformation("GetAllActivePrivilegesAsync Called");
                var list = await _unitOfWork.Privileges.GetAllActivePrivilegesAsync();
                if (list == null)
                {
                    _logger.LogError("list is Empty");
                    return new ServiceResult("Privileges list is Empty");
                }
                return new ServiceResult(true, "All Privileges List recieved successfully", list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllActivePrivilegesAsync Error: {ex.Message}");
                return new ServiceResult("Privileges list not found");
            }
        }
    }
}
