using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums.RichEnums;

namespace VolunteerManager.Data.EntityConfigurations;

internal class OrganizationRequestReplyConfiguration : IEntityTypeConfiguration<OrganizationRequestReply>
{
    public void Configure(EntityTypeBuilder<OrganizationRequestReply> builder)
    {
        builder
            .ToTable(TableName.OrganizationRequestReply, TableSchema.Dbo);

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
            .Property(organizationRequest => organizationRequest.Status)
            .IsRequired();
    }
}