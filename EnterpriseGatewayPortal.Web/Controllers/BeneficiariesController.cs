using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.Attribute;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.ViewModel.Beneficiaries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [ServiceFilter(typeof(SessionValidationAttribute))]
    [Authorize]
    public class BeneficiariesController : BaseController
    {
        private readonly IBeneficiariesService _beneficiariesService;
        private readonly ILocalBeneficiariesService _localBeneficiariesService;
        private readonly ILocalBeneficiaryValiditiesService _localBeneficiaryValiditiesService;
        private readonly ILocalPrivilegesService _localPrivilegesService;
        private IWebHostEnvironment _environment;
        private readonly ILogger<BeneficiariesController> _logger;
        public BeneficiariesController(IAdminLogService adminLogService, IWebHostEnvironment environment,
            IBeneficiariesService beneficiariesService,
            ILocalBeneficiariesService localBeneficiariesService,
            ILocalBeneficiaryValiditiesService localBeneficiaryValiditiesService,
            ILocalPrivilegesService localPrivilegesService,
            ILogger<BeneficiariesController> logger) : base(adminLogService)
        {
            _environment = environment;
            _beneficiariesService = beneficiariesService;
            _logger = logger;
            _localBeneficiariesService = localBeneficiariesService;
            _localBeneficiaryValiditiesService = localBeneficiaryValiditiesService;
            _localPrivilegesService = localPrivilegesService;
        }

        [HttpGet]
        public async Task<IActionResult> BeneficiaryList()
        {
            string logMessage;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var response = await _localBeneficiariesService.GetAllBeneficiaryUsersBySponsorDigitalIdAsync(organizationUid);
            if (!response.Success)
            {
                logMessage = $"Failed to receive the Beneficiary Users List From Local DB";
                SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                    "Get Beneficiary users details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
                return NotFound();
            }
            var BeneficiariesList = (IEnumerable<Beneficiary>)response.Resource;

            var viewModel = new BeneficiaryListViewModel()
            {
                BeneficiaryList = BeneficiariesList,
            };

            logMessage = $"Successfully received the Beneficiary Users List From Local DB";
            SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                "Get Beneficiary users details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

            return View(viewModel);

        }



        [HttpGet]
        public async Task<IActionResult> NewBeneficiary()
        {
            try
            {

                var response = await _localPrivilegesService.GetAllPrivilegesListAsync();
                if (!response.Success)
                {
                    return RedirectToAction("BeneficiaryList", "Beneficiaries");
                }

                var privileges = (IEnumerable<Privilege>)response.Resource;

                var servicePrivilages = privileges.Select(privilege => new BeneficiariesServicesDTO
                {
                    privilegeId = privilege.PrivilegeId,
                    privilegeServiceName = privilege.PrivilegeServiceName,
                    privilegeServiceDisplayName = privilege.PrivilegeServiceDisplayName,
                    isChargeable = (privilege.IsChargeable ?? false) ? 1 : 0,
                    status = privilege.Status
                }).ToList();
                BeneficiariesViewModel viewModel = new BeneficiariesViewModel
                {
                    ServicePrivilages = servicePrivilages
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("BeneficiaryList", "Beneficiaries");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBeneficiariesUser([FromBody] BeneficiariesUserViewModel beneficiariesUser)
        {
            try
            {
                String logMessage;

                var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);

                if (beneficiariesUser.BeneficiaryValidities.Count == 0)
                {
                    return Json(new { status = false, message = "Select atleast one Service" });
                }

                var beneficiaryValiditiesDTO = beneficiariesUser.BeneficiaryValidities.Select(viewModel => new BeneficiaryValidityDTO
                {
                    privilegeServiceId = int.Parse(viewModel.privilegeId),
                    validityApplicable = viewModel.validityCheckbox,
                    validFrom = !string.IsNullOrEmpty(viewModel.validityFrom) ? DateTime.Parse(viewModel.validityFrom) : (DateTime?)null,
                    validUpTo = !string.IsNullOrEmpty(viewModel.validityUpto) ? DateTime.Parse(viewModel.validityUpto) : (DateTime?)null,
                }).ToList();

                //Creating Object for central DB
                var beneficiariesSendDTO = new BeneficiariesSendDTO()
                {
                    sponsorDigitalId = organizationUid,
                    sponsorName = OrganizationName,
                    sponsorType = "ORGANIZATION",
                    beneficiaryType = "INDIVIDUAL",
                    beneficiaryName = FullName,
                    beneficiaryNin = string.IsNullOrEmpty(beneficiariesUser.NationalIdNumber) ? null : beneficiariesUser.NationalIdNumber,
                    beneficiaryMobileNumber = string.IsNullOrEmpty(beneficiariesUser.MobileNumber) ? null : beneficiariesUser.MobileNumber,
                    beneficiaryOfficeEmail = string.IsNullOrEmpty(beneficiariesUser.EmployeeEmail) ? null : beneficiariesUser.EmployeeEmail,
                    beneficiaryUgpassEmail = string.IsNullOrEmpty(beneficiariesUser.UgpassEmail) ? null : beneficiariesUser.UgpassEmail,
                    beneficiaryPassport = string.IsNullOrEmpty(beneficiariesUser.PassportNumber) ? null : beneficiariesUser.PassportNumber,
                    BeneficiaryValidities = beneficiaryValiditiesDTO,
                };

                var response = await _beneficiariesService.SaveBeneficiaryDetails(beneficiariesSendDTO);

                if (response.Success && response.Resource != null)
                {
                    var json = response.Resource.ToString();
                    BeneficiaryResponseDTO dto = JsonConvert.DeserializeObject<BeneficiaryResponseDTO>(json!);


                    var beneficiaryDetail = dto.benificiaries;

                    //Creating Object for local DB
                    Beneficiary beneficiaryUser = new Beneficiary
                    {
                        Id = beneficiaryDetail.id,
                        SponsorDigitalId = beneficiaryDetail.sponsorDigitalId,
                        SponsorName = beneficiaryDetail.sponsorName,
                        SponsorType = beneficiaryDetail.sponsorType,
                        SponsorExternalId = beneficiaryDetail.sponsorExternalId,
                        BeneficiaryDigitalId = beneficiaryDetail.beneficiaryDigitalId,
                        BeneficiaryType = beneficiaryDetail.beneficiaryType,
                        BeneficiaryName = beneficiaryDetail.beneficiaryName,
                        BeneficiaryNin = beneficiaryDetail.beneficiaryNin,
                        BeneficiaryPassport = beneficiaryDetail.beneficiaryPassport,
                        BeneficiaryMobileNumber = beneficiaryDetail.beneficiaryMobileNumber,
                        BeneficiaryOfficeEmail = beneficiaryDetail.beneficiaryOfficeEmail,
                        BeneficiaryUgpassEmail = beneficiaryDetail.beneficiaryUgPassEmail,
                        Designation = beneficiaryDetail.designation,
                        SignaturePhoto = beneficiaryDetail.signaturePhoto,
                        Status = beneficiaryDetail.status,
                        CreatedOn = beneficiaryDetail.createdOn,
                        UpdatedOn = beneficiaryDetail.updatedOn,
                        SponsorPaymentPriorityLevel = beneficiaryDetail.sponsorPaymentPriorityLevel,
                        BeneficiaryConsentAcquired = beneficiaryDetail.beneficiaryConsentAcquired,
                    };

                    var BeneficiaryValidities = dto.beneficiaryValidity.Select(validity => new BeneficiaryValidity
                    {
                        Id = validity.id,
                        BeneficiaryId = (int)validity.beneficiaryId!,
                        PrivilegeServiceId = validity.privilegeServiceId,
                        ValidityApplicable = validity.validityApplicable,
                        ValidFrom = validity.validFrom,
                        ValidUpto = validity.validUpTo,
                        Status = validity.status,
                        CreatedOn = validity.createdOn,
                        UpdatedOn = validity.updatedOn
                    }).ToList();

                    var response2 = await _localBeneficiariesService.AddBeneficiaryUserAsync(beneficiaryUser);
                    if (!response2.Success)
                    {
                        logMessage = $"Failed to Add the Beneficiary User in local DB";
                        SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                             "Get Beneficiary users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                        return Json(new { status = response2.Success, message = response2.Message });
                    }

                    var validity_response = await _localBeneficiaryValiditiesService.AddBeneficiaryValiditiesListAsync(BeneficiaryValidities);
                    if (!validity_response.Success)
                    {
                        logMessage = $"Failed to Add the Beneficiary User in local DB";
                        SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                             "Get Beneficiary users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                        return Json(new { status = validity_response.Success, message = validity_response.Message });
                    }
                    else
                    {
                        logMessage = $"Successfully Added Beneficiary User in local DB";
                        SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                             "Get Beneficiary users details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                        return Json(new { status = response2.Success, message = response2.Message });
                    }

                }
                else
                {
                    logMessage = $"Failed to Add the Beneficiary User in server DB";
                    SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                         "Get Beneficiary users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                    return Json(new { status = response.Success, message = response.Message });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Add the Beneficiary User: {ex.Message}");
                return Json(new { status = false, message = ex.Message });
            }


        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string logMessage;
            try
            {

                var response = await _localPrivilegesService.GetAllPrivilegesListAsync();
                if (!response.Success)
                {
                    return RedirectToAction("BeneficiaryList", "Beneficiaries");
                }

                var privileges = (IEnumerable<Privilege>)response.Resource;

                var servicePrivilages = privileges.Select(privilege => new BeneficiariesServicesDTO
                {
                    privilegeId = privilege.PrivilegeId,
                    privilegeServiceName = privilege.PrivilegeServiceName,
                    privilegeServiceDisplayName = privilege.PrivilegeServiceDisplayName,
                    isChargeable = (privilege.IsChargeable ?? false) ? 1 : 0,
                    status = privilege.Status
                }).ToList();

                var updateBeneficiary = await _localBeneficiariesService.GetBeneficiaryUserByIdAsync(id);
                var beneficiaryDetails = (Beneficiary)updateBeneficiary.Resource;
                var getBeneficiaryValidities = beneficiaryDetails.BeneficiaryValidities;
                if (beneficiaryDetails == null)
                {
                    logMessage = $"Failed to Get Beneficiary User Details";
                    SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                         "Get Beneficiary users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                    return NotFound();
                }


                BeneficiariesEditViewModel viewModel = new BeneficiariesEditViewModel()
                {
                    Id = beneficiaryDetails.Id,
                    SponsorDigitalId = beneficiaryDetails.SponsorDigitalId,
                    SponsorName = beneficiaryDetails.SponsorName,
                    SponsorType = beneficiaryDetails.SponsorType,
                    SponsorExternalId = beneficiaryDetails.SponsorExternalId,
                    BeneficiaryName = beneficiaryDetails.BeneficiaryName,
                    BeneficiaryDigitalId = beneficiaryDetails.BeneficiaryDigitalId,
                    BeneficiaryType = beneficiaryDetails.BeneficiaryType,
                    SponsorPaymentPriorityLevel = beneficiaryDetails.SponsorPaymentPriorityLevel,
                    BeneficiaryNin = beneficiaryDetails.BeneficiaryNin,
                    BeneficiaryPassport = beneficiaryDetails.BeneficiaryPassport,
                    BeneficiaryMobileNumber = beneficiaryDetails.BeneficiaryMobileNumber,
                    countryCode = null,
                    BeneficiaryUgpassEmail = beneficiaryDetails.BeneficiaryUgpassEmail,
                    BeneficiaryConsentAcquired = beneficiaryDetails.BeneficiaryConsentAcquired == true,
                    SignaturePhoto = beneficiaryDetails.SignaturePhoto,
                    Designation = beneficiaryDetails.Designation,
                    Status = beneficiaryDetails.Status,
                    CreatedOn = beneficiaryDetails.CreatedOn,
                    UpdatedOn = beneficiaryDetails.UpdatedOn,
                    BeneficiariedPrivilegeList = servicePrivilages

                };
                viewModel.BeneficiaryValidities = beneficiaryDetails.BeneficiaryValidities.Select(validity => new BeneficiariesValidityDTO
                {
                    PrivilegeServiceId = validity.PrivilegeServiceId,
                    ValidityApplicable = validity.ValidityApplicable == true,
                    validFrom = validity.ValidFrom,
                    validUpTo = validity.ValidUpto,
                    CreatedOn = validity.CreatedOn,
                    UpdatedOn = validity.UpdatedOn,
                    Status = validity.Status,
                    Id = validity.Id,
                    BeneficiaryId = validity.BeneficiaryId,

                }).ToList();


                if (!string.IsNullOrEmpty(beneficiaryDetails.BeneficiaryMobileNumber))
                {
                    if (beneficiaryDetails.BeneficiaryMobileNumber.StartsWith("+256"))
                    {
                        string[] mobileParts = new string[2];
                        mobileParts[0] = "+256";
                        mobileParts[1] = beneficiaryDetails.BeneficiaryMobileNumber.Substring(4);
                        viewModel.countryCode = mobileParts[0];
                        viewModel.BeneficiaryMobileNumber = mobileParts[1];
                    }
                    else if (beneficiaryDetails.BeneficiaryMobileNumber.StartsWith("+91"))
                    {
                        string[] mobileParts = new string[2];
                        mobileParts[0] = "+91";
                        mobileParts[1] = beneficiaryDetails.BeneficiaryMobileNumber.Substring(3);
                        viewModel.countryCode = mobileParts[0];
                        viewModel.BeneficiaryMobileNumber = mobileParts[1];
                    }
                    else
                    {
                        viewModel.countryCode = "";
                        viewModel.BeneficiaryMobileNumber = "";
                    }
                }

                foreach (var servicePrivilege in servicePrivilages)
                {
                    var existingValidity = beneficiaryDetails.BeneficiaryValidities
                        .FirstOrDefault(v => v.PrivilegeServiceId == servicePrivilege.privilegeId);

                    // Check if the entry exists in the ViewModel
                    var viewModelEntry = viewModel.BeneficiaryValidities
                        .FirstOrDefault(uu => uu.PrivilegeServiceId == servicePrivilege.privilegeId);

                    if (existingValidity == null)
                    {

                        viewModel.BeneficiaryValidities.Add(new BeneficiariesValidityDTO
                        {
                            PrivilegeServiceId = servicePrivilege.privilegeId,
                            PrivilegeChecked = false,
                            ValidityApplicable = false
                        });

                    }
                    else
                    {
                        viewModelEntry.PrivilegeChecked = true;

                    }
                }

                ViewBag.Id = id;

                logMessage = $"Successfully received Beneficiary User Details";
                SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                     "Get Beneficiary users details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                return View(viewModel);


            }
            catch (Exception)
            {
                return RedirectToAction("BeneficiaryList");
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBeneficiariesUser([FromBody] EditBeneficiariesViewModel beneficiariesUser)
        {
            string logMessage;
            try
            {

                if (beneficiariesUser.BeneficiaryEditValidities.Count == 0)
                {

                    return Json(new { status = false, message = "Select atleast one Service" });
                }

                var updateBeneficiary = await _localBeneficiariesService.GetBeneficiaryUserByIdAsync(beneficiariesUser.Id);
                var user1 = (Beneficiary)updateBeneficiary.Resource;

                var getBeneficiaryValidities = user1.BeneficiaryValidities;
                if (getBeneficiaryValidities.Count != 0)
                {
                    var res = await _localBeneficiaryValiditiesService.RemoveAllBeneficiaryValiditiesByBeneficiaryIdAsync(user1.Id);
                }


                if (user1 == null)
                {
                    logMessage = $"Failed to get the Beneficiary User From Local DB";
                    SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                        "Get Beneficiary user details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                    return NotFound();
                }

                //Creating Object for local DB
                user1.SponsorDigitalId = OrganizationId;
                user1.SponsorName = OrganizationName;
                user1.SponsorType = "ORGANIZATION";
                user1.BeneficiaryNin = beneficiariesUser.NationalIdNumber;
                user1.BeneficiaryPassport = beneficiariesUser.PassportNumber;
                user1.BeneficiaryMobileNumber = beneficiariesUser.MobileNumber;
                user1.BeneficiaryOfficeEmail = beneficiariesUser.EmployeeEmail;
                user1.BeneficiaryUgpassEmail = beneficiariesUser.UgpassEmail;
                user1.Status = "ACTIVE";
                user1.UpdatedOn = DateTime.Now;

                var BeneficiaryValidities = beneficiariesUser.BeneficiaryEditValidities.Select(viewModel => new BeneficiaryValidity
                {
                    BeneficiaryId = user1.Id,
                    PrivilegeServiceId = int.Parse(viewModel.privilegeId),
                    ValidityApplicable = viewModel.validityCheckbox ? true : false,
                    ValidFrom = !string.IsNullOrEmpty(viewModel.validityFrom) ? DateTime.Parse(viewModel.validityFrom) : (DateTime?)null,
                    ValidUpto = !string.IsNullOrEmpty(viewModel.validityUpto) ? DateTime.Parse(viewModel.validityUpto) : (DateTime?)null,
                    Status = "ACTIVE",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                }).ToList();

                //Creating Object for central DB
                var beneficiaryValidityDTO = beneficiariesUser.BeneficiaryEditValidities.Select(viewModel => new BeneficiaryUpdateValidityDTO
                {
                    privilegeServiceId = int.Parse(viewModel.privilegeId),
                    validityApplicable = viewModel.validityCheckbox,
                    validFrom = !string.IsNullOrEmpty(viewModel.validityFrom) ? DateTime.Parse(viewModel.validityFrom) : (DateTime?)null,
                    validUpTo = !string.IsNullOrEmpty(viewModel.validityUpto) ? DateTime.Parse(viewModel.validityUpto) : (DateTime?)null,
                }).ToList();

                var beneficiariesUpdateDTO = new BeneficiariesUpdateDTO()
                {
                    id = beneficiariesUser.Id,
                    sponsorDigitalId = OrganizationId,
                    sponsorName = OrganizationName,
                    sponsorType = "ORGANIZATION",
                    beneficiaryType = "INDIVIDUAL",
                    beneficiaryName = FullName,
                    beneficiaryNin = string.IsNullOrEmpty(beneficiariesUser.NationalIdNumber) ? null : beneficiariesUser.NationalIdNumber,
                    beneficiaryMobileNumber = string.IsNullOrEmpty(beneficiariesUser.MobileNumber) ? null : beneficiariesUser.MobileNumber,
                    beneficiaryOfficeEmail = string.IsNullOrEmpty(beneficiariesUser.EmployeeEmail) ? null : beneficiariesUser.EmployeeEmail,
                    beneficiaryUgpassEmail = string.IsNullOrEmpty(beneficiariesUser.UgpassEmail) ? null : beneficiariesUser.UgpassEmail,
                    beneficiaryPassport = string.IsNullOrEmpty(beneficiariesUser.PassportNumber) ? null : beneficiariesUser.PassportNumber,
                    BeneficiaryValidities = beneficiaryValidityDTO,
                };

                var response = await _beneficiariesService.UpdateBeneficiaryDetails(beneficiariesUpdateDTO);

                if (response.Success)
                {
                    var response2 = await _localBeneficiariesService.UpdateBeneficiaryUserAsync(user1);
                    if (!response2.Success)
                    {
                        logMessage = $"Failed to update the Beneficiary User in local DB";
                        SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                             "Get Beneficiary users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
                        return Json(new { status = response2.Success, message = response2.Message });
                    }


                    var validity_response = await _localBeneficiaryValiditiesService.AddBeneficiaryValiditiesListAsync(BeneficiaryValidities);

                    if (!validity_response.Success)
                    {
                        logMessage = $"Failed to update the Beneficiary User in local DB";
                        SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                             "Get Beneficiary users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                        return Json(new { status = validity_response.Success, message = validity_response.Message });
                    }
                    else
                    {
                        logMessage = $"Successfully Updated Beneficiary User in local DB";
                        SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                             "Get Beneficiary users details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                        return Json(new { status = response2.Success, message = response2.Message });
                    }


                }
                else
                {
                    logMessage = $"Failed to Update Beneficiary User in server DB";
                    SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                         "Get Beneficiary users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                    return Json(new { status = response.Success, message = response.Message });
                }
            }
            catch (Exception ex)
            {

                return Json(new { status = false, message = ex.Message });
            }
        }



        [HttpGet]
        public IActionResult DownloadCSV()
        {
            // Get the path to the CSV file on the server
            string csvFilePath = Path.Combine(_environment.WebRootPath, "samples/SampleAddBenificiariesUsingCSV(7).csv");

            _logger.LogInformation(csvFilePath);

            // Check if the file exists
            if (!System.IO.File.Exists(csvFilePath))
            {
                _logger.LogError("File not Found");
                _logger.LogError(_environment.WebRootPath);
                return NotFound();
            }

            // Set the response content type and headers
            string contentType = "text/csv";
            string fileName = Path.GetFileName(csvFilePath);
            return PhysicalFile(csvFilePath, contentType, fileName);
        }

        [HttpPost]
        public async Task<IActionResult> AddMultipleBeneficiariesUser([FromBody] BeneficiaryListViewModel SponsorBeneficiaryList)
        {
            try
            {
                var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
                string logMessage;

                AddMultipleBeneficiaries rawCsvDataDTO = new AddMultipleBeneficiaries()
                {
                    ListA = SponsorBeneficiaryList.CsvData
                };
                IList<BeneficiariesSendDTO> beneficiaries = new List<BeneficiariesSendDTO>();
                foreach (var data in rawCsvDataDTO.ListA)
                {
                    //Creating Object for central DB
                    BeneficiariesSendDTO beni = new()
                    {
                        sponsorDigitalId = organizationUid,
                        beneficiaryUgpassEmail = string.IsNullOrEmpty(data.UgpassEmail) ? null : data.UgpassEmail,
                        sponsorType = "ORGANIZATION",
                        beneficiaryType = "INDIVIDUAL",
                        beneficiaryMobileNumber = string.IsNullOrEmpty(data.MobNo) ? null : data.MobNo,
                        beneficiaryPassport = string.IsNullOrEmpty(data.PassportNumber) ? null : data.PassportNumber,
                        beneficiaryNin = string.IsNullOrEmpty(data.NIN) ? null : data.NIN,
                        BeneficiaryValidities = new()

                    };
                    if (data.Signature_Permission == "1")
                    {
                        BeneficiaryValidityDTO signatureValidity = new()
                        {
                            privilegeServiceId = 1,
                            validityApplicable = data.Signature_Validity_Flag == "1" ? true : false,

                        };
                        if (!string.IsNullOrEmpty(data.Signature_Valid_From))
                        {
                            if (DateTime.TryParseExact(data.Signature_Valid_From, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedValidFrom))
                            {
                                signatureValidity.validFrom = parsedValidFrom;
                            }
                        }

                        if (!string.IsNullOrEmpty(data.Signature_Valid_Upto))
                        {
                            if (DateTime.TryParseExact(data.Signature_Valid_Upto, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedValidUpTo))
                            {
                                signatureValidity.validUpTo = parsedValidUpTo;
                            }
                        }

                        beni.BeneficiaryValidities.Add(signatureValidity);
                    }

                    if (data.User_Permission == "1")
                    {
                        BeneficiaryValidityDTO userValidity = new()
                        {
                            privilegeServiceId = 3,
                            validityApplicable = data.User_Validity_Flag == "1" ? true : false,

                        };
                        if (!string.IsNullOrEmpty(data.User_Valid_From))
                        {
                            if (DateTime.TryParseExact(data.User_Valid_From, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedValidFrom))
                            {
                                userValidity.validFrom = parsedValidFrom;
                            }
                        }

                        if (!string.IsNullOrEmpty(data.User_Valid_Upto))
                        {
                            if (DateTime.TryParseExact(data.User_Valid_Upto.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedValidUpTo))
                            {
                                userValidity.validUpTo = parsedValidUpTo;
                            }
                        }

                        beni.BeneficiaryValidities.Add(userValidity);
                    }
                    beneficiaries.Add(beni);
                }
                var response = await _beneficiariesService.AddMultipleBeneficiariesAsync(beneficiaries);
                if (response.Success)
                {


                    JArray jsonArray = JArray.FromObject(response.Resource);
                    List<BeneficiariesResponseDtos> users = new List<BeneficiariesResponseDtos>();
                    List<Beneficiary> beneficiariesData = new List<Beneficiary>();
                    List<BeneficiaryValidity> multipleBeneficiaryValidities = new List<BeneficiaryValidity>();

                    foreach (var item in jsonArray)
                    {
                        var beneficiariesResponse = item["benificiariesResponseDtos"];
                        if (beneficiariesResponse != null)
                        {
                            var beneficiariesDto = beneficiariesResponse["benificiaries"].ToObject<BeneficiaryDTO>();
                            var beneficiaryValidityDtoList = beneficiariesResponse["beneficiaryValidity"].ToObject<List<BeneficiaryValidityDTO>>();

                            var beneficiariesResponseDto = new BeneficiariesResponseDtos
                            {
                                beneficiaries = beneficiariesDto,
                                beneficiaryValidity = beneficiaryValidityDtoList
                            };

                            //Creating Object for local DB
                            Beneficiary beneficiary = new Beneficiary
                            {
                                Id = beneficiariesDto.id,
                                SponsorDigitalId = beneficiariesDto.sponsorDigitalId,
                                SponsorName = beneficiariesDto.sponsorName,
                                SponsorType = beneficiariesDto.sponsorType,
                                SponsorExternalId = beneficiariesDto.sponsorExternalId,
                                BeneficiaryDigitalId = beneficiariesDto.beneficiaryDigitalId,
                                BeneficiaryType = beneficiariesDto.beneficiaryType,
                                BeneficiaryName = beneficiariesDto.beneficiaryName,
                                BeneficiaryNin = beneficiariesDto.beneficiaryNin,
                                BeneficiaryPassport = beneficiariesDto.beneficiaryPassport,
                                BeneficiaryMobileNumber = beneficiariesDto.beneficiaryMobileNumber,
                                BeneficiaryOfficeEmail = beneficiariesDto.beneficiaryOfficeEmail,
                                BeneficiaryUgpassEmail = beneficiariesDto.beneficiaryUgPassEmail,
                                Designation = beneficiariesDto.designation,
                                SignaturePhoto = beneficiariesDto.signaturePhoto,
                                Status = beneficiariesDto.status,
                                CreatedOn = beneficiariesDto.createdOn,
                                UpdatedOn = beneficiariesDto.updatedOn,
                                SponsorPaymentPriorityLevel = beneficiariesDto.sponsorPaymentPriorityLevel,
                                BeneficiaryConsentAcquired = beneficiariesDto.beneficiaryConsentAcquired

                            };
                            beneficiariesData.Add(beneficiary);
                            users.Add(beneficiariesResponseDto);

                            multipleBeneficiaryValidities.AddRange(beneficiaryValidityDtoList!.Select(validityDto => new BeneficiaryValidity
                            {
                                Id = validityDto.id,
                                BeneficiaryId = (int)validityDto.beneficiaryId!,
                                PrivilegeServiceId = validityDto.privilegeServiceId,
                                ValidityApplicable = validityDto.validityApplicable,
                                ValidFrom = validityDto.validFrom,
                                ValidUpto = validityDto.validUpTo,
                                Status = validityDto.status,
                                CreatedOn = validityDto.createdOn,
                                UpdatedOn = validityDto.updatedOn
                            }));

                        }
                    }

                    var response2 = await _localBeneficiariesService.AddMultipleBeneficiariesListAsync(beneficiariesData);
                    if (!response2.Success)
                    {
                        logMessage = $"Failed to Add the multiple beneficiaries in local DB";
                        SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                             "Get Beneficiary users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                        return Json(new { status = response2.Success, message = response2.Message });
                    }

                    var validity_response = await _localBeneficiaryValiditiesService.AddBeneficiaryValiditiesListAsync(multipleBeneficiaryValidities);
                    if (!validity_response.Success)
                    {

                        logMessage = $"Failed to Add the multiple beneficiaries in local DB";
                        SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                             "Get Beneficiary users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                        return Json(new { status = response2.Success, message = response2.Message });
                    }

                    else
                    {
                        logMessage = $"Successfully Added multiple beneficiaries in local DB";
                        SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                             "Get Beneficiary users details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                        return Json(new { status = response2.Success, message = response2.Message });
                    }

                }
                else
                {
                    logMessage = $"Failed to Add Multiple Beneficiary Users";
                    SendAdminLog(ModuleNameConstants.Beneficiaries, ServiceNameConstants.Beneficiaries,
                         "Get Beneficiary users details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                    return Json(new { status = response.Success, message = response.Message });
                }
            }
            catch (Exception)
            {
                return Json(new { status = false, message = "Error While Processing" });
            }


        }


    }
}
