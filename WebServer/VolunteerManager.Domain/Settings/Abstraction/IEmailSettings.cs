namespace VolunteerManager.Domain.Settings.Abstraction;

public interface IEmailSettings
{
    string Password { get; set; }

    public string FromEmail { get; set; }

    string FromDisplayName { get; set; }
}