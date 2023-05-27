using VolunteerManager.Domain.Settings.Abstraction;

namespace VolunteerManager.Domain.Settings.Realization;

public class AuthSettings : IAuthSettings
{
    public IEnumerable<string> AllowedOrigins { get; set; } = null!;
}