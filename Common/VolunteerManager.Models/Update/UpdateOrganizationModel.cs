namespace VolunteerManager.Models.Update;

public class UpdateOrganizationModel : IValidatableModel
{
    public Guid OrganizationId { get; set; }

    public string? Description { get; set; }
}