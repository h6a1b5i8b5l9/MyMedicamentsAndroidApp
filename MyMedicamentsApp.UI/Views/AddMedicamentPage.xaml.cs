using MyMedicamentsApp.UI.ViewModels;

namespace MyMedicamentsApp.UI.Views
{
    public partial class AddMedicamentPage : ContentPage
    {
        public AddMedicamentPage(AddMedicamentViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
} 