using Microsoft.AspNetCore.Mvc;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Models.Create;
using VolunteerManager.Server.Controllers.Base;

namespace VolunteerManager.Server.Controllers.V1;

[Route("api/v1/organizationRequests")]
[ApiExplorerSettings(GroupName = "V1")]
public class OrganizationRequestsController : BaseController
{
    private readonly IOrganizationRequestService _organizationRequestService;
    public OrganizationRequestsController(IServiceProvider services, IOrganizationRequestService organizationRequestService) : base(services) => _organizationRequestService = organizationRequestService;

    [HttpPost]
    public async Task<IActionResult> CreateOrganizationRequestAsyncAsync(
        [FromBody] CreateOrganizationRequestModel createUserModel,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(createUserModel, cancellationToken);

        await _organizationRequestService.CreateOrganizationRequestAsync(createUserModel, cancellationToken);

        return Ok();
    }
 
}
