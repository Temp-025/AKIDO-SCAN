using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class AdminLog
{
    public int Id { get; set; }

    public string? Identifier { get; set; }

    public string? Identifieremail { get; set; }

    public string? Modulename { get; set; }

    public string? Servicename { get; set; }

    public string? Activityname { get; set; }

    public string? Username { get; set; }

    public string? Datatransformation { get; set; }

    public string? Logmessagetype { get; set; }

    public string? Logmessage { get; set; }

    public string? Checksum { get; set; }

    public DateTime? Timestamp { get; set; }
}
