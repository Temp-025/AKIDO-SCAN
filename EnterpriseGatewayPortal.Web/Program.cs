using DinkToPdf;
using DinkToPdf.Contracts;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Persistance.Repositories;
using EnterpriseGatewayPortal.Core.Services;
using EnterpriseGatewayPortal.Core.Utilities;
using EnterpriseGatewayPortal.Web.Attribute;
using EnterpriseGatewayPortal.Web.Extensions;
using EnterpriseGatewayPortal.Web.Helper;
using EnterpriseGatewayPortal.Web.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NLog;
using NLog.Web;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("Init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    var securityConfig = builder.Configuration
                          .GetSection("SecurityConfig")
                          .Get<SecurityConfig>();

    // Call each setup function only if the feature is enabled
    if (securityConfig?.UseRateLimiting == true)
        EnterpriseGatewayPortal.Web.Extensions.WebHostExtensions.ConfigureRateLimiting(builder.Services, securityConfig, logger);

    if (securityConfig?.UseKestrelSettings == true)
        EnterpriseGatewayPortal.Web.Extensions.WebHostExtensions.ConfigureKestrel(builder.WebHost, securityConfig, logger);

    await ConfigureServices(builder);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    string basePath = builder.Configuration["BasePath"];
    if (!string.IsNullOrEmpty(basePath))
    {
        app.Use(async (context, next) =>
        {
            context.Request.PathBase = basePath;
            await next.Invoke();
        });
    }
    if (securityConfig?.UseSecurityHeaders == true)
        EnterpriseGatewayPortal.Web.Extensions.WebHostExtensions.ConfigureSecurityHeaders(app, securityConfig, logger);
    // Configure the HTTP request pipeline.
    //if (!app.Environment.IsDevelopment())
    //{
    //    app.UseExceptionHandler("/Home/Error");
    //    //The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //    app.UseHsts();
    //}
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseForwardedHeaders();
        // app.UseBrowserLink();
    }
    else
    {
        app.UseExceptionHandler("/Error");

        app.UseStatusCodePagesWithReExecute("/Error/{0}");
        app.UseForwardedHeaders();

        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //app.UseHsts();
    }
    //app.UseHttpsRedirection();

    var provider = new FileExtensionContentTypeProvider();
    // Add new MIME type mappings
    provider.Mappings[".res"] = "application/octet-stream";
    provider.Mappings[".pexe"] = "application/x-pnacl";
    provider.Mappings[".nmf"] = "application/octet-stream";
    provider.Mappings[".mem"] = "application/octet-stream";
    provider.Mappings[".wasm"] = "application/wasm";

    app.UseStaticFiles();

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
        ContentTypeProvider = provider
    });

    app.UseRouting();

    app.UseCookiePolicy();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, ex.Message);
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

async Task ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddHttpClient("ignoreSSL")
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };
        });
    ConfigurationManager configuration = builder.Configuration;

    var origins = configuration.GetSection("Origin_url").Get<string[]>();
    if (origins == null)
    {
        Console.WriteLine("origins is null");
    }
    else
    {
        Console.WriteLine(origins[1]);
    }

    var environment = builder.Environment;
    // Load secrets from Vault only in Staging or Production
    if (environment.IsStaging() || environment.IsProduction())
    {
        var vaultAddress = builder.Configuration["Vault:Address"];
        var vaultToken = builder.Configuration["Vault:Token"];
        var secretPath = builder.Configuration["Vault:SecretPath"];

        // Initialize Vault client
        var authMethod = new TokenAuthMethodInfo(vaultToken);
        var vaultClientSettings = new VaultClientSettings(vaultAddress, authMethod);
        var vaultClient = new VaultClient(vaultClientSettings);

        // Fetch secret data from Vault
        Secret<SecretData> secret = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(
            path: secretPath,
            mountPoint: "secret"
        );

        var data = secret.Data.Data;

        // Override configuration values
        var memoryConfig = new Dictionary<string, string?>
        {
            ["ConnectionStrings:DBConnection"] = data["ConnectionStrings:DBConnection"]?.ToString()

        };

        // Inject Vault secrets into configuration
        builder.Configuration.AddInMemoryCollection(memoryConfig);
    }
    else
    {
        Console.WriteLine("Skipping Vault secrets loading (Development environment).");
    }


    var egpConnectionString = builder.Configuration.GetConnectionString("DBConnection");
    logger.Info("Enterprise Database ConnString: " + egpConnectionString);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", policy =>
            policy.WithOrigins(origins!)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials()
        );
    });


    builder.Services.AddLogging(loggingBuilder =>
    {
        loggingBuilder.AddConsole();
        loggingBuilder.AddDebug();
    });

    var context = new CustomAssemblyLoadContext();
    if (builder.Environment.IsDevelopment())
    {
        context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));
    }
    else
    {
        context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.so"));
    }
    builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
    builder.Services.AddScoped<IClientService, clientService>();
    //builder.Services.AddScoped<ITemplateService, TemplateService>();
    //builder.Services.AddScoped<IAppConfigService, AppConfigService>();
    builder.Services.AddScoped<IOrganizationService, EnterpriseGatewayPortal.Core.Services.OrganizationService>();
    builder.Services.AddScoped<IBussinessUserService, BussinessUserService>();
    builder.Services.AddScoped<IESealDetailService, ESealDetailService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<DataExportService>();
    builder.Services.AddScoped<ILocalClientService, LocalClientService>();
    builder.Services.AddScoped<IMCValidationService, MCValidationService>();
    builder.Services.AddScoped<ILocalBusinessUsersService, LocalBusinessUsersService>();
    builder.Services.AddScoped<IOrganizationDetailService, OrganizationDetailService>();
    builder.Services.AddScoped<IOrganizationCertificateService, OrganizationCertificateService>();
    //builder.Services.AddScoped<IOrgSignatureTemplateService, OrgSignatureTemplateService>();
    builder.Services.AddScoped<IOrgEmailDomainService, OrgEmailDomainService>();
    //builder.Services.AddScoped<IDocumentIssuerService, DocumentIssuerService>();
    //builder.Services.AddScoped<IVerificationRequestsService, VerificationRequestService>();
    //builder.Services.AddScoped<ISignatureTemplateService, SignatureTemplateService>();
    builder.Services.AddScoped<IAdminLogReportsService, AdminLogReportsService>();
    //builder.Services.AddScoped<IDocumentSigningService, DocumentSigningService>();
    //builder.Services.AddScoped<IDocumentTemplatesService, DocumentTemplatesService>();
    builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
    builder.Services.AddScoped<IRazorRendererHelper, RazorRendererHelper>();
    //builder.Services.AddScoped<ISecurityQuestionService, SecurityQuestionService>();
    builder.Services.AddScoped<IUserPasswordService, UserPasswordService>();
    //builder.Services.AddScoped<IBulkSignService, BulkSignService>();
    builder.Services.AddScoped<LoginHelper>();
    builder.Services.AddScoped<SessionValidationAttribute>();
    //builder.Services.AddScoped<ILocalTemplateService, LocalTemplateService>();
    builder.Services.AddScoped<ISubscriberOrgTemplateService, SubscriberOrgTemplateService>();
    //builder.Services.AddScoped<ILocalBulkSignService, LocalBulkSignService>();
    //builder.Services.AddScoped<IDocumentService, DocumentService>();
    //builder.Services.AddScoped<IPaymentService, PaymentService>();
    builder.Services.AddScoped<IDeviceService, DeviceService>();
    //builder.Services.AddScoped<IDocumentVerifyIssuerService, DocumentVerifyIssuerService>();
    //builder.Services.AddScoped<ISubscriptionVerifyService, SubscriptionVerifyService>();
    //builder.Services.AddScoped<IBeneficiariesService, BeneficiariesService>();
    //builder.Services.AddScoped<IBuyCreditsService, BuyCreditsService>();
    //builder.Services.AddScoped<IRateCardsService, RateCardsService>();
    //builder.Services.AddScoped<IPriceSlabService, PriceSlabService>();
    //builder.Services.AddScoped<IPaymentHistoryService, PaymentHistoryService>();
    //builder.Services.AddScoped<IBalanceSheetsService, BalanceSheetsService>();
    builder.Services.AddScoped<IUsageReportsService, UsageReportsService>();
    //builder.Services.AddScoped<IDelegationService, DelegationService>();
    //builder.Services.AddScoped<IDataPivotService, DataPivotService>();
    builder.Services.AddScoped<IScopeService, ScopeService>();
    builder.Services.AddScoped<IAdminLogService, AdminLogService>();
    //builder.Services.AddScoped<ILocalBeneficiariesService, LocalBeneficiariesService>();
    builder.Services.AddScoped<ILocalBeneficiaryValiditiesService, LocalBeneficiaryValiditiesService>();
    builder.Services.AddScoped<ILocalPrivilegesService, LocalPrivilegesService>();
    builder.Services.AddScoped<IWalletService, WalletService>();
    //builder.Services.AddScoped<IQrCredentialService, QrCredentialService>();
    builder.Services.AddScoped<IUserProfileService, UserProfileService>();
    //builder.Services.AddScoped<IKycApplicationService, KycApplicationService>();
    //builder.Services.AddScoped<IKYCLogReportsService, KYCLogReportsService>();
    //builder.Services.AddScoped<IKYCMethodsService, KYCMethodsService>();
    //builder.Services.AddScoped<IConvertToPdfService, ConvertToPdfService>();
    builder.Services.AddScoped<IEmailSender, EmailSender>();

    builder.Services.AddScoped<EnterpriseGatewayPortal.Core.Utilities.BackgroundService>();
    builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();
    builder.Services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
    builder.Services.AddScoped<IStatisticsService, StatisticsService>();
    builder.Services.AddScoped<ILogClient, LogClient>();
    builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
    builder.Services.AddScoped<IRoleManagementService, RoleManagementService>();
    //builder.Services.AddScoped<IPurposeService, PurposeService>();
    //builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();
    //builder.Services.AddScoped<IAttributeServiceTransactionsService, AttributeServiceTransactionService>();
    //builder.Services.AddScoped<IDigitalFormService, DigitalFormService>();

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(Config =>
               {
                   Config.LoginPath = "/Session";
                   Config.Cookie.Name = "EnterpriseGatewayPortal";
                   Config.LogoutPath = "/Logout";
                   Config.AccessDeniedPath = new PathString("/Error/401");
               });

    //var egpConnectionString = builder.Configuration.GetConnectionString("DBConnection");
    //if (builder.Configuration.GetValue<bool>("EncryptionEnabled"))
    //{
    //    egpConnectionString = PKIMethods.Instance.
    //            PKIDecryptSecureWireData(egpConnectionString);
    //}

    //builder.Services.AddDbContext<EnterprisegatewayportalDbContext>(options =>
    //   options.UseMySql(egpConnectionString,
    //    new MySqlServerVersion("8.0.12-mysql"),

    // builder.Services.AddDbContext<EnterprisegatewayportalDbContext>(options =>
    //options.UseMySql(builder.Configuration.GetConnectionString("DBConnection"),
    //     new MySqlServerVersion("8.0.12-mysql"),
    // mySqlOptions =>
    // {
    //     mySqlOptions.EnableRetryOnFailure(
    //     maxRetryCount: 10,
    //     maxRetryDelay: TimeSpan.FromSeconds(30),
    //     errorNumbersToAdd: null);
    // }));


    builder.Services.AddDbContext<EnterprisegatewayportalDbContext>(options =>
       options.UseNpgsql(egpConnectionString));
}
