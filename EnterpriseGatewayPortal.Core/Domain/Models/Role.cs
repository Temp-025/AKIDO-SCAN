using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string? Hash { get; set; }

    public string Status { get; set; } = null!;

    public string? DisplayName { get; set; }

    public virtual ICollection<RoleActivity> RoleActivities { get; set; } = new List<RoleActivity>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
