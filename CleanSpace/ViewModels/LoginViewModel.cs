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

    public LoginViewModel(AuthServices authServices, TokenService tokenService)
    {
        _authServices = authServices;
        _tokenService = tokenService;
    }

    [RelayCommand]
    private async Task Login()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
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

            var result = await _authServices.Login(Email, Password);


            System.Diagnostics.Debug.WriteLine(result);

            if (result == null)
            {
                await Shell.Current.DisplayAlert(
                    "Errore",
                    "Email o password errati",
                    "OK");

                return;
            }

            //Salvo il token
            await _tokenService.SaveAccessToken(result.AccessToken);

            await _tokenService.SaveRefreshToken(result.RefreshToken);

            await _tokenService.SaveRole(result.Ruolo);


            //await _tokenService.SaveNome(
            //    result.Nome ?? "");

            //await _tokenService.SaveCognome(
            //    result.Cognome ?? "");

            //await _tokenService.SaveEmail(
            //    result.Cognome ?? "");

            // NON È GUEST
            await _tokenService.SetGuest(false);

            // NAVIGAZIONE
            var ruolo =
                result.Ruolo?
                .Trim()
                .ToLower();

            if (ruolo.Contains("operatore"))
            {
                await Shell.Current.GoToAsync("//operatoreTabs");
            }
            else
            {
                await Shell.Current.GoToAsync("//utenteTabs");
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
        //rimozione del token precedente
        _tokenService.Logout();

        //imposto ruolo a guest
        await _tokenService.SetGuest(true);

        //navigazione
        await Shell.Current.GoToAsync(nameof(CategoriePage));
    }
}