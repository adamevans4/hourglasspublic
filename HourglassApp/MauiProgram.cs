
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HourglassApp
{
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
				});
            // Configure your connection string
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=hourglasslocal;Trusted_Connection=True;MultipleActiveResultSets=true";

            // Register the connection string with the App instance
            builder.Services.AddSingleton(connectionString);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure(maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null)));

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<IUnitofWork, UnitofWork>();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}