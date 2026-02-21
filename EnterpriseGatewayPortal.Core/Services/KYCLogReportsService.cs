//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.DTOs;
//using EnterpriseGatewayPortal.Core.Utilities;
//using Microsoft.CodeAnalysis.Operations;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json.Serialization;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using EnterpriseGatewayPortal.Core.Exceptions;
//using EnterpriseGatewayPortal.Core.Domain.Models;
//using System.Globalization;
//using Microsoft.IdentityModel.Tokens;
//using System.Security.Cryptography;
//using System.IdentityModel.Tokens.Jwt;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class KYCLogReportsService : IKYCLogReportsService
//    {
//        private readonly IConfiguration _configuration;
//        private readonly ILogger<KYCLogReportsService> _logger;
//        private readonly IHttpClientFactory _httpClientFactory;
//        private readonly ECDsaSecurityKey _securityKey;

//        public KYCLogReportsService(IConfiguration configuration,
//            ILogger<KYCLogReportsService> logger,
//            IHttpClientFactory httpClientFactory)
//        {
//            _configuration = configuration;
//            _logger = logger;
//            _httpClientFactory = httpClientFactory;
//            //string publicKeyPem = configuration["PublicKeyPem"];

//            //if (_configuration.GetValue<bool>("EncryptionEnabled"))
//            //{
//            //    publicKeyPem = PKIMethods.Instance.
//            //                    PKIDecryptSecureWireData(publicKeyPem);
//            //}

//            string publicKeyPem = configuration["PublicKeyPem"] ?? @"
//-----BEGIN PUBLIC KEY-----
//MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAELs0Uut47nN8e+GLtWwaDeSRgsvMo
//a/VyJdGSfOdNamQiUskTAOyh8f1wlHeKUGI9DNff0PL9fYAjSQdyyA2PFg==
//-----END PUBLIC KEY-----";

//            try
//            {
//                byte[] keyBytes = ExtractDerBytesFromPem(publicKeyPem, "PUBLIC KEY");

//                var ecdsa = ECDsa.Create();
//                ecdsa.ImportSubjectPublicKeyInfo(keyBytes, out _);

//                _securityKey = new ECDsaSecurityKey(ecdsa);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Failed to load or parse the public key.");
//                throw;
//            }
//        }

//        public async Task<ServiceResult> ValidateSignedDataAsync(string signedData, string serviceName)
//        {
//            try
//            {
//                var validationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    ValidateLifetime = false,
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = _securityKey
//                };
//                var handler = new JwtSecurityTokenHandler
//                {
//                    MaximumTokenSizeInBytes = 1024 * 1024
//                };
//                handler.ValidateToken(signedData, validationParameters, out _);

//                var payLoad = handler.ReadJwtToken(signedData).Payload;

//                Dictionary<string, object> data = new Dictionary<string, object>();

//                foreach (var claim in payLoad)
//                {
//                    data[claim.Key] = claim.Value;
//                }
//                string[] parts = signedData.Split('.');

//                string plainText = DecodeBase64Payload(parts[1]);

//                //var options = new JsonSerializerOptions
//                //{
//                //    PropertyNameCaseInsensitive = true
//                //};

//                //Dictionary<string, object> payloadDict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(plainText, options);



//                if (serviceName == "CARD_STATUS_WITH_OCR" || serviceName == "BATCH_CARD_STATUS" || serviceName == "CARD_STATUS_WITH_MANUAL" || serviceName == "PASSPORT_STATUS_WITH_OCR" || serviceName == "PASSPORT_STATUS_WITH_MANUAL_ENTRY")
//                {
//                    var callStack = JsonConvert.DeserializeObject<IdValidationCallStackDTO>(plainText);
//                    string genderCode = callStack?.idCardData?.nonModifiableData?.gender ?? string.Empty;
//                    string gender = genderCode switch
//                    {
//                        "M" => "MALE",
//                        "F" => "FEMALE",
//                        _ => string.Empty
//                    };

//                    var idValidationReport = new VerifiedIdValidationResponseDTO
//                    {
//                        name = callStack?.name ?? string.Empty,
//                        idNumber = callStack?.idNumber ?? string.Empty,
//                        nationality = callStack?.nationality ?? string.Empty,
//                        issueDate = callStack?.issueDate ?? string.Empty,
//                        photo = callStack?.idCardData?.photography?.cardHolderPhoto ?? string.Empty,
//                        expiryDate = callStack?.expiryDate ?? string.Empty,
//                        gender = gender,
//                        dob = callStack?.dateOfBirth ?? string.Empty,
//                        documentStatus = callStack?.documentStatus ?? string.Empty,
//                    };

//                    return new ServiceResult(true, "Token is valid", idValidationReport);
//                }

//                else
//                {
//                    var callStack = JsonConvert.DeserializeObject<EmiratesIdResponse>(plainText);

//                    string base64 = string.Empty;
//                    if (callStack?.Data?.BinaryObjects != null && callStack.Data.BinaryObjects.Count > 0)
//                    {
//                        base64 = callStack.Data.BinaryObjects[0].BinaryBase64String;
//                    }

//                    var idValidationReport = new VerifiedIdValidationResponseDTO
//                    {
//                        name = callStack?.Name ?? string.Empty,
//                        idNumber = callStack?.IdNumber ?? string.Empty,
//                        gender = callStack?.Data?.Gender?.DescriptionEn ?? string.Empty,
//                        dob = callStack?.DateOfBirth ?? string.Empty,
//                        nationality = callStack?.Nationality ?? string.Empty,
//                        photo = base64 ?? string.Empty,
//                        expiryDate = callStack?.ExpiryDate ?? string.Empty,
//                        issueDate = callStack?.Data?.ActivePassport?.IssueDate ?? string.Empty,
//                        documentStatus = callStack?.DocumentStatus ?? string.Empty
//                    };

//                    return new ServiceResult(true, "Token is valid", idValidationReport);
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "JWT validation failed.");
//                return new ServiceResult(false, $"Token validation failed: {ex.Message}");
//            }
//        }

//        public async Task<LogReportDTO> GetKycDetailsAsync(string Id)
//        {
//            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:KYCServiceBaseAddress"]);
//            try
//            {
//                _logger.LogInformation("Get Kyc details  api call start");
//                HttpResponseMessage response = await client.GetAsync($"api/records/transaction/{Id}");
//                _logger.LogInformation("Get  Kyc details api call end");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return JsonConvert.DeserializeObject<LogReportDTO>(apiResponse.Result.ToString());



//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);

//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<IEnumerable<AdminActivity>> GetKYCLogReportAsync(int page = 1)
//        {
//            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:KYCServiceBaseAddress"]);
//            client.Timeout = TimeSpan.FromSeconds(30);

//            try
//            {
//                HttpResponseMessage response = await client.GetAsync($"api/audit-logs/{page}");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        JObject logsArray = (JObject)JToken.FromObject(apiResponse.Result);
//                        return JsonConvert.DeserializeObject<IEnumerable<AdminActivity>>(logsArray["logs"].ToString());
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }


//        public async Task<PaginatedList<LogReportDTO>> GetKYCLogReportAsync(string startDate, string endDate, string orgId, string serviceNamesCsv, int page = 1, int perPage = 10)
//        {
//            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:KYCServiceBaseAddress"]);

//            try
//            {
//                string[] serviceNamesArray = serviceNamesCsv
//                .Split(',', StringSplitOptions.RemoveEmptyEntries)
//                .Select(s => s.Trim()) // removes any leading/trailing whitespace
//                .ToArray();

//                string json = JsonConvert.SerializeObject(
//                new
//                {
//                    fromDate = startDate,
//                    toDate = endDate,
//                    perPage = perPage,
//                    page = page,
//                    serviceNames = serviceNamesArray
//                },
//                new JsonSerializerSettings
//                {
//                    ContractResolver = new CamelCasePropertyNamesContractResolver()
//                });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


//                HttpResponseMessage response = await client.PostAsync($"api/records/organization/91056d83-4331-48a4-a5fe-48ccb0d0dad4", content);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());

//                    if (apiResponse.Success)
//                    {
//                        if (apiResponse.Result is JObject resultObj)
//                        {
//                            var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(resultObj["data"].ToString());

//                            return new PaginatedList<LogReportDTO>(
//                                logs,
//                                Convert.ToInt32(resultObj["currentPage"]),
//                                perPage,
//                                Convert.ToInt32(resultObj["totalPages"]),
//                                Convert.ToInt32(resultObj["totalCount"]));
//                        }
//                        else if (apiResponse.Result is JArray resultArray)
//                        {
//                            var logs = resultArray.ToObject<List<LogReportDTO>>();

//                            return new PaginatedList<LogReportDTO>(
//                                logs,
//                                page,
//                                perPage,
//                                1,
//                                logs.Count());
//                        }
//                        else
//                        {
//                            _logger.LogError("Unexpected response format.");
//                        }
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<PaginatedList<KYCValidationResponseDTO>> GetKYCValidationReportAsync(
//            string identifier = "",
//            string logMessageType = "",
//            string organizationId = "",
//            string startDate = "",
//            string endDate = "",
//            List<string>? kycMethods = null,
//            int page = 1,
//            int perPage = 10
//        )
//        {
//            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:KYCServiceBaseAddress"]);
//            client.Timeout = TimeSpan.FromSeconds(30);

//            List<KYCValidationResponseDTO> idValidationReports = new();


//            try
//            {
//                string endpoint = $"api/records/by-identifier";

//                var requestPayload = new
//                {
//                    identifier,
//                    logMessageType,
//                    orgId = organizationId,
//                    serviceNames = kycMethods ?? new List<string>(),
//                    page,
//                    perPage,
//                    fromDate = startDate,
//                    toDate = endDate
//                };

//                string json = JsonConvert.SerializeObject(
//                    requestPayload,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
//                _logger.LogInformation("GetKYCValidationReportAsync Start.");

//                HttpResponseMessage response = await client.PostAsync(endpoint, content);

//                response.EnsureSuccessStatusCode();

//                _logger.LogInformation("GetKYCValidationReportAsync end.");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    var apiResponse = JsonConvert.DeserializeObject<KycTransResponseDTO>(await response.Content.ReadAsStringAsync());

//                    if (apiResponse != null && apiResponse.Success)
//                    {
//                        if (apiResponse.Result is not null)
//                        {
//                            _logger.LogInformation("apiResponse is not null.");
//                            var resultToken = JToken.FromObject(apiResponse.Result);
//                            List<LogReportDTO> logReportsDTO = resultToken.ToObject<List<LogReportDTO>>();

//                            if (logReportsDTO != null)
//                            {
//                                foreach (var log in logReportsDTO)
//                                {

//                                    string lastKycSuccessfulTimestampFormatted = "";
//                                    if (!string.IsNullOrWhiteSpace(log.EndTime))
//                                    {
//                                        string cleanedTimestamp = log.EndTime.Replace("T", " ");

//                                        string[] formats = {
//                                            "yyyy-MM-dd HH:mm:ss.fff", 
//                                            "yyyy-MM-dd HH:mm:ss"  
//                                        };

//                                        if (DateTime.TryParseExact(
//                                            cleanedTimestamp,
//                                            formats,
//                                            CultureInfo.InvariantCulture,
//                                            DateTimeStyles.AssumeLocal,
//                                            out DateTime parsedDateTime
//                                        ))
//                                        {
//                                            lastKycSuccessfulTimestampFormatted = parsedDateTime.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
//                                        }
//                                    }

//                                    try
//                                    {
//                                        if (log.ServiceName == "CARD_STATUS_WITH_OCR" || log.ServiceName == "BATCH_CARD_STATUS" || log.ServiceName == "CARD_STATUS_WITH_MANUAL" || log.ServiceName == "PASSPORT_STATUS_WITH_OCR" || log.ServiceName == "PASSPORT_STATUS_WITH_MANUAL_ENTRY")
//                                        {
//                                            CallStackDTO callStack = null;
//                                            CallStackRequestDTO callStackRequestDTO = null;
//                                            IdValidationCallStackResponseDTO callStackResponseDTO = null;

//                                            if (!string.IsNullOrWhiteSpace(log.CallStack))
//                                            {
//                                                try
//                                                {
//                                                    callStack = JsonConvert.DeserializeObject<CallStackDTO>(log.CallStack);
//                                                    if (callStack != null && callStack.response != null && callStack.request != null)
//                                                    {
//                                                        callStackRequestDTO = callStack.request;
//                                                        callStackResponseDTO = JsonConvert.DeserializeObject<IdValidationCallStackResponseDTO>(callStack.response.ToString());
//                                                    }
//                                                }
//                                                catch (Exception ex)
//                                                {
//                                                    _logger.LogWarning(ex, $"Failed to deserialize IdValidationCallStackDTO. Identifier: {log.Identifier}");
//                                                }
//                                            }

//                                            idValidationReports.Add(new KYCValidationResponseDTO
//                                            {
//                                                identifier = log.Identifier ?? string.Empty,
//                                                deviceId = log.DeviceId ?? string.Empty,
//                                                name = callStackResponseDTO?.name ?? string.Empty,
//                                                identifierName = callStackResponseDTO?.name ?? string.Empty,
//                                                orgName = log.ServiceProviderName ?? string.Empty,
//                                                applicationName = log.ServiceProviderAppName ?? string.Empty,
//                                                date = log.EndTime?.Contains("T") == true ? log.EndTime.Split('T')[0] : string.Empty,
//                                                time = log.EndTime?.Contains("T") == true ? log.EndTime.Split('T')[1] : string.Empty,
//                                                status = log.LogMessageType ?? string.Empty,
//                                                kycMethod = log.ServiceName ?? string.Empty,
//                                                signedResponse = callStackResponseDTO?.signedResponse?.Trim() ?? string.Empty,
//                                                nationality = callStackResponseDTO?.nationality ?? string.Empty,
//                                                photo = callStackResponseDTO?.idCardData?.photography?.cardHolderPhoto ?? string.Empty,
//                                                expiryDate = callStackResponseDTO?.expiryDate ?? string.Empty,
//                                                validationDateTime = lastKycSuccessfulTimestampFormatted,
//                                                issueDate = callStackResponseDTO?.issueDate ?? string.Empty,
//                                                documentStatus = callStackResponseDTO?.documentStatus ?? string.Empty,
//                                                request = callStackRequestDTO
//                                            });

//                                        }
//                                        else
//                                        {
//                                            CallStackDTO callStack = null;
//                                            CallStackRequestDTO callStackRequestDTO = null;
//                                            EmiratesIdResponse callStackResponseDTO = null;

//                                            if (!string.IsNullOrWhiteSpace(log.CallStack))
//                                            {
//                                                try
//                                                {
//                                                    callStack = JsonConvert.DeserializeObject<CallStackDTO>(log.CallStack);
//                                                    if (callStack != null && callStack.response != null && callStack.request != null)
//                                                    {
//                                                        callStackRequestDTO = callStack.request;
//                                                        callStackResponseDTO = JsonConvert.DeserializeObject<EmiratesIdResponse>(callStack.response.ToString());
//                                                    }
//                                                }
//                                                catch (Exception ex)
//                                                {
//                                                    _logger.LogWarning(ex, $"Failed to deserialize IdValidationCallStackDTO. Identifier: {log.Identifier}");
//                                                }
//                                            }

//                                            string base64 = string.Empty;
//                                            if (callStackResponseDTO?.Data?.BinaryObjects?.Count > 0)
//                                            {
//                                                base64 = callStackResponseDTO.Data.BinaryObjects[0]?.BinaryBase64String ?? string.Empty;
//                                            }

//                                            idValidationReports.Add(new KYCValidationResponseDTO
//                                            {
//                                                identifier = log.Identifier ?? string.Empty,
//                                                deviceId = log.DeviceId ?? string.Empty,
//                                                name = callStackResponseDTO?.Name ?? string.Empty,
//                                                identifierName = callStackResponseDTO?.Name ?? string.Empty,
//                                                orgName = log.ServiceProviderName ?? string.Empty,
//                                                applicationName = log.ServiceProviderAppName ?? string.Empty,
//                                                date = log.EndTime?.Contains("T") == true ? log.EndTime.Split('T')[0] : string.Empty,
//                                                time = log.EndTime?.Contains("T") == true ? log.EndTime.Split('T')[1] : string.Empty,
//                                                status = log.LogMessageType ?? string.Empty,
//                                                kycMethod = log.ServiceName ?? string.Empty,
//                                                signedResponse = callStackResponseDTO?.SignedResponse?.Trim() ?? string.Empty,
//                                                nationality = callStackResponseDTO?.Nationality ?? string.Empty,
//                                                photo = base64,
//                                                validationDateTime = lastKycSuccessfulTimestampFormatted,
//                                                expiryDate = callStackResponseDTO?.ExpiryDate ?? string.Empty,
//                                                request = callStackRequestDTO,
//                                                issueDate = callStackResponseDTO?.Data?.ActivePassport?.IssueDate ?? string.Empty,
//                                                documentStatus = callStackResponseDTO?.DocumentStatus,
//                                            });
//                                        }
//                                    }


//                                    catch (Exception innerEx)
//                                    {
//                                        _logger.LogWarning(innerEx, "Error while mapping individual log record.");
//                                    }
//                                }
//                                return new PaginatedList<KYCValidationResponseDTO>(
//                                    idValidationReports,
//                                    apiResponse.CurrentPage,
//                                    apiResponse.PerPage,
//                                    apiResponse.TotalPages,
//                                    apiResponse.TotalCount
//                                );
//                            }
//                        }
//                    }
//                    else
//                    {
//                        _logger.LogWarning("API responded with success = false. Message: {Message}", apiResponse?.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError("KYC API response failed. Status: {StatusCode}, URL: {Url}", response.StatusCode, response.RequestMessage?.RequestUri);
//                }
//            }
//            catch (JsonException jsonEx)
//            {
//                _logger.LogError(jsonEx, "JSON Deserialization failed.");
//            }
//            catch (HttpRequestException httpEx)
//            {
//                _logger.LogError(httpEx, "HTTP request to KYC service failed.");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Unexpected error in GetKYCValidationReportAsync.");
//            }

//            return PaginatedList<KYCValidationResponseDTO>.Create(new List<KYCValidationResponseDTO>(), page, perPage, 0, 0);
//        }


//        public async Task<OrgKycSummaryDTO> GetOrganizationIdValidationSummaryAsync(string orgId)
//        {
//            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:KYCServiceBaseAddress"]);
//            client.Timeout = TimeSpan.FromSeconds(30);
//            try
//            {
//                _logger.LogInformation("GetOrganizationIdValidationSummaryAsync start.");
//                HttpResponseMessage response = await client.GetAsync($"api/kyc-summary/organization/{orgId}");
//                _logger.LogInformation("GetOrganizationIdValidationSummaryAsync end.");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation("GetOrganizationIdValidationSummaryAsync api respone success.");
//                        var resultToken = JToken.FromObject(apiResponse.Result);
//                        var orgSummary = resultToken.ToObject<OrgKycSummaryDTO>();

//                        orgSummary.totalKycDone = orgSummary.totalKycCountSuccessful + orgSummary.totalKycCountFailed;
//                        orgSummary.totalKycDoneCurrentMonth = orgSummary.totalKycCountSuccessfulCurrentMonth + orgSummary.totalKycCountFailedCurrentMonth;

//                        //var orgKycDictionary = await GetOrgKycMethodsDict();

//                        //if (orgKycDictionary.TryGetValue(orgId, out var kycMethods))
//                        //{
//                        //    orgSummary.KycMethods = kycMethods;
//                        //}
//                        //else
//                        //{
//                        //    orgSummary.KycMethods = new List<string>();
//                        //}
//                        _logger.LogError("GetOrganizationIdValidationSummaryAsync api respone success.");
//                        return orgSummary;
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                           $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }
//            return null;
//        }

//        private async Task<KYCDevicesResponseDTO?> FetchKycDevicesApi(string? orgId)
//        {
//            var _client = _httpClientFactory.CreateClient("ignoreSSL");
//            _client.BaseAddress = new Uri(_configuration["APIServiceLocations:KYCDevicesServiceBaseAddress"]);

//            _logger.LogInformation("FetchKycDevicesApi start.");
//            var response = await _client.GetAsync($"api/KycDevices/GetOrganizationKycDevicesList/{orgId}");
//            _logger.LogInformation("FetchKycDevicesApi end.");
//            if (!response.IsSuccessStatusCode)
//                return null;

//            var json = await response.Content.ReadAsStringAsync();

//            return JsonConvert.DeserializeObject<KYCDevicesResponseDTO>(json);
//        }


//        public async Task<ServiceResult> GetKycDevicesOfOrg(string? orgId)
//        {

//            try
//            {
//                var apiResponse = await FetchKycDevicesApi(orgId);

//                if (apiResponse == null || !apiResponse.success || apiResponse.Result == null)
//                {
//                    return new ServiceResult(false, "KYC devices not found for the organization");
//                }

//                return new ServiceResult(true, "KYC devices fetched successfully", apiResponse.Result);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error fetching KYC device list.");
//                return new ServiceResult(false, "An error occurred while fetching the KYC device list");
//            }
//        }


//        public async Task<ServiceResult> GetKycDevicesCountOfOrg(string? orgId)
//        {
//            try
//            {
//                var apiResponse = await FetchKycDevicesApi(orgId);

//                if (apiResponse == null || !apiResponse.success || apiResponse.Result == null)
//                {
//                    return new ServiceResult(false, "KYC devices not found for the organization", 0);
//                }

//                int uniqueCount = apiResponse.Result.Count();

//                return new ServiceResult(true, "KYC device count fetched successfully", uniqueCount);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error fetching KYC device count.");
//                return new ServiceResult(false, "An error occurred while fetching the KYC device count", 0);
//            }
//        }



//        public async Task<PaginatedList<LogReportDTO>> GetAuthenticationFailedLogReportAsync(string startDate, string endDate, int page = 1, int perPage = 10)
//        {
//            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:AuthenticationFailedLogBaseAddress"]);

//            try
//            {
//                string json = JsonConvert.SerializeObject(
//                    new
//                    {
//                        StartDate = startDate,
//                        EndDate = endDate,
//                        TransactionStatus = "FAILED",
//                        TransactionType = "AUTHENTICATION",
//                        PerPage = perPage
//                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = await client.PostAsync($"api/audit-logs/onboarding-failure/{page}", content);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
//                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
//                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), 10, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}");
//                }

//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<string[]> GetKycMethodsListAysnc(string orgUid)
//        {
//            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
//            client.BaseAddress = new Uri(_configuration["APIServiceLocations:WalletSeerviceBaseAddress"]);
//            try
//            {
//                _logger.LogInformation("GetKycMethodsListAysnc start.");
//                HttpResponseMessage response = await client.GetAsync($"api/KycServices/GetOrgKycMethodsList/{orgUid}");
//                _logger.LogInformation("GetKycMethodsListAysnc end.");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation("GetKycMethodsListAysnc api respone success.");
//                        return JsonConvert.DeserializeObject<string[]>(apiResponse.Result.ToString());
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                       $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }

//            return null;
//        }

//        public async Task<ServiceResult> BlockKycDevice(KycDeviceBlockDTO dto)
//        {
//            try
//            {
//                HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
//                client.BaseAddress = new Uri(_configuration["APIServiceLocations:KYCDevicesServiceBaseAddress"]);

//                string url = "api/KycDevices/DeregisterKycDevice";

//                string json = JsonConvert.SerializeObject(dto);

//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//                _logger.LogInformation("BlockKycDevice api call start");
//                var response = client.PostAsync(url, content).Result;
//                _logger.LogInformation("BlockKycDevice api call end");

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        _logger.LogInformation("BlockKycDevice api respone success.");
//                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message, apiResponse.Result);
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//                return null;
//            }
//        }



//        public static string DecodeBase64Payload(string base64Payload)
//        {
//            int padding = 4 - (base64Payload.Length % 4);
//            if (padding != 4)
//            {
//                base64Payload = base64Payload.PadRight(base64Payload.Length + padding, '=');
//            }

//            base64Payload = base64Payload.Replace('-', '+').Replace('_', '/');

//            byte[] bytes = Convert.FromBase64String(base64Payload);

//            return Encoding.UTF8.GetString(bytes);
//        }

//        private static byte[] ExtractDerBytesFromPem(string pem, string label)
//        {
//            string header = $"-----BEGIN {label}-----";
//            string footer = $"-----END {label}-----";

//            int start = pem.IndexOf(header, StringComparison.Ordinal);
//            int end = pem.IndexOf(footer, StringComparison.Ordinal);

//            if (start < 0 || end < 0)
//                throw new FormatException($"PEM format is invalid. Missing {label} headers.");

//            string base64 = pem.Substring(start + header.Length, end - start - header.Length)
//                .Replace("\r", "").Replace("\n", "").Trim();

//            return Convert.FromBase64String(base64);
//        }
//    }
//}





