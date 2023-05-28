using AutoMapper;
using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.EntityFrameworkCore;
using VolunteerManager.Data.Entities;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;

namespace VolunteerManager.Domain.Services.Realization;

public class OrganizationRequestReplyService : IOrganizationRequestReplyService
{
    private readonly IRepository<OrganizationRequestReply> _organizationRequestReplyRepository;
    private readonly IMapper _mapper;

    public OrganizationRequestReplyService(
        IRepository<OrganizationRequestReply> organizationRequestRepository,
        IMapper mapper
    )
    {
        _organizationRequestReplyRepository = organizationRequestRepository;
        _mapper = mapper;
    }

    public async Task CreateOrganizationRequestReplyAsync(
        CreateOrganizationRequestReplyModel model,
        CancellationToken cancellationToken = default
    )
    {
        await _organizationRequestReplyRepository.AddAsync(
            _mapper.Map<OrganizationRequestReply>(model),
            cancellationToken
        );

        await _organizationRequestReplyRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateOrganizationRequestReplyAsync(
        UpdateOrganizationRequestReplyModel model,
        CancellationToken cancellationToken = default
    )
    {
        var requestReply = await _organizationRequestReplyRepository
            .Query()
            .FirstOrDefaultAsync(
                x => x.Id == model.ReplyId,
                cancellationToken
            );

        if (model.Status != requestReply!.Status)
        {
            requestReply.Status = model.Status;
            requestReply.UpdatedAt = DateTime.UtcNow;
        }

        await _organizationRequestReplyRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteOrganizationRequestReplyAsync(Guid requestId, CancellationToken cancellationToken = default)
    {
        var spending = await _organizationRequestReplyRepository
            .Query()
            .FirstOrDefaultAsync(
                x => x.Id == requestId,
                cancellationToken
            );

        _organizationRequestReplyRepository.Delete(spending!);

        await _organizationRequestReplyRepository.SaveChangesAsync(cancellationToken);
    }
}