using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class BeneficiariesSendDTO
    {
        public string sponsorDigitalId { get; set; }
        public string sponsorName { get; set; }
        public string sponsorType { get; set; }
        public string? sponsorExternalId { get; set; }
        public string? beneficiaryDigitalId { get; set; }
        public string beneficiaryType { get; set; }
        public string beneficiaryName { get; set; }
        public string beneficiaryNin { get; set; }
        public string beneficiaryPassport { get; set; }
        public string beneficiaryMobileNumber { get; set; }
        public string beneficiaryOfficeEmail { get; set; }
        public string beneficiaryUgpassEmail { get; set; }
        public string signaturePhoto { get; set; }
        public string designation { get; set; }
        public List<BeneficiaryValidityDTO> BeneficiaryValidities { get; set; }
    }

    public class BeneficiaryValidityDTO
    {
        public int id { get; set; }
        public int? beneficiaryId { get; set; }
        public int privilegeServiceId { get; set; }
        public bool validityApplicable { get; set; }
        public DateTime? validFrom { get; set; }
        public DateTime? validUpTo { get; set; }
        public string privilageStatus { get; set; }
        public DateTime? createdOn { get; set; }
        public DateTime? updatedOn { get; set; }
        public string status { get; set; }
    }
}
