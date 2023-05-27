using VolunteerManager.Models;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Services.Abstraction;

public interface IAuthService
{
    public Task<UserView?> GetCurrentUserAsync(
        CancellationToken cancellationToken = default
    );

    public Task<LoginView?> LoginAsync(
        LoginModel model,
        CancellationToken cancellationToken = default
    );
}