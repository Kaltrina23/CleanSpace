using CleanSpace.Views.Home;
using CleanSpace.Views.Auth;
using CleanSpace.Models;
using CleanSpace.Views.Segnala;
using CleanSpace.Views.Profilo;

namespace CleanSpace
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // ROUTES
            Routing.RegisterRoute(nameof(HomeUtente), typeof(HomeUtente));
            Routing.RegisterRoute(nameof(HomeOperatore), typeof(HomeOperatore));
            Routing.RegisterRoute(nameof(CategoriePage), typeof(CategoriePage));
            Routing.RegisterRoute(nameof(InviaSegnalazione), typeof(InviaSegnalazione));
            Routing.RegisterRoute(nameof(ProfiloUtente), typeof(ProfiloUtente));
            Routing.RegisterRoute(nameof(StoricoSegnalazioni), typeof(StoricoSegnalazioni));

        }
    }
}