namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[ServiceFilter(typeof(SessionValidationAttribute))]
    //[Authorize]
    //public class BuyCreditsController : BaseController
    //{


    //    private readonly IConfiguration _configuration;
    //    private readonly IBuyCreditsService _buyCreditsService;
    //    private readonly IOrganizationDetailService _organizationDetailService;
    //    private IWebHostEnvironment _environment;
    //    private readonly ILogger<BuyCreditsController> _logger;

    //    public BuyCreditsController(IAdminLogService adminLogService, IWebHostEnvironment environment, IOrganizationDetailService organizationDetail, IBuyCreditsService buyCreditsService, ILogger<BuyCreditsController> logger) : base(adminLogService)
    //    {
    //        _environment = environment;
    //        _organizationDetailService = organizationDetail;
    //        _buyCreditsService = buyCreditsService;
    //        _logger = logger;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> Index()
    //    {
    //        var suid = Suid;
    //        string logMessage;
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);

    //        ServicesBySUIDDTO servicesBySUIDDTO = new ServicesBySUIDDTO()
    //        {
    //            requestBody = new RequestBody { Suid = suid },
    //            serviceMethod = "getServices"
    //        };

    //        var response = await _buyCreditsService.GetAllPrevillages(servicesBySUIDDTO);
    //        if (!response.Success)
    //        {
    //            logMessage = $"Failed to get all previlages for Organization";
    //            SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //                 "Get all previlages", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //            return RedirectToAction("Index", "Dashboard");

    //        }

    //        var services = (GetServicesDTO)response.Resource; 
    //        var volumeSpocServicesList = new List<VolumeSpocServices>();
    //        foreach (var spocService in services.SpocServices)
    //        {
    //            int spocServiceId = spocService.Id;
    //            GetRateCardDTO getRateCardDTO = new GetRateCardDTO()
    //            {

    //                requestBody = new RequestBodyDto1 { serviceId = spocServiceId.ToString(), OrgId = organizationUid },
    //                serviceMethod = "getPriceSlabOrg"

    //            };
    //            var res = await _buyCreditsService.GetRateCard(getRateCardDTO);
    //            if (!res.Success)
    //            {
    //                return RedirectToAction("Index", "Dashboard");

    //            }
    //            PriceSlabDTO priceSlabDTO = (PriceSlabDTO)res.Resource;
    //            List<PricingSlabDefinition> pricingSlabDefinitions = priceSlabDTO.PricingSlabDefinitionsList;

    //            foreach (var pricingSlabDefinition in pricingSlabDefinitions)
    //            {
    //                var volumeSpocServices = new VolumeSpocServices
    //                {
    //                    SpocserviceID = spocServiceId.ToString(),
    //                    volumeFrom = pricingSlabDefinition.VolumeRangeFrom.ToString(),
    //                    volumeTo = pricingSlabDefinition.VolumeRangeTo.ToString(),
    //                    discountPercentage = pricingSlabDefinition.Discount.ToString(),
    //                };
    //                volumeSpocServicesList.Add(volumeSpocServices);
    //            }
    //        }         

    //        AvailableCreditsDTO availableCreditsDTO = new AvailableCreditsDTO()
    //        {
    //            requestBody = new RequestBodyDto { OrgId = organizationUid },
    //            serviceMethod = "getOrganizationRemainingCredits"
    //        };

    //        var response1 = await _buyCreditsService.GetAllAvailableCredits(availableCreditsDTO);
    //        if (!response1.Success)
    //        {
    //            logMessage = $"Failed to get all available credits for Organization";
    //            SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //                 "Get all available credits", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return RedirectToAction("Index", "Dashboard");

    //        }

    //        ExistingCreditsDTO existingCreditsDTO = (ExistingCreditsDTO)response1.Resource;

    //        AvailableCreditsViewModel view = new AvailableCreditsViewModel()
    //        {
    //            PostPaid = existingCreditsDTO.PostPaid,
    //            Eseal_SIGNATURE = existingCreditsDTO.Eseal_SIGNATURE,
    //            Digital_SIGNATURE = existingCreditsDTO.Digital_SIGNATURE,
    //            User_SUBSCRIPTION = existingCreditsDTO.User_SUBSCRIPTION,
    //            SpocServices = services.SpocServices,
    //            orgId = organizationUid,
    //            volumesRanges = volumeSpocServicesList
    //        };

    //        logMessage = $"Successfully received all available credits for Organization";
    //        SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //             "Get all available credits", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //        return View(view);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> PriceSummary(AvailableCreditsViewModel availableCreditsViewModel)
    //    {
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        GetRateCardDTO getRateCardDTO = new GetRateCardDTO()
    //        {

    //            requestBody = new RequestBodyDto1 { serviceId = availableCreditsViewModel.serviceId, OrgId = organizationUid },
    //            serviceMethod = "getPriceSlabOrg"

    //        };
    //        var response = await _buyCreditsService.GetRateCard(getRateCardDTO);
    //        if (!response.Success)
    //        {
    //            return Json(new { status = response.Success, message = "false" });
    //        }
    //        PriceSlabDTO priceSlabDTO = (PriceSlabDTO)response.Resource;
    //        double credits = double.Parse(availableCreditsViewModel.Credits);
    //        List<PricingSlabDefinition> pricingSlabDefinitions = priceSlabDTO.PricingSlabDefinitionsList;

    //        double rate = priceSlabDTO.Rate;
    //        double charges = priceSlabDTO.Tax;
    //        foreach (PricingSlabDefinition slabDefinition in pricingSlabDefinitions)
    //        {
    //            double discount = slabDefinition.Discount;
    //            double volumeRangeFrom = slabDefinition.VolumeRangeFrom;
    //            double volumeRangeTo = slabDefinition.VolumeRangeTo;
    //            double Id = slabDefinition.Id;
    //            string serviceDisplayName = slabDefinition.ServiceDefinitions.ServiceDisplayName;
    //            if (credits >= volumeRangeFrom && credits <= volumeRangeTo)
    //            {
    //                return Json(new { status = response.Success, message = discount, rate = rate, displayName = serviceDisplayName, tax = charges, serviceId = Id, previlageServiceID = availableCreditsViewModel.serviceId });
    //            }

    //        }
    //        return Json(new { status = response.Success, message = "false" });


    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> CreateBill([FromBody] List<PriceSummary> priceSummaryList)
    //    {
    //        List<PriceSummary> ceditList = priceSummaryList;
    //        double digitalSignatureCredits = 0;
    //        double esCredits = 0;
    //        double usCredits = 0;

    //        double digitalSignatureDiscount = 0;
    //        double esDiscount = 0;
    //        double usDiscount = 0;

    //        double digitalsignatureTax = 0;
    //        double esTax = 0;
    //        double usTax = 0;

    //        double digitalSignatureBasePrice = 0;
    //        double esBasePrice = 0;
    //        double usBasePrice = 0;

    //        double dSServideId = 0;
    //        double esServideId = 0;
    //        double usServideId = 0;

    //        var dSDisplayName = "";
    //        var usDisplayName = "";
    //        var esDisplayName = "";
    //        var org_id = "";

    //        var dsPrevilageServiceId = "";
    //        var esPrevilageServiceId = "";
    //        var usPrevilageServiceId = "";


    //        foreach (var item in priceSummaryList)
    //        {
    //            if (item.DisplayName == "DIGITAL SIGNATURE")
    //            {
    //                // If found, store its Credits and Discount in variables
    //                digitalSignatureCredits = double.Parse(item.Credits);
    //                digitalSignatureDiscount = item.Discount;
    //                digitalsignatureTax = item.Tax;
    //                digitalSignatureBasePrice = item.Rate;
    //                dSDisplayName = item.DisplayName;
    //                dSServideId = item.ServiceId;
    //                org_id = item.OrgId;
    //                dsPrevilageServiceId = item.PrevilageServiceID;

    //            }
    //            if (item.DisplayName == "ESEAL SIGNATURE")
    //            {
    //                esCredits = double.Parse(item.Credits);
    //                esDiscount = item.Discount;
    //                esDisplayName = item.DisplayName;
    //                esTax = item.Tax;
    //                esBasePrice = item.Rate;
    //                esServideId = item.ServiceId;
    //                org_id = item.OrgId;
    //                esPrevilageServiceId = item.PrevilageServiceID;

    //            }
    //            if (item.DisplayName == "USER SUBSCRIPTION")
    //            {
    //                usCredits = double.Parse(item.Credits);
    //                usDiscount = item.Discount;
    //                usDisplayName = item.DisplayName;
    //                usTax = item.Tax;
    //                usBasePrice = item.Rate;
    //                usServideId = item.ServiceId;
    //                org_id = item.OrgId;
    //                usPrevilageServiceId = item.PrevilageServiceID;
    //            }
    //        }

    //        double DS_TotalsRate = 0;
    //        double DS_Taxper = 0;
    //        double DS_TotalTax = 0;
    //        double DS_Discountper = 0;
    //        double DS_TotalDiscount = 0;
    //        double DS_TotalAmount = 0;
    //        if (dSDisplayName == "DIGITAL SIGNATURE")
    //        {
    //            DS_TotalsRate = digitalSignatureCredits * digitalSignatureBasePrice;
    //            DS_Taxper = digitalsignatureTax / 100;
    //            DS_TotalTax = DS_TotalsRate * DS_Taxper;
    //            DS_Discountper = digitalSignatureDiscount / 100;
    //            DS_TotalDiscount = DS_TotalsRate * DS_Discountper;
    //            DS_TotalAmount = DS_TotalsRate + DS_TotalTax - DS_TotalDiscount;
    //        }

    //        double ES_TotalsRate = 0;
    //        double ES_Taxper = 0;
    //        double ES_TotalTax = 0;
    //        double ES_Discountper = 0;
    //        double ES_TotalDiscount = 0;
    //        double ES_TotalAmount = 0;
    //        if (esDisplayName == "ESEAL SIGNATURE")
    //        {
    //            ES_TotalsRate = esCredits * esBasePrice;
    //            ES_Taxper = esTax / 100;
    //            ES_TotalTax = ES_TotalsRate * ES_Taxper;
    //            ES_Discountper = esDiscount / 100;
    //            ES_TotalDiscount = ES_TotalsRate * ES_Discountper;
    //            ES_TotalAmount = ES_TotalsRate + ES_TotalTax - ES_TotalDiscount;
    //        }

    //        double US_TotalsRate = 0;
    //        double US_Taxper = 0;
    //        double US_TotalTax = 0;
    //        double US_Discountper = 0;
    //        double US_TotalDiscount = 0;
    //        double US_TotalAmount = 0;
    //        if (usDisplayName == "USER SUBSCRIPTION")
    //        {
    //            US_TotalsRate = usCredits * usBasePrice;
    //            US_Taxper = usTax / 100;
    //            US_TotalTax = US_TotalsRate * US_Taxper;
    //            US_Discountper = usDiscount / 100;
    //            US_TotalDiscount = US_TotalsRate * US_Discountper;
    //            US_TotalAmount = US_TotalsRate + US_TotalTax - US_TotalDiscount;
    //        }
    //        double GrandTotal = DS_TotalAmount + ES_TotalAmount + US_TotalAmount;

    //        CreditsSummeryViewModel viewModel = new CreditsSummeryViewModel()
    //        {
    //            DS_TotalRate = DS_TotalsRate,
    //            DS_Tax = digitalsignatureTax,
    //            DS_TotalTax = DS_TotalTax, 
    //            DS_Discount = digitalSignatureDiscount,
    //            DS_TotalDiscount = DS_TotalDiscount,
    //            DS_TotalAmount = DS_TotalAmount,
    //            DS_PrevialageServiceId = dsPrevilageServiceId,
    //            DS_DisplayName = dSDisplayName,
    //            DS_ServiceId = dSServideId.ToString(),

    //            ES_TotalsRate = ES_TotalsRate,
    //            ES_Taxp = esTax,
    //            ES_TotalTax = ES_TotalTax,
    //            ES_Discount = esDiscount,
    //            ES_TotalDiscount = ES_TotalDiscount,
    //            ES_TotalAmount = ES_TotalAmount,
    //            ES_PrevialageServiceId = esPrevilageServiceId,
    //            ES_DisplayName = esDisplayName,
    //            ES_ServiceId = esServideId.ToString(),

    //            US_TotalsRate = US_TotalsRate,
    //            US_Taxper = usTax,
    //            US_TotalTax = US_TotalTax,
    //            US_Discountper = usDiscount,
    //            US_TotalDiscount = US_TotalDiscount,
    //            US_TotalAmount = US_TotalAmount,
    //            US_PrevialageServiceId = usPrevilageServiceId,
    //            US_DisplayName = usDisplayName,
    //            US_ServiceId = usServideId.ToString(),

    //            DS_Credits = digitalSignatureCredits,
    //            ES_Credits = esCredits,
    //            US_Credits = usCredits,

    //            DS_BasePrice = digitalSignatureBasePrice,
    //            ES_BasePrice = esBasePrice,
    //            US_BasePrice = usBasePrice,

    //            Org_Id = org_id,
    //            GrandTotal = GrandTotal

    //        };

    //        return PartialView("_creditsPaymentInfo", viewModel);

    //    }



    //    [HttpPost]
    //    public async Task<IActionResult> MobileEntry([FromBody] PaymentInfoViewModel viewModel)
    //    {

    //        List<PaymentInfoDto> paymentInfoList = new List<PaymentInfoDto>();

    //        foreach (var item in viewModel.PaymentInfo)
    //        {
    //            PaymentInfoDto paymentInfo = new PaymentInfoDto
    //            {
    //                Discount = item.Discount,
    //                OrgId = viewModel.OrgId,
    //                Rate = item.Rate,
    //                SlabId = item.ServiceId,
    //                ServiceName = item.ServiceName,
    //                ServiceId = item.PrevilageServiceId,
    //                StakeHolder = "ORGANIZATION",
    //                Tax = item.Tax,
    //                Volume = item.Volume
    //            };


    //            paymentInfoList.Add(paymentInfo);
    //        }

    //        PaymentRequestDTO dto = new PaymentRequestDTO()
    //        {
    //            Amount = viewModel.GrandTotal,
    //            SubscriberUniqueId = Suid,
    //            Currency = "UTX",
    //            ExternalId = "1234567",
    //            Category = "MobileMoney",
    //            PayeeNote = "payeeNote",
    //            PayerNote = "payerNote",
    //            Payer = viewModel.PayerMobile,
    //            PaymentCategory = "SPOC_SERVICE_CREDITS_FEE_COLLECTION",
    //            WalletId = "df553e68-fb22-438b-b051-cf7fa7036d4b",
    //            PaymentInfo = paymentInfoList
    //        };

    //        PaymentBillCreditsServiceDTO paymentBillCreditsServiceDTO = new PaymentBillCreditsServiceDTO()
    //        {
    //            RequestBody = dto,
    //            ServiceMethod = "payment"
    //        };

    //        PaymentRequestViewModel paymentRequestViewModel = new PaymentRequestViewModel()
    //        {
    //            PaymentRequests = dto,
    //        };

    //        return PartialView("_MobileEntry", paymentRequestViewModel);

    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Checkout(PaymentRequestViewModel viewModel)
    //    {
    //        string logMessage;
    //        PaymentRequestDTO paymentRequests = viewModel.PaymentRequests;

    //        paymentRequests.Payer = viewModel.MobNumber;
    //        PaymentBillCreditsServiceDTO paymentBillCreditsServiceDTO = new PaymentBillCreditsServiceDTO()
    //        {
    //            RequestBody = paymentRequests,
    //            ServiceMethod = "payment"
    //        };
    //        var response = await _buyCreditsService.ProceedCheckout(paymentBillCreditsServiceDTO);           
    //        if (!response.Success)
    //        {
    //            logMessage = $"Failed to Initiate the Credits for Organization";
    //            SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //                 "Get Credits Bill details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return NotFound();
    //        }
    //        var transactions = (PaymentTransactionDTO)response.Resource;

    //        logMessage = $"Successfully Initiated Credits for Organization";
    //        SendAdminLog(ModuleNameConstants.BuyCredits, ServiceNameConstants.BuyCredits,
    //             "Get Credits Bill details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //        return PartialView("_Checkout",transactions);
    //    }


    //}
}
