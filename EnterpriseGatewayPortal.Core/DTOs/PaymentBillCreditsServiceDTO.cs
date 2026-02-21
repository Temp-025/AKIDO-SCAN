using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class PaymentBillCreditsServiceDTO
    {
        public PaymentRequestDTO RequestBody { get; set; }
        public string ServiceMethod { get; set; }
    }

   
}
