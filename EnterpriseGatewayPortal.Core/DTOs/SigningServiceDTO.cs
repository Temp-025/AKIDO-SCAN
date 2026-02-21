using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SigningServiceDTO
    {
        public string accountType { get; set; }

        public string accountId { get; set; }

        public string documentType { get; set; }

        public string subscriberUniqueId { get; set; }

        // public string subscriberFullName { get; set; }

        public string organizationUid { get; set; } = null;

        public string callbackURL { get; set; }

        public placeHolderCoordinates placeHolderCoordinates { get; set; }


        public esealplaceHolderCoordinates esealPlaceHolderCoordinates { get; set; }

        public bool deligationSign { get; set; }
        public string recipientName { get; set; }
        public string recipientEncryptedString { get; set; }
    }

    public class placeHolderCoordinates
    {
        public string pageNumber { get; set; }

        public string signatureXaxis { get; set; }

        public string signatureYaxis { get; set; }
        public string imgWidth { get; set; }

        public string imgHeight { get; set; }
    }

    public class esealplaceHolderCoordinates
    {
        public string pageNumber { get; set; }

        public string signatureXaxis { get; set; }

        public string signatureYaxis { get; set; }
        public string imgWidth { get; set; }

        public string imgHeight { get; set; }
    }

    public class qrPlaceHolderCoordinates
    {
        public string pageNumber { get; set; }

        public string signatureXaxis { get; set; }

        public string signatureYaxis { get; set; }
        public string imgWidth { get; set; }

        public string imgHeight { get; set; }

    }
}
