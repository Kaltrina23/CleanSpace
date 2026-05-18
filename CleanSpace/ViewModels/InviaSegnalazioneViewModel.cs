using CleanSpace.DTOs;
using CleanSpace.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanSpace.ViewModels;

[QueryProperty(nameof(CategoriaId), "categoriaId")]
[QueryProperty(nameof(CategoriaNome), "categoriaNome")]
public partial class InviaSegnalazioneViewModel : ObservableObject
{
    private readonly ApiService _apiService;

    private readonly TokenService _tokenService;

    private int _categoriaId;

    [ObservableProperty]
    private string nomeCategoria;

    [ObservableProperty]
    private string posizione;

    [ObservableProperty]
    private string nota;

    [ObservableProperty]
    private string fotoPreview;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private bool isGuest;

    [ObservableProperty]
    private string ospiteNome;

    [ObservableProperty]
    private string ospiteCognome;

    [ObservableProperty]
    private string ospiteEmail;

    private double _latitudine;

    private double _longitudine;

    private string? _fotoBase64;

    // PROPERTY SHELL
    public string CategoriaId
    {
        set
        {
            _categoriaId = int.Parse(value);
        }
    }

    public string CategoriaNome
    {
        set
        {
            NomeCategoria =
                Uri.UnescapeDataString(value);
        }
    }

    public InviaSegnalazioneViewModel()
    {
        _apiService = new ApiService();

        _tokenService = new TokenService();

        GetLocation();

        CaricaGuest();
    }

    private async void CaricaGuest()
    {
        IsGuest =
            await _tokenService.IsGuest();
    }

    private async void GetLocation()
    {
        try
        {
            var location =
                await Geolocation.Default.GetLocationAsync();

            if (location != null)
            {
                _latitudine = location.Latitude;

                _longitudine = location.Longitude;

                Posizione =
                    $"{_latitudine:F5}, {_longitudine:F5}";
            }
        }
        catch
        {
            Posizione = "Posizione non disponibile";
        }
    }

    [RelayCommand]
    private async Task ScattaFoto()
    {
        try
        {
            var foto =
                await MediaPicker.Default.CapturePhotoAsync();

            if (foto == null)
                return;

            using var stream =
                await foto.OpenReadAsync();

            using var memory =
                new MemoryStream();

            await stream.CopyToAsync(memory);

            var bytes = memory.ToArray();

            _fotoBase64 =
                Convert.ToBase64String(bytes);

            FotoPreview = foto.FullPath;
        }
        catch
        {
            await Shell.Current.DisplayAlert(
                "Errore",
                "Impossibile aprire la fotocamera",
                "OK");
        }
    }

    [RelayCommand]
    private async Task Invia()
    {
        try
        {
            IsBusy = true;

            // VALIDAZIONE GUEST
            if (IsGuest)
            {
                if (string.IsNullOrWhiteSpace(OspiteNome) ||
                    string.IsNullOrWhiteSpace(OspiteCognome) ||
                    string.IsNullOrWhiteSpace(OspiteEmail))
                {
                    await Shell.Current.DisplayAlert(
                        "Errore",
                        "Compila tutti i campi ospite",
                        "OK");

                    return;
                }
            }

            // VALIDAZIONE POSIZIONE
            if (double.IsNaN(_latitudine) ||
                double.IsNaN(_longitudine))
            {
                await Shell.Current.DisplayAlert(
                    "Errore",
                    "Posizione non valida",
                    "OK");

                return;
            }

            var dto =
                new InviaSegnalazioneDto
                {
                    CategoriaId = _categoriaId,

                    Latitudine =
                        Convert.ToDecimal(_latitudine),

                    Longitudine =
                        Convert.ToDecimal(_longitudine),

                    Nota = Nota,

                    FotoBase64 = _fotoBase64,

                    OspiteNome = OspiteNome,

                    OspiteCognome = OspiteCognome,

                    OspiteEmail = OspiteEmail
                };

            await _apiService.InviaSegnalazione(dto);

            await Shell.Current.DisplayAlert(
                "Successo",
                "Segnalazione inviata correttamente",
                "OK");

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert(
                "Errore",
                ex.Message,
                "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
