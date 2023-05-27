using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VolunteerManager.Domain.Mapper.Profiles;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Services.Realization;
using VolunteerManager.Domain.Settings.Abstraction;
using VolunteerManager.Domain.Settings.Realization;
using VolunteerManager.Domain.Validators;
using VolunteerManager.Models;
using VolunteerManager.Models.Change;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;

namespace VolunteerManager.Domain.DependencyInjection;

public static class DomainDependencyInjectionExtension
{
    public static IServiceCollection RegisterDomainLayer(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services
        .AddServices()
        .AddValidators()
        .AddSettings(configuration)
        .AddMapper();

    private static IServiceCollection AddServices(
        this IServiceCollection services
    ) => services
        .AddHttpContextAccessor()
        .AddTransient<IEmailService, EmailService>()
        .AddTransient<IRazorViewToStringRenderer, RazorViewToStringRenderer>()
        .AddTransient<IValidationService, ValidationService>()
        .AddTransient<IUserService, UserService>()
        .AddTransient<IAuthService, AuthService>()
        .AddTransient<IOrganizationService, OrganizationService>()
        .AddTransient<IOrganizationRequestService, OrganizationRequestService>();

    private static IServiceCollection AddValidators(
        this IServiceCollection services
    ) => services
        .AddTransient<IValidator<LoginModel>, LoginModelValidator>()
        .AddTransient<IValidator<ChangePasswordModel>, ChangePasswordModelValidator>()
        .AddTransient<IValidator<CreateUserModel>, CreateUserModelValidator>()
        .AddTransient<IValidator<ResetPasswordModel>, ResetPasswordModelValidator>()
        .AddTransient<IValidator<SetNewPasswordModel>, SetNewPasswordModelValidator>()
        .AddTransient<IValidator<CreateOrganizationModel>, CreateOrganizationModelValidator>()
        .AddTransient<IValidator<UpdateOrganizationModel>, UpdateOrganizationModelValidator>()
        .AddTransient<IValidator<InviteUserToOrganizationModel>, InviteUserToOrganizationModelValidator>()
        .AddTransient<IValidator<CreateOrganizationInvocationReplyModel>, CreateRequestModelValidator>()
        .AddTransient<IValidator<UpdateOrganizationRequestModel>, UpdateRequestModelValidator>()
        .AddTransient<IValidator<CreateOrganizationRequestModel>, CreateOrganizationRequestModelValidation>();

    private static IServiceCollection AddMapper(
        this IServiceCollection services
    ) => services
        .AddAutoMapper(config => config.AddProfiles(new List<Profile>
        {
            new UserMapperProfile(),
            new OrganizationMapperProfile(),
            new OrganizationRequestMapperProfile()
        }));

    private static IServiceCollection AddSettings(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtSettings = new JwtSettings();
        var emailSettings = new EmailSettings();
        var urlSettings = new UrlSettings();

        configuration
            .GetSection("AccessTokenSettings")
            .Bind(jwtSettings);

        configuration
            .GetSection("EmailSettings")
            .Bind(emailSettings);

        configuration
            .GetSection("UrlSettings")
            .Bind(urlSettings);

        services
            .AddTransient<IJwtSettings, JwtSettings>(_ => jwtSettings)
            .AddTransient<IEmailSettings, EmailSettings>(_ => emailSettings)
            .AddTransient<IUrlSettings, UrlSettings>(_ => urlSettings)
            .AddTransient<IJwtSettings, JwtSettings>(_ => jwtSettings);

        return services;
    }
}