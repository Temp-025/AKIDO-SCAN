using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class Configuration
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }
}
