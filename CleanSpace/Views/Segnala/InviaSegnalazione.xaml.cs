using CleanSpace.ViewModels;

namespace CleanSpace.Views.Segnala;

[QueryProperty(nameof(CategoriaId), "categoriaId")]
[QueryProperty(nameof(CategoriaNome), "categoriaNome")]

public partial class InviaSegnalazione : ContentPage
{
    private readonly InviaSegnalazioneViewModel _viewModel;

    public string CategoriaId
    {
        set
        {
            _viewModel.CategoriaId = int.Parse(value);
        }
    }

    public string CategoriaNome
    {
        set
        {
            _viewModel.CategoriaNome = Uri.UnescapeDataString(value);
        }
    }

    public InviaSegnalazione(InviaSegnalazioneViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = _viewModel;
    }

    async void Foto(object sender, EventArgs e)
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
            status = await Permissions.RequestAsync<Permissions.Camera>();

        if (status != PermissionStatus.Granted)
            return;

        if (MediaPicker.Default.IsCaptureSupported){
            FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                using var stream = await photo.OpenReadAsync();
                string base64 = ConvertToBase64(stream);
                await DisplayAlert("OK", "Foto convertita in Base64!", "OK"); 
            }
        }

    }

    string ConvertToBase64(Stream stream)
    {
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        byte[] imageBytes = memoryStream.ToArray();

        return Convert.ToBase64String(imageBytes);
    }
}