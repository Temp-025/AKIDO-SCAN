using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class QRCodeSigningSendDTO
    {
        public List<string> publicData { get; set; }
        public List<string> privateData { get; set; }
        public string document { get; set; }
        public bool? portrait { get; set; }
        public string credentialId { get; set; }
        [JsonProperty("qrCoordinates")]
        public QrCodeCoordinates qrPlaceHolderCoordinates { get; set; }

    }

    public class QrCodeCoordinates
    {
        public string x { get; set; }
        public string y { get; set; }
        public string pageNumber { get; set; }
    }
}
