using VolunteerManager.Data.Enums;

namespace VolunteerManager.Models.Create;

public class CreateOrganizationRequestModel : IValidatableModel
{
    public Guid OrganizationId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;

    public OrganizationRequestCategory Category { get; set; }

    public string? ImageDataUrl { get; set; }

    public IEnumerable<string> Skills { get; set; } = null!;
}