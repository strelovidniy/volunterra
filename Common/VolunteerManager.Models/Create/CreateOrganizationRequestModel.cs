namespace VolunteerManager.Models.Create;

public class CreateOrganizationRequestModel : IValidatableModel
{
    public Guid OrganizationId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string ImageData { get; set; } = null!;
}