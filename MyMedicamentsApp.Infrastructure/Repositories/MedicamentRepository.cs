using MyMedicamentsApp.Core.Interfaces;
using MyMedicamentsApp.Core.Models;
using MyMedicamentsApp.Infrastructure.Data;

namespace MyMedicamentsApp.Infrastructure.Repositories
{
    public class MedicamentRepository : IMedicamentRepository
    {
        private readonly DatabaseService _databaseService;
        
        public MedicamentRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        
        public async Task<IEnumerable<Medicament>> GetAllAsync()
        {
            var entities = await _databaseService.Database.Table<MedicamentEntity>().ToListAsync();
            return entities.Select(e => e.ToDomain());
        }
        
        public async Task<Medicament?> GetByIdAsync(Guid id)
        {
            var entity = await _databaseService.Database.Table<MedicamentEntity>()
                .Where(e => e.Id == id.ToString())
                .FirstOrDefaultAsync();
                
            return entity?.ToDomain();
        }
        
        public async Task<Medicament> AddAsync(Medicament medicament)
        {
            var entity = MedicamentEntity.FromDomain(medicament);
            await _databaseService.Database.InsertAsync(entity);
            return medicament;
        }
        
        public async Task<Medicament> UpdateAsync(Medicament medicament)
        {
            var entity = MedicamentEntity.FromDomain(medicament);
            await _databaseService.Database.UpdateAsync(entity);
            return medicament;
        }
        
        public async Task DeleteAsync(Guid id)
        {
            await _databaseService.Database.DeleteAsync<MedicamentEntity>(id.ToString());
        }
    }
} 