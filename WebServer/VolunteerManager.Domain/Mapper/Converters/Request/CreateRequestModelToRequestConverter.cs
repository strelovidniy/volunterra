using AutoMapper;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Mapper.Converters.Request;

internal class CreateRequestModelToRequestConverter : ITypeConverter<CreateRequestModel, Data.Entities.Request>
{
    public Data.Entities.Request Convert(
        CreateRequestModel createRequestModel,
        Data.Entities.Request request,
        ResolutionContext context
    ) => new()
    {
        Description = createRequestModel.Description,
        Name = createRequestModel.Name,
        TotalAmount = createRequestModel.TotalAmount,
        ImageDataUrl = null
    };
}