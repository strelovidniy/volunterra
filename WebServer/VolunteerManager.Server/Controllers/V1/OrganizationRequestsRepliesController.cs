using Microsoft.AspNetCore.Mvc;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.Server.Controllers.Base;

namespace VolunteerManager.Server.Controllers.V1;

[Route("api/v1/organizationRequestReplies")]
[ApiExplorerSettings(GroupName = "V1")]
public class OrganizationRequestsRepliesController : BaseController
{
    private readonly IOrganizationRequestReplyService _organizationRequestReplyService;
    public OrganizationRequestsRepliesController(IServiceProvider services, IOrganizationRequestReplyService organizationRequestReplyService) : base(services) => _organizationRequestReplyService = organizationRequestReplyService;

    [HttpPost]
    public async Task<IActionResult> CreateOrganizationRequestAsyncAsync(
        [FromBody] CreateOrganizationRequestReplyModel createUserModel,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(createUserModel, cancellationToken);

        await _organizationRequestReplyService.CreateOrganizationRequestReplyAsync(createUserModel, cancellationToken);

        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateOrganizationRequestAsyncAsync(
        [FromBody] UpdateOrganizationRequestReplyModel updateModel,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(updateModel, cancellationToken);

        await _organizationRequestReplyService.UpdateOrganizationRequestReplyAsync(updateModel, cancellationToken);

        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteOrganizationRequestAsyncAsync(
        [FromQuery] Guid requestId,
        CancellationToken cancellationToken = default)
    {
        await _organizationRequestReplyService.DeleteOrganizationRequestReplyAsync(requestId, cancellationToken);

        return Ok();
    }
}
