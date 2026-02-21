using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IDocumentService
    {
        Task<ServiceResult> DeleteDocumentByIdListAsync(List<string> idList);
        Task<ServiceResult> DownloadSignedDocumentAsync(string fileID);
        Task<ServiceResult> GetDocumentDetaildByIdAsync(string id);
        Task<ServiceResult> GetDocumentDisplayDetaildByIdAsync(string id);
        Task<ServiceResult> GetDraftDocumentListAsync(UserDTO userDTO);
        Task<ServiceResult> SaveNewDocumentAsync(SaveNewDocumentDTO document, UserDTO userDetails);
        Task<ServiceResult> SendSigningRequestAsync(SigningRequestDTO signingRequest, UserDTO userDTO, string accessToken);

        Task<ServiceResult> GetInitialPreviewImgAsync(string token);

        Task<ServiceResult> GetSignaturePreviewAsync(UserDTO user);
        Task<ServiceResult> DeclineDocumentSigningAsync(string tempId, string suid, string comment);
        Task<ServiceResult> RecallDocumentToSignAsync(string docId);
    }
}
