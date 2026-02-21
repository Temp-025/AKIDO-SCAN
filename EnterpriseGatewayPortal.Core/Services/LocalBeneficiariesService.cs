namespace EnterpriseGatewayPortal.Core.Services
{
    //public class LocalBeneficiariesService : ILocalBeneficiariesService
    //{
    //    private readonly ILogger<LocalBeneficiariesService> _logger;
    //    private readonly IUnitOfWork _unitOfWork;
    //    public LocalBeneficiariesService(ILogger<LocalBeneficiariesService> logger, IUnitOfWork unitOfWork)
    //    {
    //        _logger = logger;
    //        _unitOfWork = unitOfWork;
    //    }

    //    public async Task<ServiceResult> GetBeneficiaryUserByIdAsync(int id)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("GetBeneficiariesByIdAsync Called");
    //            var user = await _unitOfWork.Beneficiaries.GetBeneficiariesByIdAsync(id);
    //            if (user == null)
    //            {
    //                _logger.LogError("Beneficiary not found");
    //                return new ServiceResult("Beneficiary user not found");
    //            }
    //            return new ServiceResult(true, "Beneficiary User recieved successfully", user);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"GetBeneficiariesByIdAsync Error: {ex.Message}");
    //            return new ServiceResult("Beneficiary User not found");
    //        }
    //    }

    //    public async Task<ServiceResult> GetAllBeneficiaryUsersBySponsorDigitalIdAsync(string orgUid)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("GetAllBeneficiaryUsersBySponsorDigitalIdAsync Called");
    //            var list = await _unitOfWork.Beneficiaries.GetAllBeneficiariesBySponsorDigitalIdAsync(orgUid);
    //            if (list == null)
    //            {
    //                _logger.LogError("list is Empty");
    //                return new ServiceResult("Beneficiaries list is Empty");
    //            }
    //            return new ServiceResult(true, "All Beneficiaries List recieved successfully", list);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"GetAllBeneficiaryUsersBySponsorDigitalIdAsync Error: {ex.Message}");
    //            return new ServiceResult("Beneficiaries list not found");
    //        }
    //    }

    //    public async Task<ServiceResult> AddBeneficiaryUserAsync(Beneficiary model)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("AddBusinessUserAsync Called");

    //            var isBeneficiaryEmailExist = await _unitOfWork.Beneficiaries.IsBeneficiaryEmailExistsAsync(model.BeneficiaryUgpassEmail); 
    //            if (isBeneficiaryEmailExist)
    //            {
    //                _logger.LogError("Beneficiary User Already Exists");
    //                return new ServiceResult("Beneficiary user Already Exists");
    //            }

    //            var beneficiary = await _unitOfWork.Beneficiaries.AddBeneficiariaryUserAsync(model);
    //            if (beneficiary == null)
    //            {
    //                _logger.LogError("Beneficiary User not added");
    //                return new ServiceResult("Beneficiary User not added");
    //            }
    //            return new ServiceResult(true, "Beneficiary user added successfully", beneficiary);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"AddBeneficiaryUserAsync Error: {ex.Message}");
    //            return new ServiceResult("Beneficiary user not added");
    //        }
    //    }

    //    public async Task<ServiceResult> AddMultipleBeneficiariesListAsync(List<Beneficiary> models)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("AddMultipleBeneficiariesListAsync Called");

    //            var beneficiary = await _unitOfWork.Beneficiaries.AddMultipleBeneficiaryUsersListAsync(models);
    //            if (beneficiary == null)
    //            {
    //                _logger.LogError("Beneficiary Users not added");
    //                return new ServiceResult("Beneficiary Users not added");
    //            }
    //            return new ServiceResult(true, beneficiary.Count > 1
    //                                        ? "All Beneficiary users added successfully"
    //                                        : "Beneficiary users added successfully", beneficiary);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"AddMultipleBeneficiariesListAsync Error: {ex.Message}");
    //            return new ServiceResult("Beneficiary Users not added");
    //        }
    //    }

    //    public async Task<ServiceResult> UpdateBeneficiaryUserAsync(Beneficiary model)
    //    {
    //        try
    //        {
    //            _logger.LogInformation("UpdateBeneficiaryUserAsync Called");

    //            var beneficiary = await _unitOfWork.Beneficiaries.UpdateBeneficiaries(model);
    //            if (beneficiary == null)
    //            {
    //                _logger.LogError("Beneficiary User not updated");
    //                return new ServiceResult("Beneficiary User not updated");
    //            }
    //            return new ServiceResult(true, "Beneficiary user updated successfully", beneficiary);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"UpdateBeneficiaries Error: {ex.Message}");
    //            return new ServiceResult("Beneficiary user not updated");
    //        }
    //    }
    //}
}
