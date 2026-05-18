using System.Text;
using System.Text.Json;
using CleanSpace.Models;
using CleanSpace.DTOs;
using System.Net.Http.Headers;

namespace CleanSpace.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            #if ANDROID

            var handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) =>
                {
                    return true;
                };

            _httpClient = new HttpClient(handler);

            #else

            _httpClient = new HttpClient();

            #endif

            _httpClient.BaseAddress =
                new Uri("https://10.0.2.2:7097/api/");
        }

        public async Task InviaSegnalazione(InviaSegnalazioneDto dto)
        {
            var json = JsonSerializer.Serialize(dto);//Conversione oggetto C# in JSON

            var content = new StringContent(json);//Creazione contenuto HTTP

            content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");//Invio Json

            await _httpClient.PostAsync(//Invia POST /api/segnalazioni
                "segnalazioni",
                content);
        }

        public async Task<List<Categoria>> GetCategorie()
        {
            var response =
                await _httpClient.GetAsync("categorie");

            response.EnsureSuccessStatusCode();

            var json =
                await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Categoria>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<List<SegnalazioneRispostaDto>> GetSegnalazioni()
        {
            var token =
                await SecureStorage.GetAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response =
                await _httpClient.GetAsync("segnalazioni");

            response.EnsureSuccessStatusCode();

            var json =
                await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize
                <List<SegnalazioneRispostaDto>>
                (
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );
        }

        public async Task AggiornaPriorita(int id,string priorita)
        {
            var token =
                await SecureStorage.GetAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var content =
                new StringContent(
                    JsonSerializer.Serialize(priorita),
                    Encoding.UTF8,
                    "application/json");

            var response =
                await _httpClient.PutAsync(
                    $"segnalazioni/{id}/priorita",
                    content);

            response.EnsureSuccessStatusCode();
        }


        public async Task ApprovaSegnalazione(int id)
        {
            var token =
                await SecureStorage.GetAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response =
                await _httpClient.PutAsync(
                    $"segnalazioni/{id}/approva",
                    new StringContent("\"Alta\"",
                    Encoding.UTF8,
                    "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task EliminaSegnalazione(int id)
        {
            var token =
                await SecureStorage.GetAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response =
                await _httpClient.DeleteAsync(
                    $"segnalazioni/{id}");

            response.EnsureSuccessStatusCode();
        }
    }
}