using VolunteerManager.UI.Domain.Settings.Abstraction;

namespace VolunteerManager.UI.Domain.Settings.Realization;

internal class UrlSettings : IUrlSettings
{
    public string WebApiUrl { get; set; } = null!;
}