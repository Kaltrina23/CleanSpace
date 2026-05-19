using System.Windows.Input;

namespace CleanSpace.Views.Components;

public partial class CategoriaCard : ContentView
{
    public CategoriaCard()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TitoloProperty =
        BindableProperty.Create(
            nameof(Titolo),
            typeof(string),
            typeof(CategoriaCard));

    public string Titolo
    {
        get => (string)GetValue(TitoloProperty);
        set => SetValue(TitoloProperty, value);
    }

    public static readonly BindableProperty DescrizioneProperty =
        BindableProperty.Create(
            nameof(Descrizione),
            typeof(string),
            typeof(CategoriaCard));

    public string Descrizione
    {
        get => (string)GetValue(DescrizioneProperty);
        set => SetValue(DescrizioneProperty, value);
    }


    public static readonly BindableProperty IconaProperty =
        BindableProperty.Create(
            nameof(Icona),
            typeof(string),
            typeof(CategoriaCard));

    public string Icona
    {
        get => (string)GetValue(IconaProperty);
        set => SetValue(IconaProperty, value);
    }

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(CategoriaCard));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }


    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(CategoriaCard));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
}