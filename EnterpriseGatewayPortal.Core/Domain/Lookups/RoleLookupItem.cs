using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Lookups
{
    public class RoleLookupItem : LookupItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
