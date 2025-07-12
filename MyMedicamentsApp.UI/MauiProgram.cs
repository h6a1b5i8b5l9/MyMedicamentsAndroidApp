using Microsoft.Extensions.Logging;
using MyMedicamentsApp.Core.Interfaces;
using MyMedicamentsApp.Infrastructure.Data;
using MyMedicamentsApp.Infrastructure.Repositories;
using MyMedicamentsApp.UI.ViewModels;
using MyMedicamentsApp.UI.Views;

namespace MyMedicamentsApp.UI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Register Infrastructure Services
		builder.Services.AddSingleton<DatabaseService>();
		builder.Services.AddSingleton<IMedicamentRepository, MedicamentRepository>();

		// Register ViewModels
		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddTransient<AddMedicamentViewModel>();

		// Register Views with factory
		builder.Services.AddTransient<MainPage>(serviceProvider =>
		{
			var viewModel = serviceProvider.GetRequiredService<MainViewModel>();
			return new MainPage(viewModel);
		});
		builder.Services.AddTransient<AddMedicamentPage>();

		// Register Shell
		builder.Services.AddSingleton<AppShell>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
