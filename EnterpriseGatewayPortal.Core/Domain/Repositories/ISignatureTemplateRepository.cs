using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface ISignatureTemplateRepository : IGenericRepository<SignatureTemplate>
    {
        Task<SignatureTemplate?> AddSignatureTemplateAsync(SignatureTemplate model);
        Task<IEnumerable<SignatureTemplate>> GetAllSignatureTemplateAsync();
        Task<SignatureTemplate> GetSignatureTemplateByIdAsync(int id);
        Task<bool> IsSignatureTemplateExistsWithIDAsync(int id);
        bool RemoveSignatureTemplate(SignatureTemplate model);
        Task<bool> RemoveSignatureTemplateById(int id);
        Task<SignatureTemplate> UpdateSignatureTemplate(SignatureTemplate model);
    }
}
