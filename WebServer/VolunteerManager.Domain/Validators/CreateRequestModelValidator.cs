using FluentValidation;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Validators.Extensions;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Validators;

internal class CreateRequestModelValidator : AbstractValidator<CreateRequestModel>
{
    public CreateRequestModelValidator()
    {
        RuleFor(createRequestModel => createRequestModel.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.RequestDescriptionRequired)
            .MaximumLength(2000)
            .WithStatusCode(StatusCode.RequestDescriptionMustHaveNotMoreThan2000Characters);

        RuleFor(createRequestModel => createRequestModel.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.RequestNameRequired)
            .MaximumLength(250)
            .WithStatusCode(StatusCode.RequestNameMustHaveNotMoreThan250Characters);

        RuleFor(createRequestModel => createRequestModel.TotalAmount)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.TotalAmountRequired);
    }
}