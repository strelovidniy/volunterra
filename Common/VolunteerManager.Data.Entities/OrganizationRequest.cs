using EntityFrameworkCore.RepositoryInfrastructure;
using VolunteerManager.Data.Enums;

namespace VolunteerManager.Data.Entities;

public class OrganizationRequest : Entity, IEntity
{
    public Guid OrganizationId { get; set; }

    public Organization? Organization { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;
    public OrganizationRequestCategory OrganizationRequestCategory { get; set; }

    public IEnumerable<Skill> Skills { get; set; } 
    public IEnumerable<Achievement> Achievements { get; set; } 
   // public string ImageData { get; set; } = null!;
}