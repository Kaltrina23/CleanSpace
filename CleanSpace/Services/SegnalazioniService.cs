using CleanSpace.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanSpace.Services;

public class SegnalazioniService : ApiService
{
    public SegnalazioniService(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task InviaSegnalazione(InviaSegnalazioneDto dto)
    {
        await AddAuthorizationHeader();

        var json = JsonSerializer.Serialize(dto);

        var content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

        var response =
            await HttpClient.PostAsync(
                "segnalazioni",
                content);

        response.EnsureSuccessStatusCode();
    }


    public async Task<int> GetComuneID(double lat, double lon)
    {
        var response =
            await HttpClient.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lon}&key=AIzaSyBnnFLX8o76B0sS5lfXOXHFzjtiFDsS-nw");
        response.EnsureSuccessStatusCode();

        GoogleGeocodeResponse? datiPosizioneGoogle = await response.Content.ReadFromJsonAsync<GoogleGeocodeResponse>();
        string? nomeComune = datiPosizioneGoogle.Results.First().AddressComponents.FirstOrDefault(x => x.Types.Contains("locality"))?.LongName;

        await AddAuthorizationHeader();

        var responseComune = await HttpClient.GetAsync($"segnalazioni/{nomeComune}/getIdComune");
        responseComune.EnsureSuccessStatusCode();
        string content = await responseComune.Content.ReadAsStringAsync();
        int idComune = Convert.ToInt32(content);
        return idComune;
    }

    // Lista
    public async Task<List<SegnalazioneRispostaDto>>GetSegnalazioni()
    {
        await AddAuthorizationHeader();

        var response =
            await HttpClient.GetAsync(
                "segnalazioni");

        response.EnsureSuccessStatusCode();

        var json =
            await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<SegnalazioneRispostaDto>>
        (
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        ) ?? new List<SegnalazioneRispostaDto>();
    }

    public async Task ApprovaSegnalazione(int id)
    {
        await AddAuthorizationHeader();

        var response =
            await HttpClient.PutAsync(
                $"segnalazioni/{id}/approva",
                new StringContent(
                    "\"Alta\"",
                    Encoding.UTF8,
                    "application/json"));

        response.EnsureSuccessStatusCode();
    }

    public async Task AggiornaPriorita(int id, string priorita)
    {
        await AddAuthorizationHeader();

        var content =
            new StringContent(
                JsonSerializer.Serialize(priorita),
                Encoding.UTF8,
                "application/json");

        var response =
            await HttpClient.PutAsync(
                $"segnalazioni/{id}/priorita",
                content);

        response.EnsureSuccessStatusCode();
    }

    public async Task EliminaSegnalazione(int id)
    {
        await AddAuthorizationHeader();

        var response =
            await HttpClient.DeleteAsync(
                $"segnalazioni/{id}");

        response.EnsureSuccessStatusCode();
    }
}



public class GoogleGeocodeResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("results")]
    public List<GeocodeResult> Results { get; set; } = new();

    [JsonPropertyName("error_message")]
    public string? ErrorMessage { get; set; }
}

public class GeocodeResult
{
    [JsonPropertyName("address_components")]
    public List<AddressComponent> AddressComponents { get; set; } = new();

    [JsonPropertyName("formatted_address")]
    public string FormattedAddress { get; set; } = string.Empty;
}

public class AddressComponent
{
    [JsonPropertyName("long_name")]
    public string LongName { get; set; } = string.Empty;

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; } = string.Empty;

    [JsonPropertyName("types")]
    public List<string> Types { get; set; } = new();
}

public class IndirizzoMaui
{
    public string Via { get; set; } = string.Empty;
    public string Civico { get; set; } = string.Empty;
    public string Citta { get; set; } = string.Empty;
    public string Stato { get; set; } = string.Empty; // "HI"
    public string Cap { get; set; } = string.Empty;
    public string IndirizzoCompleto { get; set; } = string.Empty;

   
}