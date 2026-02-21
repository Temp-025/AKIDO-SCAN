using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class MakerChecker
{
    public int Id { get; set; }

    public string OperationType { get; set; } = null!;

    public string? OperationPriority { get; set; }

    public int ActivityId { get; set; }

    public string RequestData { get; set; } = null!;

    public int MakerId { get; set; }

    public int MakerRoleId { get; set; }

    public string State { get; set; } = null!;

    public string? Comment { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual Activity Activity { get; set; } = null!;

    public virtual User Maker { get; set; } = null!;
}
