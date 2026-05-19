using CleanSpace.ViewModels;

namespace CleanSpace.Views.Profilo;

public partial class ProfiloUtente : ContentPage
{
	public ProfiloUtente(ProfiloUtenteViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}