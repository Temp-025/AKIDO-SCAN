using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class OrganizationWrappedKey
{
    public string CertificateSerialNumber { get; set; } = null!;

    public string? WrappedKey { get; set; }
}
