using MyMedicamentsApp.UI.ViewModels;

namespace MyMedicamentsApp.UI.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
} 