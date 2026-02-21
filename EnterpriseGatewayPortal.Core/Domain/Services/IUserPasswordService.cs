using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IUserPasswordService
    {
        Task<Response> ChangePassword(int userId, string oldPassword, string newPassword);
        Task<Response> ResetPassword(int userId, string newPassword);
    }
}
