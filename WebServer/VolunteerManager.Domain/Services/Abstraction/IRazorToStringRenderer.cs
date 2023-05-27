using VolunteerManager.Data.Enums.RichEnums;
using VolunteerManager.Domain.Models.ViewModels;

namespace VolunteerManager.Domain.Services.Abstraction;

public interface IRazorViewToStringRenderer
{
    public Task<string> RenderViewToStringAsync<TModel>(
        EmailViewLocation viewName,
        TModel model
    ) where TModel : class, IEmailViewModel;
}