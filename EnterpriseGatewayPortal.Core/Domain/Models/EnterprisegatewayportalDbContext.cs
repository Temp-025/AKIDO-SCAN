using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class EnterprisegatewayportalDbContext : DbContext
{
    public EnterprisegatewayportalDbContext()
    {
    }

    public EnterprisegatewayportalDbContext(DbContextOptions<EnterprisegatewayportalDbContext> options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }
    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<AdminLog> AdminLogs { get; set; }

    public virtual DbSet<Beneficiary> Beneficiaries { get; set; }

    public virtual DbSet<BeneficiaryValidity> BeneficiaryValidities { get; set; }

    public virtual DbSet<Bulksign> Bulksigns { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Configuration> Configurations { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<EncDecKey> EncDecKeys { get; set; }

    public virtual DbSet<Filestorage> Filestorages { get; set; }

    public virtual DbSet<MakerChecker> MakerCheckers { get; set; }

    public virtual DbSet<OrgEmailDomain> OrgEmailDomains { get; set; }

    public virtual DbSet<OrgSignatureTemplate> OrgSignatureTemplates { get; set; }

    public virtual DbSet<OrgSubscriberEmail> OrgSubscriberEmails { get; set; }

    public virtual DbSet<OrganizationCertificate> OrganizationCertificates { get; set; }

    public virtual DbSet<OrganizationDetail> OrganizationDetails { get; set; }

    public virtual DbSet<Privilege> Privileges { get; set; }

    public virtual DbSet<Recepient> Recepients { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleActivity> RoleActivities { get; set; }

    public virtual DbSet<SignatureTemplate> SignatureTemplates { get; set; }

    public virtual DbSet<Subscriberorgtemplate> Subscriberorgtemplates { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSecurityQue> UserSecurityQues { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("activities_pkey");

            entity.ToTable("activities");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("CATEGORY");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(260)
                .HasColumnName("DISPLAY_NAME");
            entity.Property(e => e.Enabled).HasColumnName("ENABLED");
            entity.Property(e => e.Hash)
                .HasMaxLength(260)
                .HasColumnName("HASH");
            entity.Property(e => e.McEnabled).HasColumnName("MC_ENABLED");
            entity.Property(e => e.McSupported).HasColumnName("MC_SUPPORTED");
            entity.Property(e => e.Name)
                .HasMaxLength(260)
                .HasColumnName("NAME");
            entity.Property(e => e.ParentId).HasColumnName("PARENT_ID");
        });

        modelBuilder.Entity<AdminLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AdminLogs_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activityname)
                .HasMaxLength(60)
                .HasColumnName("activityname");
            entity.Property(e => e.Checksum)
                .HasColumnType("text")
                .HasColumnName("checksum");
            entity.Property(e => e.Datatransformation)
                .HasColumnType("text")
                .HasColumnName("datatransformation");
            entity.Property(e => e.Identifier)
                .HasMaxLength(60)
                .HasColumnName("identifier");
            entity.Property(e => e.Identifieremail)
                .HasMaxLength(60)
                .HasColumnName("identifieremail");
            entity.Property(e => e.Logmessage)
                .HasColumnType("text")
                .HasColumnName("logmessage");
            entity.Property(e => e.Logmessagetype)
                .HasMaxLength(60)
                .HasColumnName("logmessagetype");
            entity.Property(e => e.Modulename)
                .HasMaxLength(60)
                .HasColumnName("modulename");
            entity.Property(e => e.Servicename)
                .HasMaxLength(60)
                .HasColumnName("servicename");
            entity.Property(e => e.Timestamp).HasColumnType("timestamp with time zone");
            entity.Property(e => e.Username)
                .HasMaxLength(60)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Beneficiary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("beneficiaries_pkey");

            entity.ToTable("beneficiaries");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BeneficiaryConsentAcquired).HasColumnName("beneficiary_consent_acquired");
            entity.Property(e => e.BeneficiaryDigitalId)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_digital_id");
            entity.Property(e => e.BeneficiaryMobileNumber)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_mobile_number");
            entity.Property(e => e.BeneficiaryName)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_name");
            entity.Property(e => e.BeneficiaryNin)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_nin");
            entity.Property(e => e.BeneficiaryOfficeEmail)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_office_email");
            entity.Property(e => e.BeneficiaryPassport)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_passport");
            entity.Property(e => e.BeneficiaryType)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_type");
            entity.Property(e => e.BeneficiaryUgpassEmail)
                .HasMaxLength(100)
                .HasColumnName("beneficiary_ugpass_email");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .HasColumnName("designation");
            entity.Property(e => e.SignaturePhoto).HasColumnName("signature_photo");
            entity.Property(e => e.SponsorDigitalId)
                .HasMaxLength(100)
                .HasColumnName("sponsor_digital_id");
            entity.Property(e => e.SponsorExternalId)
                .HasMaxLength(100)
                .HasColumnName("sponsor_external_id");
            entity.Property(e => e.SponsorName)
                .HasMaxLength(100)
                .HasColumnName("sponsor_name");
            entity.Property(e => e.SponsorPaymentPriorityLevel).HasColumnName("sponsor_payment_priority_level");
            entity.Property(e => e.SponsorType)
                .HasMaxLength(100)
                .HasColumnName("sponsor_type");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<BeneficiaryValidity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("beneficiary_validity_pkey");

            entity.ToTable("beneficiary_validity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BeneficiaryId).HasColumnName("beneficiary_id");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.PrivilegeServiceId).HasColumnName("privilege_service_id");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
            entity.Property(e => e.ValidFrom)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("valid_from");
            entity.Property(e => e.ValidUpto)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("valid_upto");
            entity.Property(e => e.ValidityApplicable).HasColumnName("validity_applicable");

            entity.HasOne(d => d.Beneficiary).WithMany(p => p.BeneficiaryValidities)
                .HasForeignKey(d => d.BeneficiaryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("beneficiary_validity_fk");

            entity.HasOne(d => d.PrivilegeService).WithMany(p => p.BeneficiaryValidities)
                .HasForeignKey(d => d.PrivilegeServiceId)
                .HasConstraintName("beneficiary_validity_fk_1");
        });

        modelBuilder.Entity<Bulksign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bulksign_pkey");

            entity.ToTable("bulksign");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("completed_at");
            entity.Property(e => e.CorelationId)
                .HasMaxLength(36)
                .HasColumnName("corelation_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EsealAnnotations).HasColumnName("eseal_annotations");
            entity.Property(e => e.OrganizationId)
                .HasMaxLength(36)
                .HasColumnName("organization_id");
            entity.Property(e => e.OwnerEmail)
                .HasMaxLength(255)
                .HasColumnName("owner_email");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(255)
                .HasColumnName("owner_name");
            entity.Property(e => e.QrcodeAnnotations).HasColumnName("qrcode_annotations");
            entity.Property(e => e.Result)
                .HasColumnType("json")
                .HasColumnName("result");
            entity.Property(e => e.SignatureAnnotations).HasColumnName("signature_annotations");
            entity.Property(e => e.SignedBy)
                .HasMaxLength(255)
                .HasColumnName("signed_by");
            entity.Property(e => e.SignedPath)
                .HasMaxLength(255)
                .HasColumnName("signed_path");
            entity.Property(e => e.SignerEmail)
                .HasMaxLength(255)
                .HasColumnName("signer_email");
            entity.Property(e => e.SourcePath)
                .HasMaxLength(255)
                .HasColumnName("source_path");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Suid)
                .HasMaxLength(36)
                .HasColumnName("suid");
            entity.Property(e => e.TemplateId)
                .HasMaxLength(36)
                .HasColumnName("template_id");
            entity.Property(e => e.TemplateName)
                .HasMaxLength(255)
                .HasColumnName("template_name");
            entity.Property(e => e.Transaction)
                .HasMaxLength(255)
                .HasColumnName("transaction");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.HasIndex(e => e.ApplicationName, "clients_application_name_key").IsUnique();

            entity.HasIndex(e => e.ApplicationUrl, "clients_application_url_key").IsUnique();

            entity.HasIndex(e => e.ClientId, "clients_client_id_key").IsUnique();

            entity.HasIndex(e => e.RedirectUri, "clients_redirect_uri_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApplicationName)
                .HasMaxLength(50)
                .HasColumnName("application_name");
            entity.Property(e => e.ApplicationType).HasColumnName("application_type");
            entity.Property(e => e.ApplicationUrl)
                .HasMaxLength(512)
                .HasColumnName("application_url");
            entity.Property(e => e.ClientId)
                .HasMaxLength(64)
                .HasColumnName("client_id");
            entity.Property(e => e.ClientSecret)
                .HasMaxLength(64)
                .HasColumnName("client_secret");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.EncryptionCert).HasColumnName("encryption_cert");
            entity.Property(e => e.GrantTypes).HasColumnName("grant_types");
            entity.Property(e => e.Hash)
                .HasMaxLength(260)
                .HasColumnName("hash");
            entity.Property(e => e.LogoutUri).HasColumnName("logout_uri");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.PublicKeyCert).HasColumnName("public_key_cert");
            entity.Property(e => e.RedirectUri)
                .HasMaxLength(512)
                .HasColumnName("redirect_uri");
            entity.Property(e => e.ResponseTypes).HasColumnName("response_types");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.WithPkce).HasColumnName("with_pkce");
        });

        modelBuilder.Entity<Configuration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("configuration_pkey");

            entity.ToTable("configuration");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasMaxLength(3500)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_pkey");

            entity.ToTable("document");

            entity.HasIndex(e => e.DocumentId, "document_document_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountType)
                .HasMaxLength(255)
                .HasColumnName("account_type");
            entity.Property(e => e.AllowToAssignSomeone).HasColumnName("allow_to_assign_someone");
            entity.Property(e => e.Annotations).HasColumnName("annotations");
            entity.Property(e => e.AutoReminders).HasColumnName("auto_reminders");
            entity.Property(e => e.CompleteSignList)
                .HasColumnType("json")
                .HasColumnName("complete_sign_list");
            entity.Property(e => e.CompleteTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("complete_time");
            entity.Property(e => e.CreateTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_time");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DaysToComplete)
                .HasMaxLength(10)
                .HasColumnName("days_to_complete");
            entity.Property(e => e.DisableOrder).HasColumnName("disable_order");
            entity.Property(e => e.DocumentBlockedTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("document_blocked_time");
            entity.Property(e => e.DocumentId)
                .HasMaxLength(36)
                .HasColumnName("document_id");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(255)
                .HasColumnName("document_name");
            entity.Property(e => e.EdmsId)
                .HasMaxLength(255)
                .HasColumnName("edms_id");
            entity.Property(e => e.EsealAnnotations).HasColumnName("eseal_annotations");
            entity.Property(e => e.ExpireDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expire_date");
            entity.Property(e => e.FileId)
                .HasMaxLength(36)
                .HasColumnName("file_id");
            entity.Property(e => e.HtmlSchema).HasColumnName("html_schema");
            entity.Property(e => e.IsDocumentBlocked).HasColumnName("is_document_blocked");
            entity.Property(e => e.IsForm).HasColumnName("is_form");
            entity.Property(e => e.MultiSign).HasColumnName("multi_sign");
            entity.Property(e => e.OrganizationId)
                .HasMaxLength(36)
                .HasColumnName("organization_id");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(255)
                .HasColumnName("organization_name");
            entity.Property(e => e.OwnerEmail)
                .HasMaxLength(255)
                .HasColumnName("owner_email");
            entity.Property(e => e.OwnerId)
                .HasMaxLength(36)
                .HasColumnName("owner_id");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(255)
                .HasColumnName("owner_name");
            entity.Property(e => e.PendingSignList)
                .HasColumnType("json")
                .HasColumnName("pending_sign_list");
            entity.Property(e => e.QrcodeAnnotations).HasColumnName("qrcode_annotations");
            entity.Property(e => e.RecepientCount).HasColumnName("recepient_count");
            entity.Property(e => e.RemindEvery)
                .HasMaxLength(255)
                .HasColumnName("remind_every");
            entity.Property(e => e.SignaturesRequiredCount).HasColumnName("signatures_required_count");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Watermark)
                .HasMaxLength(255)
                .HasColumnName("watermark");

            entity.HasOne(d => d.File).WithMany(p => p.Documents)
                .HasPrincipalKey(p => p.Fileid)
                .HasForeignKey(d => d.FileId)
                .HasConstraintName("document_file_fk");
        });

        modelBuilder.Entity<EncDecKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("enc_dec_key_pkey");

            entity.ToTable("enc_dec_key");

            entity.HasIndex(e => e.UniqueId, "enc_dec_key_unique_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlgId)
                .HasMaxLength(50)
                .HasColumnName("alg_id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Key1).HasColumnName("key1");
            entity.Property(e => e.Key1Length).HasColumnName("key1_length");
            entity.Property(e => e.Key2).HasColumnName("key2");
            entity.Property(e => e.Key2Length).HasColumnName("key2_length");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Purpose)
                .HasMaxLength(50)
                .HasColumnName("purpose");
            entity.Property(e => e.UniqueId)
                .HasMaxLength(50)
                .HasColumnName("unique_id");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Filestorage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("filestorage_pkey");

            entity.ToTable("filestorage");

            entity.HasIndex(e => e.Fileid, "filestorage_fileid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.File).HasColumnName("file");
            entity.Property(e => e.Fileid)
                .HasMaxLength(36)
                .HasColumnName("fileid");
        });

        modelBuilder.Entity<MakerChecker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("maker_checker_pkey");

            entity.ToTable("maker_checker");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityId).HasColumnName("activity_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.MakerId).HasColumnName("maker_id");
            entity.Property(e => e.MakerRoleId).HasColumnName("maker_role_id");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.OperationPriority)
                .HasMaxLength(50)
                .HasColumnName("operation_priority");
            entity.Property(e => e.OperationType)
                .HasMaxLength(50)
                .HasColumnName("operation_type");
            entity.Property(e => e.RequestData).HasColumnName("request_data");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");

            entity.HasOne(d => d.Activity).WithMany(p => p.MakerCheckers)
                .HasForeignKey(d => d.ActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_maker_checker_activity");

            entity.HasOne(d => d.Maker).WithMany(p => p.MakerCheckers)
                .HasForeignKey(d => d.MakerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_maker_checker_maker");
        });

        modelBuilder.Entity<OrgEmailDomain>(entity =>
        {
            entity.HasKey(e => e.OrgDomainId).HasName("org_email_domains_pkey");

            entity.ToTable("org_email_domains");

            entity.Property(e => e.OrgDomainId).HasColumnName("org_domain_id");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.EmailDomain)
                .HasMaxLength(100)
                .HasColumnName("email_domain");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrgEmailDomains)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("org_email_domains_fk");
        });

        modelBuilder.Entity<OrgSignatureTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("org_signature_templates_pkey");

            entity.ToTable("org_signature_templates");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.TemplateId).HasColumnName("template_id");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrgSignatureTemplates)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .HasConstraintName("fk_org_signature_templates_org");
        });

        modelBuilder.Entity<OrgSubscriberEmail>(entity =>
        {
            entity.HasKey(e => e.OrgContactsId).HasName("org_subscriber_email_pkey");

            entity.ToTable("org_subscriber_email");

            entity.Property(e => e.OrgContactsId).HasColumnName("org_contacts_id");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .HasColumnName("designation");
            entity.Property(e => e.EmployeeEmail)
                .HasMaxLength(255)
                .HasColumnName("employee_email");
            entity.Property(e => e.IsBulkSign).HasColumnName("is_bulk_sign");
            entity.Property(e => e.IsDelegate).HasColumnName("is_delegate");
            entity.Property(e => e.IsDigitalForm).HasColumnName("is_digital_form");
            entity.Property(e => e.IsEsealPreparatory).HasColumnName("is_eseal_preparatory");
            entity.Property(e => e.IsEsealSignatory).HasColumnName("is_eseal_signatory");
            entity.Property(e => e.IsOrgSignatory).HasColumnName("is_org_signatory");
            entity.Property(e => e.IsTemplate).HasColumnName("is_template");
            entity.Property(e => e.LsaPrivilege).HasColumnName("lsa_privilege");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(100)
                .HasColumnName("mobile_number");
            entity.Property(e => e.NationalIdNumber)
                .HasMaxLength(100)
                .HasColumnName("national_id_number");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(100)
                .HasColumnName("passport_number");
            entity.Property(e => e.ShortSignature).HasColumnName("short_signature");
            entity.Property(e => e.SignaturePhoto).HasColumnName("signature_photo");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasColumnName("status");
            entity.Property(e => e.SubscriberUid)
                .HasMaxLength(100)
                .HasColumnName("subscriber_uid");
            entity.Property(e => e.UgpassEmail)
                .HasMaxLength(100)
                .HasColumnName("ugpass_email");
            entity.Property(e => e.UgpassUserLinkApproved).HasColumnName("ugpass_user_link_approved");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrgSubscriberEmails)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .HasConstraintName("fk_org_subscriber_email_org");
        });

        modelBuilder.Entity<OrganizationCertificate>(entity =>
        {
            entity.HasKey(e => e.CertificateSerialNumber).HasName("organization_certificates_pkey");

            entity.ToTable("organization_certificates");

            entity.HasIndex(e => e.PkiKeyId, "organization_certificates_pki_key_id_key").IsUnique();

            entity.Property(e => e.CertificateSerialNumber)
                .HasMaxLength(500)
                .HasColumnName("certificate_serial_number");
            entity.Property(e => e.CerificateExpiryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cerificate_expiry_date");
            entity.Property(e => e.CertificateData)
                .HasMaxLength(8048)
                .HasColumnName("certificate_data");
            entity.Property(e => e.CertificateIssueDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("certificate_issue_date");
            entity.Property(e => e.CertificateStatus)
                .HasMaxLength(45)
                .HasColumnName("certificate_status");
            entity.Property(e => e.CertificateType)
                .HasMaxLength(100)
                .HasColumnName("certificate_type");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(45)
                .HasColumnName("organization_uid");
            entity.Property(e => e.PkiKeyId)
                .HasMaxLength(200)
                .HasColumnName("pki_key_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(128)
                .HasColumnName("remarks");
            entity.Property(e => e.TransactionReferenceId)
                .HasMaxLength(100)
                .HasColumnName("transaction_reference_id");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.WrappedKey)
                .HasMaxLength(5000)
                .HasColumnName("wrapped_key");

            entity.HasOne(d => d.OrganizationU).WithMany(p => p.OrganizationCertificates)
                .HasPrincipalKey(p => p.OrganizationUid)
                .HasForeignKey(d => d.OrganizationUid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_organization_certificates_org");
        });

        modelBuilder.Entity<OrganizationDetail>(entity =>
        {
            entity.HasKey(e => e.OrganizationDetailsId).HasName("organization_details_pkey");

            entity.ToTable("organization_details");

            entity.HasIndex(e => e.OrganizationUid, "organization_details_organization_uid_key").IsUnique();

            entity.Property(e => e.OrganizationDetailsId).HasColumnName("organization_details_id");
            entity.Property(e => e.AgentUrl)
                .HasMaxLength(100)
                .HasColumnName("agent_url");
            entity.Property(e => e.AuthorizedLetterForSignatories).HasColumnName("authorized_letter_for_signatories");
            entity.Property(e => e.CorporateOfficeAddress)
                .HasMaxLength(500)
                .HasColumnName("corporate_office_address");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasMaxLength(100)
                .HasColumnName("created_on");
            entity.Property(e => e.ESealImage).HasColumnName("e_seal_image");
            entity.Property(e => e.EnablePostPaidOption).HasColumnName("enable_post_paid_option");
            entity.Property(e => e.IncorporationFile).HasColumnName("incorporation_file");
            entity.Property(e => e.OrgName)
                .HasMaxLength(100)
                .HasColumnName("org_name");
            entity.Property(e => e.OrganizationEmail)
                .HasMaxLength(100)
                .HasColumnName("organization_email");
            entity.Property(e => e.OrganizationSegments)
                .HasMaxLength(100)
                .HasColumnName("organization_segments");
            entity.Property(e => e.OrganizationStatus)
                .HasMaxLength(100)
                .HasColumnName("organization_status");
            entity.Property(e => e.OrganizationUid)
                .HasMaxLength(100)
                .HasColumnName("organization_uid");
            entity.Property(e => e.OtherEsealDocument).HasColumnName("other_eseal_document");
            entity.Property(e => e.OtherLegalDocument).HasColumnName("other_legal_document");
            entity.Property(e => e.SignedPdf).HasColumnName("signed_pdf");
            entity.Property(e => e.SpocUgpassEmail)
                .HasMaxLength(100)
                .HasColumnName("spoc_ugpass_email");
            entity.Property(e => e.TaxFile).HasColumnName("tax_file");
            entity.Property(e => e.TaxNo)
                .HasMaxLength(100)
                .HasColumnName("tax_no");
            entity.Property(e => e.UniqueRegdNo)
                .HasMaxLength(100)
                .HasColumnName("unique_regd_no");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasMaxLength(100)
                .HasColumnName("updated_on");
        });

        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.HasKey(e => e.PrivilegeId).HasName("privilege_pkey");

            entity.ToTable("privilege");

            entity.Property(e => e.PrivilegeId).HasColumnName("privilege_id");
            entity.Property(e => e.IsChargeable).HasColumnName("is_chargeable");
            entity.Property(e => e.PrivilegeServiceDisplayName)
                .HasMaxLength(100)
                .HasColumnName("privilege_service_display_name");
            entity.Property(e => e.PrivilegeServiceName)
                .HasMaxLength(100)
                .HasColumnName("privilege_service_name");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Recepient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recepient_pkey");

            entity.ToTable("recepient");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accesstoken).HasColumnName("accesstoken");
            entity.Property(e => e.Accounttype)
                .HasMaxLength(255)
                .HasColumnName("accounttype");
            entity.Property(e => e.Allowcomments).HasColumnName("allowcomments");
            entity.Property(e => e.Alternatesignatories)
                .HasColumnType("jsonb")
                .HasColumnName("alternatesignatories");
            entity.Property(e => e.Correlationid)
                .HasMaxLength(36)
                .HasColumnName("correlationid");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Decline).HasColumnName("decline");
            entity.Property(e => e.Declineremark).HasColumnName("declineremark");
            entity.Property(e => e.Delegationid)
                .HasMaxLength(36)
                .HasColumnName("delegationid");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Esealorgid)
                .HasMaxLength(255)
                .HasColumnName("esealorgid");
            entity.Property(e => e.Hasdelegation).HasColumnName("hasdelegation");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.Organizationid)
                .HasMaxLength(36)
                .HasColumnName("organizationid");
            entity.Property(e => e.Organizationname)
                .HasMaxLength(255)
                .HasColumnName("organizationname");
            entity.Property(e => e.Recepientid)
                .HasMaxLength(36)
                .HasColumnName("recepientid");
            entity.Property(e => e.Referredby)
                .HasMaxLength(255)
                .HasColumnName("referredby");
            entity.Property(e => e.Referredto)
                .HasMaxLength(255)
                .HasColumnName("referredto");
            entity.Property(e => e.Signaturemandatory).HasColumnName("signaturemandatory");
            entity.Property(e => e.Signedby)
                .HasMaxLength(255)
                .HasColumnName("signedby");
            entity.Property(e => e.Signingcompletetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("signingcompletetime");
            entity.Property(e => e.Signingreqtime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("signingreqtime");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.Suid)
                .HasMaxLength(36)
                .HasColumnName("suid");
            entity.Property(e => e.Takenaction).HasColumnName("takenaction");
            entity.Property(e => e.Tempid)
                .HasMaxLength(36)
                .HasColumnName("tempid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Temp).WithMany(p => p.Recepients)
                .HasPrincipalKey(p => p.DocumentId)
                .HasForeignKey(d => d.Tempid)
                .HasConstraintName("fk_recepient_document");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .HasColumnName("display_name");
            entity.Property(e => e.Hash)
                .HasMaxLength(260)
                .HasColumnName("hash");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<RoleActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_activity_pkey");

            entity.ToTable("role_activity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityId).HasColumnName("activity_id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.GeoLocCoordinates).HasColumnName("geo_loc_coordinates");
            entity.Property(e => e.Hash)
                .HasMaxLength(260)
                .HasColumnName("hash");
            entity.Property(e => e.IsChecker).HasColumnName("is_checker");
            entity.Property(e => e.IsEnabled)
                .HasDefaultValue(true)
                .HasColumnName("is_enabled");
            entity.Property(e => e.LocationOnlyAccess).HasColumnName("location_only_access");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.NativeAccess).HasColumnName("native_access");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.WebAccess).HasColumnName("web_access");

            entity.HasOne(d => d.Activity).WithMany(p => p.RoleActivities)
                .HasForeignKey(d => d.ActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role_activity_activity");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleActivities)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role_activity_role");
        });

        modelBuilder.Entity<SignatureTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("signature_templates_pkey");

            entity.ToTable("signature_templates");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasColumnName("display_name");
            entity.Property(e => e.SamplePreview).HasColumnName("sample_preview");
            entity.Property(e => e.TemplateId)
                .HasMaxLength(100)
                .HasColumnName("template_id");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Subscriberorgtemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscriberorgtemplate_pkey");

            entity.ToTable("subscriberorgtemplate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Organizationid)
                .HasMaxLength(36)
                .HasColumnName("organizationid");
            entity.Property(e => e.Suid)
                .HasMaxLength(36)
                .HasColumnName("suid");
            entity.Property(e => e.Templateid)
                .HasMaxLength(36)
                .HasColumnName("templateid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Template).WithMany(p => p.Subscriberorgtemplates)
                .HasPrincipalKey(p => p.Templateid)
                .HasForeignKey(d => d.Templateid)
                .HasConstraintName("fk_subscriberorgtemplate_template");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Templateid }).HasName("template_pkey");

            entity.ToTable("template");

            entity.HasIndex(e => e.Templateid, "template_templateid_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Templateid)
                .HasMaxLength(36)
                .HasColumnName("templateid");
            entity.Property(e => e.Annotations).HasColumnName("annotations");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasMaxLength(255)
                .HasColumnName("createdby");
            entity.Property(e => e.Documentname)
                .HasMaxLength(255)
                .HasColumnName("documentname");
            entity.Property(e => e.Edmsid)
                .HasMaxLength(50)
                .HasColumnName("edmsid");
            entity.Property(e => e.Emaillist)
                .HasColumnType("jsonb")
                .HasColumnName("emaillist");
            entity.Property(e => e.Esealannotations).HasColumnName("esealannotations");
            entity.Property(e => e.Esealsignaturetemplate).HasColumnName("esealsignaturetemplate");
            entity.Property(e => e.Htmlschema).HasColumnName("htmlschema");
            entity.Property(e => e.Qrcodeannotations).HasColumnName("qrcodeannotations");
            entity.Property(e => e.Qrcoderequired).HasColumnName("qrcoderequired");
            entity.Property(e => e.Rolelist)
                .HasColumnType("jsonb")
                .HasColumnName("rolelist");
            entity.Property(e => e.Settingconfig).HasColumnName("settingconfig");
            entity.Property(e => e.Signaturetemplate).HasColumnName("signaturetemplate");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Templatefile).HasColumnName("templatefile");
            entity.Property(e => e.Templatename)
                .HasMaxLength(255)
                .HasColumnName("templatename");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby)
                .HasMaxLength(255)
                .HasColumnName("updatedby");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BadPasswordCnt).HasColumnName("bad_password_cnt");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.LastBadLoginTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_bad_login_time");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(50)
                .HasColumnName("mobile_number");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(128)
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(260)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Suid)
                .HasMaxLength(50)
                .HasColumnName("suid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_users_role");
        });

        modelBuilder.Entity<UserSecurityQue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_security_ques_pkey");

            entity.ToTable("user_security_ques");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Answer)
                .HasMaxLength(780)
                .HasColumnName("answer");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(150)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Question)
                .HasMaxLength(780)
                .HasColumnName("question");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(150)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
