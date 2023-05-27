using EntityFrameworkCore.RepositoryInfrastructure;

namespace VolunteerManager.Data.Entities;

public class Organization : Entity, IEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageDataUrl { get; set; }

    public string GoogleEmail { get; set; } = null!;

    public IEnumerable<User>? Users { get; set; }

    public IEnumerable<OrganizationRequest>? OrganizationRequests { get; set; }
}