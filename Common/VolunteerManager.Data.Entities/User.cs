using EntityFrameworkCore.RepositoryInfrastructure;
using VolunteerManager.Data.Enums;

namespace VolunteerManager.Data.Entities;

public class User : Entity, IEntity
{
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}";

    public UserStatus Status { get; set; }

    public Guid? InvitationToken { get; set; }

    public Guid? VerificationCode { get; set; }

    public Guid? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpirationDate { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? ImageDataUrl { get; set; }

    public Organization? Organization { get; set; }
    public ContactInfo? ContactInfo { get; set; }
    public Guid? OrganizationId { get; set; }
    public Guid? ContactInfoId { get; set; }
    public IEnumerable<OrganizationRequestReply>? RequestReplies { get; set; }
    
    public IEnumerable<Achievement>? Achievements { get; set; }
    public bool IsOrganizationOwner { get; set; }

    public bool IsOrganizationAdmin { get; set; }
}