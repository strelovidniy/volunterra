using Microsoft.AspNetCore.Authorization;
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

    public OrganizationRequestsController(
        IServiceProvider services,
        IOrganizationRequestService organizationRequestService
    ) : base(services) => _organizationRequestService = organizationRequestService;

    [HttpPost]
    public async Task<IActionResult> CreateOrganizationRequestAsyncAsync(
        [FromBody] CreateOrganizationRequestModel createUserModel,
        CancellationToken cancellationToken = default
    )
    {
        await ValidateAsync(createUserModel, cancellationToken);

        await _organizationRequestService.CreateOrganizationRequestAsync(createUserModel, cancellationToken);

        return Ok();
    }

    [HttpPost("image")]
    public async Task<IActionResult> UploadOrganizationRequestImageAsync(
        [FromForm] string fileContent,
        CancellationToken cancellationToken = default
    )
    {
        var decodedContent = Convert.FromBase64String(fileContent);

        await _organizationRequestService.UploadRequestImageAsync(decodedContent, cancellationToken);

        return Ok();
    }

    [HttpGet("images/{imageName}")]
    public async Task<IActionResult> GetAvatarAsync(
        string imageName,
        CancellationToken cancellationToken = default
    )
    {
        var image = await System.IO.File.ReadAllBytesAsync(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Files",
                "Requests",
                imageName
            ),
            cancellationToken
        );

        return File(image, "image/png");
    }

    [AllowAnonymous]
    [HttpGet("{requestId:guid}")]
    public async Task<IActionResult> GetOrganizationRequestAsync(
        Guid requestId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _organizationRequestService.GetOrganizationRequestAsync(requestId, cancellationToken);

        return Ok(response);
    }

    [HttpGet("locations")]
    public async Task<IActionResult> GetLocationAsync(
        CancellationToken cancellationToken = default
    )
    {
        var response = await _organizationRequestService.GetLocationsAsync(cancellationToken);

        return Ok(response);
    }
}