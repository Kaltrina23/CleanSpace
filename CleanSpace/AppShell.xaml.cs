using CleanSpace.Views.Home;
using CleanSpace.Views.Auth;
using CleanSpace.Models;
using CleanSpace.Views.Segnala;

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



        }
    }
}