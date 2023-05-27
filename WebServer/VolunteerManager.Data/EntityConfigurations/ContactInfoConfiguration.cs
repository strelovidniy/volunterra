using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums.RichEnums;

namespace VolunteerManager.Data.EntityConfigurations;

internal class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
{
    public void Configure(EntityTypeBuilder<ContactInfo> builder)
    {
        builder
            .ToTable(TableName.ContactInfo, TableSchema.Dbo);

        builder
            .HasKey(cf => cf.Id);

        builder
            .Property(cf => cf.Id)
            .HasDefaultValueSql(DefaultSqlValue.NewGuid)
            .IsRequired();

        builder
            .Property(cf => cf.CreatedAt)
            .HasDefaultValueSql(DefaultSqlValue.NowUtc)
            .IsRequired();

        builder
            .Property(cf => cf.UpdatedAt)
            .IsRequired(false);

        builder
            .Property(cf => cf.City)
            .HasMaxLength(255)
            .IsRequired(false);

        builder
            .Property(cf => cf.LinkedInUrl)
            .HasMaxLength(255)
            .IsRequired(false);
        
        builder
            .Property(cf => cf.PhoneNumber)
            .HasMaxLength(255)
            .IsRequired(false);
        
        builder
            .Property(cf => cf.Region)
            .HasMaxLength(255)
            .IsRequired(false);
        
        builder
            .Property(cf => cf.City)
            .HasMaxLength(255)
            .IsRequired(false);

        builder
            .HasOne(cf => cf.Users)
            .WithOne(user => user.ContactInfo!)
            .OnDelete(DeleteBehavior.Restrict);
   
        builder
            .HasOne(cf => cf.Organization)
            .WithOne(user => user.ContactInfo!)
            .OnDelete(DeleteBehavior.Restrict);

    }
}