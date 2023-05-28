using EntityFrameworkCore.RepositoryInfrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VolunteerManager.Data.Context;
using VolunteerManager.Data.Entities;

namespace VolunteerManager.Data.DependencyInjection;

public static class DataDependencyInjectionExtension
{
    public static IServiceCollection RegisterDataLayer(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services
        .AddDbContext(configuration)
        .AddRepositories();

    private static IServiceCollection AddDbContext(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services.AddDbContext<VolunteerManagerContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString("SqlServer"))
            .EnableSensitiveDataLogging();
    });

    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    ) => services
        .CreateRepositoryBuilderWithContext<VolunteerManagerContext>()
        .AddRepository<User>()
        .AddRepository<Organization>()
        .AddRepository<OrganizationRequest>()
        .AddRepository<OrganizationInvocationReply>()
        .Build();
}