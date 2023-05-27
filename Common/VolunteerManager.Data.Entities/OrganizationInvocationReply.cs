using EntityFrameworkCore.RepositoryInfrastructure;
using VolunteerManager.Data.Enums;

namespace VolunteerManager.Data.Entities;
public class OrganizationInvocationReply : Entity, IEntity
{
    public Guid? UserId { get; set; }

    public User? User { get; set; }

    public Guid? OrganizationId { get; set; }

    public Organization? Organization { get; set; }

    public UserStatus Status { get; set; }
}