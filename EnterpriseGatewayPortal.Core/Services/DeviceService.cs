using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Core.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class DeviceService : IDeviceService
    {
        public readonly ILogger<DeviceService> _logger;
        private readonly HttpClient _client;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DeviceService(ILogger<DeviceService> logger, IConfiguration configuration,
            HttpClient httpClient,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _client = httpClient;
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:DeviceServiceBaseAddress"]!);
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<ServiceResult> checkStatus(string deviceId, string clientId)
        {
            try
            {
                string url = $"organization/api/get/deviceid/{clientId}";
                var response = _client.GetAsync(url).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JArray jArray = JArray.FromObject(apiResponse.Result);

                        var deviceIdList = JsonConvert.DeserializeObject<List<string>>(jArray.ToString())!;

                        if (deviceIdList.Count() == 0) return new ServiceResult(true, "sucess");

                        foreach (var item in deviceIdList)
                        {
                            if (item == deviceId)
                            {
                                return new ServiceResult(true, "sucess");
                            }
                        }
                        return new ServiceResult("Device not Registered");
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex.Message);
            }
        }

        public async Task<ServiceResult> CheckLicence(string clientId, string type)
        {
            try
            {
                string url = $"SignatureWebService/api/digital/signature/get/active/license/status?clientId={clientId}&type={type}";
                var response = _client.GetAsync(url).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, "success");
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(apiResponse.Message);
                    }
                }
                else
                {
                    return new ServiceResult("Internal Error");
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex.Message);
            }
        }
        public async Task<ServiceResult> ActiveLicence(string clientId, string type)
        {
            try
            {
                string url = $"SignatureWebService/api/digital/signature/get/activate/license?clientId={clientId}&type={type}";
                var response = _client.PostAsync(url, null).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, "success");
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult("Licence is not Active");
                    }
                }
                else
                {
                    return new ServiceResult("Internal Error");
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex.Message);
            }
        }

        public Task<ServiceResult> ReadLicence()
        {
            try
            {
                _logger.LogInformation("Read License File");
                string fileName = "license.txt";
                string webRootPath = _webHostEnvironment.WebRootPath;
                string licenseFolderPath = Path.Combine(webRootPath, "License");
                string filePath = Path.Combine(licenseFolderPath, fileName);
                string encryptedText = "";
                _logger.LogInformation($"License File Path: {filePath}");
                if (File.Exists(filePath))
                {
                    encryptedText = File.ReadAllText(filePath);
                }
                else
                {
                    _logger.LogError("File Not Found");
                    return Task.FromResult(new ServiceResult("File Not Found"));
                }
                _logger.LogInformation($"Encrypted Text: {encryptedText}");
                var decryptedText = PKIMethods.Instance.PKIDecryptSecureWireData(encryptedText);
                _logger.LogInformation($"Decrypted Text: {decryptedText}");
                var licenseDTO = JsonConvert.DeserializeObject<LicenceDTO>(decryptedText);
                if (licenseDTO == null)
                {
                    _logger.LogError("Failed to deserialize licence details");
                    return Task.FromResult(new ServiceResult("Failed to get Licence Details"));
                }
                return Task.FromResult(new ServiceResult(true, "Successfully Fetched", licenseDTO));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Task.FromResult(new ServiceResult("Failed to get Licence Details"));
            }
        }

    }
}

