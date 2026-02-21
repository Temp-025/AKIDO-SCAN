using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class Subscriberorgtemplate
{
    public int Id { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public string? Suid { get; set; }

    public string? Organizationid { get; set; }

    public string? Templateid { get; set; }

    public virtual Template? Template { get; set; }
}
