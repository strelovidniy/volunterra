using VolunteerManager.Data.Enums;

namespace VolunteerManager.Models.Create;

public class CreateOrganizationRequestReplyModel : IValidatableModel
{
    public Guid? OrganizationRequestId { get; set; }

    public UserStatus Status { get; set; }
}