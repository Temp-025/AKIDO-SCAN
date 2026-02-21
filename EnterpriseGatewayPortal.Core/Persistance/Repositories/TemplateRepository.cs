using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class TemplateRepository : GenericRepository<Template, EnterprisegatewayportalDbContext>, ITemplateRepository
    {
        private readonly ILogger _logger;
        public TemplateRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<Template> SaveTemplateAsync(Template template)
        {
            Context.Templates.Add(template);
            await Context.SaveChangesAsync();
            return template;
        }

        public async Task<Template> GetTemplateAsync(string id)
        {
            return await Context.Templates.SingleOrDefaultAsync(uu => uu.Templateid == id);
        }

        public async Task<bool> DeleteTemplateAsync(string templateId)
        {
            var template = await Context.Templates.SingleOrDefaultAsync(uu => uu.Templateid == templateId);
            if (template == null)
            {
                return false;
            }

            Context.Templates.Remove(template);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTemplateById(Template template)
        {
            var existingTemplate = await Context.Templates.FindAsync(template.Id);
            if (existingTemplate == null)
            {
                return false;
            }

            Context.Entry(existingTemplate).CurrentValues.SetValues(template);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Template>> GetAllTemplatesByIdAsync(string name)
        {
            try
            {
                return await Context.Templates.OrderByDescending(x => x.Updatedat).Where(u => u.Createdby == name).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                _logger.LogError($"Error in GetAllTemplatesByIdAsync: {ex}");
                _logger.LogError($"Error in GetAllTemplatesByIdAsync: {ex.Message}");
                return null; // Re-throw the exception to propagate it up the call stack
            }
        }

    }
}
