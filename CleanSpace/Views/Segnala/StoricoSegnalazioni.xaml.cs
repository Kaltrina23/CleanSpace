using CleanSpace.ViewModels;

namespace CleanSpace.Views.Profilo;

public partial class StoricoSegnalazioni: ContentPage
{
    public StoricoSegnalazioni(StoricoSegnalazioniViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}