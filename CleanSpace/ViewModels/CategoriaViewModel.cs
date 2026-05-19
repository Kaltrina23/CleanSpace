using CleanSpace.Models;
using CleanSpace.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CleanSpace.Views.Segnala;

namespace CleanSpace.ViewModels;

public partial class CategoriaViewModel : ObservableObject
{
    private readonly CategorieService _apiService;

    public ObservableCollection<Categoria>
        Categorie
    { get; set; } = new();

    public CategoriaViewModel(CategorieService apiService)
    {
        _apiService = apiService;

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

    [RelayCommand]
    private async Task TornaHome()
    {
        await Shell.Current.GoToAsync("..");
    }
}