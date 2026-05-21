using CleanSpace.DTOs;
using CleanSpace.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace CleanSpace.ViewModels;

public partial class InviaSegnalazioneViewModel : ObservableObject
{
    private readonly SegnalazioniService _segnalazioniService;

    private readonly TokenService _tokenService;

    public Segnalazione Segnalazione { get; set; } = new Segnalazione();
    public int CategoriaID = 1;
    public string CategoriaNomeQuery = "nome";
    public bool IsGuest { get; set; }

    public InviaSegnalazioneViewModel (SegnalazioniService segnalazioniService, TokenService tokenService)
    {
        _segnalazioniService = segnalazioniService;

        _tokenService = tokenService;

        Task.Run(() =>
        {
            CaricaGuest();
        });
    }

    private async Task CaricaGuest()
    {
        IsGuest =  await _tokenService.IsGuest();
    }

    public async Task  SalvaFoto(FileResult foto)
    {
        Segnalazione.FotobBase64 = await ConvertToBase64(foto);
    }

    public async Task SalvaPosizione (double latitudine, double longitudine)
    {
        Segnalazione.Posizione.Lat = latitudine;
        Segnalazione.Posizione.Long = longitudine;
        int idComune = await _segnalazioniService.GetComuneID(latitudine, longitudine);
        Segnalazione.Posizione.IDComune = idComune;
    }

    private async Task<string> ConvertToBase64(FileResult photo)
    {
        var stream = await photo.OpenReadAsync();
        using var memoryStream = new MemoryStream();

        await stream.CopyToAsync(memoryStream);
        byte[] imageBytes = memoryStream.ToArray();

        return Convert.ToBase64String(imageBytes);
    }

    [RelayCommand]
    public async Task Invia()
    {
        try
        {
            if (IsGuest)
            {
                if (string.IsNullOrWhiteSpace(Segnalazione.OspiteNome) || string.IsNullOrWhiteSpace(Segnalazione.OspiteCognome) || string.IsNullOrWhiteSpace(Segnalazione.OspiteEmail))
                {
                    await Shell.Current.DisplayAlert(
                        "Errore",
                        "Compila tutti i campi",
                        "OK");
                    return;
                }
            }

            //if (double.IsNaN(_latitudine) || double.IsNaN(_longitudine))
            //{
            //    await Shell.Current.DisplayAlert(
            //        "Errore",
            //        "Inserire la posizione",
            //        "OK");
            //    return;
            //}


            InviaSegnalazioneDto dto = new InviaSegnalazioneDto();
            dto.CategoriaId = CategoriaID;
            dto.ComuneId = Segnalazione.Posizione.IDComune;
            dto.Latitudine = (decimal)Segnalazione.Posizione.Lat;
            dto.Longitudine = (decimal)Segnalazione.Posizione.Long;
            dto.Nota = Segnalazione.Nota;
            dto.FotoBase64 = Segnalazione.FotobBase64;
            dto.OspiteNome = Segnalazione.OspiteNome;
            dto.OspiteCognome = Segnalazione.OspiteCognome;
            dto.OspiteEmail = Segnalazione.OspiteEmail;


            await _segnalazioniService.InviaSegnalazione(dto);

            await Shell.Current.DisplayAlert(
                "Successo",
                "Segnalazione inviata correttamente",
                "OK");

            await Shell.Current.GoToAsync("/HomeUtente");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert(
                "Errore",
                ex.Message,
                "OK");
        }
    }
}