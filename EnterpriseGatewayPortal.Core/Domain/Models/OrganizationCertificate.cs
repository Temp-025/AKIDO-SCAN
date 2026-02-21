using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class OrganizationCertificate
{
    public string CertificateSerialNumber { get; set; } = null!;

    public string OrganizationUid { get; set; } = null!;

    public string PkiKeyId { get; set; } = null!;

    public string CertificateData { get; set; } = null!;

    public string WrappedKey { get; set; } = null!;

    public DateTime CertificateIssueDate { get; set; }

    public DateTime CerificateExpiryDate { get; set; }

    public string CertificateStatus { get; set; } = null!;

    public string? Remarks { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CertificateType { get; set; }

    public string? TransactionReferenceId { get; set; }

    public virtual OrganizationDetail OrganizationU { get; set; } = null!;
}
