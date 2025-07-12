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
		builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<AddMedicamentViewModel>();

		// Register Views
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<AddMedicamentPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
