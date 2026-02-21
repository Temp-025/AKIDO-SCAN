using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class OrganizationDocumentCheck
{
    public int OrganizationDocCheckId { get; set; }

    public string? OrganizationUid { get; set; }

    public string? DocumentCheckBox { get; set; }

    public virtual OrganizationDetail? OrganizationU { get; set; }
}
