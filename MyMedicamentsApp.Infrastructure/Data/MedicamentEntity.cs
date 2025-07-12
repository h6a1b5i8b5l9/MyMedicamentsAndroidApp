using SQLite;
using MyMedicamentsApp.Core.Enums;

namespace MyMedicamentsApp.Infrastructure.Data
{
    [Table("Medicaments")]
    public class MedicamentEntity
    {
        [PrimaryKey]
        public string Id { get; set; } = string.Empty;
        
        [NotNull]
        public string Name { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        [NotNull]
        public DateTime ExpirationDate { get; set; }
        
        [NotNull]
        public int Category { get; set; } // Store enum as int
        
        public string? PhotoPath { get; set; }
        
        // Conversion methods
        public static MedicamentEntity FromDomain(MyMedicamentsApp.Core.Models.Medicament medicament)
        {
            return new MedicamentEntity
            {
                Id = medicament.Id.ToString(),
                Name = medicament.Name,
                Description = medicament.Description,
                ExpirationDate = medicament.ExpirationDate,
                Category = (int)medicament.Category,
                PhotoPath = medicament.PhotoPath
            };
        }
        
        public MyMedicamentsApp.Core.Models.Medicament ToDomain()
        {
            return new MyMedicamentsApp.Core.Models.Medicament
            {
                Id = Guid.Parse(Id),
                Name = Name,
                Description = Description,
                ExpirationDate = ExpirationDate,
                Category = (MedicamentCategory)Category,
                PhotoPath = PhotoPath
            };
        }
    }
} 