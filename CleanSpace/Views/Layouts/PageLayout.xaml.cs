namespace CleanSpace.Views.Layouts;

public partial class PageLayout : ContentView
{
    public PageLayout()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TitoloProperty =
        BindableProperty.Create(
            nameof(Titolo),
            typeof(string),
            typeof(PageLayout));

    public string Titolo
    {
        get => (string)GetValue(TitoloProperty);
        set => SetValue(TitoloProperty, value);
    }
}