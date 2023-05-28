#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums.RichEnums;

namespace VolunteerManager.Data.EntityConfigurations;

internal class SkillsConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder
            .HasKey(user => user.Id);

        builder
            .ToTable(TableName.Skills, TableSchema.Dbo);

        builder
            .Property(user => user.Id)
            .HasDefaultValueSql(DefaultSqlValue.NewGuid)
            .IsRequired();
        
        builder
            .HasMany(e => e.OrganizationRequests)
            .WithMany(e => e.Skills);
    }
}