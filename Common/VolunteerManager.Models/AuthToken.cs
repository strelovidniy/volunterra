namespace VolunteerManager.Models;

public class AuthToken
{
    public string? Token { get; set; }

    public string? ExpireAt { get; set; }

    public Guid? RefreshToken { get; set; }
}