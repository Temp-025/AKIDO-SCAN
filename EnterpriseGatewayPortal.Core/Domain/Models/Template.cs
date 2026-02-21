using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class Template
{
    public int Id { get; set; }

    public string Templateid { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public string? Templatename { get; set; }

    public string? Documentname { get; set; }

    public string? Annotations { get; set; }

    public string? Esealannotations { get; set; }

    public string? Qrcodeannotations { get; set; }

    public bool? Qrcoderequired { get; set; }

    public string? Settingconfig { get; set; }

    public int? Signaturetemplate { get; set; }

    public int? Esealsignaturetemplate { get; set; }

    public string? Status { get; set; }

    public string? Edmsid { get; set; }

    public string? Createdby { get; set; }

    public string? Updatedby { get; set; }

    public string? Rolelist { get; set; }

    public string? Emaillist { get; set; }

    public byte[]? Templatefile { get; set; }

    public string? Htmlschema { get; set; }

    public virtual ICollection<Subscriberorgtemplate> Subscriberorgtemplates { get; set; } = new List<Subscriberorgtemplate>();
}
