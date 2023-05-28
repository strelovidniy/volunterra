using VolunteerManager.Data.Enums;

namespace VolunteerManager.Models.Update;

public class UpdateOrganizationRequestReplyModel : IValidatableModel
{
    public Guid? UserId { get; set; }

    public Guid? OrganizationRequestId { get; set; }

    public UserStatus Status { get; set; }
}