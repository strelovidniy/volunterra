using VolunteerManager.Data.Enums;

namespace VolunteerManager.Models.Create;

public class CreateOrganizationInvocationReplyModel : IValidatableModel
{
    public Guid? UserId { get; set; }

    public Guid? OrganizationRequestId { get; set; }

    public UserStatus Status { get; set; }
}