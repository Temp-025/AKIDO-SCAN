using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class OrganizationCertificateLifeCycle
{
    public int OrganizationCertificateLifeCycleId { get; set; }

    public string OrganizationUid { get; set; } = null!;

    public string? CertificateSerialNumber { get; set; }

    public string CertificateStatus { get; set; } = null!;

    public string? RevocationReason { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CertificateType { get; set; }

    public virtual OrganizationDetail OrganizationU { get; set; } = null!;
}
