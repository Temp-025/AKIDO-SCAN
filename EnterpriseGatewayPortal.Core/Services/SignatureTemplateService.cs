//using EnterpriseGatewayPortal.Core.Domain.Models;
//using EnterpriseGatewayPortal.Core.Domain.Repositories;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using Microsoft.Extensions.Logging;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class SignatureTemplateService : ISignatureTemplateService
//    {
//        private readonly ILogger<SignatureTemplateService> _logger;
//        private readonly IUnitOfWork _unitOfWork;

//        public SignatureTemplateService(ILogger<SignatureTemplateService> logger, IUnitOfWork unitOfWork)
//        {
//            _logger = logger;
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<ServiceResult> GetAllSignatureTemplateListAsync()
//        {
//            try
//            {
//                _logger.LogInformation("GetAllSignatureTemplateList Called");
//                var list = await _unitOfWork.SignatureTemplate.GetAllSignatureTemplateAsync();
//                if (list == null)
//                {
//                    _logger.LogError("list is Empty");
//                    return new ServiceResult("list is Empty");
//                }
//                return new ServiceResult(true, "All Signature Template List recieved successfully", list);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"GetAllSignatureTemplateList Error: {ex.Message}");
//                return new ServiceResult("Signature Template list not found");
//            }
//        }

//        public async Task<ServiceResult> GetSignatureTemplateByIdAsync(int id)
//        {
//            try
//            {
//                _logger.LogInformation("GetSignatureTemplateByIdAsync Called");
//                var organization = await _unitOfWork.SignatureTemplate.GetSignatureTemplateByIdAsync(id);
//                if (organization == null)
//                {
//                    _logger.LogError("SignatureTemplate not found");
//                    return new ServiceResult("Signature Template not found");
//                }
//                return new ServiceResult(true, "Signature Template recieved successfully", organization);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"GetSignatureTemplateByIdAsync Error: {ex.Message}");
//                return new ServiceResult("Signature Template not found");
//            }
//        }

//        public async Task<ServiceResult> AddSignatureTemplateAsync(SignatureTemplate model)
//        {
//            try
//            {
//                _logger.LogInformation("AddSignatureTemplateAsync Called");

//                var isSignatureTemplateNameExist = await _unitOfWork.SignatureTemplate.IsSignatureTemplateExistsWithIDAsync(model.Id);
//                if (isSignatureTemplateNameExist)
//                {
//                    _logger.LogError("Signature Template Already Exists");
//                    return new ServiceResult("Signature Template Already Exists");
//                }

//                var organization = await _unitOfWork.SignatureTemplate.AddSignatureTemplateAsync(model);
//                if (organization == null)
//                {
//                    _logger.LogError("SignatureTemplate not added");
//                    return new ServiceResult("Signature Template not added");
//                }
//                return new ServiceResult(true, "Signature Template added successfully", organization);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"AddSignatureTemplateAsync Error: {ex.Message}");
//                return new ServiceResult("Signature Template not added");
//            }
//        }

//        public async Task<ServiceResult> UpdateSignatureTemplateAsync(SignatureTemplate model)
//        {
//            try
//            {
//                _logger.LogInformation("UpdateSignatureTemplateAsync Called");

//                var organization = await _unitOfWork.SignatureTemplate.UpdateSignatureTemplate(model);
//                if (organization == null)
//                {
//                    _logger.LogError("Signature Template not updated");
//                    return new ServiceResult("Signature Template not updated");
//                }
//                return new ServiceResult(true, "Signature Template updated successfully", organization);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"UpdateSignatureTemplate Error: {ex.Message}");
//                return new ServiceResult("Signature Template not updated");
//            }
//        }

//        public async Task<ServiceResult> DeleteSignatureTemplateAsync(SignatureTemplate model)
//        {
//            try
//            {
//                _logger.LogInformation("DeleteSignatureTemplateAsync Called");
//                var organization = _unitOfWork.SignatureTemplate.RemoveSignatureTemplate(model);
//                if (organization)
//                {
//                    _logger.LogError("Signature Template not deleted");
//                    return new ServiceResult("Signature Template not deleted");
//                }
//                return new ServiceResult(true, "Signature Template deleted successfully", organization);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"DeleteSignatureTemplateAsync Error: {ex.Message}");
//                return new ServiceResult("Signature Template not deleted");
//            }
//        }

//        public async Task<ServiceResult> DeleteSignatureTemplateByIdAsync(int id)
//        {
//            try
//            {
//                _logger.LogInformation("DeleteSignatureTemplateAsync Called");
//                var organization = await _unitOfWork.SignatureTemplate.RemoveSignatureTemplateById(id);
//                if (!organization)
//                {
//                    _logger.LogError("Signature Template not deleted");
//                    return new ServiceResult("Signature Template not deleted");
//                }
//                return new ServiceResult(true, "Signature Template deleted successfully", organization);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"DeleteSignatureTemplateAsync Error: {ex.Message}");
//                return new ServiceResult("Signature Template not deleted");
//            }
//        }

//    }
//}
