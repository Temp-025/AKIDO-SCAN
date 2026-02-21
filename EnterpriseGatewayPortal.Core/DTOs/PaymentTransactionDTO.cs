using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class PaymentTransactionDTO
    {
        public string payer { get; set; }
        public string payerName { get; set; }
        public string category { get; set; }
        public string status { get; set; }
        public string paymentChannel { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string externalId { get; set; }
        public double amount { get; set; }
        public string payerNote { get; set; }
        public string payeeNote { get; set; }
        public string currency { get; set; }
        public Wallet wallet { get; set; }
        public string chargeModel { get; set; }
        public string createdBy { get; set; }
        public double transactionCharge { get; set; }
        public double vendorCharge { get; set; }
        public double totalTransactionCharge { get; set; }
        public string vendor { get; set; }
        public string vendorTransactionId { get; set; }
        public string reconciledOn { get; set; }
        public string transactions { get; set; }
        public string id { get; set; }
        public string createdAt { get; set; }
    }

    public class Wallet
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
