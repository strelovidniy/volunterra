using VolunteerManager.Domain.Settings.Abstraction;

namespace VolunteerManager.Domain.Settings.Realization;

internal class UrlSettings : IUrlSettings
{
    public string ResetPasswordUrl { get; set; } = null!;

    public string RegisterUrl { get; set; } = null!;

    public string WebApiUrl { get; set; } = null!;

    public string TwitterUrl { get; set; } = null!;

    public string FacebookUrl { get; set; } = null!;

    public string InstagramUrl { get; set; } = null!;
}