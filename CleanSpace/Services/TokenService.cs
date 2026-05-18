using System;
using System.Threading.Tasks;

namespace CleanSpace.Services;

public class TokenService
{
    private const string AccessTokenKey = "access_token";

    private const string RefreshTokenKey = "refresh_token";

    private const string RuoloKey = "user_role";

    private const string NomeKey = "user_nome";

    private const string CognomeKey = "user_cognome";

    private const string GuestKey = "is_guest";

    public async Task SaveAccessToken(string token)
    {
        await SecureStorage.SetAsync(
            AccessTokenKey,
            token);
    }

    public async Task<string?> GetAccessToken()
    {
        return await SecureStorage.GetAsync(
            AccessTokenKey);
    }

    public async Task SaveRefreshToken(string token)
    {
        await SecureStorage.SetAsync(
            RefreshTokenKey,
            token);
    }

    public async Task<string?> GetRefreshToken()
    {
        return await SecureStorage.GetAsync(
            RefreshTokenKey);
    }

    public async Task SaveRole(string role)
    {
        await SecureStorage.SetAsync(
            RuoloKey,
            role);
    }

    public async Task<string?> GetRole()
    {
        return await SecureStorage.GetAsync(
            RuoloKey);
    }

    public async Task SaveNome(string nome)
    {
        await SecureStorage.SetAsync(
            NomeKey,
            nome ?? "");
    }

    public async Task<string?> GetNome()
    {
        return await SecureStorage.GetAsync(
            NomeKey);
    }

    public async Task SaveCognome(string cognome)
    {
        await SecureStorage.SetAsync(
            CognomeKey,
            cognome ?? "");
    }

    public async Task<string?> GetCognome()
    {
        return await SecureStorage.GetAsync(
            CognomeKey);
    }

    // GUEST
    public async Task SetGuest(bool isGuest)
    {
        await SecureStorage.SetAsync(
            GuestKey,
            isGuest.ToString());
    }

    public async Task<bool> IsGuest()
    {
        var value =
            await SecureStorage.GetAsync(
                GuestKey);

        return bool.TryParse(value, out var result)
            && result;
    }

    public void Logout()
    {
        SecureStorage.Remove(AccessTokenKey);

        SecureStorage.Remove(RefreshTokenKey);

        SecureStorage.Remove(RuoloKey);

        SecureStorage.Remove(NomeKey);

        SecureStorage.Remove(CognomeKey);

        SecureStorage.Remove(GuestKey);
    }
}