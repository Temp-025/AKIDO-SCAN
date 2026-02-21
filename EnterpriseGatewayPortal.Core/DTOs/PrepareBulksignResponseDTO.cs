using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class PrepareBulksignResponseDTO
    {
        public string CorelationId { get; set; }

        public string Suid { get; set; }

        public string Email { get; set; }

        public string OrganizationId { get; set; }

        public string CallBackUrl { get; set; }

        public string SourcePath { get; set; }

        public string DestinationPath { get; set; }

        public placeHolderCoordinates PlaceHolderCoordinates { get; set; }

        public esealplaceHolderCoordinates EsealPlaceHolderCoordinates { get; set; }

        public QrCodePlaceHolderCoordinates QrCodePlaceHolderCoordinates { get; set; }

        public bool QrCodeRequired { get; set; }

        public string AgentUrl { get; set; }

        public int SignatureTemplateId { get; set; }

        public int EsealSignatureTemplateId { get; set; }
    }

    public class QrCodePlaceHolderCoordinates
    {
        public string pageNumber { get; set; }

        public string signatureXaxis { get; set; }

        public string signatureYaxis { get; set; }
    }
}
