using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class OrganizationDirector
{
    public int OrganizationDirectorsId { get; set; }

    public string? OrganizationUid { get; set; }

    public string? DirectorsEmails { get; set; }

    public virtual OrganizationDetail? OrganizationU { get; set; }
}
