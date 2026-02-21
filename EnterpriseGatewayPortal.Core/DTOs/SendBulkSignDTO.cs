namespace EnterpriseGatewayPortal.Core.DTOs
{
	public class SendBulkSignDTO
	{
        public string UgpassEmailId { get; set; }
        public string CorrelationId { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public string AccessToken { get; set; }
        public PlaceHolderCoordinates placeHolderCoordinates { get; set; }
        public PlaceHolderCoordinates esealPlaceHolderCoordinates { get; set; }

	}

	public class SignatureRequestModel
	{
		public string SourcePath { get; set; } // Path where documents to be signed are stored
		public string DestinationPath { get; set; } // Path where processed documents are saved
		public string Id { get; set; } // UgPass email ID
		public string CorrelationId { get; set; } // Unique GUID value for transaction status
		public PlaceHolderCoordinates PlaceHolderCoordinates { get; set; } // Visible signature coordinates
		public PlaceHolderCoordinates EsealPlaceHolderCoordinates { get; set; } // Eseal signature coordinates
	}
}
