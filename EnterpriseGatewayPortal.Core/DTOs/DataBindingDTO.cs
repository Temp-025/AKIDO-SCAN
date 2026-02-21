using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DataBindingDTO
    {
        public string? Name { get; set; }
        public List<string> SupportedMethods { get; set; }
    }

}
