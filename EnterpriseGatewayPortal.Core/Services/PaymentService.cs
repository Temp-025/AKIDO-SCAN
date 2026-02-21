//using EnterpriseGatewayPortal.Core.Constants;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication.Payment;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.Net;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class PaymentService : IPaymentService
//    {
//        private readonly HttpClient _client;
//        private readonly IConfiguration _configuration;        
//        private readonly ILogger<PaymentService> _logger;

//        public PaymentService(ILogger<PaymentService> logger,
//            IConfiguration configuration,            
//             HttpClient httpClient)
//        {
//            _configuration = configuration;
//            _client = httpClient;
//            _client.Timeout = TimeSpan.FromMinutes(10);
//            _client.BaseAddress = new Uri(_configuration["Config:IDP_Config:IDP_url"]);
//            _logger = logger;
//        }
//        public async Task<ServiceResult> IsCreditAvailable(UserDTO userdata, bool isEsealPresent, bool isSignaturePresent = false)
//        {
//            try
//            {
//                var Url = "";
//                if (userdata.AccountType == AccountTypeConstants.Self)
//                {
//                    Url = _configuration.GetValue<string>("Config:PriceDetailsUrl") + "get/remaining-credits?suid=" + userdata.Suid;
//                }
//                else
//                {
//                    Url = _configuration.GetValue<string>("Config:PriceDetailsUrl") + "get-org-rem-credits?orgId=" + userdata.OrganizationId;
//                }

//                var response = await _client.GetAsync(Url);
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var result = false;
//                        var msg = "";

//                        if (userdata.AccountType == AccountTypeConstants.Self)
//                        {
//                            result = int.Parse(apiResponse.Result.ToString()) > 0;
//                            msg = (result) ? "Credits available" : "Credits are not available";
//                            return new ServiceResult(true, msg, result);
//                        }
//                        else
//                        {
//                            JObject obj = JsonConvert.DeserializeObject<JObject>(apiResponse.Result.ToString());
//                            if (obj["postPaid"].Value<bool>())
//                            {
//                                return new ServiceResult(true, apiResponse.Message,true);
//                            }
//                            else
//                            {
//                                if (isSignaturePresent)
//                                {
//                                    var value = obj["digital_SIGNATURE"].Value<double>();
//                                    result = value > 0;
//                                    msg = (result) ? "Credits available" : "Credits are not available";
//                                    if (!result)
//                                        return new ServiceResult(true, msg, result);
//                                }
//                                if (isEsealPresent)
//                                {
//                                    var value = obj["eseal_SIGNATURE"].Value<double>();
//                                    result = value > 0;
//                                    msg = (result) ? "Credits available" : "Eseal credits are not available";
//                                    if (!result)
//                                        return new ServiceResult(true, msg, result);
//                                }

//                                return new ServiceResult(true, "Credits available", result);
//                            }

//                        }

//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception e)
//            {
//                _logger.LogError("IsCreditAvailable  Exception :  {0}", e.Message);
//            }

//            return new ServiceResult("IsCreditAvailable Exception");
//        }

//        public async Task<ServiceResult> GetCreditDeatails(UserDTO userdata)
//        {
//            try
//            {
//                var response = await _client.GetAsync(_configuration.GetValue<string>("Config:PriceDetailsUrl") + "get-balance-sheet-subscriber?suid=" + userdata.Suid + "&serviceId=1");

//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
//                    if (apiResponse.Success)
//                    {
//                        var customJsonSerializerSettings = new JsonSerializerSettings
//                        {
//                            NullValueHandling = NullValueHandling.Ignore
//                        };
//                        CreditDetails details = JsonConvert.DeserializeObject<CreditDetails>(apiResponse.Result.ToString(), customJsonSerializerSettings);
//                        var Creditdetails = new PaymentDetails
//                        {
//                            AvailableCredit = details.totalCredits - details.totalDebits,
//                            TotalCredit = details.totalCredits
//                        };

//                        return new ServiceResult(true, apiResponse.Message, Creditdetails);
//                    }
//                    else
//                    {
//                        _logger.LogError(apiResponse.Message);
//                        return new ServiceResult(apiResponse.Message);
//                    }
//                }
//                else
//                {
//                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
//                               $"with status code={response.StatusCode}");
//                }
//            }
//            catch (Exception e)
//            {
//                _logger.LogError("IsCreditAvailable  Exception :  {0}", e.Message);
//            }

//            return new ServiceResult("GetCreditDeatails Exception");
//        }
//    }
//}
