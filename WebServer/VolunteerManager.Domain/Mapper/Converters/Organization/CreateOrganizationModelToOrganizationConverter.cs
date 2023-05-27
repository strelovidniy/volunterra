using AutoMapper;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Helpers;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Mapper.Converters.Organization;

internal class CreateOrganizationModelToOrganizationConverter
    : ITypeConverter<CreateOrganizationModel, Data.Entities.Organization>
{
    public Data.Entities.Organization Convert(
        CreateOrganizationModel createOrganizationModel,
        Data.Entities.Organization organization,
        ResolutionContext context
    ) => new()
    {
        Name = createOrganizationModel.OrganizationName,
        Description = createOrganizationModel.OrganizationDescription,
        GoogleEmail = createOrganizationModel.Email,
        Users = new List<Data.Entities.User>
        {
            new()
            {
                Email = createOrganizationModel.Email,
                FirstName = createOrganizationModel.FirstName,
                LastName = createOrganizationModel.LastName,
                PasswordHash = PasswordHasher.GetHash(createOrganizationModel.Password),
                IsOrganizationAdmin = true,
                IsOrganizationOwner = true,
                Status = UserStatus.Active
            }
        }
    };
}