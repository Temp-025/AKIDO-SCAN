using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.DigitalForm
{
    public class DigitalFormFillViewModel
    {
        public string TemplateId { get; set; }
        public string EdmsId { get; set; }
        public string Nationality { get; set; }
        public string PrimaryIdentifier { get; set; }
        public string SecondaryIdentifier { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DigitalFormName { get; set; }

    }   
}
