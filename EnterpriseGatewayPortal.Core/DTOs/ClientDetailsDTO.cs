using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class ClientDetailsDTO
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string OrganizationId { get; set; }
        public string LogoutUrl { get; set; }
    }
}
