using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseGatewayPortal.Core.Domain.Models;
using Newtonsoft.Json;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class BeneficiaryResponseDTO
    {
        //public Beneficiary benificiaries { get; set; }
        public BeneficiaryDTO benificiaries { get; set; }
        public List<BeneficiaryValidityDTO> beneficiaryValidity { get; set; }
    }
    public class BeneficiaryDTO
    {
        public int id { get; set; }
        public string sponsorDigitalId { get; set; }
        public string sponsorName { get; set; }
        public string sponsorType { get; set; }
        public int sponsorPaymentPriorityLevel { get; set; }
        public string sponsorExternalId { get; set; }
        public string beneficiaryDigitalId { get; set; }
        public string beneficiaryType { get; set; }
        public string beneficiaryName { get; set; }
        public string beneficiaryNin { get; set; }
        public string beneficiaryPassport { get; set; }
        public string beneficiaryMobileNumber { get; set; }
        public string beneficiaryOfficeEmail { get; set; }
        public string beneficiaryUgPassEmail { get; set; }
        public bool beneficiaryConsentAcquired { get; set; }
        public string signaturePhoto { get; set; }
        public string designation { get; set; }
        public string status { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
    }

    public class BeneficiariesResponseDtos
    {
        public BeneficiaryDTO beneficiaries { get; set; }
        public List<BeneficiaryValidityDTO> beneficiaryValidity { get; set; }
    }

}
