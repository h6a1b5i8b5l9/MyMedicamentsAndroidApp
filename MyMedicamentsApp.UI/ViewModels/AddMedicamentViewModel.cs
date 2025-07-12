using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMedicamentsApp.Core.Enums;
using MyMedicamentsApp.Core.Interfaces;
using MyMedicamentsApp.Core.Models;

namespace MyMedicamentsApp.UI.ViewModels
{
    public partial class AddMedicamentViewModel : ObservableObject
    {
        private readonly IMedicamentRepository _medicamentRepository;
        
        [ObservableProperty]
        private string _name = string.Empty;
        
        [ObservableProperty]
        private string _description = string.Empty;
        
        [ObservableProperty]
        private DateTime _expirationDate = DateTime.Today.AddMonths(1);
        
        [ObservableProperty]
        private MedicamentCategory _selectedCategory = MedicamentCategory.Other;
        
        [ObservableProperty]
        private string _photoPath = string.Empty;
        
        [ObservableProperty]
        private bool _isSaving;
        
        [ObservableProperty]
        private string _errorMessage = string.Empty;
        
        public AddMedicamentViewModel(IMedicamentRepository medicamentRepository)
        {
            _medicamentRepository = medicamentRepository;
            SaveCommand = new AsyncRelayCommand(SaveMedicamentAsync, CanSave);
            
            // Update SaveCommand can-execute when properties change
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Name) || e.PropertyName == nameof(ExpirationDate))
                {
                    SaveCommand.NotifyCanExecuteChanged();
                }
            };
        }
        
        public ICommand SaveCommand { get; }
        
        // Available categories for binding to picker
        public MedicamentCategory[] AvailableCategories => Enum.GetValues<MedicamentCategory>();
        
        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name) && ExpirationDate > DateTime.Today;
        }
        
        private async Task SaveMedicamentAsync()
        {
            try
            {
                IsSaving = true;
                ErrorMessage = string.Empty;
                
                var medicament = new Medicament
                {
                    Id = Guid.NewGuid(),
                    Name = Name.Trim(),
                    Description = Description?.Trim(),
                    ExpirationDate = ExpirationDate,
                    Category = SelectedCategory,
                    PhotoPath = PhotoPath?.Trim()
                };
                
                await _medicamentRepository.AddAsync(medicament);
                
                // Reset form
                Name = string.Empty;
                Description = string.Empty;
                ExpirationDate = DateTime.Today.AddMonths(1);
                SelectedCategory = MedicamentCategory.Other;
                PhotoPath = string.Empty;
                
                // Navigate back after successful save
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error saving medicament: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"Error saving medicament: {ex.Message}");
            }
            finally
            {
                IsSaving = false;
            }
        }
    }
} 