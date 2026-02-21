using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class ESealDetailService : IESealDetailService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ESealDetailService> _logger;
        private readonly IConfiguration _configuration;
        string orgUID;
        public ESealDetailService(HttpClient httpClient, IConfiguration configuration, ILogger<ESealDetailService> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:OrganizationOnboardingServiceBaseAddress"]!);
            orgUID = _configuration.GetValue<string>("OrganizationUid");
        }

        public async Task<EsealImageDTO> GetESealLogoAsync(string organizationUid)
        {
            try
            {
                _logger.LogInformation("Get ESeal Logo by organization uid api call start");
                var response = await _httpClient.GetAsync($"api/get/eseal-logo/{organizationUid}");
                _logger.LogInformation("Get ESeal Logo by organization uid api call end");


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new EsealImageDTO() { base64 = apiResponse.Result.ToString() };
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<ServiceResult> GetEsealCertificateStatus(string organizationUid)
        {

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/get/orgStatus?orgUid={organizationUid}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while getting Eseal-Certificate Status. Please try later.");
        }


        public async Task<ServiceResult> UpdateEsealLogo(string base64Image, string organizationUid)
        {

            try
            {
                ESealImageUpdateDTO viewModel = new()
                {
                    orgUid = organizationUid,
                    eSealImage = base64Image
                };

                var request = JsonConvert.SerializeObject(viewModel);
                StringContent content = new(request, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await _httpClient.PostAsync("api/post/update/eseal-logo", content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while getting Eseal-Certificate Status. Please try later.");
        }

        public async Task<OrganizationCertificate> GetCertificateDetails(string organizationUid)
        {
            var orgUID = organizationUid;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/get/organizationCertificateDetails/{orgUID}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var certificate = JsonConvert.DeserializeObject<CertificateDetailsDTO>(Convert.ToString(apiResponse.Result.ToString())!);
                        return new OrganizationCertificate()
                        {
                            CertificateSerialNumber = certificate.CertificateSerialNumber,
                            OrganizationUid = certificate.OrganizationUid,
                            PkiKeyId = certificate.PkiKeyId,
                            CertificateData = certificate.CertificateData,
                            WrappedKey = certificate.WrappedKey,
                            CertificateIssueDate = certificate.certificateStartDate,
                            CerificateExpiryDate = certificate.certificateEndDate,
                            CertificateStatus = certificate.CertificateStatus,
                            Remarks = certificate.Remarks,
                            CreatedDate = certificate.creationDate,
                            UpdatedDate = certificate.updatedDate,
                            CertificateType = certificate.CertificateType,
                            TransactionReferenceId = certificate.TransactionReferenceId,
                        };
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return null;

                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
