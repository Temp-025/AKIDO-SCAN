using DocumentFormat.OpenXml.Wordprocessing;

namespace EnterpriseGatewayPortal.Web.ViewModel.DigitalForm
{
    public class SigningViewModel
    {
        public string Idp_Token { get; set; }
        public string OrgUId { get; set; }
        public string TemplateId { get; set; }
        public string AccToken { get; set; }
        public bool isEseal { get; set; } = false;
        public IFormFile File { get; set; }
        public string FormFieldData { get; set; }
    }

    public class FormField
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Value { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Nationality { get; set; }
    }
}
