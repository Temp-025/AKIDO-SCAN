using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IDigitalFormService
    {
        Task<ServiceResult> GetDocumentTemplateListAsync(string token);
        Task<ServiceResult> GetDocumentTemplatePublishListAsync(string token);
        Task<ServiceResult> GetDocumentTemplatePublishGlobalListAsync(string token);
        Task<ServiceResult> GetSignatureTemplateList(string token);
        Task<ServiceResult> GetResponseListCount(string templateId, string token);
        //Task<ServiceResult> GetResponsesList(string templateId, string token);
        Task<ServiceResult> GetCsvResponseSheet(string templateId, string token);
        Task<ServiceResult> GetDocTemplateById(string templateId, string token);
        Task<ServiceResult> GetPreviewTemplateAsync(string edmsId, string token);
        Task<ServiceResult> GetDigitalFormFilldataAsync(string Suid, string token);
        Task<ServiceResult> UpdateTemplateStatusAsync(string templateId,string action,string token);
        Task<ServiceResult> SaveNewDocTemplate(SaveNewDocumentTemplateDTO SaveNewDocumentTemplateDTO, string token);
        Task<ServiceResult> UpdateDocTemplate(UpdateDocumentTemplateDTO updateDocumentTemplateDTO, string token);
        Task<ServiceResult> SaveNewDigitalFormRespons(SigningDigitalFormDTO SigningDigitalFormDTO, string token);
    }
}
