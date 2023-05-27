using VolunteerManager.Data.Enums.RichEnums;
using VolunteerManager.Domain.Models.ViewModels;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Settings.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace VolunteerManager.Domain.Services.Realization;

internal class RazorViewToStringRenderer : IRazorViewToStringRenderer
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IServiceProvider _serviceProvider;
    private readonly ITempDataProvider _tempDataProvider;
    private readonly IUrlSettings _urlSettings;
    private readonly IRazorViewEngine _viewEngine;
    private readonly ILogger<RazorViewToStringRenderer> _logger;

    public RazorViewToStringRenderer(
        IRazorViewEngine viewEngine,
        IServiceProvider serviceProvider,
        ITempDataProvider tempDataProvider,
        IHttpContextAccessor contextAccessor,
        IUrlSettings urlSettings,
        ILogger<RazorViewToStringRenderer> logger
    )
    {
        _viewEngine = viewEngine;
        _contextAccessor = contextAccessor;
        _serviceProvider = serviceProvider;
        _tempDataProvider = tempDataProvider;
        _urlSettings = urlSettings;
        _logger = logger;
    }

    public async Task<string> RenderViewToStringAsync<TModel>(EmailViewLocation viewName, TModel model)
        where TModel : class, IEmailViewModel
    {
        var actionContext = GetActionContext();
        var view = FindView(actionContext, viewName);

        await using var output = new StringWriter();

        var viewContext = new ViewContext(
            actionContext,
            view,
            new ViewDataDictionary<TModel>(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary()
            )
            {
                Model = model,
                ["WhiteBackgroundUrl"] = $"{_urlSettings.WebApiUrl}api/v1/static-files/white-background",
                ["IconUrl"] = $"{_urlSettings.WebApiUrl}api/v1/static-files/icon",
                ["TwitterIconUrl"] = $"{_urlSettings.WebApiUrl}api/v1/static-files/twitter-icon",
                ["FacebookIconUrl"] = $"{_urlSettings.WebApiUrl}api/v1/static-files/facebook-icon",
                ["InstagramIconUrl"] = $"{_urlSettings.WebApiUrl}api/v1/static-files/instagram-icon",
                ["DividerUrl"] = $"{_urlSettings.WebApiUrl}api/v1/static-files/divider",
                ["FacebookUrl"] = _urlSettings.FacebookUrl,
                ["InstagramUrl"] = _urlSettings.InstagramUrl,
                ["TwitterUrl"] = _urlSettings.TwitterUrl
            },
            new TempDataDictionary(
                actionContext.HttpContext,
                _tempDataProvider
            ),
            output,
            new HtmlHelperOptions()
        );

        await view.RenderAsync(viewContext);

        return output.ToString();
    }

    private IView FindView(ActionContext actionContext, string viewName)
    {
        var getViewResult = _viewEngine.GetView(null, viewName, true);

        if (getViewResult.Success)
        {
            return getViewResult.View;
        }

        var findViewResult = _viewEngine.FindView(actionContext, viewName, true);

        if (findViewResult.Success)
        {
            return findViewResult.View;
        }

        var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);

        var errorMessage = string.Join(
            Environment.NewLine,
            new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(
                searchedLocations
            )
        );

        _logger.LogError(errorMessage);

        throw new InvalidOperationException(errorMessage);
    }

    private ActionContext GetActionContext()
    {
        var context = _contextAccessor.HttpContext
            ?? new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };

        return new ActionContext(
            context,
            context.GetRouteData(),
            new ActionDescriptor()
        );
    }
}