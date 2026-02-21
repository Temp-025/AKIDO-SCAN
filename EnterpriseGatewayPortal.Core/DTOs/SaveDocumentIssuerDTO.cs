using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SaveDocumentIssuerDTO
    {
        public int Id { get; set; }
        public string? IssuerUid { get; set; }

        public string? IssuerOrgName { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public string? Status { get; set; }

        public string? DocumentName { get; set; }

        public string? AllowedConsumers { get; set; }

        public string? AllowedIssuers { get; set; }

        public string? Technical { get; set; }

        public string? Qr { get; set; }

        public string? Truecopy { get; set; }

        public decimal? SubscriptionFee { get; set; }
    }

    public class Verification
    {
        public string VerificationType { get; set; }
        public string Price { get; set; }
        public string Duration { get; set; }
    }
}
