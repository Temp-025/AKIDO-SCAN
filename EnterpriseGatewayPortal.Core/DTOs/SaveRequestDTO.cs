using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class PlaceHolderCoordinates
    {
        public string? pageNumber { get; set; }
        public string? signatureXaxis { get; set; }
        public string? signatureYaxis { get; set; }
    }

    public class SaveRequestDTO
    {
        public string corelationId { get; set; }
        public string suid { get; set; }
        public string email { get; set; }
        public string organizationId { get; set; }
        public string callBackUrl { get; set; }
        public string sourcePath { get; set; }
        public string destinationPath { get; set; }
        public PlaceHolderCoordinates placeHolderCoordinates { get; set; }
        public object eseadPlaceHolderCoordinates { get; set; }
        public object qrCodePlaceHolderCoordinates { get; set; }
        public bool qrCodeRequired { get; set; }
        public string agentUrl { get; set; }
        public int signatureTemplateId { get; set; }
        public int eseadSignatureTemplateId { get; set; }
    }
}
