using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services.Communication
{
    public class UserResponse : BaseResponse<User>
    {
        public UserResponse(User user) : base(user) { }

        public UserResponse(string message) : base(message) { }
        public UserResponse(User category, string message) :
    base(category, message)
        { }
    }
}
