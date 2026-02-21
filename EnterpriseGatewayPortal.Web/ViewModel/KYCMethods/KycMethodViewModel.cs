using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Web.ViewModel.KYCMethods
{
    public class KycMethodViewModel
    {
        public string orgName { get; set; }
        public List<KycMethodItem> Methods { get; set; } = new List<KycMethodItem>();

        public VerificationMethodsStatsItem Stats = new VerificationMethodsStatsItem();
        public string filterStatus { get; set; }
    }

    public class ActiveMethodsViewModel
    {
        public List<KycMethodItem> ActiveMethods { get; set; } = new List<KycMethodItem>();
    }

    public class RequestNewMethodsViewModel
    {
        public List<KycMethodItem> RequestNewMethods { get; set; } = new List<KycMethodItem>();
    }

    public class AllRequestedMethodsViewModel
    {
        public List<KycMethodItem> AllRequestedMethods { get; set; } = new List<KycMethodItem>();
    }

    public class KycMethodItem
    {
        public string MethodCode { get; set; }

        public string MethodUid { get; set; }

        public string MethodType { get; set; }

        public string MethodName { get; set; }

        public decimal Pricing { get; set; }

        public string ProcessingTime { get; set; }

        public string ConfidenceThreshold { get; set; }

        public List<string> TargetSegments { get; set; }

        public List<string> AttributesList { get; set; }

        public List<string> MandatoryAttributes { get; set; }

        public List<string> OptionalAttributes { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string RequestStatus { get; set; }

        public bool IsRequested { get; set; }

        public string RequestedDate { get; set; }

        public string ModifiedDate { get; set; }
    }

    public class VerificationMethodsStatsItem
    {
        public int Total { get; set; }
        public int Active { get; set; }
        public int Pending { get; set; }
        public int Rejected { get; set; }
    }

    public class AttributesNameDTO
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
