using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnterpriseGatewayPortal.Web.Helper
{
    public class LoginHelper
    {
        public IConfiguration configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfigurationService _configurationService;
        private readonly IDeviceService _deviceService;
        private readonly ILocalClientService _localClientService;
        public LoginHelper(IConfiguration _configuration,
            IHttpClientFactory httpClientFactory,
            IConfigurationService configurationService,
            IDeviceService deviceService,
            ILocalClientService localClientService)
        {
            _httpClientFactory = httpClientFactory;
            configuration = _configuration;
            _configurationService = configurationService;
            _deviceService = deviceService;
            _localClientService = localClientService;
        }
        public async Task<string> GetAuthorizationUrl(string nonce, string state)
        {
            try
            {
                //var privatekeyConfiguration = new Configuration();
                //var ClientDetailsConfiguration = new Configuration();
                //var configuartionListinDb = await _configurationService.GetAllConfiguration();
                //var configurationList = (IEnumerable<Configuration>)configuartionListinDb.Resource;
                //foreach (var configuration in configurationList)
                //{
                //    if(configuration.Name== "PrivateKey")
                //    {
                //        privatekeyConfiguration = configuration;
                //    }
                //    if (configuration.Name == "ClientDetails")
                //    {
                //        ClientDetailsConfiguration = configuration;
                //    }
                //}
                var licenseResult = await _deviceService.ReadLicence();

                if (licenseResult == null || !licenseResult.Success)
                {
                    return null;
                }

                var licenceDetails = (LicenceDTO)licenseResult.Resource;

                var clientDetails = await _localClientService.GetClientByClientIdAsync(licenceDetails.clientId);
                if (clientDetails == null)
                {
                    return null;
                }

                var openid = configuration.GetValue<Boolean>("openid");
                var authorizationURl = configuration.GetValue<string>("DTIDP_Config:authorizeUrl");
                authorizationURl = authorizationURl +
                       "?client_id={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}";

                var scope = configuration.GetValue<string>("DTIDP_Config:scope");
                //var encryption = configuration.GetValue<bool>("EncryptionEnabled");
                //var clientDetails = JsonConvert.DeserializeObject<ClientDetailsDTO>(ClientDetailsConfiguration.Value);
                if (openid)
                {
                    var JwtObject = new JwtTokenObj();

                    JwtObject.Issuer = clientDetails.ClientId;
                    JwtObject.Audience = configuration["DTIDP_Config:Issuer"];
                    JwtObject.RedirecUri = clientDetails.RedirectUri;
                    JwtObject.ResponseType = "code";
                    JwtObject.Expiry = 60;
                    JwtObject.Scope = scope;
                    JwtObject.State = state;
                    JwtObject.Nonce = nonce;
                    var response = JWTTokenManager.GenerateJWTToken(JwtObject);
                    //var response = JWTTokenManager.GenerateJWTToken(JwtObject, encryption);
                    authorizationURl = authorizationURl + "&nonce={4}&request={5}";
                    return String.Format(authorizationURl, clientDetails.ClientId, clientDetails.RedirectUri,
                                     scope, state, nonce, response);
                }

                return String.Format(authorizationURl, clientDetails.ClientId, clientDetails.RedirectUri,
                                     scope.Replace("openid ", ""), state);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<JObject> GetAccessToken(string code)
        {
            try
            {
                //var privatekeyConfiguration = new Configuration();
                //var ClientDetailsConfiguration = new Configuration();
                //var configuartionListinDb = await _configurationService.GetAllConfiguration();
                //var configurationList = (IEnumerable<Configuration>)configuartionListinDb.Resource;
                //foreach (var configuration in configurationList)
                //{
                //    if (configuration.Name == "PrivateKey")
                //    {
                //        privatekeyConfiguration = configuration;
                //    }
                //    if (configuration.Name == "ClientDetails")
                //    {
                //        ClientDetailsConfiguration = configuration;
                //    }
                //}
                //var clientDetails = JsonConvert.DeserializeObject<ClientDetailsDTO>(ClientDetailsConfiguration.Value);
                var licenseResult = await _deviceService.ReadLicence();

                if (licenseResult == null || !licenseResult.Success)
                {
                    return null;
                }

                var licenceDetails = (LicenceDTO)licenseResult.Resource;

                var clientDetails = await _localClientService.GetClientByClientIdAsync(licenceDetails.clientId);
                if (clientDetails == null)
                {
                    return null;
                }
                var openid = configuration.GetValue<Boolean>("openid");

                var TokenUrl = configuration.GetValue<string>("DTIDP_Config:dt_tokenUrl");

                if (string.IsNullOrEmpty(TokenUrl))
                {
                    throw new InvalidOperationException("Token URL cannot be empty.");
                }

                var encryption = configuration.GetValue<bool>("EncryptionEnabled");

                var data = new Dictionary<string, string>
                {
                   { "code", code },
                   { "client_id", clientDetails.ClientId },
                   { "redirect_uri", clientDetails.RedirectUri! },
                   { "grant_type", "authorization_code" }
                };

                if (openid)
                {
                    var ClientAssertionType = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer";

                    var requestObject = new JwtTokenObj();
                    requestObject.Expiry = 60;
                    requestObject.Audience = configuration["DTIDP_Config:dt_tokenUrl"];
                    requestObject.Issuer = clientDetails.ClientId;
                    requestObject.Subject = clientDetails.ClientId;

                    var ClientAssertion = JWTTokenManager.GenerateJWTToken(requestObject, encryption);
                    if (null == ClientAssertion)
                    {
                        throw new Exception("Fail to generate JWT token for Token request.");
                    }

                    data.Add("client_assertion_type", ClientAssertionType);
                    data.Add("client_assertion", ClientAssertion);

                }
                var content = new FormUrlEncodedContent(data);

                var authToken = Encoding.ASCII.GetBytes($"{clientDetails.ClientId}:{clientDetails.ClientSecret}");

                HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
                client.BaseAddress = new Uri(TokenUrl);

                var authzHeader = "Basic  " + Convert.ToBase64String(authToken);
                client.DefaultRequestHeaders.Add(configuration["AccessTokenHeaderName"]!,
                    authzHeader);
                client.BaseAddress = new Uri(TokenUrl);

                var response = await client.PostAsync(TokenUrl, content);
                if (response == null)
                {
                    throw new Exception("GetAccessToken responce getting null");
                }
                if (!response.IsSuccessStatusCode)
                {
                    dynamic error = new JObject();
                    error.error = response.StatusCode;
                    error.error_description = response.ReasonPhrase;
                    return error;
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    return JObject.Parse(responseString);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<JObject> GetUserInfo(string accessToken)
        {
            try
            {
                var UserInfoUrl = configuration.GetValue<string>(
                    "DTIDP_Config:dt_userinfoUrl");

                if (String.IsNullOrEmpty(UserInfoUrl))
                    throw new InvalidOperationException("User info URL cannot be NULL.");

                HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
                client.BaseAddress = new Uri(UserInfoUrl);
                var authzHeader = "Bearer  " + accessToken;
                client.DefaultRequestHeaders.Add(configuration["AccessTokenHeaderName"]!,
                    authzHeader);

                var response = await client.GetAsync(UserInfoUrl);
                if (response == null)
                {
                    throw new Exception("get user info responce getting null");
                }
                if (!response.IsSuccessStatusCode)
                {
                    dynamic error = new JObject();
                    error.error = response.StatusCode;
                    error.error_description = response.ReasonPhrase;
                    return error;
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject info = JObject.Parse(responseString);
                    return info;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> GetLogoutUrl(string idToken = null, string state = null)
        {
            try
            {
                //var ClientDetailsConfiguration = new Configuration();
                //var configuartionListinDb = await _configurationService.GetAllConfiguration();
                //var configurationList = (IEnumerable<Configuration>)configuartionListinDb.Resource;
                //foreach (var configuration in configurationList)
                //{
                //    if (configuration.Name == "ClientDetails")
                //    {
                //        ClientDetailsConfiguration = configuration;
                //    }
                //}
                //var clientDetails = JsonConvert.DeserializeObject<ClientDetailsDTO>(ClientDetailsConfiguration.Value);
                var licenseResult = await _deviceService.ReadLicence();

                if (licenseResult == null || !licenseResult.Success)
                {
                    return null;
                }

                var licenceDetails = (LicenceDTO)licenseResult.Resource;

                var clientDetails = await _localClientService.GetClientByClientIdAsync(licenceDetails.clientId);
                if (clientDetails == null)
                {
                    return null;
                }
                var isOpenId = configuration.GetValue<bool>("OpenId_Connect");
                if (isOpenId == true)
                {
                    var LogoutURl = configuration.GetValue<string>("DTIDP_Config:EndSessionEndpoint") +
                    "?id_token_hint={0}&post_logout_redirect_uri={1}&state={2}";

                    //generate idp logout url using id_token,PostLogoutRedirectUri and state value
                    return String.Format(LogoutURl, idToken, clientDetails.LogoutUri,
                                 state);
                }
                else
                {
                    return String.Format(configuration.GetValue<string>("DTIDP_Config:signOutUrl")!, clientDetails.LogoutUri);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ClaimsPrincipal> ValidateJwt(string jwt)
        {
            try
            {
                //var ClientDetailsConfiguration = new Configuration();
                //var configuartionListinDb = await _configurationService.GetAllConfiguration();
                //var configurationList = (IEnumerable<Configuration>)configuartionListinDb.Resource;
                //foreach (var configuration in configurationList)
                //{
                //    if (configuration.Name == "ClientDetails")
                //    {
                //        ClientDetailsConfiguration = configuration;
                //    }
                //}

                //var clientDetails = JsonConvert.DeserializeObject<ClientDetailsDTO>(ClientDetailsConfiguration.Value);
                var licenseResult = await _deviceService.ReadLicence();

                if (licenseResult == null || !licenseResult.Success)
                {
                    return null;
                }

                var licenceDetails = (LicenceDTO)licenseResult.Resource;

                var clientDetails = await _localClientService.GetClientByClientIdAsync(licenceDetails.clientId);
                if (clientDetails == null)
                {
                    return null;
                }
                var parameters = new TokenValidationParameters
                {
                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        var client = new HttpClient();
                        var response = client.GetAsync(configuration["DTIDP_Config:JwksUri"]).Result;
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(responseString);
                        return keys.Keys;
                    },
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["DTIDP_Config:Issuer"],
                    ValidAudience = clientDetails.ClientId,
                    NameClaimType = "name",
                };

                var handler = new JwtSecurityTokenHandler();
                handler.InboundClaimTypeMap.Clear();
                var user = handler.ValidateToken(jwt, parameters, out var _);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string generateApiToken(UserDTO user, int expire_in)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretkey = configuration["ApiTokenKeySecret"];
            //var key = Encoding.ASCII.GetBytes(configuration["ApiTokenKeySecret"]);
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("ApiTokenKeySecret")!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)) }),
                Expires = DateTime.Now.AddSeconds(expire_in),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
