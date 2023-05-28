using VolunteerManager.Data.Enums;

namespace VolunteerManager.Models.Update;

public class UpdateOrganizationRequestReplyModel : IValidatableModel
{
    public Guid? ReplyId { get; set; }

    public UserStatus Status { get; set; }
}