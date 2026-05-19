using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using CleanSpace.DTOs;
using CleanSpace.Models;

namespace CleanSpace.Services;

public class AuthServices
{
    private readonly HttpClient _httpClient;

    public AuthServices(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress =
            new Uri("https://10.0.2.2:7097/api/");

        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }
   
    public async Task<AuthResultModel?> Login(string email, string password)
    {
        var request = new AuthRequestDto
        {
            Email = email,
            Password = password
        };


        var response =
            await _httpClient.PostAsJsonAsync(
                "auth/login",
                request);

        Console.WriteLine(response.StatusCode);

        if (!response.IsSuccessStatusCode)
            return null;

        if (!response.Headers.Contains("authorization"))
            return null;

        var authorization =
            response.Headers
                .GetValues("authorization")
                .FirstOrDefault();

        if (string.IsNullOrWhiteSpace(authorization))
            return null;

        var accessToken =
            authorization.Replace("Bearer ", "");

        string? refreshToken = null;

        if (response.Headers.Contains("refreshtoken"))
        {
            refreshToken =
                response.Headers
                    .GetValues("refreshtoken")
                    .FirstOrDefault();
        }

        string ruolo = "Utente";

        try
        {
            var handler = new JwtSecurityTokenHandler();

            var jwtToken =
                handler.ReadJwtToken(accessToken);

            foreach (var claim in jwtToken.Claims)
            {
                Console.WriteLine($"{claim.Type} = {claim.Value}");
            }

            ruolo =
                jwtToken.Claims
                    .FirstOrDefault(x =>
                        x.Type.Contains("role"))
                    ?.Value
                ?? "Utente";
        }
        catch
        {
            ruolo = "Utente";
        }

        return new AuthResultModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Ruolo = ruolo
        };
    }
}