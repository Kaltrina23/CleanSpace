using CommunityToolkit.Mvvm.ComponentModel;
using CleanSpace.Services;

namespace CleanSpace.ViewModels;

public partial class HomeUtenteViewModel : ObservableObject
{
    private readonly TokenService _tokenService;

    [ObservableProperty]
    private string nomeUtente;

    public HomeUtenteViewModel()
    {
        _tokenService = new TokenService();

        Task.Run(async () => await CaricaUtente());
    }

    private async Task CaricaUtente()
    {
        var nome = await _tokenService.GetNome();

        var cognome = await _tokenService.GetCognome();

        NomeUtente = $"{nome} {cognome}";
    }
}