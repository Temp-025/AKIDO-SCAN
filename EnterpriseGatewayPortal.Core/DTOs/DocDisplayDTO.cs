namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DocDisplayDTO
    {
        public string DocumentName { get; set; }

        public string OwnerEmail { get; set; }

        public string OwnerName { get; set; }

        public string Status { get; set; }

        public int SignatoryCount { get; set; }

        public VerifySigningRequestResponse VerificationDetails { get; set; }

        public string Document { get; set; }

        //public IList<UserDetails> SignerDetails { get; set; } = new List<UserDetails>();
    }
    public class UserDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class VerifySignedDocumentApiResponse
    {
        public IList<signatureDeatils> signatureVerificationDetails { get; set; }
    }

    public class VerifySigningRequestResponse
    {
        public IList<signatureDeatils> recpList { get; set; }

        public int signCount { get; set; }
    }

    public class signatureDeatils
    {
        public string signedBy { get; set; }
        public DateTime signedTime { get; set; }
        public DateTime validationTime { get; set; }
        public bool signatureValid { get; set; }
    }

    public class VerifySignedDocumentDTO
    {
        public string docData { get; set; }

        public string suid { get; set; }

        public string email { get; set; }
    }
}
