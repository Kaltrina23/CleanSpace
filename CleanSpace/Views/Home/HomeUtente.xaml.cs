using CleanSpace.Views.Segnala;
using Microsoft.Maui.Controls;
using System;


namespace CleanSpace.Views.Home;

public partial class HomeUtente : ContentPage
{
	public HomeUtente()
	{
		InitializeComponent();
	}

    private async void ClickSegnala(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new CategoriePage());

    }
}