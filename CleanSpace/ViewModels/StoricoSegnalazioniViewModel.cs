using CommunityToolkit.Mvvm.ComponentModel;
using CleanSpace.Models;
using CleanSpace.Services;
using System.Collections.ObjectModel;
using CleanSpace.DTOs;

namespace CleanSpace.ViewModels;

public partial class StoricoSegnalazioniViewModel : ObservableObject
{
    private readonly StoricoSegnalazioniService _service;

    public ObservableCollection <StoricoSegnalazioneDto> Segnalazioni { get; set; } = new();

    public StoricoSegnalazioniViewModel(StoricoSegnalazioniService service)
    {
        _service = service;

        Task.Run(async () =>  await LoadStorico());
    }

    private async Task LoadStorico()
    {
        try
        {
            var lista =  await _service.GetStorico();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Segnalazioni.Clear();

                foreach (var item in lista)
                {
                    Segnalazioni.Add(item);
                }
            });
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