namespace VolunteerManager.Models.Views;

public class RequestView
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageDataUrl { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal RemainingAmount { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? RequestUpdatedAt { get; set; }

    public UserView? CreatedBy { get; set; }
}