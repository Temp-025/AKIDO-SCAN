using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class SubscriberOrgTemplateRepository : GenericRepository<Subscriberorgtemplate, EnterprisegatewayportalDbContext>, ISubscriberOrgTemplateRepository
    {
        private readonly ILogger _logger;
        public SubscriberOrgTemplateRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<Subscriberorgtemplate> SaveSubscriberOrgTemplate(Subscriberorgtemplate subscriberOrgTemplate)
        {
            Context.Subscriberorgtemplates.Add(subscriberOrgTemplate);
            await Context.SaveChangesAsync();
            return subscriberOrgTemplate;
        }

        public async Task<bool> DeleteSubscriberOrgTemplate(string templateId, UserDTO userDTO)
        {
            var subscriberOrgTemplate = await Context.Subscriberorgtemplates
                .FirstOrDefaultAsync(x => x.Suid == userDTO.Suid && x.Organizationid == userDTO.OrganizationId && x.Templateid == templateId);

            if (subscriberOrgTemplate == null)
            {
                return false;
            }

            Context.Subscriberorgtemplates.Remove(subscriberOrgTemplate);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<Subscriberorgtemplate>> GetTemplateListBySuidAndOrgId(string suid, string orgId)
        {
            var templateCollection = Context.Templates;
            //var subscriberOrgTemplates = await Context.Subscriberorgtemplates
            //    .Where(x => x.Suid == suid && x.OrganizationId == orgId)
            //    .OrderByDescending(x => x.CreatedAt)
            //    .Include(x => x.Template)
            //    .ToListAsync();
            var subscriberOrgTemplates = await Context.Subscriberorgtemplates
                .AsNoTracking()  // Optimizes read-only queries
                .Where(x => x.Suid == suid && x.Organizationid == orgId)
                .OrderByDescending(x => x.Createdat)
                .Include(x => x.Template)
                .ToListAsync();

            return subscriberOrgTemplates;
        }

        public async Task<IList<Subscriberorgtemplate>> GetTemplateListByOrgId(string orgId)
        {
            var templateCollection = Context.Templates;
            var subscriberOrgTemplates = await Context.Subscriberorgtemplates
                .Where(x => x.Organizationid == orgId)
                .OrderByDescending(x => x.Createdat)
                .Include(x => x.Template)
                .ToListAsync();

            return subscriberOrgTemplates;
        }
    }
}
