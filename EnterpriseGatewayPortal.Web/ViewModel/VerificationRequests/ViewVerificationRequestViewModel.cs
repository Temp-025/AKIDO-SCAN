namespace EnterpriseGatewayPortal.Web.ViewModel.VerificationRequests
{
    public class ViewVerificationRequestViewModel
    {
        public int Id { get; set; }

        public int? DocumentId { get; set; }

        public string? IssuerOrgName { get; set; }

        public string? RelyingpartyUid { get; set; }

        public string? IssuerUid { get; set; }

        public string? VerifiedBy { get; set; }

        public string? RequestedBy { get; set; }

        public string? FileId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public string? Status { get; set; }

        public string? VerificationType { get; set; }

        public string? DocumentType { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
