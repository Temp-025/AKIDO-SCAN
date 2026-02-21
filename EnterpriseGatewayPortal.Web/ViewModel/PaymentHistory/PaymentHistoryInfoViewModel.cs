using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.PaymentHistory
{
    public class PaymentHistoryInfoViewModel
    {
        [Display(Name = "Payment Channel")]
        public string PaymentChannel { get; set; }

        [Display(Name = "Total Amount")]
        public double TotalAmount { get; set; }

        [Display(Name = "Transaction Id")]
        public string TransactionReferenceId { get; set; }

        [Display(Name = "Acknowledgement Id")]
        public string AggregatorAcknowledgementId { get; set; }

        [Display(Name = "Created On")]
        public string CreatedOn { get; set; }

        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; }
    }
}
