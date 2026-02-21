using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface ILocalBulkSignService
    {
        Task<ServiceResult> BulkSignCallBackAsync(BulkSignCallBackDTO bulkSignCallBackDTO);
        Task<ServiceResult> DownloadSignedDocumentAsync(string fileName, string corelationId);
        Task<ServiceResult> FailBulkSigningRequestAsync(string CorrelationId);
        Task<ServiceResult> GetBulkSigDataAsync(string corelationId);
        Task<ServiceResult> GetBulkSigDataListAsync(UserDTO userDTO);
        Task<ServiceResult> GetBulkSigDataListByTemplateIdAsync(string templateID);
        Task<ServiceResult> GetBulkSignerListAsync(string OrgId);
        Task<ServiceResult> GetReceivedBulkSignListAsync(UserDTO user);
        Task<ServiceResult> GetSentBulkSignListAsync(UserDTO user);
        Task<ServiceResult> PrepareBulkSigningRequestAsync(string templateId, UserDTO userDTO);
        //Task<ServiceResult> SaveBulkSigningRequestAsync(string templateId, string transactionName, UserDTO userDTO);
        Task<ServiceResult> SaveBulkSigningRequestAsync(string templateId, string transactionName, UserDTO userDTO, string? signerEmail = null);
        Task<ServiceResult> UpdateBulkSigningSourceDestinationAsync(UpdatePathDTO dto);
        Task<ServiceResult> UpdateBulkSigningStatusAsync(string corelationId, bool forSigner);

        Task<ServiceResult> BulkSignStatusAsync(string correlationId, string accessToken);

        Task<ServiceResult> SendBulkSignRequestAsync(SendBulkSignDTO signDTO);
        Task<ServiceResult> GetFilesListFromPath(FilesPathDTO model);
        Task<ServiceResult> UpdateBulkSignResultAsync(BulkSignCallBackDTO bulkSignCallBackDTO);
        //Task<ServiceResult> CompletedBulkSigningRequestAsync(string CorrelationId);
        Task<ServiceResult> CompletedBulkSigningRequestAsync(string CorrelationId, BulkSignCallBackDTO bulkSignCallBackDTO);


    }
}
