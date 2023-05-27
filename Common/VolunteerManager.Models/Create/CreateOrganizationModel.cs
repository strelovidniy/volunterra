namespace VolunteerManager.Models.Create;

public class CreateOrganizationModel : IValidatableModel
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;

    public string OrganizationName { get; set; } = null!;

    public string? OrganizationDescription { get; set; }
}