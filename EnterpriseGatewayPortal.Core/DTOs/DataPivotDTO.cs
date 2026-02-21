using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public partial class DataPivot
    {
        public int Id { get; set; }
        public string DataPivotUid { get; set; }
        public string Name { get; set; }
        public string AuthScheme { get; set; }
        public int ScopeId { get; set; }
        public string Description { get; set; }
        public string OrgnizationId { get; set; }
        public string AttributeConfiguration { get; set; }
        public string ServiceConfiguration { get; set; }
        public string PublicKeyCert { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Status { get; set; }

        public virtual Scope Scope { get; set; }
    }
    public class ServiceConfiguration
    {
        public string Serviceurl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }


    }

    public partial class Scope
    {
        public Scope()
        {
            DataPivots = new HashSet<DataPivot>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool UserConsent { get; set; }
        public bool DefaultScope { get; set; }
        public bool MetadataPublish { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Status { get; set; }
        public string ClaimsList { get; set; }
        public bool IsClaimsPresent { get; set; }

        public virtual ICollection<DataPivot> DataPivots { get; set; }
    }

    
}
