using CleanSpace.ViewModels;
using Microsoft.Maui.Controls;


namespace CleanSpace.Views.Segnala;

public partial class InviaSegnalazione : ContentPage
{
    public InviaSegnalazione()
    {
        InitializeComponent();

        BindingContext = new InviaSegnalazioneViewModel();
    }
}