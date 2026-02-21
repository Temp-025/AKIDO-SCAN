//using Microsoft.Extensions.Logging;
//using EnterpriseGatewayPortal.Core.DTOs;
//using EnterpriseGatewayPortal.Core.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using EnterpriseGatewayPortal.Core.Domain.Repositories;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class SecurityQuestionService:ISecurityQuestionService
//    {
//        private readonly ILogger<SecurityQuestionService> _logger;
//        private readonly IUnitOfWork _unitOfWork;
//        public SecurityQuestionService(ILogger<SecurityQuestionService> logger,IUnitOfWork unitOfWork)
//        {
//            _logger = logger;
//            _unitOfWork = unitOfWork;
//        }
//        public async Task<UserSecurityQueResponse> CreateUserSecurityQnsAns(UserSecurityQue userSecurityQue)
//        {
//            int userId = (int)userSecurityQue.UserId;
//            var userSecInDb = await _unitOfWork.UsersSecurityQue.GetAllUserSecQueAnsAsync(userId);
//            if (userSecInDb.Count() == 2)
//            {
//                // Log the exception 
//                return new UserSecurityQueResponse("User security questions already provisioned");
//            }

//            userSecurityQue.CreatedDate = DateTime.Now;
//            userSecurityQue.ModifiedDate = DateTime.Now;

//            await _unitOfWork.UsersSecurityQue.AddAsync(userSecurityQue);

//            try
//            {
//                _unitOfWork.Save();

//            }
//            catch (Exception ex)
//            {
//                // Log the exception
//                _logger.LogError("Add User Sec qns failed: {0}", ex.Message);
//                return new UserSecurityQueResponse("An error occurred while creating the user security question/answer. Please contact the admin.");
//            }

//            var userSecCount = await _unitOfWork.UsersSecurityQue.GetAllUserSecQueAnsAsync((int)userSecurityQue.UserId);
//            if (userSecCount.Count() >= 2)
//            {

//                var userInDb = await _unitOfWork.Users.GetByIdAsync((int)userSecurityQue.UserId);
//                if (null == userInDb)
//                {
//                    return new UserSecurityQueResponse("An error occurred while creating the user security question/answer. Please contact the admin.");
//                }

//                userInDb.Status = "ACTIVE";

//                try
//                {
//                    _unitOfWork.Users.Update(userInDb);
//                    _unitOfWork.Save();
//                    return new UserSecurityQueResponse(userSecurityQue);
//                }
//                catch
//                {
//                    return new UserSecurityQueResponse("An error occurred while creating the user security question/answer. Please contact the admin.");
//                }
//            }
//            return new UserSecurityQueResponse(userSecurityQue);

//        }

//        public async Task<GetAllUserSecurityQueResponse> GetUserSecurityQuestions(int userId)
//        {

//            // Variable declaration
//            GetAllUserSecurityQueResponse response = new GetAllUserSecurityQueResponse();


//            var userSecQues = await _unitOfWork.UsersSecurityQue.GetAllUserSecQueAnsAsync(userId);
//            if (userSecQues.Count() == 0)
//            {
//                response.Success = false;
//                response.Message = "User Security Questions not found";
//                return response;
//            }

//            var SecQueList = new List<SecurityQuestions>();
//            foreach (var item in userSecQues)
//            {
//                var secQue = new SecurityQuestions()
//                {
//                    Id = item.Id,
//                    Question = item.Question
//                };

//                SecQueList.Add(secQue);
//            }

//            response.Success = true;
//            response.Result = SecQueList;
//            return response;
//        }

//        public async Task<UserSecurityQueResponse> DeleteUserSecurityQnsAns(UserSecurityQue userSecurityQue)
//        {
//            var userSecQueinDb = await _unitOfWork.UsersSecurityQue.GetByIdAsync(userSecurityQue.Id);
//            if (null == userSecQueinDb)
//            {
//                return new UserSecurityQueResponse("Could not delete user security question/answer. Please contact admin");
//            }


//            _unitOfWork.UsersSecurityQue.Remove(userSecQueinDb);

//            try
//            {
//                await _unitOfWork.SaveAsync();
//                return new UserSecurityQueResponse(userSecurityQue);
//            }
//            catch
//            {
//                return new UserSecurityQueResponse("Could not delete user security question/answer. Please contact admin");
//            }
//        }

//        public async Task<UserSecurityQueResponse> UpdateUserSecurityQnsAns(UserSecurityQue userSecurityQue)
//        {
//            var userSecQueinDb = await _unitOfWork.UsersSecurityQue.GetByIdAsync(userSecurityQue.Id);
//            if (null == userSecQueinDb)
//            {
//                // Log the exception 
//                return new UserSecurityQueResponse("No user security question/answer found with given ID. Please contact the admin.");
//            }

//            userSecQueinDb.Question = userSecurityQue.Question;
//            userSecQueinDb.Answer = userSecurityQue.Answer;
//            userSecQueinDb.UpdatedBy = "sysadmin";
//            userSecQueinDb.ModifiedDate = DateTime.Now;

//            try
//            {
//                _unitOfWork.UsersSecurityQue.Update(userSecQueinDb);
//                await _unitOfWork.SaveAsync();

//                return new UserSecurityQueResponse(userSecQueinDb);
//            }
//            catch (Exception)
//            {
//                // Log the exception 
//                return new UserSecurityQueResponse("An error occurred while updating the user security question/answer. Please contact the admin.");
//            }
//        }

//        public async Task<ValidateUserSecQueResponse> ValidateUserSecurityQuestions(ValidateUserSecQueRequest request)
//        {

//            // Variable declaration
//            ValidateUserSecQueResponse response = new ValidateUserSecQueResponse();


//            // Get all user security questions
//            var userSecQueAns = await _unitOfWork.UsersSecurityQue.GetAllUserSecQueAnsAsync(int.Parse(request.uuid));
//            int i = 0;

//            // Validate all the security questions
//            foreach (var item in request.secQueAns)
//            {
//                // Check the user security questions in request security questions
//                foreach (var item1 in userSecQueAns)
//                {
//                    // Check if secque matches
//                    if (item.secQue == item1.Question)
//                    {
//                        // Check the answer
//                        if (item.answer != item1.Answer)
//                        {
//                            response.Success = false;
//                            response.Message = "User Security Questions/Answers not matched";

//                            return response;
//                        }
//                        i++;
//                    }
//                }
//            }

//            // Check if all the request security questions match with user sec questions
//            if (i != request.secQueAns.Count)
//            {
//                response.Success = false;
//                response.Message = "Security Questions/Answers not matched with User";

//                return response;
//            }

//            // Generate sessionid


//            // Send Success response
//            response.Success = true;


//            return response;
//        }
//    }
//}
