using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums.RichEnums;

namespace VolunteerManager.Data.EntityConfigurations;

internal class OrganizationRequestConfiguration : IEntityTypeConfiguration<OrganizationRequest>
{
    public void Configure(EntityTypeBuilder<OrganizationRequest> builder)
    {
        builder
            .ToTable(TableName.OrganizationRequests, TableSchema.Dbo);

        builder
            .HasKey(organizationRequest => organizationRequest.Id);

        builder
            .Property(organizationRequest => organizationRequest.Id)
            .HasDefaultValueSql(DefaultSqlValue.NewGuid)
            .IsRequired();

        builder
            .Property(organizationRequest => organizationRequest.CreatedAt)
            .HasDefaultValueSql(DefaultSqlValue.NowUtc)
            .IsRequired();

        builder
            .Property(organizationRequest => organizationRequest.UpdatedAt)
            .IsRequired(false);

        builder
            .Property(organizationRequest => organizationRequest.OrganizationId)
            .IsRequired();

        builder
            .HasMany(x => x.RequestReplies)
            .WithOne(x => x.OrganizationRequest)
            .HasForeignKey(x => x.OrganizationRequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}