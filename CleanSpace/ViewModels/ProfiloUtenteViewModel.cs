using CleanSpace.Services;
using CleanSpace.Views.Auth;
using CleanSpace.Views.Profilo;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanSpace.ViewModels;

public partial class ProfiloUtenteViewModel : ObservableObject
{
    private readonly TokenService _tokenService;

    [ObservableProperty]
    private string nome;

    [ObservableProperty]
    private string cognome;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private bool isBusy;

    public bool IsNotBusy => !IsBusy;

    public ProfiloUtenteViewModel(TokenService tokenService)
    {
        _tokenService = tokenService;

        Task.Run(async () => await LoadUtente());
    }

    partial void OnIsBusyChanged(bool value)
    {
        OnPropertyChanged(nameof(IsNotBusy));
    }

    private async Task LoadUtente()
    {
        IsBusy = true;

        try
        {
            Nome = await _tokenService.GetNome();
            Cognome = await _tokenService.GetCognome();
            Email = await _tokenService.GetEmail();

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

    //[RelayCommand]
    //private async Task Salva()
    //{
    //    if (IsBusy)
    //        return;

    //    IsBusy = true;

    //    try
    //    {
    //        await _tokenService.SaveNome(Nome);

    //        await _tokenService.SaveCognome(Cognome);

    //        await _tokenService.SaveEmail(Email);

    //        await Shell.Current.DisplayAlert(
    //            "Successo",
    //            "Profilo aggiornato",
    //            "OK");
    //    }
    //    catch (Exception ex)
    //    {
    //        await Shell.Current.DisplayAlert(
    //            "Errore",
    //            ex.Message,
    //            "OK");
    //    }
    //    finally
    //    {
    //        IsBusy = false;
    //    }
    //}

    [RelayCommand]
    private async Task ApriStorico()
    {
        await Shell.Current.GoToAsync(nameof(StoricoSegnalazioni));
    }

    [RelayCommand]
    private async Task Logout()
    {
        _tokenService.Logout();

        Application.Current.MainPage = new AppShell();

        await Shell.Current.GoToAsync( $"//{nameof(LoginPage)}");
    }
}