using CleanSpace.DTOs;
using CleanSpace.Models;
using System.Net.Http.Json;

namespace CleanSpace.Services;

public class StoricoSegnalazioniService: ApiService
{
    public StoricoSegnalazioniService(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<StoricoSegnalazioneDto>> GetStorico()
    {
        await AddAuthorizationHeader();

        return await HttpClient
            .GetFromJsonAsync<
                List<StoricoSegnalazioneDto>>(
                "segnalazioni/StoricoSegnalazioni")
            ?? new();
    }
}