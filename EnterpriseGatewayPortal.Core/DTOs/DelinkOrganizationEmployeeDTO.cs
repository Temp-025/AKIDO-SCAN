using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DelinkOrganizationEmployeeDTO
    {
        public string OrganizationUID { get; set; }
        public string EmployeeEmail { get; set; }
        public string EndDate { get; set; }
    }
}
