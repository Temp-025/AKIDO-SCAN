using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services.Communication
{
    public class UserSecurityQueResponse : BaseResponse<UserSecurityQue>
    {
        public UserSecurityQueResponse(UserSecurityQue category) : base(category) { }

        public UserSecurityQueResponse(string message) : base(message) { }
    }
}
