using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class Recepients
    {
       
        public virtual string _id { get; set; }

        public DateTime CreatedAt { get; set; }

       
        public DateTime UpdatedAt { get; set; }

        public string Suid { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public bool Decline { get; set; }

        public string DeclineRemark { get; set; }

        public string Status { get; set; }

       
        public string Tempid { get; set; }

        public DateTime SigningReqTime { get; set; }

        
        public DateTime SigningCompleteTime { get; set; }

        public bool TakenAction { get; set; } = false;

        public bool HasDelegation { get; set; } = false;

        public string DelegationId { get; set; }

        public string CorrelationId { get; set; }

        public string AccessToken { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationId { get; set; }

        public string AccountType { get; set; }

        public string EsealOrgId { get; set; } = string.Empty;

        //new added
        public IList<User> AlternateSignatories { get; set; } = new List<User>();

        public string SignedBy { get; set; } = string.Empty;

        public string ReferredBy { get; set; } = string.Empty;

        public string ReferredTo { get; set; } = string.Empty;

        public bool AllowComments { get; set; }

        public bool SignatureMandatory { get; set; }
    }
}
