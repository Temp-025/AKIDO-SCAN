using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class FillFormDetailsDTO
    {
        public string SuID { get; set; }
        public string OnboardingMethod { get; set; }
        public SubscriberDataDto SubscriberData { get; set; }
        public string TemplateId { get; set; }
        public string ConsentId { get; set; }
        public string Acknowledgement { get; set; }
        public string SubscriberType { get; set; }
        public string OnboardingApprovalStatus { get; set; }
        public string CertStatus { get; set; }
        public string OnboardingPaymentStatus { get; set; }
        public string LevelOfAssurance { get; set; }
        public string SubscriberDocuments { get; set; }
    }
    public class SubscriberDataDto
    {
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string PrimaryIdentifier { get; set; }
        public string SecondaryIdentifier { get; set; }
        public string DocumentType { get; set; }
        public string DocumentCode { get; set; }
        public string OptionalData1 { get; set; }
        public string OptionalData2 { get; set; }
        public string DocumentNumber { get; set; }
        public string IssuingState { get; set; }
        public string PhotoVerificationPerc { get; set; }
        public string SubscriberSelfie { get; set; }
        public string SubscriberUniqueId { get; set; }
        public string GeoLocation { get; set; }
        public string Remarks { get; set; }
    }
}
