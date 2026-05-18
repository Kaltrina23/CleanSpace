using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanSpace.Services;
using CleanSpace.Views.Home;
using CleanSpace.Views.Segnala;
using CleanSpace.Models;

namespace CleanSpace.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly AuthServices _authServices;

    private readonly TokenService _tokenService;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private bool isBusy;

    public LoginViewModel(
        AuthServices authServices,
        TokenService tokenService)
    {
        _authServices = authServices;
        _tokenService = tokenService;
    }

    [RelayCommand]
    private async Task Login()
    {
        if (string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert(
                "Errore",
                "Compila tutti i campi",
                "OK");

            return;
        }

        try
        {
            IsBusy = true;

            var result =
                await _authServices.Login(
                    Email,
                    Password);

            if (result == null)
            {
                await Shell.Current.DisplayAlert(
                    "Errore",
                    "Email o password errati",
                    "OK");

                return;
            }

            // SALVATAGGIO TOKEN
            await _tokenService.SaveAccessToken(
                result.AccessToken);

            await _tokenService.SaveRefreshToken(
                result.RefreshToken);

            await _tokenService.SaveRole(
                result.Ruolo);

            await _tokenService.SaveNome(
                result.Nome ?? "");

            await _tokenService.SaveCognome(
                result.Cognome ?? "");

            // NON È GUEST
            await _tokenService.SetGuest(false);

            // NAVIGAZIONE
            var ruolo =
                result.Ruolo?
                .Trim()
                .ToLower();

            if (ruolo.Contains("operatore"))
            {
                await Shell.Current.GoToAsync(nameof(HomeOperatore));
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(HomeUtente));
            }
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

    [RelayCommand]
    private async Task GuestLogin()
    {
        // PULIZIA DATI LOGIN PRECEDENTE
        _tokenService.Logout();

        // IMPOSTO GUEST
        await _tokenService.SetGuest(true);

        // NAVIGAZIONE
        await Shell.Current.GoToAsync(nameof(CategoriePage));
    }
}