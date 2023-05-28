using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolunteerManager.Domain.Extensions;
using VolunteerManager.Models;

namespace VolunteerManager.Server.Controllers.Base;

[Authorize]
[ApiController]
public class BaseController : ControllerBase
{
    private readonly IServiceProvider _services;

    protected Guid CurrentUserId => HttpContext.GetCurrentUserId();

    public BaseController(
        IServiceProvider services
    ) => _services = services;

    protected Task ValidateAsync<T>(T validatableModel, CancellationToken cancellationToken = default)
        where T : class, IValidatableModel =>
        _services.GetRequiredService<IValidator<T>>().ValidateAndThrowAsync(validatableModel, cancellationToken);
}