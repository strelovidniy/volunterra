using VolunteerManager.Data.Enums;

namespace VolunteerManager.Models.Update
{
    internal class UpdateOrganizationInvocationReplyModel
    {
        public Guid? UserId { get; set; }

        public Guid? OrganizationRequestId { get; set; }

        public UserStatus Status { get; set; }
    }
}
