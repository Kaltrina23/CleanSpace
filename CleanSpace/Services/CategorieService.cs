using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using CleanSpace.Models;

namespace CleanSpace.Services;

public class CategorieService : ApiService
{
    public CategorieService(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public async Task<List<Categoria>> GetCategorie()
    {
        var response =
            await HttpClient.GetAsync("categorie");

        response.EnsureSuccessStatusCode();

        var json =
            await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Categoria>>
        (
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        ) ?? new List<Categoria>();
    }
}
