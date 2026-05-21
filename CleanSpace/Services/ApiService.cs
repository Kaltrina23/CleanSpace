using System.Text;
using System.Text.Json;
using CleanSpace.Models;
using CleanSpace.DTOs;
using System.Net.Http.Headers;

namespace CleanSpace.Services;

public class ApiService
{
    protected readonly HttpClient HttpClient;

    public ApiService(HttpClient httpClient)
    {

        this.HttpClient = httpClient;
    }

    protected async Task AddAuthorizationHeader()
    {
        var token =  await SecureStorage.GetAsync("access_token");

        if (!string.IsNullOrEmpty(token))
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}