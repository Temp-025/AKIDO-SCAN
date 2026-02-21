using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }

        public string Suid { get; set; }

        public string Name { get; set; }

        public DateTime AccessTokenExpiryTime { get; set; }

        public string OrganizationName { get; set; } = "";

        public string OrganizationId { get; set; } = "";

        public string AccountType { get; set; } = "";
    }
}
