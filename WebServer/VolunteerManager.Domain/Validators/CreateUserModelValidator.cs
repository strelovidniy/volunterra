using FluentValidation;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Validators.Extensions;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Validators;

internal class CreateUserModelValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserModelValidator(IValidationService validationService)
    {
        RuleFor(createUserModel => createUserModel.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.EmailRequired)
            .EmailAddress()
            .WithStatusCode(StatusCode.InvalidEmailFormat)
            .MustAsync(validationService.IsEmailUniqueAsync)
            .WithStatusCode(StatusCode.EmailAlreadyExists);

        RuleFor(createUserModel => createUserModel.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.PasswordRequired)
            .MinimumLength(8)
            .WithStatusCode(StatusCode.PasswordMustHaveAtLeast8Characters)
            .MaximumLength(32)
            .WithStatusCode(StatusCode.PasswordMustHaveNotMoreThan32Characters)
            .Matches("[A-Z]")
            .WithStatusCode(StatusCode.PasswordMustHaveAtLeastOneUppercaseLetter)
            .Matches("[a-z]")
            .WithStatusCode(StatusCode.PasswordMustHaveAtLeastOneLowercaseLetter)
            .Matches("[0-9]")
            .WithStatusCode(StatusCode.PasswordMustHaveAtLeastOneDigit);

        RuleFor(createUserModel => createUserModel.ConfirmPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.ConfirmPasswordRequired)
            .Equal(createUserModel => createUserModel.Password)
            .WithStatusCode(StatusCode.PasswordsDoNotMatch);

        RuleFor(createUserModel => createUserModel.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.FirstNameRequired)
            .Must(firstName => !firstName.Contains(" "))
            .WithStatusCode(StatusCode.FirstNameShouldNotContainWhiteSpace);

        RuleFor(createUserModel => createUserModel.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.LastNameRequired)
            .Must(lastName => !lastName.Contains(" "))
            .WithStatusCode(StatusCode.LastNameShouldNotContainWhiteSpace);
    }
}