using MyMedicamentsApp.Core.Enums;

namespace MyMedicamentsApp.Core.Models
{
    public class Medicament
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public MedicamentCategory Category { get; set; }
        public string? PhotoPath { get; set; }
    }
} 