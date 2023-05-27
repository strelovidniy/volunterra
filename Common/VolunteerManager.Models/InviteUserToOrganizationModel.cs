namespace VolunteerManager.Models;

public class InviteUserToOrganizationModel : IValidatableModel
{
    public Guid OrganizationId { get; set; }

    public string Email { get; set; } = null!;
}