using VolunteerManager.Domain.Settings.Abstraction;

namespace VolunteerManager.Domain.Settings.Realization;

public class JwtSettings : IJwtSettings
{
    public string TokenSecretString { get; set; } = null!;

    public long SecondsToExpireToken { get; set; }

    public long SecondsToExpireRefreshToken { get; set; }

    public string Issuer { get; set; } = null!;

    public IEnumerable<string> Audiences { get; set; } = null!;
}