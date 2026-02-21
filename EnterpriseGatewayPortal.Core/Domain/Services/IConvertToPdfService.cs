using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IConvertToPdfService
    {
        FileContentResult ConvertExcelToPdf(IFormFile file);
        FileContentResult? ConvertDocToPdf(IFormFile file);
        Task<ServiceResult> ConvertToPdf(IFormFile file);

        Task<ServiceResult> AddCommentsToPdf(CommentrequestDTO request);

        Task<ServiceResult> InitialWatermark(InitialWatermarkDTO request, Dictionary<string, IFormFile> imageFiles);
    }
}
