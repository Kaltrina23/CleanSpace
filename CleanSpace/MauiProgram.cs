using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm;
using CleanSpace.ViewModels;
using CleanSpace.Models;
using CleanSpace.Views.Home;
using CleanSpace.Services;
using Microsoft.Extensions.Logging;
using CleanSpace.Views.Segnala;
using CleanSpace.Views.Profilo;

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
            // HTTP CLIENT
            builder.Services.AddSingleton(sp =>
            {
                var handler = new HttpClientHandler();

                #if ANDROID

                    handler.ServerCertificateCustomValidationCallback =
                        (message, cert, chain, errors) => true;

                #endif

                var client = new HttpClient(handler);

                client.BaseAddress =
                    new Uri("https://10.0.2.2:7097/api/");

                return client;
            });

            builder.Services.AddSingleton<AuthServices>();
            builder.Services.AddSingleton<TokenService>();
            builder.Services.AddSingleton<CategorieService>();
            builder.Services.AddSingleton<SegnalazioniService>();
            builder.Services.AddSingleton<ProfiloService>();
            builder.Services.AddSingleton<StoricoSegnalazioniService>();


            //Viewmodels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<HomeUtenteViewModel>();
            builder.Services.AddTransient<HomeOperatoreViewModel>();
            builder.Services.AddTransient<CategoriaViewModel>();
            builder.Services.AddTransient<InviaSegnalazioneViewModel>();
            builder.Services.AddTransient<ProfiloUtenteViewModel>();
            builder.Services.AddTransient<StoricoSegnalazioniViewModel>();


            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomeUtente>();
            builder.Services.AddTransient<HomeOperatore>();
            builder.Services.AddTransient<CategoriePage>();
            builder.Services.AddTransient<InviaSegnalazione>();
            builder.Services.AddTransient<ProfiloUtente>();
            builder.Services.AddTransient<StoricoSegnalazioni>();








#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
