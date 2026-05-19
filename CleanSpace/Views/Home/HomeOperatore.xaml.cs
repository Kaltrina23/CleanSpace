using Microsoft.Maui.Controls;
using CleanSpace.ViewModels;

namespace CleanSpace.Views.Home;

public partial class HomeOperatore : ContentPage
{
	public HomeOperatore(HomeOperatoreViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}