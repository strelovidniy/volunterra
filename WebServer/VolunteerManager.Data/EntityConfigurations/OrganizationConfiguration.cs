using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums.RichEnums;

namespace VolunteerManager.Data.EntityConfigurations;

internal class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder
            .ToTable(TableName.Organizations, TableSchema.Dbo);

        builder
            .HasKey(organization => organization.Id);

        builder
            .Property(organization => organization.Id)
            .HasDefaultValueSql(DefaultSqlValue.NewGuid)
            .IsRequired();

        builder
            .Property(organization => organization.CreatedAt)
            .HasDefaultValueSql(DefaultSqlValue.NowUtc)
            .IsRequired();

        builder
            .Property(organization => organization.UpdatedAt)
            .IsRequired(false);

        builder
            .Property(organization => organization.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(organization => organization.GoogleEmail)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(organization => organization.Description)
            .HasMaxLength(2048)
            .IsRequired(false);

        builder
            .Property(organization => organization.ImageDataUrl)
            .HasMaxLength(255)
            .IsRequired(false);

        builder
            .HasMany(organization => organization.Users)
            .WithOne(user => user.Organization!)
            .HasForeignKey(user => user.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);
   
        builder
            .HasOne(organization => organization.ContactInfo)
            .WithOne(cf => cf.Organization)
            .HasForeignKey<ContactInfo>(cf => cf.Id)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(organization => organization.OrganizationRequests)
            .WithOne(organizationRequest => organizationRequest.Organization!)
            .HasForeignKey(organizationRequest => organizationRequest.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}