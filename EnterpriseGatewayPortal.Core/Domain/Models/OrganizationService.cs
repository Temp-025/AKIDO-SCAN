using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class OrganizationService
{
    public int OrganizationServiceId { get; set; }

    public string? OrganizationUid { get; set; }

    public string? ServiceName { get; set; }

    public virtual OrganizationDetail? OrganizationU { get; set; }
}
