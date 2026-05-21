namespace CleanSpace.Models;

public class AuthResultModel
{
    public int IDUtente { get; set; }
    public string AccessToken { get; set; }
    public int ExpiresSec { get; set; }
    public string RefreshToken { get; set; }
    public string Ruolo { get; set; }
}