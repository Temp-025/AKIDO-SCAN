using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class GetServicesDTO
    {
        public SubscriberServices? SubscriberServices { get; set; }
        public List<SpocServices>? SpocServices { get; set; }
        public List<OrgList>? OrgList { get; set; }
    }

    public class SubscriberServices
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceDisplayName { get; set; }
        public string? Status { get; set; }
        public bool Pricingslabapplicable { get; set; }
    }

    public class SpocServices
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceDisplayName { get; set; }
        public string? Status { get; set; }
        public bool Pricingslabapplicable { get; set; }
    }

    public class RateCard
    {
        public double? Amount { get; set; }
        public double? Tax { get; set; }
        public double? AggregatorFee { get; set; }
        public double? Discount { get; set; }
        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
    }

    public class OrgList
    {
        public string? CertificateStatus { get; set; }
        public string? CertificateStartDate { get; set; }
        public string? CertificateEndDate { get; set; }
        public string? OrganizationUid { get; set; }
        public string? OrgName { get; set; }
        public string? LastPayment { get; set; }
        public bool PostPaid { get; set; }
        public RateCard? RateCard { get; set; }
    }


}
