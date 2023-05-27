using EntityFrameworkCore.RepositoryInfrastructure;

namespace VolunteerManager.Data.Entities;

public class OrganizationRequest : Entity, IEntity
{
    public Guid OrganizationId { get; set; }

    public Organization? Organization { get; set; }

    public Guid RequestId { get; set; }

    public Request? Request { get; set; }

    public decimal ServedCount { get; set; }
}