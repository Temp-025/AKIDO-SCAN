using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class CertificateDetailsDTO
    {
        public string CertificateSerialNumber { get; set; } = null!;

        public string OrganizationUid { get; set; } = null!;

        public string PkiKeyId { get; set; } = null!;

        public string CertificateData { get; set; } = null!;

        public string WrappedKey { get; set; } = null!;

        public DateTime certificateStartDate { get; set; }

        public DateTime certificateEndDate { get; set; }

        public string CertificateStatus { get; set; } = null!;

        public string? Remarks { get; set; }

        public DateTime creationDate { get; set; }

        public DateTime? updatedDate { get; set; }

        public string? CertificateType { get; set; }

        public string? TransactionReferenceId { get; set; }
    }
}
