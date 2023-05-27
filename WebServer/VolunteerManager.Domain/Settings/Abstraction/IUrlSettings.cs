namespace VolunteerManager.Domain.Settings.Abstraction;

public interface IUrlSettings
{
    public string ResetPasswordUrl { get; set; }

    public string RegisterUrl { get; set; }

    public string WebApiUrl { get; set; }

    public string TwitterUrl { get; set; }

    public string FacebookUrl { get; set; }

    public string InstagramUrl { get; set; }
}