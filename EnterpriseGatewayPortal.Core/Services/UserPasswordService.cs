using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Core.Utilities;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class UserPasswordService : IUserPasswordService
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserPasswordService> _logger;
        private readonly IPasswordHelper _passwordHelper;

        public UserPasswordService(IUserService userService, IUnitOfWork unitOfWork, ILogger<UserPasswordService> logger, IPasswordHelper passwordHelper)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _passwordHelper = passwordHelper;
        }
        public async Task<Response> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var response = new Response();

            var userInDb = await _unitOfWork.Users.GetByIdAsync(userId);
            if (null == userInDb)
            {
                response.Success = false;
                response.Message = "No user found with ID";
                return response;
            }

            if (!userInDb.Status.Equals("NEW") && !userInDb.Status.Equals("ACTIVE"))
            {
                response.Success = false;
                response.Message = "User status is not ACTIVE/NEW";
                return response;
            }
            if (!_passwordHelper.verifyPassword(userInDb.Password, oldPassword))
            {
                response.Success = false;
                response.Message = "Old Password did not match";
                return response;
            }
            if (_passwordHelper.verifyPassword(userInDb.Password, newPassword))
            {
                response.Success = false;
                response.Message = "Old Password matches with new password";
                return response;
            }
            if (userInDb.Status.Equals("NEW"))
            {
                userInDb.Status = "ACTIVE";
            }
            var encryptionKey = await _unitOfWork.EncDecKeys.GetByIdAsync(24);
            if (encryptionKey == null)
            {
                return new Response();
            }

            //Hash Password 
            var hashedPassword = _passwordHelper.hashPassword(newPassword);
            try
            {
                userInDb.Password = hashedPassword;
                _unitOfWork.Users.Update(userInDb);
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to update password";
                return response;
            }
            response.Success = true;
            response.Message = "Successfully updated password";
            return response;
        }
        public async Task<Response> ResetPassword(int userId, string newPassword)
        {
            var response = new Response();

            var userInDb = await _unitOfWork.Users.GetByIdAsync(userId);
            if (null == userInDb)
            {
                response.Success = false;
                response.Message = "No user found with ID";
                return response;
            }

            if (!userInDb.Status.Equals("NEW") && !userInDb.Status.Equals("ACTIVE"))
            {
                response.Success = false;
                response.Message = "User status is not ACTIVE/NEW";
                return response;
            }
            try
            {
                userInDb.Password = _passwordHelper.hashPassword(newPassword);
                _unitOfWork.Users.Update(userInDb);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update Password");
                response.Success = false;
                response.Message = "Failed to update password";
                return response;
            }
            response.Success = true;
            response.Message = "Successfully updated password";
            return response;
        }
    }
}
