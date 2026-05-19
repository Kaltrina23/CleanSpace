using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CleanSpace.Services;
using CleanSpace.Views.Segnala;

namespace CleanSpace.ViewModels;

public partial class HomeUtenteViewModel : ObservableObject
{
    private readonly TokenService _tokenService;

    [ObservableProperty]
    private string nomeUtente;

    public HomeUtenteViewModel
    (
        TokenService tokenService
    )
    {
        _tokenService = tokenService;

        Task.Run(CaricaUtente);
    }

    private async Task CaricaUtente()
    {
        try
        {
            var nome =
                await _tokenService.GetNome();

            var cognome =
                await _tokenService.GetCognome();

            NomeUtente =
                $"{nome} {cognome}";
        }
        catch
        {
            NomeUtente =
                "Utente";
        }
    }

    [RelayCommand]
    private async Task Segnala()
    {
        await Shell.Current.GoToAsync(
            nameof(CategoriePage));
    }
}