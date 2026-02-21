using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SubscriptionAllListDTO
    {
        public int Id { get; set; }
        public string? IssuerUid { get; set; }
        public string? DocumentIssuerName { get; set; }
        public string? DocumentTypes { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? SubscriptionDate { get; set; }
        public string? Status { get; set; }
        public string? SubscriptionFee { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
