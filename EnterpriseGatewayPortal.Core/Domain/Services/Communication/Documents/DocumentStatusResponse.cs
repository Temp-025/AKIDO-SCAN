namespace EnterpriseGatewayPortal.Core.Domain.Services.Communication.Documents
{
    public class DocumentStatusResponse
    {
        public OwnDocumentStatusResponse ownDocumentStatus { get; set; }
        public OtherDocumentStatusResponse otherDocumentStatus { get; set; }
    }
}
