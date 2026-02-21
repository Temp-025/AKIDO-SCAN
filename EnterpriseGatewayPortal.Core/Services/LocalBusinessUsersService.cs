using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class LocalBusinessUsersService : ILocalBusinessUsersService
    {
        private readonly ILogger<LocalBusinessUsersService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public LocalBusinessUsersService(ILogger<LocalBusinessUsersService> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        public async Task<ServiceResult> GetBusinessUserByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("GetBusinessUserByOrgUidAsync Called");
                var user = await _unitOfWork.BusinessUsers.GetOrgSubscriberEmailByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError("User not found");
                    return new ServiceResult("Business sser not found");
                }
                return new ServiceResult(true, "Business User recieved successfully", user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetBusinessUserByOrgUidAsync Error: {ex.Message}");
                return new ServiceResult("Business User not found");
            }
        }
        public async Task<ServiceResult> GetBusinessUserByOrgUidAsync(string orgUid)
        {
            try
            {
                _logger.LogInformation("GetBusinessUserByOrgUidAsync Called");
                var user = await _unitOfWork.BusinessUsers.GetOrgSubscriberEmailByUIDAsync(orgUid);
                if (user == null)
                {
                    _logger.LogError("User not found");
                    return new ServiceResult("Business sser not found");
                }
                return new ServiceResult(true, "Business User recieved successfully", user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetBusinessUserByOrgUidAsync Error: {ex.Message}");
                return new ServiceResult("Business User not found");
            }
        }

        public async Task<ServiceResult> GetAllBusinessUsersByOrgUidAsync(string orgUid)
        {
            try
            {
                _logger.LogInformation("GetAllBusinessUsersByOrgUidAsync Called");
                var list = await _unitOfWork.BusinessUsers.GetAllOrgSubscriberEmailsByOrgUIDAsync(orgUid);
                if (list == null)
                {
                    _logger.LogError("list is Empty");
                    return new ServiceResult("Business User list is Empty");
                }
                return new ServiceResult(true, "All Business User List recieved successfully", list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllBusinessUsersByOrgUidAsync Error: {ex.Message}");
                return new ServiceResult("Business User list not found");
            }
        }

        public async Task<ServiceResult> GetAllBusinessUserListAsync()
        {
            try
            {
                _logger.LogInformation("GetAllBusinessUserList Called");
                var list = await _unitOfWork.BusinessUsers.GetAllOrgSubscriberEmailAsync();
                if (list == null)
                {
                    _logger.LogError("list is Empty");
                    return new ServiceResult("Business User list is Empty");
                }
                return new ServiceResult(true, "All Business User List recieved successfully", list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllBusinessUserList Error: {ex.Message}");
                return new ServiceResult("Business User list not found");
            }
        }

        public async Task<ServiceResult> GetBulkSignerListAsync()
        {
            try
            {
                _logger.LogInformation("GetAllBusinessUserList Called");
                var list = await _unitOfWork.BusinessUsers.GetAllOrgSubscriberEmailwithBulkSignAsync();
                if (list == null)
                {
                    _logger.LogError("list is Empty");
                    return new ServiceResult("Business User list is Empty");
                }
                return new ServiceResult(true, "All Business User List recieved successfully", list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllBusinessUserList Error: {ex.Message}");
                return new ServiceResult("Business User list not found");
            }
        }
        public async Task<ServiceResult> AddBusinessUserAsync(OrgSubscriberEmail model)
        {
            try
            {
                _logger.LogInformation("AddBusinessUserAsync Called");

                var isBusinessUserNameExist = await _unitOfWork.BusinessUsers.IsOrgSubscriberEmailExistsWithUIDAsync(model.OrganizationUid!);
                if (isBusinessUserNameExist)
                {
                    _logger.LogError("Business User Already Exists");
                    return new ServiceResult("Business User Already Exists");
                }

                var organization = await _unitOfWork.BusinessUsers.AddOrgSubscriberEmailAsync(model);
                if (organization == null)
                {
                    _logger.LogError("BusinessUser not added");
                    return new ServiceResult("Business User not added");
                }
                return new ServiceResult(true, "Business User added successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddBusinessUserAsync Error: {ex.Message}");
                return new ServiceResult("Business User not added");
            }
        }

        public async Task<ServiceResult> AddBusinessUsersListAsync(List<OrgSubscriberEmail> models)
        {
            try
            {
                _logger.LogInformation("AddBusinessUsersListAsync Called");

                var organization = await _unitOfWork.BusinessUsers.AddOrgSubscriberEmailsListAsync(models);
                if (organization == null)
                {
                    _logger.LogError("BusinessUser not added");
                    return new ServiceResult("Business Users not added");
                }
                return new ServiceResult(true, organization.Count > 1
                                            ? "All Business Users added successfully"
                                            : "Business User added successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddBusinessUsersListAsync Error: {ex.Message}");
                return new ServiceResult("Business User not added");
            }
        }

        public async Task<ServiceResult> UpdateBusinessUserAsync(OrgSubscriberEmail model)
        {
            try
            {
                _logger.LogInformation("UpdateBusinessUserAsync Called");

                var organization = await _unitOfWork.BusinessUsers.UpdateOrgSubscriberEmail(model);
                if (organization == null)
                {
                    _logger.LogError("Business User not updated");
                    return new ServiceResult("Business User not updated");
                }
                return new ServiceResult(true, "Business User updated successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateBusinessUser Error: {ex.Message}");
                return new ServiceResult("Business User not updated");
            }
        }
        public async Task<ServiceResult> DeleteBusinessUserAsync(OrgSubscriberEmail model)
        {
            try
            {
                _logger.LogInformation("UpdateBusinessUserAsync Called");

                var organization = await _unitOfWork.BusinessUsers.UpdateOrgSubscriberEmail(model);
                if (organization == null)
                {
                    _logger.LogError("Business User not found");
                    return new ServiceResult("Business User not found");
                }
                return new ServiceResult(true, "Business User deleted successfully", organization);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateBusinessUser Error: {ex.Message}");
                return new ServiceResult("Failed to delete business user");
            }
        }
        public async Task<ServiceResult> GetBusinessUserByEmailAsync(string Email)
        {
            try
            {
                _logger.LogInformation("GetBusinessUserByEmailAsync Called");
                var user = await _unitOfWork.BusinessUsers.GetOrgSubscriberEmailByEmailAsync(Email);
                if (user == null)
                {
                    _logger.LogError("User not found");
                    return new ServiceResult("Business user not found");
                }
                return new ServiceResult(true, "Business User recieved successfully", user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetBusinessUserByEmailAsync Error: {ex.Message}");
                return new ServiceResult("Business User not found");
            }
        }

        public async Task<ServiceResult> GetBusinessUserByEmployeeEmailAsync(string Email)
        {
            try
            {
                _logger.LogInformation("GetBusinessUserByEmployeeEmailAsync Called");
                var user = await _unitOfWork.BusinessUsers.GetOrgSubscriberEmailByEmployeeEmailAsync(Email);
                if (user == null)
                {
                    _logger.LogError("User not found");
                    return new ServiceResult("Business user not found");
                }
                return new ServiceResult(true, "Business User recieved successfully", user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetBusinessUserByEmployeeEmailAsync Error: {ex.Message}");
                return new ServiceResult("Business User not found");
            }
        }
    }
}
