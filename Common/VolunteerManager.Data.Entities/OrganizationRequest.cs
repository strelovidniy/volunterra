using EntityFrameworkCore.RepositoryInfrastructure;

namespace VolunteerManager.Data.Entities;

public class OrganizationRequest : Entity, IEntity
{
    public Guid OrganizationId { get; set; }

    public Organization? Organization { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string ImageData { get; set; } = null!;

}