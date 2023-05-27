namespace VolunteerManager.Models.Update;

public class UpdateOrganizationRequestModel : IValidatableModel
{
    public Guid Id { get; set; }

    public string? Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageData { get; set; }
}