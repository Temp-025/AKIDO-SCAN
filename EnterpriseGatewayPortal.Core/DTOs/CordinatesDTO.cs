namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class CordinatesDTO
    {


        public string EdmsUrl { get; set; }

        public bool QrCodeRequired { get; set; }
        public Cordinates signature { get; set; }

        public Cordinates eseal { get; set; }
    }

    public class Cordinates
    {
        public string fieldName { get; set; }
        public float posX { get; set; }
        public float posY { get; set; }
        public int PageNumber { get; set; }
        public float width { get; set; }
        public float height { get; set; }
    }
}

