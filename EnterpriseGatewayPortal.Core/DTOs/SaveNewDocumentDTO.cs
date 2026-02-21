using Microsoft.AspNetCore.Http;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SaveNewDocumentDTO
    {
        public IFormFile file { get; set; }

        public string model { get; set; }
    }
    public class Model
    {
        public string tempid { get; set; }
        //public string action { get; set; }
        public Docdetails docdetails { get; set; }
        public string docData { get; set; }
        public string fileName { get; set; }
        public Signcords signCords { get; set; }
        public QrCords QrCords { get; set; }
        public Esealcords esealCords { get; set; }
        //public string watermarkText { get; set; }
        public string actoken { get; set; }
        public bool qrCodeRequired { get; set; } = false;
        //public int rotation { get; set; }
        public string htmlSchema { get; set; }
    }

    public class Docdetails
    {
        public string ownerName { get; set; }
        public List<Receps> receps { get; set; }
        public string tempname { get; set; }
        public string daysToComplete { get; set; }
        public string annotations { get; set; }
        //public string noteToAll { get; set; }
        public string orgn_name { get; set; }
        public string watermark { get; set; }
        public DateTime expiredate { get; set; }
    }

    public class Signcords
    {
        public string coordinates { get; set; }
    }

    public class QrCords
    {
        public string coordinates { get; set; }
    }

    public class Esealcords
    {
        public string coordinates { get; set; }
    }

    public class Receps
    {
        public string index { get; set; }
        public int order { get; set; }
        public string email { get; set; }

        public string suid { get; set; }
        public string name { get; set; }
        public bool eseal { get; set; }
        public string orgUID { get; set; }
        public string orgName { get; set; }

    }
}
