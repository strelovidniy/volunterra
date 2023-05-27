using VolunteerManager.Models;
using VolunteerManager.Models.Change;
using VolunteerManager.Models.Create;
using VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Domain.Services.Realization;

internal class UserService : IUserService
{
    private readonly IVolunteerManagerHttpClient _httpClient;

    public UserService(
        IVolunteerManagerHttpClient httpClient
    ) => _httpClient = httpClient;

    public Task ResetPasswordAsync(
        ResetPasswordModel resetPasswordModel,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PostAsync(
            "api/v1/users/reset-password",
            _httpClient.CreateJsonContent(resetPasswordModel),
            cancellationToken
        );

    public Task SetNewPasswordAsync(
        SetNewPasswordModel setNewPasswordModel,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PostAsync(
            "api/v1/users/set-new-password",
            _httpClient.CreateJsonContent(setNewPasswordModel),
            cancellationToken
        );

    public Task CreateUserAsync(
        CreateUserModel createUserModel,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PostAsync(
            "api/v1/users/sign-up",
            _httpClient.CreateJsonContent(createUserModel),
            cancellationToken
        );

    public Task ChangeAvatarAsync(
        byte[] bytes,
        CancellationToken cancellationToken = default
    )
    {
        var base64Content = Convert.ToBase64String(bytes);

        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(base64Content), "fileContent");

        return _httpClient
            .PostAsync(
                "api/v1/users/change-avatar",
                formData,
                cancellationToken
            );
    }

    public Task ChangePasswordAsync(
        ChangePasswordModel changePasswordModel,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PutAsync(
            "api/v1/users/change-password",
            _httpClient.CreateJsonContent(changePasswordModel),
            cancellationToken
        );
}