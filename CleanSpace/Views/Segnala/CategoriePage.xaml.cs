using CleanSpace.ViewModels;

namespace CleanSpace.Views.Segnala;

public partial class CategoriePage : ContentPage
{
    public CategoriePage()
    {
        InitializeComponent();

        BindingContext = new CategoriaViewModel();
    }

    private async void ClickHomeUtente(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}