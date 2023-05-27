namespace VolunteerManager.Models.Views;

public class OrganizationView
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageDataUrl { get; set; }

    public string GoogleEmail { get; set; } = null!;

    public IEnumerable<UserView>? Users { get; set; }

    public IEnumerable<OrganizationRequestView>? OrganizationRequests { get; set; }
}