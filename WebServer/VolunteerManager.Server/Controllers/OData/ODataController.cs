using AutoMapper;
using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using VolunteerManager.Data.Entities;
using VolunteerManager.Domain.Extensions;
using VolunteerManager.Domain.Validators.Runtime;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Models;
using ODataControllerBase = Microsoft.AspNetCore.OData.Routing.Controllers.ODataController;

namespace VolunteerManager.Server.Controllers.OData;

[Authorize]
[Route("api/odata")]
[ApiExplorerSettings(GroupName = "OData")]
public class ODataController : ODataControllerBase
{
    private readonly IMapper _mapper;

    private Guid CurrentUserId => HttpContext.GetCurrentUserId();

    public ODataController(
        IMapper mapper
    ) => _mapper = mapper;

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(
        ODataQueryOptions<User> options,
        [FromServices] IRepository<User> repository,
        CancellationToken cancellationToken = default
    )
    {
        var currentUser = await repository
            .Query()
            .FirstOrDefaultAsync(user => user.Id == CurrentUserId, cancellationToken);

        RuntimeValidator.Assert(currentUser?.OrganizationId is not null, Data.Enums.StatusCode.Forbidden);

        return Ok(new ODataResponse<UserView>(
            options.Context.ToString() ?? string.Empty,
            _mapper
                .Map<List<UserView>>(
                    options
                        .ApplyTo(
                            repository
                                .Query()
                                .Where(user => user.OrganizationId == currentUser!.OrganizationId)
                        )
                        .Cast<User>()
                        .ToList()
                ),
            (int) (options.Request.ODataFeature().TotalCount ?? 0)
        ));
    }

    [HttpGet("requests")]
    public IActionResult GetRequests(
        ODataQueryOptions<Request> options,
        [FromServices] IRepository<Request> repository
    ) => Ok(new ODataResponse<RequestView>(
        options.Context.ToString() ?? string.Empty,
        _mapper
            .Map<List<RequestView>>(
                options
                    .ApplyTo(
                        repository
                            .Query()
                    )
                    .Cast<Request>()
                    .ToList()
            ),
        (int) (options.Request.ODataFeature().TotalCount ?? 0)
    ));

    [HttpGet("organization")]
    public IActionResult GetOrganizations(
        ODataQueryOptions<Organization> options,
        [FromServices] IRepository<Organization> repository
    ) => Ok(new ODataResponse<OrganizationView>(
        options.Context.ToString() ?? string.Empty,
        _mapper.Map<List<OrganizationView>>(
            options
                .ApplyTo(
                    repository
                        .Query()
                )
                .Cast<Organization>()
                .ToList()
        ),
        (int) (options.Request.ODataFeature().TotalCount ?? 0)
    ));
}