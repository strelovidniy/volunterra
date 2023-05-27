namespace VolunteerManager.Models.Update;

public class UpdateRequestModel : IValidatableModel
{
    public Guid RequestId { get; set; }

    public string? Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageData { get; set; }

    public int? TotalAmount { get; set; }
}