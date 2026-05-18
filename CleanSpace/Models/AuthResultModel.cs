namespace CleanSpace.Models;

public class AuthResultModel
{
    public string Email { get; set; }
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public string AccessToken { get; set; }
    public int ExpiresSec { get; set; }
    public string RefreshToken { get; set; }
    public string Ruolo { get; set; }
}