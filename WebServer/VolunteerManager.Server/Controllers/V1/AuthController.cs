using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Models;
using VolunteerManager.Server.Controllers.Base;

namespace VolunteerManager.Server.Controllers.V1;

[Route("api/v1/auth")]
[ApiExplorerSettings(GroupName = "V1")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(
        IServiceProvider services,
        IAuthService authService
    ) : base(services) => _authService = authService;

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(
        [FromBody] LoginModel model,
        CancellationToken cancellationToken = default
    ) => Ok(
        await _authService.LoginAsync(
            model,
            cancellationToken
        )
    );

    [HttpGet("me")]
    public async Task<IActionResult> CurrentUserInfoAsync(
        CancellationToken cancellationToken = default
    ) => Ok(
        await _authService.GetCurrentUserAsync(
            cancellationToken
        )
    );
}