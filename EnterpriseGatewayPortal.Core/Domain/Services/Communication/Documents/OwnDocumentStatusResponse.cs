namespace EnterpriseGatewayPortal.Core.Domain.Services.Communication.Documents
{
    public class OwnDocumentStatusResponse
    {
        public long cnt_in_progress { get; set; }

        public long cnt_expired { get; set; }

        public long cnt_declined { get; set; }

        public long cnt_completed { get; set; }
    }
}
