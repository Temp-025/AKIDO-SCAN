using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class BulkSignerListDTO
    {
        public List<string> bulkSignerList { get; set; }
        public List<string> bulkSignerEsealList { get; set; }
    }
}
