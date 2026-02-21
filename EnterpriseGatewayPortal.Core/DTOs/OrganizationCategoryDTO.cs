using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class OrganizationCategoryDTO
    {
        public int id { get; set; }
        public string categoryName { get; set; }
        public string createdOn { get; set; }
        public string updatedOn { get; set; }
        public string status { get; set; }
    }
}
