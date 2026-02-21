using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services.Communication
{
    public class roleRequest
    {
        public Role role;
        public IDictionary<int, bool> selectedActivityIds;
    }
    public class RoleResponse : BaseResponse<Role>
    {
        public RoleResponse(Role role) : base(role) { }

        public RoleResponse(string message) : base(message) { }
        public RoleResponse(Role category, string message) :
            base(category, message)
        { }
    }
}
