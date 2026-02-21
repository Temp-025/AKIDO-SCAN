//using EnterpriseGatewayPortal.Core.Domain.Repositories;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json.Serialization;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class QrCredentialService : IQrCredentialService
//    {
//        private readonly HttpClient _client;
//        private readonly HttpClient _Idpclient;
//        private readonly ILogger<DataPivotService> _logger;
//        private readonly IConfiguration _configuration;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IEmailSenderService _emailSenderService;
//        private readonly IPaymentService _paymentService;
//        public QrCredentialService(HttpClient httpClient, HttpClient Idpclient, IPaymentService paymentService, IConfiguration configuration, ILogger<DataPivotService> logger, IUnitOfWork unitOfWork, IEmailSenderService emailSenderService)
//        {
//            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:WalletSeerviceBaseAddress"]);
//            Idpclient.BaseAddress = new Uri(configuration["Config:IDP_Config:IDP_url"]);
//            _logger = logger;
//            _client = httpClient;
//            _Idpclient = Idpclient;
//            _unitOfWork = unitOfWork;
//            _emailSenderService = emailSenderService;
//            _configuration = configuration;
//            _paymentService = paymentService;
//        }



//        public async Task<IEnumerable<QrCredentialDTO>> GetAllQRCredentialsList(string orgUid)
//        {
//            try
//            {

//                HttpResponseMessage response = await _client.GetAsync($"QrCredential/GetCredentialListByOrgUid?orgUid={orgUid}");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var qrCredentialList = JsonConvert.DeserializeObject<IEnumerable<QrCredentialDTO>>(apiResponse.Result.ToString());
//                        return qrCredentialList;
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

//        public async Task<ServiceResult> AddQRCredentialsAsync(QrCredentialDTO QRCredentialAddDTO)
//        {

//            try
//            {
//                string json = JsonConvert.SerializeObject(QRCredentialAddDTO,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
//                HttpResponseMessage response = await _client.PostAsync("QrCredential/CreateCredential", content);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);

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

//        public async Task<ServiceResult> QRTestCredentialByDocumentId(QrTestCredentialDTO qrTestCredentialDTO)
//        {
//            try
//            {
//                string json = JsonConvert.SerializeObject(qrTestCredentialDTO,
//                   new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
//                _logger.LogInformation("Test Credential Started");
//                HttpResponseMessage response = await _client.PostAsync("QrCredential/TestCredential", content);
//                _logger.LogInformation("Test Credential Ended");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        //var list = await _unitOfWork.OrganizationDetail.GetAllOrganizationDetailAsync();
//                        //var mailBody = "";
//                        //foreach (var org in list)
//                        //{
//                        //    mailBody = $"Greetings! Administrator,\n\nEnterprise Admin has submitted a Test Credential request for the organization \"{org.OrgName}\". Kindly do the needful.\n\nThanks!";

//                        //}
//                        //// Retrieve email recipients from the configuration file
//                        //var emailRecipients = _configuration.GetSection("EmailRecipients").Get<string[]>();

//                        //// Create the message
//                        //var message = new Message(
//                        //    emailRecipients, // Pass the array of email addresses
//                        //    "Welcome to EnterpriseGateway Portal", // Subject
//                        //    mailBody // Body of the email
//                        //);


//                        try
//                        {
//                            //await _emailSenderService.SendEmail(message);
//                            return new ServiceResult(true, apiResponse.Message);
//                        }
//                        catch (Exception ex)
//                        {
//                            _logger.LogError("Send Email Failed {0}", ex.Message);

//                            return new ServiceResult(false, "Unable to send email");
//                        }

//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);

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

//        public async Task<IEnumerable<QrCredentialVerifierDTO>> GetAllQRCredentialsListByOrgId(string orgID)
//        {
//            try
//            {

//                HttpResponseMessage response = await _client.GetAsync($"api/QrCredentialVerifiers/GetQrCredentialVerifiersListByOrganizationId/{orgID}");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var credentialList = JsonConvert.DeserializeObject<IEnumerable<QrCredentialVerifierDTO>>(apiResponse.Result.ToString());
//                        return credentialList;
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

//        public async Task<string[]> GetQRCredentialNamesAndIdListAysnc(string orgId)
//        {
//            try
//            {

//                HttpResponseMessage response = await _client.GetAsync($"QrCredential/GetVerifiableCredentialList?orgid={orgId}");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
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

//        public async Task<QrCredentialDTO> GetQRCredentialById(string id)
//        {

//            try
//            {

//                HttpResponseMessage response = await _client.GetAsync($"QrCredential/GetCredentialByUid/{id}");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var credential = JsonConvert.DeserializeObject<QrCredentialDTO>(apiResponse.Result.ToString());
//                        return credential;

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

//        public async Task<ServiceResult> AddQRVerifyCredentialRequestAsync(QrCredentialVerifierDTO verifyCredential)
//        {
//            try
//            {
//                string json = JsonConvert.SerializeObject(verifyCredential,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
//                HttpResponseMessage response = await _client.PostAsync("api/QrCredentialVerifiers/CreateQrCredentialVerifier", content);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return new ServiceResult(true, apiResponse.Message);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);

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

//        public async Task<QrCredentialVerifierDTO> GetQRVerifyCredentialById(int id)
//        {

//            try
//            {

//                HttpResponseMessage response = await _client.GetAsync($"api/QrCredentialVerifiers/GetQrCredentialVerifierById/{id}");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var credential = JsonConvert.DeserializeObject<QrCredentialVerifierDTO>(apiResponse.Result.ToString());
//                        return credential;

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

//        public async Task<ServiceResult> UpdateVerifyQRCredentialRequestAsync(QrCredentialVerifierDTO verifyCredential)
//        {
//            try
//            {
//                string json = JsonConvert.SerializeObject(verifyCredential,
//                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
//                HttpResponseMessage response = await _client.PostAsync("api/QrCredentialVerifiers/UpdateQrCredentialVerifier", content);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return new ServiceResult(true, apiResponse.Message);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);

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

//        public async Task<IEnumerable<QrCredentialVerifierDTO>> GetAllCredentialsVerifieriesListByOrgId(string orgID)
//        {
//            try
//            {
//                HttpResponseMessage response = await _client.GetAsync($"api/QrCredentialVerifiers/GetCredentialVerifierListByIssuerId/{orgID}");
//                //HttpResponseMessage response = await _client.GetAsync($"api/CredentialVerifiers/GetCredentialVerifierListByIssuerId/0763a957-3410-4500-8763-841ebb463090");
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var credentialList = JsonConvert.DeserializeObject<IEnumerable<QrCredentialVerifierDTO>>(apiResponse.Result.ToString());
//                        return credentialList;
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                    $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }
//            return null;
//        }


//        public async Task<QrCredentialVerifierDTO> GetVerifyCredentialRequestById(int id)
//        {

//            try
//            {

//                HttpResponseMessage response = await _client.GetAsync($"api/QrCredentialVerifiers/GetQrCredentialVerifierById/{id}");

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var credential = JsonConvert.DeserializeObject<QrCredentialVerifierDTO>(apiResponse.Result.ToString());
//                        return credential;


//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                    $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }
//            return null;
//        }

//        public async Task<ServiceResult> ActivateWalletVerifierSubscribeRequest(ApproveRejectWalletVerifierReqDTO dto)
//        {
//            try
//            {
//                string json = JsonConvert.SerializeObject(dto,
//                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
//                HttpResponseMessage response = await _client.PostAsync("api/QrCredentialVerifiers/ActivateCredential", content);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);


//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                    $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }
//            return null;
//        }

//        public async Task<ServiceResult> RejectWalletVerifierSubscribeRequest(ApproveRejectWalletVerifierReqDTO dto)
//        {
//            try
//            {
//                string json = JsonConvert.SerializeObject(dto,
//                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
//                HttpResponseMessage response = await _client.PostAsync("api/QrCredentialVerifiers/RejectCredential", content);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(false, apiResponse.Message);


//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
//                    $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, ex.Message);
//            }
//            return null;
//        }

//    }
//}
