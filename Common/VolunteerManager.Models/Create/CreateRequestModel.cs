namespace VolunteerManager.Models.Create;

public class CreateRequestModel : IValidatableModel
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageData { get; set; }

    public decimal TotalAmount { get; set; }
}