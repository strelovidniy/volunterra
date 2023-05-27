using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums.RichEnums;

namespace VolunteerManager.Data.EntityConfigurations;

internal class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder
            .ToTable(TableName.Requests, TableSchema.Dbo);

        builder
            .HasKey(request => request.Id);

        builder
            .HasKey(request => request.Id);

        builder
            .Property(request => request.Id)
            .HasDefaultValueSql(DefaultSqlValue.NewGuid)
            .IsRequired();

        builder
            .Property(request => request.CreatedAt)
            .HasDefaultValueSql(DefaultSqlValue.NowUtc)
            .IsRequired();

        builder
            .Property(request => request.UpdatedAt)
            .IsRequired(false);

        builder
            .Property(request => request.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(request => request.Description)
            .IsRequired(false)
            .HasMaxLength(2048);

        builder
            .Property(request => request.ImageDataUrl)
            .IsRequired(false)
            .HasMaxLength(255);

        builder
            .Property(request => request.CreatedById)
            .IsRequired();

        builder
            .Property(request => request.TotalAmount)
            .IsRequired();

        builder
            .Ignore(request => request.RemainingAmount);

        builder
            .HasMany(request => request.OrganizationRequests)
            .WithOne(organizationRequest => organizationRequest.Request!)
            .HasForeignKey(organizationRequest => organizationRequest.RequestId)
            .HasPrincipalKey(request => request.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(request => request.CreatedBy)
            .WithMany(user => user.CreatedRequests)
            .HasForeignKey(request => request.CreatedById)
            .HasPrincipalKey(user => user.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}