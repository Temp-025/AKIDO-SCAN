using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface ITemplateRepository : IGenericRepository<Template>
    {
        Task<bool> DeleteTemplateAsync(string templateId);
        Task<Template> GetTemplateAsync(string id);
        Task<Template> SaveTemplateAsync(Template template);
        Task<bool> UpdateTemplateById(Template template);
        Task<IEnumerable<Template>> GetAllTemplatesByIdAsync(string name);
    }
}
