namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class VerificationModelDTO
    {

        public int DocumentID { get; set; }
        public string? RelyingpartyType { get; set; }
        public string? RelyingpartyName { get; set; }
        public string? RelyingpartyEmail { get; set; }
        public string? RelyingpartyUid { get; set; }

        public string? IssuerUid { get; set; }

        public string? IssuerOrgName { get; set; }

        public string? RequestedBy { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public string? Status { get; set; }

        public string? VerificationType { get; set; }

        public string? DocumentType { get; set; }
        public string? SUID { get; set; }

    }
}
