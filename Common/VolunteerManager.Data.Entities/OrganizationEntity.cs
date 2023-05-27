using EntityFrameworkCore.RepositoryInfrastructure;

namespace VolunteerManager.Data.Entities;

public class OrganizationEntity : Entity, IEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    
    // add skills

    public string? ImageDataUrl { get; set; }

    public string GoogleEmail { get; set; } = null!;

    public IEnumerable<OrganizationRequest>? OrganizationRequests { get; set; }
}