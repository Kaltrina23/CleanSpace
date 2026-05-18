using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm;
using CleanSpace.ViewModels;
using CleanSpace.Models;
using CleanSpace.Views.Home;
using CleanSpace.Services;
using Microsoft.Extensions.Logging;

namespace CleanSpace
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Services
            builder.Services.AddSingleton(sp =>
            {
                var handler = new HttpClientHandler();

                handler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true;

                var httpClient = new HttpClient(handler);

                return httpClient;
            });

            builder.Services.AddSingleton<AuthServices>();
            builder.Services.AddSingleton<TokenService>();


            //Viewmodels
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomeUtente>();
            builder.Services.AddTransient<HomeOperatore>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
