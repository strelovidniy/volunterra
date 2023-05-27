namespace VolunteerManager.Models.Create;

public class CreateOrganizationRequestModel : IValidatableModel
{
    public Guid RequestId { get; set; }

    public int ServedCount { get; set; }
}