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
    public interface IDocumentVerifyIssuerService
    {
        Task<ServiceResult> GetAllDocumentListAsync();
        Task<ServiceResult> SaveVerificationDetails(VerificationModelDTO verificationModelDto, IFormFile file);
       // Task<IEnumerable<DocumentVerifyListDTO>> GetAllVerificationDetailList();
        Task<ServiceResult> GetVerificationDetailListBySuidAndOrgUidAsync(string orgId, string suid);

        //Task<DocumentVerifyListDTO> GetVerificationDetailByIdAsync(int id);
        Task<ServiceResult> GetVerificationDetailByIdAsync(int id);
        Task<ServiceResult> GetAllIssuerOrgNamesListAsync();
        Task<ServiceResult> GetDocTypeListByIdAsync(string id);
        Task<ServiceResult> GetVerificationMethodListByDocTypeAsync(string docType, string id);
       // Task<ServiceResult> GetDownloadReportByFileId(string fileId);
        Task<DownloadVerifyDocumentDTO> GetDownloadReportByFileId(string fileId);
    }
}
