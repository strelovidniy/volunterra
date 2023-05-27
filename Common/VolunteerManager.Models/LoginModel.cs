namespace VolunteerManager.Models;

public class LoginModel : IValidatableModel
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}