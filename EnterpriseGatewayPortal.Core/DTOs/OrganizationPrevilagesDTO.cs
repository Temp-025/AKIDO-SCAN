using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class OrganizationPrevilagesDTO
    {
        public OrganisationPrivilegesDto OrganisationPrivileges { get; set; }
        public bool WalletCertificateStatus { get; set; }

        public List<string> Privileges { get; set; }

    }
    public class OrganisationPrivilegesDto
    {
        public int Id { get; set; }
        public string OrganizationId { get; set; }
        public bool EsealCertificate { get; set; }
        public bool DigitalEngagementServices { get; set; }
        public bool DigitalVaultCertificate { get; set; }
        public bool DataProvider { get; set; }
        public bool RelyingParty { get; set; }
        public bool DocumentIssuer { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
