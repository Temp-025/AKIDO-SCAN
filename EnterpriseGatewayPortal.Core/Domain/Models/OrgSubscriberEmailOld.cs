using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class OrgSubscriberEmailOld
{
    public int OrgContactsId { get; set; }

    public string? OrganizationUid { get; set; }

    public string? SubEmailList { get; set; }

    public bool? IsEsealSignatory { get; set; }

    public bool? IsEsealPreparatory { get; set; }

    public bool? IsOrgSignatory { get; set; }

    public string? Designation { get; set; }

    public string? SignaturePhoto { get; set; }

    public sbyte? IsTemplate { get; set; }

    public sbyte? IsBulkSign { get; set; }

    public string? SubscriberUid { get; set; }

    public virtual OrganizationDetail? OrganizationU { get; set; }
}
