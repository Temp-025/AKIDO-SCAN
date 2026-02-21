using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SaveNewTemplateDTO
    {
        public IFormFile file { get; set; }

        public string model { get; set; }

    }

    public class TemplateModel
    {
        public string? templateName { get; set; }

        public string? templateId { get; set; }

        public string? documentName { get; set; }

        public string? settingConfig { get; set; }

        public Signcoordinates signCords { get; set; }

        public Esealcoordinates esealCords { get; set; }

        public QrCoordinates qrCords { get; set; }

        public bool qrCodeRequired { get; set; }

        public IList<Roles> roleList { get; set; }

        public IList<string> emailList { get; set; }

        public int signatureTemplate { get; set; }

        public int esealSignatureTemplate { get; set; }

        public int rotation { get; set; }

    }

    public class Roles
    {
        public int order { get; set; }

        public string? role { get; set; }

        public bool eseal { get; set; }
    }

    public class Signcoordinates
    {
        public string? coordinates { get; set; }
    }

    public class Esealcoordinates
    {
        public string? coordinates { get; set; }
    }

    public class QrCoordinates
    {
        public string? coordinates { get; set; }
    }
}
