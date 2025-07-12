using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMedicamentsApp.Core.Enums;
using MyMedicamentsApp.Core.Interfaces;
using MyMedicamentsApp.Core.Models;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Media;

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
            ChoosePhotoCommand = new AsyncRelayCommand(ChoosePhotoAsync);
            TakePhotoCommand = new AsyncRelayCommand(TakePhotoAsync);
            
            // Update SaveCommand can-execute when properties change
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Name) || e.PropertyName == nameof(ExpirationDate))
                {
                    SaveCommand.NotifyCanExecuteChanged();
                }
            };
        }
        
        public AsyncRelayCommand SaveCommand { get; }
        public AsyncRelayCommand ChoosePhotoCommand { get; }
        public AsyncRelayCommand TakePhotoCommand { get; }
        
        // Available categories for binding to picker
        public MedicamentCategory[] AvailableCategories => Enum.GetValues<MedicamentCategory>();
        
        public DateTime MinExpirationDate => DateTime.Today;
        
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
        
        private async Task ChoosePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                if (photo != null)
                {
                    var localPath = await SavePhotoToLocalAsync(photo);
                    PhotoPath = localPath;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error choosing photo: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"Error choosing photo: {ex.Message}");
            }
        }
        
        private async Task TakePhotoAsync()
        {
            try
            {
                if (MediaPicker.IsCaptureSupported)
                {
                    var photo = await MediaPicker.CapturePhotoAsync();
                    if (photo != null)
                    {
                        var localPath = await SavePhotoToLocalAsync(photo);
                        PhotoPath = localPath;
                    }
                }
                else
                {
                    ErrorMessage = "Camera is not supported on this device";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error taking photo: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"Error taking photo: {ex.Message}");
            }
        }
        
        private async Task<string> SavePhotoToLocalAsync(FileResult photo)
        {
            var localPath = Path.Combine(FileSystem.AppDataDirectory, "MedicamentPhotos");
            
            // Create directory if it doesn't exist
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            
            // Generate unique filename
            var fileName = $"medicament_{Guid.NewGuid()}.jpg";
            var fullPath = Path.Combine(localPath, fileName);
            
            // Copy the photo to local storage
            using var stream = await photo.OpenReadAsync();
            using var fileStream = File.Create(fullPath);
            await stream.CopyToAsync(fileStream);
            
            return fullPath;
        }
    }
} 