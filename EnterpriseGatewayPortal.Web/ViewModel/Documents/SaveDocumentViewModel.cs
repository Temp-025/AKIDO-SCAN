using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.Documents
{
    public class SaveDocumentViewModel
    {
       
       

        public string? fileName { get; set; }

        public string? settingConfig { get; set; }


        //public cordinates SignCords { get; set; }
        //public Dictionary<string, string> signCords { get; set; } = new Dictionary<string, string>();
        public object signCords { get; set; }

        //public Dictionary<string, string> esealCords { get; set; } = new Dictionary<string, string>();
        //public cordinates EsealCords { get; set; }

        public object esealCords { get; set; }

        //public Dictionary<string, string> qrCords { get; set; } = new Dictionary<string, string>();
        //public cordinates QrCords { get; set; }
        public object qrCords { get; set; }
        public string actoken { get; set; }
        public bool qrCodeRequired { get; set; } 

        public IList<Roles> roleList { get; set; }

        public IList<string> emailList { get; set; }

        public int signatureTemplate { get; set; }

        public int esealSignatureTemplate { get; set; }

        public int rotation { get; set; }

        public Docdetails docdetails { get; set; }

        public string docData { get; set; }
        public string htmlSchema { get; set; }

    }
    

    public class Coordinates
    {
        public string? fieldName { get; set; }
        public float posX { get; set; }
        public float posY { get; set; }
        public int PageNumber { get; set; }
        public float width { get; set; }
        public float height { get; set; }
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

