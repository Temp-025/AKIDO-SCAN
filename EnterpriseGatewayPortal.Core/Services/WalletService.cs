using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class WalletService : IWalletService
    {
        private readonly HttpClient _client;
        private readonly HttpClient _Idpclient;
        private readonly HttpClient _serviceProviderClient;
        private readonly HttpClient _simulationClient;
        private readonly HttpClient _socialBenefitClient;
        private readonly ILogger<WalletService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSenderService _emailSenderService;
        //private readonly IPaymentService _paymentService;
        public WalletService(HttpClient httpClient, HttpClient Idpclient, HttpClient serviceProviderClient, HttpClient socialBenefitClient, HttpClient simulationClient, IConfiguration configuration, ILogger<WalletService> logger, IUnitOfWork unitOfWork, IEmailSenderService emailSenderService)
        {
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:WalletSeerviceBaseAddress"]!);
            Idpclient.BaseAddress = new Uri(configuration["Config:IDP_Config:IDP_url"]!);
            serviceProviderClient.BaseAddress = new Uri(configuration["APIServiceLocations:WalletServiceProviderServiceBaseAddress"]!);
            simulationClient.BaseAddress = new Uri(configuration["APIServiceLocations:SimulationServiceBaseAddress"]!);
            socialBenefitClient.BaseAddress = new Uri(configuration["APIServiceLocations:SocialProtectionBaseAddress"]!);
            _logger = logger;
            _client = httpClient;
            _Idpclient = Idpclient;
            _serviceProviderClient = serviceProviderClient;
            _unitOfWork = unitOfWork;
            _emailSenderService = emailSenderService;
            _configuration = configuration;
            _simulationClient = simulationClient;
            _socialBenefitClient = socialBenefitClient;
        }

        public async Task<IEnumerable<CredentialListDTO>> GetAllCredentialsList(string orgUid)
        {
            try
            {

                HttpResponseMessage response = await _client.GetAsync($"Credential/GetCredentialListByOrgUid?orgUid={orgUid}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var credentialList = JsonConvert.DeserializeObject<IEnumerable<CredentialListDTO>>(apiResponse.Result!.ToString()!);
                        _logger.LogInformation("Credential List Response: {data}",
                            JsonConvert.SerializeObject(credentialList, Formatting.Indented));
                        return credentialList;
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
        public async Task<IEnumerable<VerifyCredentialDTO>> GetAllCredentialsListByOrgId(string orgID)
        {
            try
            {

                HttpResponseMessage response = await _client.GetAsync($"api/CredentialVerifiers/GetCredentialVerifiersListByOrganizationId/{orgID}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var credentialList = JsonConvert.DeserializeObject<IEnumerable<VerifyCredentialDTO>>(apiResponse.Result!.ToString()!);
                        return credentialList;
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

        public async Task<string[]> GetCategoryNamesAndIdAysnc()
        {
            try
            {
                _logger.LogInformation("GetCategoryNameAndIdList api call start");
                HttpResponseMessage response = await _Idpclient.GetAsync($"api/MultiPivot/GetCategoryNameAndIdList");
                _logger.LogInformation("GetCategoryNameAndIdList api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        _logger.LogInformation("GetCategoryNameAndIdList api response : " + apiResponse.Result);
                        return JsonConvert.DeserializeObject<string[]>(apiResponse.Result!.ToString()!);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<string[]> GetCredentialNamesAndIdListAysnc(string orgId)
        {
            try
            {

                HttpResponseMessage response = await _client.GetAsync($"Credential/GetVerifiableCredentialList?orgid={orgId}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<string[]>(apiResponse.Result!.ToString()!);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }


        public async Task<Dictionary<string, string>> GetAuthSchemeTypesAysnc()
        {
            try
            {
                _logger.LogInformation("GetAuthSchemeList api call start");
                HttpResponseMessage response = await _client.GetAsync($"Credential/GetAuthSchemeList");
                _logger.LogInformation("GetAuthSchemeList api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        _logger.LogInformation("GetAuthSchemeList api response : " + apiResponse.Result);
                        var schemeDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiResponse.Result!.ToString()!);
                        return schemeDict;
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }


        public async Task<string[]> GetCredentialNamesAndIdAysnc(string orgId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"Credential/GetCredentialNameIdList?credentialId={orgId}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<string[]>(apiResponse.Result!.ToString()!);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<IEnumerable<VerifyCredentialDTO>> GetAllCredentialsVerifieriesListByOrgId(string orgID)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/CredentialVerifiers/GetCredentialVerifierListByIssuerId/{orgID}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var credentialList = JsonConvert.DeserializeObject<IEnumerable<VerifyCredentialDTO>>(apiResponse.Result!.ToString()!);
                        return credentialList;
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

        public async Task<VerifyCredentialDTO> GetVerifyCredentialRequestById(int id)
        {

            try
            {

                HttpResponseMessage response = await _client.GetAsync($"api/CredentialVerifiers/GetCredentialVerifierById/{id}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var credential = JsonConvert.DeserializeObject<VerifyCredentialDTO>(apiResponse.Result!.ToString()!);
                        return credential;

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

        public async Task<ServiceResult> ActivateWalletVerifierSubscribeRequest(ApproveRejectWalletVerifierReqDTO dto)
        {
            try
            {
                string json = JsonConvert.SerializeObject(dto,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("api/CredentialVerifiers/ActivateCredential", content);
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
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                    $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }

        public async Task<ServiceResult> RejectWalletVerifierSubscribeRequest(ApproveRejectWalletVerifierReqDTO dto)
        {
            try
            {
                string json = JsonConvert.SerializeObject(dto,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("api/CredentialVerifiers/RejectCredential", content);
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
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                    $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }

        public async Task<ServiceResult> AddCredentialsAsync(CredentialListDTO CredentialAddDTO)
        {
            try
            {
                string json = JsonConvert.SerializeObject(CredentialAddDTO,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("Credential/CreateCredential", content);
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
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }

        public async Task<ServiceResult> AddVerifyCredentialRequestAsync(VerifyCredentialDTO verifyCredential)
        {
            try
            {
                string json = JsonConvert.SerializeObject(verifyCredential,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("api/CredentialVerifiers/CreateCredentialVerifier", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }
        public async Task<ServiceResult> UpdateVerifyCredentialRequestAsync(VerifyCredentialDTO verifyCredential)
        {
            try
            {
                string json = JsonConvert.SerializeObject(verifyCredential,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("api/CredentialVerifiers/UpdateCredentialVerifier", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }

        public async Task<ServiceResult> RevokeredentialsAsync(RevokeCredentialDTO revokeCredentialDTO)
        {
            try
            {
                string json = JsonConvert.SerializeObject(revokeCredentialDTO,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("ProvisionStatus/RevokeProvision", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }

        public async Task<CredentialListDTO> GetCredentialById(string id)
        {

            try
            {
                _logger.LogInformation("GetCredentialByUid api call start");

                HttpResponseMessage response = await _client.GetAsync($"Credential/GetCredentialByUid/{id}");
                _logger.LogInformation("GetCredentialByUid api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        _logger.LogInformation("GetCredentialByUid api response : " + apiResponse.Result);
                        var credential = JsonConvert.DeserializeObject<CredentialListDTO>(apiResponse.Result!.ToString()!);
                        return credential;

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

        public async Task<DefaultDataAttrbutesDTO> GetAttrbutesList()
        {
            try
            {
                _logger.LogInformation("GetAttributes api call start");
                HttpResponseMessage response = await _client.GetAsync($"Credential/GetAttributes");
                _logger.LogInformation("GetAttributes api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);

                    if (apiResponse.Success)
                    {
                        _logger.LogInformation("GetAttributes api response : " + apiResponse.Result);
                        var dataAttributes = JsonConvert.DeserializeObject<List<DataAttribute>>(apiResponse.Result!.ToString()!);
                        return new DefaultDataAttrbutesDTO
                        {
                            DataAttributes = dataAttributes
                        };
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

        public async Task<DefaultDataAttrbutesDTO> GetPidAttrbutesList()
        {
            try
            {
                HttpResponseMessage response = await _simulationClient.GetAsync($"PIDSimulation/pid/attributes");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);

                    if (apiResponse.Success)
                    {
                        var dataAttributes = JsonConvert.DeserializeObject<List<DataAttribute>>(apiResponse.Result!.ToString()!);
                        return new DefaultDataAttrbutesDTO
                        {
                            DataAttributes = dataAttributes
                        };
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

        public async Task<DefaultDataAttrbutesDTO> GetMdlAttrbutesList()
        {
            try
            {
                HttpResponseMessage response = await _simulationClient.GetAsync($"MDLSimulations/driving-license/attribute");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);

                    if (apiResponse.Success)
                    {
                        var dataAttributes = JsonConvert.DeserializeObject<List<DataAttribute>>(apiResponse.Result!.ToString()!);
                        return new DefaultDataAttrbutesDTO
                        {
                            DataAttributes = dataAttributes
                        };
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

        public async Task<DefaultDataAttrbutesDTO> GetSocialBenefitAttrbutesList()
        {
            try
            {
                HttpResponseMessage response = await _socialBenefitClient.GetAsync($"api/redeem/attributes");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);

                    if (apiResponse.Success)
                    {
                        var dataAttributes = JsonConvert.DeserializeObject<List<DataAttribute>>(apiResponse.Result!.ToString()!);
                        return new DefaultDataAttrbutesDTO
                        {
                            DataAttributes = dataAttributes
                        };
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
        public async Task<VerifyCredentialDTO> GetVerifyCredentialById(int id)
        {

            try
            {

                HttpResponseMessage response = await _client.GetAsync($"api/CredentialVerifiers/GetCredentialVerifierById/{id}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var credential = JsonConvert.DeserializeObject<VerifyCredentialDTO>(apiResponse.Result!.ToString()!);
                        return credential;

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
        public async Task<IEnumerable<WalletConfigurationDTO>> GetWalletConfiguration(string credentialUId)
        {

            try
            {

                HttpResponseMessage response = await _client.GetAsync($"Credential/GetCredentialDetails?credentialId={credentialUId}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var walletConfiguration = JsonConvert.DeserializeObject<IEnumerable<WalletConfigurationDTO>>(apiResponse.Result!.ToString()!);
                        return walletConfiguration;

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

        public async Task<ServiceResult> TestCredentialByDocumentId(TestCredentialRequestDTO testCredentialRequestDTO)
        {
            try
            {
                string json = JsonConvert.SerializeObject(testCredentialRequestDTO,
                   new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                _logger.LogInformation("Test Credential Started");
                HttpResponseMessage response = await _client.PostAsync("Credential/TestCredential", content);
                _logger.LogInformation("Test Credential Ended");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {

                        return new ServiceResult(true, apiResponse.Message);

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
            return null;
        }

        public async Task<ServiceResult> SendToApprovalRequest(ApprovalRequestDTO dto)
        {
            try
            {
                string json = JsonConvert.SerializeObject(dto,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("Credential/SendToApproval", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }




        public async Task<ServiceResult> UpdateCredentialsAsync(CredentialListDTO credentialListDTO)
        {
            try
            {
                _logger.LogInformation("UpdateCredentialsAsync API call start");
                string jsonPayload = JsonConvert.SerializeObject(credentialListDTO,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync($"Credential/UpdateCredential", content);

                _logger.LogInformation("UpdateCredentialsAsync API call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());

                    if (apiResponse.Success)
                    {
                        _logger.LogInformation(apiResponse.Message);

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
                    return new ServiceResult("Internal Error");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

            }
            return new ServiceResult(false, "An error occurred while updating credential. Please try later.");


        }

        public async Task<IEnumerable<WalletDomainDTO>> GetCredentialConsentDetails()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/WalletDomainApi/GetDomainsList");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    _logger.LogInformation("GetCredentialConsentDetails api call start");
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    _logger.LogInformation("GetCredentialConsentDetails api call end");
                    if (apiResponse.Success)
                    {
                        var DomainConsent = JsonConvert.DeserializeObject<IEnumerable<WalletDomainDTO>>(apiResponse.Result!.ToString()!);
                        return DomainConsent;

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

        public async Task<List<OrganizationCategoryDTO>> GetOrgCategoriesList()
        {
            try
            {
                _logger.LogInformation("categories api call start");
                HttpResponseMessage response = await _serviceProviderClient.GetAsync($"public/api/get/all/categories");
                _logger.LogInformation("categories api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);

                    if (apiResponse.Success)
                    {
                        _logger.LogInformation("categories api response : " + apiResponse.Result);
                        var orgCategories = JsonConvert.DeserializeObject<List<OrganizationCategoryDTO>>(apiResponse.Result!.ToString()!);
                        return orgCategories;

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

    }
}
