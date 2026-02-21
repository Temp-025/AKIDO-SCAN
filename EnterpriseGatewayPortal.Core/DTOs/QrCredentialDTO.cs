using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class QrCredentialDTO
    {
        public int Id { get; set; }

        public string? credentialName { get; set; }

        public string? displayName { get; set; }

        public string? credentialId { get; set; }

        public string? credentialUId { get; set; }

        public string? remarks { get; set; }

        public string? verificationDocType { get; set; }

        public QrAttributesDTO dataAttributes { get; set; }

        public string? authenticationScheme { get; set; }

        public string? categoryId { get; set; }

        public string? organizationId { get; set; }

        public List<string> serviceDetails { get; set; }

        public string? credentialOffer { get; set; }

        public DateTime createdDate { get; set; }
        public bool portraitVerificationRequired { get; set; }

        public string? logo { get; set; }
        public string? status { get; set; }
    }
    public class QrAttributesDTO
    {
        public List<DataAttributesDTO> publicAttributes { get; set; }
        public List<DataAttributesDTO> privateAttributes { get; set; }
    }
    public class DataAttributesDTO
    {
        public string? displayName { get; set; }
        public string? attribute { get; set; }
        public int dataType { get; set; }
    }
}
