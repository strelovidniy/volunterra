#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums;
using VolunteerManager.Data.Enums.RichEnums;

namespace VolunteerManager.Data.EntityConfigurations;

internal class AchievementsConfiguration : IEntityTypeConfiguration<Achievement>
{
    public void Configure(EntityTypeBuilder<Achievement> builder)
    {
        builder
            .HasKey(user => user.Id);

        builder
            .ToTable(TableName.Achievements, TableSchema.Dbo);

        builder
            .Property(user => user.Id)
            .HasDefaultValueSql(DefaultSqlValue.NewGuid)
            .IsRequired();

        builder
            .Property(user => user.Type)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValue(AchievementType.Beginner)
            .IsRequired();
        
        builder
            .Property(user => user.Title)
            .HasConversion<string>()
            .HasMaxLength(500)
            .IsRequired();
        
        builder
            .Property(user => user.Seniority)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValue(Seniority.Starter)
            .IsRequired();
    }
}