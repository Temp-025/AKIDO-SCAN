using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.Extensions.Logging;
//using PartyPollingBoothPortal.Core.Domain.Repositories;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EnterprisegatewayportalDbContext _context;
        private ILogger<UnitOfWork> _logger;
        private IClientRepository _client;
        private IActivityRespository _activityRespository;
        private IMakerCheckerRepository _makerCheckerRepository;
        private IOrganizationDetailRepository _organizationDetailRepository;
        private IOrgSignatureTemplateRepository _organizationSignatureTemplateRepository;
        private IOrganizationCertificateRepository _organizationCertificateRepository;
        private IOrgEmailDomainRepository _orgEmailDomainRepository;
        private ISignatureTemplateRepository _signatureTemplateRepository;
        private IBusinessUsersRepository _businessUsersRepository;
        private ITemplateRepository _templateRepository;
        private ISubscriberOrgTemplateRepository _subscriberOrgTemplateRepository;
        private IBulkSignRepository _bulkSignRepository;
        private IDocumentRepository _documentRepository;
        //private IRecepientRepository _recepientRepository;
        //private IFileStorageRepository _fileStorageRepository;
        private IUserRepository _user;
        private IRoleRepository _role;
        //private IUserSecurityQueRepository _userSecurityQue;
        private IEncDecKeyRepository _encDecKeys;
        private IConfigurationRepository _configurationRepository;
        private IBeneficiariesRepository _beneficiariesRepository;
        private IBeneficiaryValiditiesRepository _beneficiaryValiditiesRepository;
        private IPrivilegesRepository _privilegesRepository;
        private IAdminLogRepository _adminLogRepository;

        public UnitOfWork(ILogger<UnitOfWork> Logger, EnterprisegatewayportalDbContext Enterprisegatewayportal)
        {
            _context = Enterprisegatewayportal;
            _logger = Logger;
        }

        public IClientRepository Client
        {
            get { return _client ??= _client ?? new ClientRepository(_logger, _context); }
        }
        public IActivityRespository Activities
        {
            get { return _activityRespository ??= _activityRespository ?? new ActivityRepository(_logger, _context); }
        }
        public IMakerCheckerRepository MakerChecker
        {
            get { return _makerCheckerRepository ??= _makerCheckerRepository ?? new MakerCheckerRepository(_logger, _context); }
        }
        public IOrganizationDetailRepository OrganizationDetail
        {
            get { return _organizationDetailRepository ??= new OrganizationDetailRepository(_logger, _context); }
        }
        public IOrganizationCertificateRepository OrganizationCertificate
        {
            get { return _organizationCertificateRepository ??= new OrganizationCertificateRepository(_logger, _context); }
        }
        public IOrgSignatureTemplateRepository OrgSignatureTemplate
        {
            get { return _organizationSignatureTemplateRepository ??= new OrgSignatureTemplateRepository(_logger, _context); }
        }
        public IOrgEmailDomainRepository OrgEmailDomain
        {
            get { return _orgEmailDomainRepository ??= new OrgEmailDomainRepository(_logger, _context); }
        }
        public ISignatureTemplateRepository SignatureTemplate
        {
            get { return _signatureTemplateRepository ??= new SignatureTemplateRepository(_logger, _context); }
        }
        public IBusinessUsersRepository BusinessUsers
        {
            get { return _businessUsersRepository ??= new BusinessUsersRepository(_logger, _context); }
        }

        public IBeneficiariesRepository Beneficiaries
        {
            get { return _beneficiariesRepository ??= new BeneficiariesRepository(_logger, _context); }
        }

        public IBeneficiaryValiditiesRepository BeneficiaryValidities
        {
            get { return _beneficiaryValiditiesRepository ??= new BeneficiaryValiditiesRepository(_logger, _context); }
        }

        public IPrivilegesRepository Privileges
        {
            get { return _privilegesRepository ??= new PrivilegesRepository(_logger, _context); }
        }
        public ITemplateRepository Template
        {
            get { return _templateRepository ??= new TemplateRepository(_logger, _context); }
        }
        public ISubscriberOrgTemplateRepository SubscriberOrgTemplate
        {
            get { return _subscriberOrgTemplateRepository ??= new SubscriberOrgTemplateRepository(_logger, _context); }
        }
        public IBulkSignRepository BulkSign
        {
            get { return _bulkSignRepository ??= new BulkSignRepository(_logger, _context); }
        }
        public IDocumentRepository Document
        {
            get { return _documentRepository ??= new DocumentRepository(_logger, _context); }
        }
        //public IRecepientRepository Recepient
        //{
        //    get { return _recepientRepository ??= new RecepientRepository(_logger, _context); }
        //}
        //public IFileStorageRepository FileStorage
        //{
        //    get { return _fileStorageRepository ??= new FileStorageRepository(_logger, _context); }
        //}
        public IUserRepository Users
        {
            get { return _user ??= new UserRepository(_logger, _context); }
        }
        public IRoleRepository Roles
        {
            get { return _role ??= new RoleRepository(_logger, _context); }
        }
        //public IUserSecurityQueRepository UsersSecurityQue
        //{
        //    get { return _userSecurityQue ??= new UserSecurityQueRepository(_logger, _context); }
        //}
        public IEncDecKeyRepository EncDecKeys
        {
            get { return _encDecKeys = _encDecKeys ?? new EncDecKeyRepository(_logger, _context); }
        }
        public IConfigurationRepository Configurations
        {
            get { return _configurationRepository = _configurationRepository ?? new ConfigurationRepository(_logger, _context); }
        }



        public IAdminLogRepository AdminLog
        {
            get { return _adminLogRepository ??= new AdminLogRepository(_logger, _context); }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void DisableDetectChanges()
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            return;
        }

        public void EnableDetectChanges()
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
            return;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
