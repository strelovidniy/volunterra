using VolunteerManager.Data.Enums;

namespace VolunteerManager.Models.Create;

public class CreateOrganizationRequestReplyModel : IValidatableModel
{
    public Guid? RequestId { get; set; }

    public UserStatus Status { get; set; }
}