using VolunteerManager.Data.Enums;

namespace VolunteerManager.Models.Views;

public class UserView
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}";

    public UserStatus Status { get; set; }

    public string? ImageDataUrl { get; set; }

    public OrganizationView? Organization { get; set; }

    public IEnumerable<AchievementView> Achievements { get; set; } = new List<AchievementView>();
}