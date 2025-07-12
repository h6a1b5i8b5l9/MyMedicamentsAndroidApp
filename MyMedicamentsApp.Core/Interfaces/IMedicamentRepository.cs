using MyMedicamentsApp.Core.Models;

namespace MyMedicamentsApp.Core.Interfaces
{
    public interface IMedicamentRepository
    {
        Task<IEnumerable<Medicament>> GetAllAsync();
        Task<Medicament?> GetByIdAsync(Guid id);
        Task<Medicament> AddAsync(Medicament medicament);
        Task<Medicament> UpdateAsync(Medicament medicament);
        Task DeleteAsync(Guid id);
    }
} 