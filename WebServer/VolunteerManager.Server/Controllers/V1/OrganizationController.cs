using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Models.Create;
using VolunteerManager.Server.Controllers.Base;

namespace VolunteerManager.Server.Controllers.V1;
[Route("api/v1/users")]
[ApiExplorerSettings(GroupName = "V1")]
public class OrganizationController : BaseController
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IServiceProvider services, IOrganizationService organizationService) : base(services) => _organizationService = organizationService;
    
    [AllowAnonymous]
    [HttpPost("organization-sign-up")]
    public async Task<IActionResult> SignUpAsOrganizationAsync(
        [FromBody] CreateOrganizationModel createUserModel,
        CancellationToken cancellationToken = default
    )
    {
        await ValidateAsync(createUserModel, cancellationToken);

        await _organizationService.CreateOrganizationAsync(createUserModel, cancellationToken);

        return Ok();
    }
}