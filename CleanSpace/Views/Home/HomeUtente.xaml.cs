using CleanSpace.ViewModels;
using CleanSpace.Views.Profilo;

namespace CleanSpace.Views.Home;

public partial class HomeUtente : ContentPage
{
    public HomeUtente(HomeUtenteViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}