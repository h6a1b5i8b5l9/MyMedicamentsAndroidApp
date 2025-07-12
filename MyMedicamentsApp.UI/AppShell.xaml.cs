using MyMedicamentsApp.UI.Views;

namespace MyMedicamentsApp.UI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(AddMedicamentPage), typeof(AddMedicamentPage));
	}
}
