using AutoMapper;
using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums;
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

    [HttpGet("organizationRequests")]
    public IActionResult GetRequests(
        ODataQueryOptions<OrganizationRequest> options,
        [FromServices] IRepository<OrganizationRequest> repository
    ) => Ok(new ODataResponse<OrganizationRequestView>(
        options.Context.ToString() ?? string.Empty,
        _mapper
            .Map<List<OrganizationRequestView>>(
                options
                    .ApplyTo(
                        repository
                            .Query()
                    )
                    .Cast<OrganizationRequest>()
                    .ToList()
            ),
        (int) (options.Request.ODataFeature().TotalCount ?? 0)
    ));

    [HttpGet("organizationRequestsInvocations")]
    public IActionResult GetRequestsInvocations(
        ODataQueryOptions<OrganizationRequestReply> options,
        [FromServices] IRepository<OrganizationRequestReply> repository
    )
    {
        var testData = new List<OrganizationRequestView>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Category = OrganizationRequestCategory.Humanitarian,
                Description
                    = "Humanitarian ipsum dolor sit amet, consectetur adipiscing elit. Donec ac est nibh. Cras rutrum, ipsum et iaculis mattis, purus tellus viverra sapien, id feugiat ante eros ac mass",
                Title = "Humanitarian ipsum dolor sit amet",
                Location = "id feugiat ante eros ac mass",
                RequestDate = DateTime.Today,
                ImageDataUrl
                    = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Patron_with_Golden_Paw_award.jpg/280px-Patron_with_Golden_Paw_award.jpg",
                RequestUpdatedAt = DateTime.Today,
                CreatedBy = new UserView
                {
                    Status = UserStatus.Pending
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = OrganizationRequestCategory.Humanitarian,
                Description
                    = "Humanitarian ipsum dolor sit amet, consectetur adipiscing elit. Donec ac est nibh. Cras rutrum, ipsum et iaculis mattis, purus tellus viverra sapien, id feugiat ante eros ac mass",
                Title = "Humanitarian ipsum dolor sit amet",
                Location = "id feugiat ante eros ac mass",
                RequestDate = DateTime.Today,
                RequestUpdatedAt = DateTime.Today,
                ImageDataUrl
                    = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Patron_with_Golden_Paw_award.jpg/280px-Patron_with_Golden_Paw_award.jpg",
                CreatedBy = new UserView
                {
                    Status = UserStatus.Pending
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = OrganizationRequestCategory.Humanitarian,
                Description
                    = "Humanitarian ipsum dolor sit amet, consectetur adipiscing elit. Donec ac est nibh. Cras rutrum, ipsum et iaculis mattis, purus tellus viverra sapien, id feugiat ante eros ac mass",
                Title = "Humanitarian ipsum dolor sit amet",
                Location = "id feugiat ante eros ac mass",
                RequestDate = DateTime.Today,
                RequestUpdatedAt = DateTime.Today,
                ImageDataUrl
                    = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Patron_with_Golden_Paw_award.jpg/280px-Patron_with_Golden_Paw_award.jpg",
                CreatedBy = new UserView
                {
                    Status = UserStatus.Pending
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = OrganizationRequestCategory.Donation,
                Description
                    = "Donation ipsum dolor sit amet, consectetur adipiscing elit. Donec ac est nibh. Cras rutrum, ipsum et iaculis mattis, purus tellus viverra sapien, id feugiat ante eros ac mass",
                Title = "Donation ipsum dolor sit amet",
                Location = "id feugiat ante eros ac mass",
                RequestDate = DateTime.Today,
                RequestUpdatedAt = DateTime.Today,
                ImageDataUrl
                    = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Patron_with_Golden_Paw_award.jpg/280px-Patron_with_Golden_Paw_award.jpg",
                CreatedBy = new UserView
                {
                    Status = UserStatus.Pending
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = OrganizationRequestCategory.Donation,
                Description
                    = "Donation ipsum dolor sit amet, consectetur adipiscing elit. Donec ac est nibh. Cras rutrum, ipsum et iaculis mattis, purus tellus viverra sapien, id feugiat ante eros ac mass",
                Title = "Donation ipsum dolor sit amet",
                Location = "id feugiat ante eros ac mass",
                RequestDate = DateTime.Today,
                RequestUpdatedAt = DateTime.Today,
                ImageDataUrl
                    = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Patron_with_Golden_Paw_award.jpg/280px-Patron_with_Golden_Paw_award.jpg",
                CreatedBy = new UserView
                {
                    Status = UserStatus.Pending
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = OrganizationRequestCategory.Donation,
                Description
                    = "Donation Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac est nibh. Cras rutrum, ipsum et iaculis mattis, purus tellus viverra sapien, id feugiat ante eros ac mass",
                Title = "Donation Lorem ipsum dolor sit amet",
                Location = "id feugiat ante eros ac mass",
                RequestDate = DateTime.Today,
                RequestUpdatedAt = DateTime.Today,
                ImageDataUrl
                    = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Patron_with_Golden_Paw_award.jpg/280px-Patron_with_Golden_Paw_award.jpg",
                CreatedBy = new UserView
                {
                    Status = UserStatus.Pending
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = OrganizationRequestCategory.Work,
                Description
                    = "WORK Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac est nibh. Cras rutrum, ipsum et iaculis mattis, purus tellus viverra sapien, id feugiat ante eros ac mass",
                Title = "WORK Lorem ipsum dolor sit amet",
                Location = "id feugiat ante eros ac mass",
                RequestDate = DateTime.Today,
                RequestUpdatedAt = DateTime.Today,
                ImageDataUrl
                    = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Patron_with_Golden_Paw_award.jpg/280px-Patron_with_Golden_Paw_award.jpg",
                CreatedBy = new UserView
                {
                    Status = UserStatus.Pending
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = OrganizationRequestCategory.Work,
                Description
                    = "WORK Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac est nibh. Cras rutrum, ipsum et iaculis mattis, purus tellus viverra sapien, id feugiat ante eros ac mass",
                Title = "WORK Lorem ipsum dolor sit amet",
                Location = "id feugiat ante eros ac mass",
                RequestDate = DateTime.Today,
                RequestUpdatedAt = DateTime.Today,
                ImageDataUrl
                    = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Patron_with_Golden_Paw_award.jpg/280px-Patron_with_Golden_Paw_award.jpg",
                CreatedBy = new UserView
                {
                    Status = UserStatus.Pending
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = OrganizationRequestCategory.Work,
                Description
                    = "WORK Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac est nibh. Cras rutrum, ipsum et iaculis mattis, purus tellus viverra sapien, id feugiat ante eros ac mass",
                Title = "WORK Lorem ipsum dolor sit amet",
                Location = "id feugiat ante eros ac mass",
                RequestDate = DateTime.Today,
                RequestUpdatedAt = DateTime.Today,
                ImageDataUrl
                    = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Patron_with_Golden_Paw_award.jpg/280px-Patron_with_Golden_Paw_award.jpg",
                CreatedBy = new UserView
                {
                    Status = UserStatus.Pending
                }
            }
        };

        return Ok(new ODataResponse<OrganizationRequestView>(string.Empty, testData, testData.Count));
    }

    // [HttpGet("organizationRequestsInvocations")]
    // public IActionResult GetRequestsInvocations(
    //     ODataQueryOptions<OrganizationInvocationReply> options,
    //     [FromServices] IRepository<OrganizationInvocationReply> repository
    // ) => Ok(new ODataResponse<OrganizationRequestView>(
    //     options.Context.ToString() ?? string.Empty,
    //     _mapper
    //         .Map<List<OrganizationRequestView>>(
    //             options
    //                 .ApplyTo(
    //                     repository
    //                         .Query()
    //                 )
    //                 .Cast<OrganizationInvocationReply>()
    //                 .ToList()
    //         ),
    //     (int) (options.Request.ODataFeature().TotalCount ?? 0)
    // ));

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
    
    [HttpGet("skills")]
    public IActionResult GetSkills(
        ODataQueryOptions<Skill> options,
        [FromServices] IRepository<Skill> repository
    ) => Ok(new ODataResponse<SkillView>(
        options.Context.ToString() ?? string.Empty,
        _mapper.Map<List<SkillView>>(
            options
                .ApplyTo(
                    repository
                        .Query()
                )
                .Cast<Skill>()
                .ToList()
        ),
        (int) (options.Request.ODataFeature().TotalCount ?? 0)
    ));
    
    [HttpGet("achievements")]
    public IActionResult GetAchievements(
        ODataQueryOptions<Achievement> options,
        [FromServices] IRepository<Achievement> repository
    ) => Ok(new ODataResponse<AchievementView>(
        options.Context.ToString() ?? string.Empty,
        _mapper.Map<List<AchievementView>>(
            options
                .ApplyTo(
                    repository
                        .Query()
                )
                .Cast<Achievement>()
                .ToList()
        ),
        (int) (options.Request.ODataFeature().TotalCount ?? 0)
    ));
}