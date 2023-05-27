namespace VolunteerManager.Models.Views;

public class OrganizationRequestView
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImageDataUrl { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateTime RequestDate { get; set; }

    public DateTime? RequestUpdatedAt { get; set; }

    public UserView? CreatedBy { get; set; }
}