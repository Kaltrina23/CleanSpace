using CleanSpace.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace CleanSpace.Views.Segnala;

[QueryProperty(nameof(CategoriaId), "categoriaId")]

public partial class InviaSegnalazione : ContentPage
{
    private readonly InviaSegnalazioneViewModel _viewModel;

    public string CategoriaId
    {
        set
        {
            _viewModel.CategoriaID = int.Parse(value);
        }
    }


    public InviaSegnalazione(InviaSegnalazioneViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

        MapPosition.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(45.434245, 12.334466), Distance.FromKilometers(3)));
    }


    async void ScattaFoto(object sender, EventArgs e)
    {
        if (!MediaPicker.Default.IsCaptureSupported)
        {
            await DisplayAlert("Errore", "Fotocamera non accessibile per questo dispositivo", "OK");
            return;
        }

        FileResult? photo = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
        {
            Title = "Scatta un foto"
        });

        //caso in cui l'utente decide di non scattare la foto
        if (photo == null)
            return;

        await _viewModel.SalvaFoto(photo);

        ImmagineScattata.Source = ImageSource.FromFile(photo.FullPath);
        ImmagineScattata.IsVisible = true;
    }
    private void PosizioneImpostata(object sender, MapClickedEventArgs e)
    {
        double latitudine = e.Location.Latitude;
        double longitudine = e.Location.Longitude;

        _viewModel.SalvaPosizione(latitudine, longitudine);
    }

    async void Invia(object sender, EventArgs e)
    {
        IsBusy = true;
        await _viewModel.Invia();
        IsBusy = false;
    }
}