using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class MCValidationService : IMCValidationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MCValidationService> _logger;

        public MCValidationService(IUnitOfWork unitOfWork, ILogger<MCValidationService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> IsMCEnabled(int activityId)
        {
            // Get operation auth scheme
            var activity = await _unitOfWork.Activities.GetByIdAsync(activityId);
            if (null == activity)
            {
                _logger.LogError("Operation authenticationscheme not found");
                return false;
            }

            if (activity.McEnabled)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<BooleanResponse> IsCheckerApprovalRequired(
                int activityID, string operationType, string maker, string requestData)
        {


            var makerInDb = await _unitOfWork.Users.GetUserbyUuidAsync(maker);
            if (null == makerInDb)
            {
                _logger.LogError("User not found");
                return new BooleanResponse("User not found");
            }

            var makerChecker = new MakerChecker();
            makerChecker.CreatedDate = DateTime.Now;
            makerChecker.ModifiedDate = DateTime.Now;
            makerChecker.OperationPriority = "HIGH";
            makerChecker.OperationType = operationType;
            makerChecker.State = "PENDING";
            makerChecker.MakerRoleId = (int)makerInDb.RoleId!;
            makerChecker.ActivityId = activityID;
            makerChecker.MakerId = makerInDb.Id;
            makerChecker.RequestData = requestData;

            try
            {
                await _unitOfWork.MakerChecker.AddAsync(makerChecker);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("CheckApprovalRequired Failed: {0}", ex.Message);
                return new BooleanResponse("An error occurred while procesing your request");
            }
            return new BooleanResponse(true);
        }
    }
}
