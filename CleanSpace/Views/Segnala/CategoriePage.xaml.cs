using CleanSpace.ViewModels;

namespace CleanSpace.Views.Segnala;

public partial class CategoriePage : ContentPage
{
    public CategoriePage(CategoriaViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    private async void ClickHomeUtente(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}