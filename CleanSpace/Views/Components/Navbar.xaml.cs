using CleanSpace.Services;
using CleanSpace.ViewModels;
using CleanSpace.Views.Profilo;

namespace CleanSpace.Views.Components;

public partial class Navbar : ContentView
{
    bool IsUtente;
    readonly TokenService _tokenService;

    public Navbar()
	{
        _tokenService = IPlatformApplication.Current.Services.GetService<TokenService>();
        BindingContext = this;
        Task.Run(async ()=>
        {
            await SetIsUtente();
        });
        InitializeComponent();
    }
    private async Task SetIsUtente()
    {
        string ruolo = await _tokenService.GetRole();
        if (ruolo == "utente")
            IsUtente = true;
        else
            IsUtente = false;
    }
    private async void ApriProfilo(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfiloUtente));
    }
}