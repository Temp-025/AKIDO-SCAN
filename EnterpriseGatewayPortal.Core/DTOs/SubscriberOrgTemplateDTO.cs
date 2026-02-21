using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SubscriberOrgTemplateDTO
    {
        public string Suid { get; set; }

        public string OrganizationId { get; set; }

        //public string PublishStatus { get; set; }

        public string TemplateId { get; set; }

        public IList<Template>? TemplateDetails { get; set; }

        public virtual string _id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
