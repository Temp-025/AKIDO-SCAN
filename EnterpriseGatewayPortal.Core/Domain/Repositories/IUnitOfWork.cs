namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        //IUserSecurityQueRepository UsersSecurityQue { get; }
        IOrganizationDetailRepository OrganizationDetail { get; }
        IOrgSignatureTemplateRepository OrgSignatureTemplate { get; }
        ISignatureTemplateRepository SignatureTemplate { get; }
        IOrganizationCertificateRepository OrganizationCertificate { get; }
        IClientRepository Client { get; }
        IBusinessUsersRepository BusinessUsers { get; }
        IBeneficiariesRepository Beneficiaries { get; }
        IBeneficiaryValiditiesRepository BeneficiaryValidities { get; }
        IPrivilegesRepository Privileges { get; }
        IActivityRespository Activities { get; }
        IMakerCheckerRepository MakerChecker { get; }
        IEncDecKeyRepository EncDecKeys { get; }
        ITemplateRepository Template { get; }
        ISubscriberOrgTemplateRepository SubscriberOrgTemplate { get; }
        IBulkSignRepository BulkSign { get; }
        IDocumentRepository Document { get; }
        //IRecepientRepository Recepient { get; }
        //IFileStorageRepository FileStorage { get; }
        IOrgEmailDomainRepository OrgEmailDomain { get; }
        IConfigurationRepository Configurations { get; }

        IAdminLogRepository AdminLog { get; }

        void DisableDetectChanges();
        void EnableDetectChanges();
        int Save();
        Task<int> SaveAsync();
    }
}
