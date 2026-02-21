

namespace EnterpriseGatewayPortal.Web.ViewModel.ESealRegistration
{
    public class BaseESealRegistrationViewModel
    {
        public BaseESealRegistrationViewModel()
        {
            DocumentListCheckbox = new List<DocumentListItem>()
            {
                new DocumentListItem{Id =1, DisplayName = "Authorized Letter for Signatories", IsSelected =false},
                new DocumentListItem{Id =2, DisplayName = "E-seal Image", IsSelected =false},
                new DocumentListItem{Id =3, DisplayName = "Incorporation", IsSelected =false},
                new DocumentListItem{Id =4, DisplayName = "Tax", IsSelected =false},
                new DocumentListItem{Id =5, DisplayName = "Additional Legal Document", IsSelected =false}
            };

            //Countries = new List<SelectListItem>()
            //{
            //    new SelectListItem {Text = "India", Value = "1"},
            //    new SelectListItem {Text = "Uganda", Value = "2"}
            //};
            Countries = new List<string>() { "Uganda" };
        }

        public List<DocumentListItem> DocumentListCheckbox { get; set; }

        public List<string> Countries { set; get; }
    }
}
