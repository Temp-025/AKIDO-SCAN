using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class Bulksign
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? TemplateId { get; set; }

    public string? TemplateName { get; set; }

    public string? OrganizationId { get; set; }

    public string? Suid { get; set; }

    public string? Transaction { get; set; }

    public string? SignatureAnnotations { get; set; }

    public string? EsealAnnotations { get; set; }

    public string? QrcodeAnnotations { get; set; }

    public string? SourcePath { get; set; }

    public string? SignedPath { get; set; }

    public string? Status { get; set; }

    public string? CorelationId { get; set; }

    public string? SignedBy { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? OwnerName { get; set; }

    public string? OwnerEmail { get; set; }

    public string? SignerEmail { get; set; }

    public string? Result { get; set; }
}
