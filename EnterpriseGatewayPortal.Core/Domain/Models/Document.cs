using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class Document
{
    public int Id { get; set; }

    public string DocumentId { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? FileId { get; set; }

    public string? DocumentName { get; set; }

    public string? OwnerId { get; set; }

    public string? OwnerEmail { get; set; }

    public string? OwnerName { get; set; }

    public string? DaysToComplete { get; set; }

    public bool? AutoReminders { get; set; }

    public string? RemindEvery { get; set; }

    public string? Status { get; set; }

    public DateTime? CreateTime { get; set; }

    public DateTime? CompleteTime { get; set; }

    public string? Annotations { get; set; }

    public string? EsealAnnotations { get; set; }

    public string? QrcodeAnnotations { get; set; }

    public DateTime? ExpireDate { get; set; }

    public string? EdmsId { get; set; }

    public string? Watermark { get; set; }

    public bool? IsDocumentBlocked { get; set; }

    public DateTime? DocumentBlockedTime { get; set; }

    public bool? DisableOrder { get; set; }

    public bool? AllowToAssignSomeone { get; set; }

    public bool? MultiSign { get; set; }

    public string? PendingSignList { get; set; }

    public string? CompleteSignList { get; set; }

    public int? RecepientCount { get; set; }

    public int? SignaturesRequiredCount { get; set; }

    public string? OrganizationName { get; set; }

    public string? OrganizationId { get; set; }

    public string? AccountType { get; set; }

    public bool? IsForm { get; set; }

    public string? HtmlSchema { get; set; }

    public virtual Filestorage? File { get; set; }

    public virtual ICollection<Recepient> Recepients { get; set; } = new List<Recepient>();
}
