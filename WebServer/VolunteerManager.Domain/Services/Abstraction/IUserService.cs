using VolunteerManager.Models;
using VolunteerManager.Models.Change;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Services.Abstraction;

public interface IUserService
{
    public Task ResetPasswordAsync(
        ResetPasswordModel resetPasswordModel,
        CancellationToken cancellationToken = default
    );

    public Task SetNewPasswordAsync(
        SetNewPasswordModel setNewPasswordModel,
        CancellationToken cancellationToken = default
    );

    public Task CreateUserAsync(
        CreateUserModel createUserModel,
        CancellationToken cancellationToken = default
    );

    public Task<UserView?> GetUserAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    public Task ChangePasswordAsync(
        ChangePasswordModel changePasswordModel,
        CancellationToken cancellationToken = default
    );

    public Task ChangeAvatarAsync(
        byte[] avatar,
        CancellationToken cancellationToken = default
    );
}