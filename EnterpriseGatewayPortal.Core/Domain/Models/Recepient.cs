using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class Recepient
{
    public int Id { get; set; }

    public string? Recepientid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public string? Suid { get; set; }

    public string? Email { get; set; }

    public string? Name { get; set; }

    public int? Order { get; set; }

    public bool? Decline { get; set; }

    public string? Declineremark { get; set; }

    public string? Status { get; set; }

    public string? Tempid { get; set; }

    public DateTime? Signingreqtime { get; set; }

    public DateTime? Signingcompletetime { get; set; }

    public bool? Takenaction { get; set; }

    public bool? Hasdelegation { get; set; }

    public string? Delegationid { get; set; }

    public string? Correlationid { get; set; }

    public string? Accesstoken { get; set; }

    public string? Organizationname { get; set; }

    public string? Organizationid { get; set; }

    public string? Accounttype { get; set; }

    public string? Esealorgid { get; set; }

    public string? Alternatesignatories { get; set; }

    public string? Signedby { get; set; }

    public string? Referredby { get; set; }

    public string? Referredto { get; set; }

    public bool? Allowcomments { get; set; }

    public bool? Signaturemandatory { get; set; }

    public virtual Document? Temp { get; set; }
}
