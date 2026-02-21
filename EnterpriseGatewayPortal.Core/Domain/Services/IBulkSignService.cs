using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IBulkSignService
    {
        Task<ServiceResult> GetBulkSigDataListAsync(string token);
        Task<ServiceResult> GetReceivedBulkSignList(string token);
        Task<ServiceResult> GetSentBulkSignListAsync(string token);
        Task<ServiceResult> GetDocumentDetails(string correlationId, string token);
        Task<byte[]> DownloadAsync(DocumentDownloadDTO documentDownloadDTO);
        Task<ServiceResult> GetBulkSignRequest(string Id, string token);
        Task<ServiceResult> BulkSignAsync(SignDTO signDTO);
        Task<ServiceResult> SendFiles(List<IFormFile> files, string organizationUid,string uploadKey);
        Task<ServiceResult> SaveRequest(string id, string name, string token);
        Task<ServiceResult> ChangePath(UpdatePathDTO pathDTO, string token);
        Task<ServiceResult> UpdateStatus(string correlationId, bool forSigner, string token);
        Task<ServiceResult> UpdateDocumentStatus(string corelationId);
        Task<ServiceResult> SaveRequestByPreparator(string id, string name, string email, string token);
        Task<ServiceResult> getFiles(string Path);
        Task<ServiceResult> getFilesList(FilesPathDTO model);
        Task<ServiceResult> GetFileConfiguration(string token);
    }
}
