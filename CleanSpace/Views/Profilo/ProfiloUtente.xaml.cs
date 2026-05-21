using CleanSpace.ViewModels;
using System.Runtime.CompilerServices;

namespace CleanSpace.Views.Profilo;

public partial class ProfiloUtente : ContentPage
{
	private readonly ProfiloUtenteViewModel _viewModel;
    public ProfiloUtente(ProfiloUtenteViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
	}
}