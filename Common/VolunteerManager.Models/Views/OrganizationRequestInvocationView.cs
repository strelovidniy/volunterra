namespace VolunteerManager.Models.Views;

public class OrganizationRequestInvocationView
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime RequestDate { get; set; }

    public DateTime? RequestUpdatedAt { get; set; }
    
    public UserView? CreatedBy { get; set; }
}