using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

using CleanSpace.DTOs;

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