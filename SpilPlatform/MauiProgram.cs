using Microsoft.Extensions.Logging;

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

            return builder.Build();
        }
    }
}