using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class PaymentRequestDTO
    {
        public double Amount { get; set; }
        public string Category { get; set; }
        public string Currency { get; set; }
        public string ExternalId { get; set; }
        public string PayeeNote { get; set; }
        public string Payer { get; set; }
        public string PayerNote { get; set; }
        public string PaymentCategory { get; set; }
        public List<PaymentInfoDto> PaymentInfo { get; set; }
        public string SubscriberUniqueId { get; set; }
        public string WalletId { get; set; }
    }

    public class PaymentInfoDto
    {
        public double Discount { get; set; }
        public string OrgId { get; set; }
        public double Rate { get; set; }
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string SlabId { get; set; }
        public string StakeHolder { get; set; }
        public double Tax { get; set; }
        public double Volume { get; set; }
    }

   
}
