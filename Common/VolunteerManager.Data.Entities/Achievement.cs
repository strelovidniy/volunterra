using EntityFrameworkCore.RepositoryInfrastructure;
using VolunteerManager.Data.Enums;

namespace VolunteerManager.Data.Entities;

public class Achievement : Entity, IEntity
{
    public AchievementType Type { get; set; }

    public Seniority Seniority { get; set; }

    public string Title { get; set; }

    public User User { get; set; }

    public Guid UserId { get; set; }
}