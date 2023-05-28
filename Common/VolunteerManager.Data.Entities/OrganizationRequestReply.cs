using EntityFrameworkCore.RepositoryInfrastructure;
using VolunteerManager.Data.Enums;

namespace VolunteerManager.Data.Entities;
public class OrganizationRequestReply : Entity, IEntity
{
    public Guid? UserId { get; set; }

    public User? User { get; set; }

    public Guid? OrganizationRequestId { get; set; }

    public OrganizationRequest? OrganizationRequest { get; set; }
    public UserStatus Status { get; set; }
}