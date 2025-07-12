using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMedicamentsApp.Core.Interfaces;
using MyMedicamentsApp.Core.Models;

namespace MyMedicamentsApp.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IMedicamentRepository? _medicamentRepository;
        
        [ObservableProperty]
        private ObservableCollection<Medicament> _medicaments = new();
        
        [ObservableProperty]
        private bool _isLoading;
        
        [ObservableProperty]
        private string _searchText = string.Empty;
        
        public MainViewModel(IMedicamentRepository? medicamentRepository = null)
        {
            _medicamentRepository = medicamentRepository;
            LoadMedicamentsCommand = new AsyncRelayCommand(LoadMedicamentsAsync);
            RefreshCommand = new AsyncRelayCommand(LoadMedicamentsAsync);
            AddNewCommand = new AsyncRelayCommand(AddNewAsync);
            
            // Only load medicaments if repository is available
            if (_medicamentRepository != null)
            {
                _ = LoadMedicamentsAsync();
            }
        }
        
        public ICommand AddNewCommand { get; }
        
        public ICommand LoadMedicamentsCommand { get; }
        public ICommand RefreshCommand { get; }
        
        private async Task LoadMedicamentsAsync()
        {
            try
            {
                IsLoading = true;
                
                if (_medicamentRepository == null)
                {
                    System.Diagnostics.Debug.WriteLine("Repository is null, skipping medicament load");
                    return;
                }
                
                var medicaments = await _medicamentRepository.GetAllAsync();
                
                Medicaments.Clear();
                foreach (var medicament in medicaments)
                {
                    Medicaments.Add(medicament);
                }
            }
            catch (Exception ex)
            {
                // TODO: Add proper error handling/logging
                System.Diagnostics.Debug.WriteLine($"Error loading medicaments: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }
        
        // Helper method to check if medicament is expired
        public static bool IsExpired(Medicament medicament)
        {
            return medicament.ExpirationDate.Date < DateTime.Today;
        }
        
        // Helper method to get days until expiration
        public static int DaysUntilExpiration(Medicament medicament)
        {
            return (medicament.ExpirationDate.Date - DateTime.Today).Days;
        }
        
        // Helper method to get expiration status text
        public static string GetExpirationStatus(Medicament medicament)
        {
            if (IsExpired(medicament))
                return "EXPIRED";
                
            var days = DaysUntilExpiration(medicament);
            if (days <= 7)
                return $"Expires in {days} days";
            if (days <= 30)
                return $"Expires in {days} days";
                
            return $"Expires in {days} days";
        }
        
        private async Task AddNewAsync()
        {
            // For now, just show a simple message since we don't have Shell navigation
            await Application.Current.MainPage.DisplayAlert("Add New", "Add New Medicament functionality will be implemented soon!", "OK");
        }
    }
} 