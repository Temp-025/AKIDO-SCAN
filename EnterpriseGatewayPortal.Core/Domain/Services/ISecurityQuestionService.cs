using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface ISecurityQuestionService
    {
        Task<UserSecurityQueResponse> CreateUserSecurityQnsAns(UserSecurityQue userSecurityQue);
        Task<GetAllUserSecurityQueResponse> GetUserSecurityQuestions(int userId);
        Task<UserSecurityQueResponse> DeleteUserSecurityQnsAns(UserSecurityQue userSecurityQue);
        Task<UserSecurityQueResponse> UpdateUserSecurityQnsAns(UserSecurityQue userSecurityQue);
        Task<ValidateUserSecQueResponse> ValidateUserSecurityQuestions(ValidateUserSecQueRequest request);
    }
}
