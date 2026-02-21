using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class SignatureTemplate
{
    public int Id { get; set; }

    public string TemplateId { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? Type { get; set; }

    public string? SamplePreview { get; set; }
}
