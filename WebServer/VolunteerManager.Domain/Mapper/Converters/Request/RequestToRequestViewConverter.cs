using AutoMapper;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Mapper.Converters.Request;

internal class RequestToRequestViewConverter : ITypeConverter<Data.Entities.Request, RequestView>
{
    public RequestView Convert(
        Data.Entities.Request request,
        RequestView requestView,
        ResolutionContext context
    ) => new()
    {
        Id = request.Id,
        Description = request.Description,
        RequestDate = request.CreatedAt,
        RequestUpdatedAt = request.UpdatedAt,
        CreatedBy = context.Mapper.Map<UserView>(request.CreatedBy),
        Name = request.Name,
        TotalAmount = request.TotalAmount,
        RemainingAmount = request.RemainingAmount,
        ImageDataUrl = request.ImageDataUrl
    };
}