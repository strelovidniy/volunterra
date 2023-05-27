using FluentValidation;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Validators.Extensions;
using VolunteerManager.Models;

namespace VolunteerManager.Domain.Validators;

internal class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator(IValidationService validationService)
    {
        RuleFor(loginModel => loginModel.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.EmailRequired)
            .WithStatusCode(StatusCode.InvalidEmailFormat)
            .MustAsync(validationService.IsUserExistsAsync)
            .WithStatusCode(StatusCode.UserNotFound);

        RuleFor(loginModel => loginModel.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.PasswordRequired)
            .MinimumLength(8)
            .WithStatusCode(StatusCode.PasswordLengthExceeded);
    }
}