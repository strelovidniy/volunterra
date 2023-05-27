using EntityFrameworkCore.RepositoryInfrastructure;

namespace VolunteerManager.Data.Entities;

public class OrganizationRequest : Entity, IEntity
{
    public Guid OrganizationId { get; set; }

    public Organization? Organization { get; set; }

    public Guid RequestId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }

}