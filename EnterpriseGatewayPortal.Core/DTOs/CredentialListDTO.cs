using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class CredentialListDTO
    {

        public int Id { get; set; }
        public string? CredentialName { get; set; }
        public string? CredentialId { get; set; }

        public string? CredentialUId { get; set; }
        public string? VerificationDocType { get; set; }
        public List<DataAttribute> DataAttributes { get; set; }
        public string? AuthenticationScheme { get; set; }
        public string? CategoryId { get; set; }
        public string? OrganizationId { get; set; }
        public string? ServiceDetails { get; set; }
        public string? Status { get; set; }

        public string? Logo {  get; set; }

        public string? DisplayName {  get; set; }

        public DateTime CreatedDate { get; set; }
        public string? trustUrl { get; set; }
        public int validity { get; set; }
        public List<int> categories { get; set; } = new List<int>();


    }
    public class DataAttribute
    {
        public string? Attribute { get; set; }
        public int DataType { get; set; }

        public string? DisplayName {  get; set; }
    }
}
