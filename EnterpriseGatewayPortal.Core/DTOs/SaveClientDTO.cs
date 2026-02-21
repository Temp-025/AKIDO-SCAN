using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SaveClientDTO
    {
        public int Id { get; set; }


        public string ApplicationType { get; set; }

       
        public string ApplicationName { get; set; }


        public string ApplicationUri { get; set; }

        
        public string RedirectUri { get; set; }

        public string GrantTypes { get; set; }

        public IEnumerable<string> GrantTypesList { get; set; }

        public string Scopes { get; set; }

       
        public IEnumerable<string> ScopesList { get; set; }


       
        public string LogoutUri { get; set; }


       
        public string OrganizationId { get; set; }


        public bool WithPkce { get; set; }

       
        public IFormFile Cert { get; set; }

        public string Base64Cert { get; set; }

        [Required]
        [Display(Name = "Authemtication Schema")]
        public string AuthSchemaId { get; set; }

        public List<SelectListItem> AuthSchemasList { get; set; }

    }
}
