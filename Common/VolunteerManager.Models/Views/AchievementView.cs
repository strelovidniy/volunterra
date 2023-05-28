using VolunteerManager.Data.Enums;

namespace VolunteerManager.Models.Views;

public class AchievementView
{
    public Guid Id { get; set; }

    public AchievementType Type { get; set; }

    public Seniority Seniority { get; set; }

    public string Title { get; set; }
}