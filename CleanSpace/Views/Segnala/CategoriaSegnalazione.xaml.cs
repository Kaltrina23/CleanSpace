using CleanSpace.ViewModels;
using Microsoft.Maui.Controls;


namespace CleanSpace.Views.Segnala;

public partial class CategoriaSegnalazione : ContentPage
{
    public CategoriaSegnalazione()
    {
        InitializeComponent();

        BindingContext = new CategoriaViewModel();
    }


    private async void ClickHomeUtente(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}