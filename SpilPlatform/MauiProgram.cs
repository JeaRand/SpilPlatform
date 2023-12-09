using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpilPlatform.Services;
using SpilPlatform.Mvvm.ViewModels;
using SpilPlatform.Mvvm.Views;

namespace SpilPlatform
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Avenir Light.ttf", "UnderOverskrift");
                    fonts.AddFont("Baskerville-Regular Regular.ttf", "Brødtekst");
                    fonts.AddFont("Goudy Old Style Bold.ttf", "Overskrift");
                });

#if DEBUG
		    builder.Logging.AddDebug();
#endif

            // Register singletons
            builder.Services.AddSingleton<CategoryDataService>();
            builder.Services.AddSingleton<GameDataService>();
            builder.Services.AddSingleton<UserDataService>();
            builder.Services.AddSingleton<SessionManagementService>();
            builder.Services.AddTransient<AggregationViewModel>();
            builder.Services.AddTransient<LoginViewModel>();

            return builder.Build();
        }
    }
}