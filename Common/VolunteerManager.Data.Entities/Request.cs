using EntityFrameworkCore.RepositoryInfrastructure;

namespace VolunteerManager.Data.Entities;

public class Request : Entity, IEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageDataUrl { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal RemainingAmount => TotalAmount - (OrganizationRequests?.Sum(d => d.ServedCount) ?? 0);

    public IEnumerable<OrganizationRequest>? OrganizationRequests { get; set; }

    public User? CreatedBy { get; set; }

    public Guid CreatedById { get; set; }
}