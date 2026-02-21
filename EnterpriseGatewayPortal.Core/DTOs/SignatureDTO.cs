namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SignatureDTO
    {
        public string documentType { get; set; }
        public string id { get; set; }
        public bool qrCodeRequired { get; set; }
        public string qrCodeData { get; set; }
        public placeHolderCoordinates placeHolderCoordinates { get; set; }
        public esealplaceHolderCoordinates esealPlaceHolderCoordinates { get; set; }
        public qrPlaceHolderCoordinates qrPlaceHolderCoordinates { get; set; }

    }
}
