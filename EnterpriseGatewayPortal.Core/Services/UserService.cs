using EnterpriseGatewayPortal.Core.Domain.Lookups;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Core.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;


namespace EnterpriseGatewayPortal.Core.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly IPasswordHelper _passwordHelper;

        public UserService(
            IUnitOfWork unitOfWork,
            ILogger<UserService> logger,
            IEmailSenderService emailSenderService,
            IEmailSender emailSender,
            IConfiguration configuration,
            IPasswordHelper passwordHelper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailSenderService = emailSenderService;
            _configuration = configuration;
            _emailSender = emailSender;
            _passwordHelper = passwordHelper;
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ-abcdefghijklmnopqrstuvwxyz@#$0123456789";

            var result = new char[length];
            var bytes = new byte[length];

            RandomNumberGenerator.Fill(bytes);

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[bytes[i] % chars.Length];
            }

            return new string(result);
        }

        public async Task<UserResponse> RevertUser(User user)
        {
            if (user == null)
            {
                return new UserResponse("User is null");
            }
            try
            {
                _unitOfWork.Users.Remove(user);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return new UserResponse("Failed to remove user");
            }
            return new UserResponse("Failed to add user");
        }
        public async Task<UserResponse> IsUserValid(string Email, string Password)
        {
            var wrongpassword = false;
            var response = new Response();

            var user = await _unitOfWork.Users.IsUserExist(Email);

            if (user == null)
            {
                return new UserResponse("User not found");
            }
            if (user.Status == "DELETED")
            {
                return new UserResponse("user not active");
            }
            if (user.Status == "SUSPENDED")
            {
                return new UserResponse("Your account is suspended");
            }

            if (!_passwordHelper.verifyPassword(user.Password ?? "", Password))
            {
                wrongpassword = true;
            }

            if (wrongpassword)
            {
                if (user.BadPasswordCnt == null)
                {
                    user.BadPasswordCnt = 1;
                }
                else
                {
                    user.BadPasswordCnt = user.BadPasswordCnt + 1;
                }
                int BadPasswordCount = _configuration.GetSection("BadPasswordCount").Get<int>();
                user.LastBadLoginTime = DateTime.Now;
                if (user.BadPasswordCnt >= BadPasswordCount)
                {
                    user.Status = "SUSPENDED";
                }
                try
                {
                    _unitOfWork.Users.Update(user);
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Database Exception {0}", ex.Message);
                    return new UserResponse("Internal Error");
                }
                return new UserResponse("Invalid Credentials");
            }
            if (user.BadPasswordCnt > 0)
            {
                user.BadPasswordCnt = 0;
                try
                {
                    _unitOfWork.Users.Update(user);
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Database Exception {0}", ex.Message);
                    return new UserResponse("Internal Error");
                }
            }
            return new UserResponse(user, "Success");

        }
        public async Task<UserResponse> AddUser(User user)
        {
            var response = new Response();
            if (user == null)
            {
                return new UserResponse("User is null");
            }
            var isEmailExist = await _unitOfWork.Users.IsUserExistsbyEmailAsync(user.Email!);

            if (isEmailExist)
            {
                return new UserResponse("User Email already exist");
            }
            var isMobilenoExist = await _unitOfWork.Users.IsUserExistsbyPhonenoAsync(user.MobileNumber);
            if (isMobilenoExist)
            {
                return new UserResponse("User mobile number already exist");
            }

            var password = RandomString(10);
            var hashedPassword = _passwordHelper.hashPassword(password);

            user.CreatedDate = DateTime.Now;
            user.ModifiedDate = DateTime.Now;
            user.Status = "NEW";
            user.Uuid = Guid.NewGuid().ToString();
            user.Password = hashedPassword;

            try
            {
                await _unitOfWork.Users.AddUserAsync(user);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("DatabaseExceptionAddingUser" + ex.Message);
                response.Success = false;
                response.Message = "Failed to add user";
            }
            var PortalLoginUrl = _configuration["PortalLoginUrl"];
            var mailBody = string.Format("Hi {0},\n{1}\n \n Email: {2}\n \nPassword: {3}\n \nPortal Login Url: {4}",
                user.Name, "Welcome to EnterpriseGateway portal below are your Login Credentials", user.Email, password, PortalLoginUrl);

            var message = new Message(new string[]
            {
            user.Email
            },
            "Welcome to EnterpriseGatewayportal",
            mailBody
            );

            try
            {
                //await _emailSenderService.SendEmail(message);
                await _emailSender.SendEmail(message);

                return new UserResponse(user, "User created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Send Email Failed {0}", ex.Message);
                var userresp = await RevertUser(user);
                return new UserResponse(user, "Unable to send email");
            }
        }
        public async Task<UserResponse> UpdateUser(User user)
        {
            if (user == null)
            {
                return new UserResponse("User is null");
            }
            var userInDb = await _unitOfWork.Users.GetUserByIdAsync(user.Id);
            if (userInDb == null)
            {
                return new UserResponse("Failed to get user details");
            }
            var userbyEmail = await _unitOfWork.Users.IsUserExist(user.Email!);

            if (userbyEmail != null && userbyEmail.Id != user.Id)
            {
                return new UserResponse("Email Already exist");
            }
            var isMobilenoExist = await _unitOfWork.Users.GetUserbyPhoneno(user.MobileNumber);

            if (isMobilenoExist != null && isMobilenoExist.Id != user.Id)
            {
                return new UserResponse("User mobile number already exist");
            }

            userInDb.MobileNumber = user.MobileNumber;
            userInDb.Email = user.Email;
            userInDb.ModifiedDate = DateTime.Now;
            try
            {
                _unitOfWork.Users.UpdateUser(userInDb);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("DatabaseExceptionUpdatingUser" + ex.Message);
                return new UserResponse("Failed to add User");
            }
            return new UserResponse(null, "Successfully Updated");
        }
        public async Task<IEnumerable<User>> GetUsersList()
        {
            return await _unitOfWork.Users.GetAllUserAsync();
        }
        public async Task<UserResponse> DeleteUser(int Id)
        {
            var userInDb = await _unitOfWork.Users.GetByIdAsync(Id);
            if (userInDb == null)
            {
                return new UserResponse("User not found");
            }
            userInDb.ModifiedDate = DateTime.Now;
            userInDb.Status = "DELETED";
            try
            {
                _unitOfWork.Users.Update(userInDb);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return new UserResponse("Failed to delete user");
            }
            return new UserResponse(null, "Successfully deleted user");
        }
        public async Task<UserResponse> EditUser(int Id)
        {
            var UserinDb = await _unitOfWork.Users.GetByIdAsync(Id);
            if (null == UserinDb)
            {
                return new UserResponse("User not found");
            }
            return new UserResponse(UserinDb);
        }
        public async Task<User> GetUserAsync(int Id)
        {
            var UserinDb = await _unitOfWork.Users.GetByIdAsync(Id);
            if (null == UserinDb)
            {
                return null;
            }
            return UserinDb;
        }
        public async Task<IEnumerable<RoleLookupItem>> GetRoleLookupsAsync()
        {
            return await _unitOfWork.Roles.GetRoleLookupItemsAsync();
        }
        public async Task<User> GetUserByEmail(string Email)
        {
            var user = await _unitOfWork.Users.IsUserExist(Email);

            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<List<User>> ListUsersAsync()
        {
            _logger.LogInformation("ListUsersAsync");
            return await _unitOfWork.Users.GetAllUsersWithRolesAsync();
        }
        public async Task<UserResponse> AdminResetPassword(int id)
        {
            var UserinDb = await _unitOfWork.Users.GetByIdAsync(id);
            if (null == UserinDb)
            {
                return null;
            }

            var password = RandomString(10);
            var hashedPassword = _passwordHelper.hashPassword(password);

            UserinDb.ModifiedDate = DateTime.Now;
            UserinDb.Password = hashedPassword;

            try
            {
                _unitOfWork.Users.Update(UserinDb);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("DatabaseExceptionUpdatingUser" + ex.Message);
                return new UserResponse("Failed to reset password");
            }
            var mailBody = string.Format("Hi {0},\n{1}\n \nPassword: {2}\n",
                UserinDb.Name, "Welcome to Enterprise Gateway portal below are your Login Credentials", password);

            var message = new Message(new string[]
            {
            UserinDb.Email
            },
            "Welcome to Enterprise Gateway portal",
            mailBody
            );

            try
            {
                // await _emailSenderService.SendEmail(message);
                await _emailSender.SendEmail(message);

                return new UserResponse(UserinDb, "User created successfully");
            }
            catch
            {
                //var userresp = await RevertUser(UserinDb);
                return new UserResponse(UserinDb, "Unable to send email");
            }
        }
        public async Task<UserResponse> ActivateUser(int Id)
        {
            var userInDb = await _unitOfWork.Users.GetByIdAsync(Id);
            if (userInDb == null)
            {
                return new UserResponse("User not found");
            }
            userInDb.ModifiedDate = DateTime.Now;
            userInDb.Status = "ACTIVE";
            try
            {
                _unitOfWork.Users.Update(userInDb);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return new UserResponse("Failed to Activate user");
            }
            return new UserResponse(null, "Successfully Activated user");
        }
        public async Task<bool> IsUserExist(string Email)
        {
            var user = await _unitOfWork.Users.IsUserExist(Email);

            if (user == null)
            {
                return false;
            }
            return true;
        }

    }
}
