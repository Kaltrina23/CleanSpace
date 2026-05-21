using CleanSpace.DTOs;
using System.Net.Http.Json;

namespace CleanSpace.Services;

public class ProfiloService : ApiService
{
    public ProfiloService(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ProfiloDto> GetProfilo()
    {
        await AddAuthorizationHeader();

        return await HttpClient
            .GetFromJsonAsync<ProfiloDto>(
                "utenti/profilo");
    }

    public async Task AggiornaProfilo(AggiornaProfiloDto dto)
    {
        await AddAuthorizationHeader();

        var response =
            await HttpClient.PutAsJsonAsync(
                "utenti/profilo",
                dto);

        response.EnsureSuccessStatusCode();
    }
}