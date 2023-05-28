using VolunteerManager.Data.Enums;

namespace VolunteerManager.Models.Views;

public class OrganizationRequestReplyView
{
    public Guid Id { get; set; }

    public UserStatus Status { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? RequestUpdatedAt { get; set; }

    public UserView? CreatedBy { get; set; }
}