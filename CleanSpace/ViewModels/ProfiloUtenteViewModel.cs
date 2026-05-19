using CleanSpace.DTOs;
using CleanSpace.Services;
using CleanSpace.Views.Auth;
using CleanSpace.Views.Profilo;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanSpace.ViewModels;

public partial class ProfiloUtenteViewModel
    : ObservableObject
{
    private readonly ProfiloService _profiloService;

    [ObservableProperty]
    private string nome;

    [ObservableProperty]
    private string cognome;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private bool isBusy;

    public bool IsNotBusy => !IsBusy;

    public ProfiloUtenteViewModel(
        ProfiloService profiloService)
    {
        _profiloService = profiloService;

        Task.Run(async () =>
            await LoadUtente());
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
            var utente =
                await _profiloService
                    .GetProfilo();

            Nome = utente.Nome;

            Cognome = utente.Cognome;

            Email = utente.Email;
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
    private async Task Salva()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            var dto =
                new AggiornaProfiloDto
                {
                    Nome = Nome,
                    Cognome = Cognome,
                    Email = Email
                };

            await _profiloService
                .AggiornaProfilo(dto);

            await Shell.Current.DisplayAlert(
                "Successo",
                "Profilo aggiornato",
                "OK");
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
    private async Task ApriStorico()
    {
        await Shell.Current.GoToAsync(
            nameof(StoricoSegnalazioni));
    }

    [RelayCommand]
    private async Task Logout()
    {
        SecureStorage.Remove("access_token");

        Application.Current.MainPage =
            new AppShell();

        await Shell.Current.GoToAsync(
            $"//{nameof(LoginPage)}");
    }
}