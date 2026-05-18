using CleanSpace.ViewModels;
using CleanSpace.Views.Home;
using CleanSpace.Services;
using Microsoft.Maui.Controls;

namespace CleanSpace.Views.Auth;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}
