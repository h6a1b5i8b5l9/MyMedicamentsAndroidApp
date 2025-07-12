using MyMedicamentsApp.UI.Views;

namespace MyMedicamentsApp.UI;

public partial class AppShell : ContentPage
{
	public AppShell()
	{
		try{
			InitializeComponent();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error initializing AppShell: {ex.Message}");
			throw;
        }
	}
}
