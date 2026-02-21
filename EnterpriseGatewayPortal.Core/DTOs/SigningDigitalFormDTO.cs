using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SigningDigitalFormDTO
    {
        public IFormFile File { get; set; }
        public string Idp_Token { get; set; }
        public string OrgUId { get; set; }
        public string TemplateId { get; set; }
        public string AccToken { get; set; }
        public string FormFieldData { get; set; }

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
}
