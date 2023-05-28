#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums;
using VolunteerManager.Data.Enums.RichEnums;

namespace VolunteerManager.Data.EntityConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(user => user.Id);

        builder
            .ToTable(TableName.Users, TableSchema.Dbo);

        builder.HasIndex(user => user.Email)
            .IsUnique();

        builder
            .Property(user => user.Id)
            .HasDefaultValueSql(DefaultSqlValue.NewGuid)
            .IsRequired();

        builder
            .Property(user => user.CreatedAt)
            .HasDefaultValueSql(DefaultSqlValue.NowUtc)
            .IsRequired();

        builder
            .Property(user => user.UpdatedAt)
            .IsRequired(false);

        builder
            .Property(user => user.InvitationToken)
            .IsRequired(false);

        builder
            .Property(user => user.VerificationCode)
            .IsRequired(false);

        builder
            .Property(user => user.FirstName)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(user => user.LastName)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(user => user.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Ignore(user => user.FullName);

        builder
            .Property(user => user.PasswordHash)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(user => user.ImageDataUrl)
            .HasMaxLength(255)
            .IsRequired(false);

        builder
            .Property(user => user.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValue(UserStatus.Pending)
            .IsRequired();
        
        builder
            .HasOne(organization => organization.ContactInfo)
            .WithOne(cf => cf.User)
            .HasForeignKey<ContactInfo>(cf => cf.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(organization => organization.RequestReplies)
            .WithOne(user => user.User!)
            .HasForeignKey(user => user.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(organization => organization.Achievements)
            .WithOne(user => user.User!)
            .HasForeignKey(user => user.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}