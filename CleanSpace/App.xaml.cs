using CleanSpace.Views.Auth;
using CleanSpace.ViewModels;
using CleanSpace.Services;

namespace CleanSpace;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}