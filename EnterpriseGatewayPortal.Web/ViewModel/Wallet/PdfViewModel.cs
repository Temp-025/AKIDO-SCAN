using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.Wallet
{
    public class PdfViewModel
    {
        public string CredentialId { get; set; }

        public string? DocumentType { get; set; }

        public string? CredentialName { get; set; }


        public string? VerificationDocType { get; set; }


        public List<DataAttribute> DataAttributes { get; set; }


        public string? AuthenticationScheme { get; set; }
        public string? CategoryId { get; set; }


        public string? Category { get; set; }
        public string? OrganizationId { get; set; }
        public string? ServiceDetails { get; set; }
        public string? Status { get; set; }
        public string? CredentialUId { get; set; }

        public string? DisplayName { get; set; }
        public string? Logo { get; set; }
        public byte[]? CredentialPdfReport { get; set; }
        public string? Email { get; set; }
        public int credential_localId { get; set; }
        public IFormFile File { get; set; }
    }
}
