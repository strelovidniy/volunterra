#nullable enable

using Microsoft.EntityFrameworkCore;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.EntityConfigurations;

namespace VolunteerManager.Data.Context;

internal class VolunteerManagerContext : DbContext
{
    public virtual DbSet<User> Users { get; set; } = null!;

    public virtual DbSet<Organization> Organizations { get; set; } = null!;

    public virtual DbSet<Request> Requests { get; set; } = null!;

    public virtual DbSet<OrganizationRequest> OrganizationRequests { get; set; } = null!;

    public VolunteerManagerContext(DbContextOptions<VolunteerManagerContext> options)
        : base(options)
    {
    }

    public VolunteerManagerContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
        modelBuilder.ApplyConfiguration(new RequestConfiguration());
        modelBuilder.ApplyConfiguration(new OrganizationRequestConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}