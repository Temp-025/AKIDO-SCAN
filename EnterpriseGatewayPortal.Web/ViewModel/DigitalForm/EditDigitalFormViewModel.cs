using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EnterpriseGatewayPortal.Web.ViewModel.DigitalForm
{
    public class EditDigitalFormViewModel
    {
        public IList<SignatureTemplatesDTO> Templates { get; set; }
        public IFormFile File { get; set; }
        public string Model {  get; set; }
        public string EdmsId { get; set; }       
        public string TemplateId { get; set; }
        public string documentName { get; set; }
        public string TemplateName { get; set; }
        public string advancedSettings2 { get; set; }
        public AdvancedSettingsViewModel advancedSettings { get; set; }
        public string daysToComplete { get; set; }
        public string numberOfSignatures { get; set; }
        public bool allSigRequired { get; set; }
        public bool publishGlobally { get; set; }
        public bool sequentialSigning { get; set; }
        public string roles { get; set; }
        public string rolesConfig { get; set; }

        public class TemplateModel
        {
            public DocumentTemplateModel docConfig { get; set; }

            public List<TemplateRole> roles { get; set; }

            public string rolesConfig { get; set; }
        }

        public class AdvancedSettingsViewModel
        {
            public string SignatureSelectedName { get; set; }
            public string SignatureSelectedImage { get; set; }
            //public bool PreviewSignSelected { get; set; }
            //public bool PreviewESealSelected { get; set; }
            public string ESealSelectedImage { get; set; }
            public string ESealSelectedName { get; set; }
            public int SignatureSelected { get; set; }
            public int ESealSelected { get; set; }
        }

        public class TemplateRole
        {
            public string email { get; set; }

            public string name { get; set; }

            public string description { get; set; }
        }
        public class RoleDetails
        {
            public string roleId { get; set; }

            public string email { get; set; }

            public TemplateRole role { get; set; }

            public string annotationsList { get; set; }

            public placeHolderCoordinates placeHolderCoordinates { get; set; }

            public esealplaceHolderCoordinates esealPlaceHolderCoordinates { get; set; }
        }
    }
}
