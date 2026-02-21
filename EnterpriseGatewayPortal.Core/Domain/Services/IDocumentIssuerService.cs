using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IDocumentIssuerService
    {
        Task<IEnumerable<SaveDocumentIssuerDTO>> GetAllDocIssuerListAsync();

        Task<ServiceResult> AddDocumentissuerAsync(SaveDocumentIssuerDTO document);

        Task<ServiceResult> UpdateDocuemntIssuerStatusAsync(UpdatedocumentIssuerStatusDTO updatedocumentIssuerStatus);

        Task<SaveDocumentIssuerDTO> GetDocIssuerDetailsByIdAsync(int docIssuerId);

        Task<ServiceResult> UpdateDocumentissuerAsync(SaveDocumentIssuerDTO document);
    }
}
