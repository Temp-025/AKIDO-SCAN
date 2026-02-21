using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class KycApplicationDTO
    {
        public int Id { get; set; }

        public string OrganizationId { get; set; }

        public string ApplicationName { get; set; }
        public string Status { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}
