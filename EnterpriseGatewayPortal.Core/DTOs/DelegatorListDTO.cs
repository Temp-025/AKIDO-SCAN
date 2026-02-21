using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DelegatorListDTO
    {
        public string delegatorSuid { get; set; }
        public string delegatorName { get; set; }
        public string delegatorEmail { get; set; }
        public string organizationId { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public string documentType { get; set; }
        public string delegationStatus { get; set; }
        public string consentData { get; set; }
        public string delegatorConsentDataSignature { get; set; }
        public List<DelegateeDTO> delegatees { get; set; }
        public string createdBy { get; set; }
        public object updatedBy { get; set; }
        public string _id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class DelegateeDTO
    {
        public string delegationId { get; set; }
        public string delegateeSuid { get; set; }
        public string organizationId { get; set; }
        public string delegateeEmail { get; set; }
        public string fullName { get; set; }
        public string thumbnail { get; set; }
        public string consentStatus { get; set; }
        public DateTime consentDateTime { get; set; }
        public object delegateConsentDataSignature { get; set; }
        public string createdBy { get; set; }
        public object updatedBy { get; set; }
        public string _id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    
}
