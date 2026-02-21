namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class QrDecryptDTO
    {
        public string qr_code_format { get; set; }

        public QRData qr_code_data { get; set; }
    }

    public class QRData
    {
        public string document_id { get; set; }

        public string url { get; set; } = null;

        public CriticalData critical_data { get; set; } = null;

        public List<Signatories> signatories { get; set; }
    }

    public class CriticalData
    {
        public string entityName { get; set; }

        public string docSerialNo { get; set; }

        public bool faceRequired { get; set; } = false;
    }

    public class Signatories
    {
        public string name { get; set; }

        public string orgname { get; set; }
    }
}
