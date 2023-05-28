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

    public List<Skill> Skills { get; } = new();
    
   // public string ImageData { get; set; } = null!;
}