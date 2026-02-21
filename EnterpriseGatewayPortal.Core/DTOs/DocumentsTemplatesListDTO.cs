using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DocumentsTemplatesListDTO
    {
        public string? TemplateName { get; set; }

        public string? TemplateId { get; set; }

        public string? DocumentName { get; set; }

        public string? SettingConfig { get; set; }

        public string? SignCords { get; set; }

        public string? EsealCords { get; set; }

        public string? QrCords { get; set; }

        public bool QrCodeRequired { get; set; }

        public IList<Roles> RoleList { get; set; }

        public IList<string> EmailList { get; set; }

        public int SignatureTemplate { get; set; }

        public int EsealSignatureTemplate { get; set; }

        public int Rotation { get; set; }

        public string _id { get; set; }

        public string edmsId { get; set; }

        public string? status { get; set; }

        public string? createdBy { get; set;}

        public string Annotations { get; set; }

        public string EsealAnnotations { get; set; }

        public string QrCodeAnnotations { get; set; }

    }

    

}
