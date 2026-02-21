using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DocumentSigningMyListDTO
    {
        public virtual string _id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string DocumentName { get; set; }

        public string OwnerID { get; set; }

        public string OwnerEmail { get; set; }

        public string OwnerName { get; set; }

        public string DaysToComplete { get; set; }

        public string AutoReminders { get; set; }

        public string RemindEvery { get; set; }

        public string Status { get; set; }

       
        public DateTime CreateTime { get; set; }

        
        public DateTime CompleteTime { get; set; }

        public string Annotations { get; set; }

        public string EsealAnnotations { get; set; }

        public string QrCodeAnnotations { get; set; }

        
        public DateTime ExpireDate { get; set; }

        public string EdmsId { get; set; }

        public IList<Recepients> Recepients { get; set; }

        public string Watermark { get; set; }

        public bool IsDocumentBlocked { get; set; } = false;

        
        public DateTime DocumentBlockedTime { get; set; }

        public bool DisableOrder { get; set; }

        public bool AllowToAssignSomeone { get; set; }

        public bool MultiSign { get; set; }

        public IList<User> PendingSignList { get; set; } = new List<User>();

        public IList<User> CompleteSignList { get; set; } = new List<User>();

        public int RecepientCount { get; set; }

        public int SignaturesRequiredCount { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationId { get; set; }

        public string AccountType { get; set; }
    }
}
