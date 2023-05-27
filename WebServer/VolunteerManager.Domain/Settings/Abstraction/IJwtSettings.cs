namespace VolunteerManager.Domain.Settings.Abstraction;

public interface IJwtSettings
{
    public string TokenSecretString { get; set; }

    public long SecondsToExpireToken { get; set; }

    public long SecondsToExpireRefreshToken { get; set; }

    public string Issuer { get; set; }

    public IEnumerable<string> Audiences { get; set; }
}