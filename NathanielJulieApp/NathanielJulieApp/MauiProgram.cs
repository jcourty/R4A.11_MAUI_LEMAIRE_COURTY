using Microsoft.Extensions.Logging;
using NathanielJulieApp.ViewModels;
using NathanielJulieApp.Views;

namespace NathanielJulieApp
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
                });

            // Enregistrement des ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<Onglet1ViewModel>();
            builder.Services.AddSingleton<onglet2ViewModel>();
            builder.Services.AddSingleton<Onglet3ViewModel>();

            // Enregistrement des Views
            builder.Services.AddSingleton<MainView>();
            builder.Services.AddSingleton<Onglet1View>();
            builder.Services.AddSingleton<Onglet2View>();
            builder.Services.AddSingleton<Onglet3View>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
