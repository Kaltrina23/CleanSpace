using CleanSpace.Models;
using CleanSpace.Services;
using CleanSpace.Views.Segnala;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CleanSpace.ViewModels
{
    public partial class CategoriaViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        public ObservableCollection<Categoria> Categorie { get; set; } = new();

        public CategoriaViewModel()
        {
            _apiService = new ApiService();

            Task.Run(async () =>
                await LoadCategorie());
        }

        private async Task LoadCategorie()
        {
            try
            {
                var lista =
                    await _apiService.GetCategorie();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Categorie.Clear();

                    foreach (var categoria in lista)
                    {
                        Categorie.Add(categoria);
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

        [RelayCommand]
        private async Task SelezionaCategoria(
            Categoria categoria)
        {
            if (categoria == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(InviaSegnalazione)}" +
                $"?categoriaId={categoria.Id}" +
                $"&categoriaNome={categoria.Nome}");
        }
    }
}