using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Uuid { get; set; }

    public string Name { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string? Email { get; set; }

    public bool Gender { get; set; }

    public int? RoleId { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? LastBadLoginTime { get; set; }

    public int? BadPasswordCnt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string ModifiedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? Suid { get; set; }

    public virtual ICollection<MakerChecker> MakerCheckers { get; set; } = new List<MakerChecker>();

    public virtual Role? Role { get; set; }
}
