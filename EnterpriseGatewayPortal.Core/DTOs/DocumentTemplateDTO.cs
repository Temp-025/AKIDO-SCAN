using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DocumentTemplateDTO
    {
        public string TemplateName { get; set; }

        public string OrganizationUid { get; set; }

        public string Email { get; set; }

        public string Suid { get; set; }

        public string Status { get; set; }

        public string EdmsId { get; set; }

        public string DocumentName { get; set; }

        public string AdvancedSettings { get; set; }

        public string DaysToComplete { get; set; }

        public string NumberOfSignatures { get; set; }

        public bool AllSigRequired { get; set; }

        public bool PublishGlobally { get; set; }

        public bool SequentialSigning { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string Model { get; set; }

        public virtual string _id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    
    }
}
