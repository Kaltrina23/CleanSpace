using CleanSpace.Models;
using CleanSpace.Services;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CleanSpace.DTOs;

namespace CleanSpace.ViewModels
{
    public partial class HomeOperatoreViewModel
    {
        private readonly ApiService _apiService;

        private List<SegnalazioneRispostaDto> _tutteSegnalazioni;

        public ObservableCollection<SegnalazioneRispostaDto>
            Segnalazioni { get; set; } = new();

        public HomeOperatoreViewModel()
        {
            _apiService = new ApiService();

            _tutteSegnalazioni = new List<SegnalazioneRispostaDto>();

            Task.Run(async () => await LoadSegnalazioni());
        }

        private async Task LoadSegnalazioni()
        {
            var lista = await _apiService.GetSegnalazioni();

            _tutteSegnalazioni = lista;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Segnalazioni.Clear();

                foreach (var segnalazione in lista)
                {
                    Segnalazioni.Add(segnalazione);
                }
            });
        }

        [RelayCommand]
        private void MostraTutte()
        {
            Filtra(null);
        }

        [RelayCommand]
        private void MostraInAttesa()
        {
            Filtra("In Attesa");
        }

        [RelayCommand]
        private void MostraApprovate()
        {
            Filtra("Approvata");
        }

        private void Filtra(string stato)
        {
            Segnalazioni.Clear();

            var lista =
                string.IsNullOrEmpty(stato)
                ? _tutteSegnalazioni
                : _tutteSegnalazioni
                    .Where(x =>
                        x.Stato.Trim().ToLower()
                        == stato.Trim().ToLower())
                    .ToList();

            foreach (var item in lista)
            {
                Segnalazioni.Add(item);
            }
        }

        [RelayCommand]
        private async Task PrioritaAlta(SegnalazioneRispostaDto segnalazione)
        {
            if (segnalazione == null) return;

            await _apiService.AggiornaPriorita(
                segnalazione.Id,
                "Alta");

            segnalazione.Priorita = "Alta";

            await Shell.Current.DisplayAlert(
                "Priorità aggiornata",
                "La priorità è stata impostata su Alta",
                "OK");
        }

        [RelayCommand]
        private async Task PrioritaMedia(SegnalazioneRispostaDto segnalazione)
        {
            if (segnalazione == null) return;

            await _apiService.AggiornaPriorita(
                segnalazione.Id,
                "Media");

            segnalazione.Priorita = "Media";

            await Shell.Current.DisplayAlert(
                "Priorità aggiornata",
                "La priorità è stata impostata su Media",
                "OK");
        }

        [RelayCommand]
        private async Task PrioritaBassa(SegnalazioneRispostaDto segnalazione)
        {
            if (segnalazione == null) return;

            await _apiService.AggiornaPriorita(
                segnalazione.Id,
                "Bassa");

            segnalazione.Priorita = "Bassa";

            await Shell.Current.DisplayAlert(
                "Priorità aggiornata",
                "La priorità è stata impostata su Bassa",
                "OK");
        }


        [RelayCommand]
        private async Task Approva(SegnalazioneRispostaDto segnalazione)
        {
            await _apiService
                .ApprovaSegnalazione(segnalazione.Id);

            await Shell.Current.DisplayAlert(
               "Richiesta approvata",
               "La segnalazione è stata approvata con successo",
               "OK");

            await LoadSegnalazioni();
        }

        [RelayCommand]
        private async Task Elimina(SegnalazioneRispostaDto segnalazione)
        {
            await _apiService
                .EliminaSegnalazione(segnalazione.Id);

            await Shell.Current.DisplayAlert(
              "Richiesta eliminata",
              "La segnalazione è stata eliminata con successo",
              "OK");

            await LoadSegnalazioni();
        }
    }
}