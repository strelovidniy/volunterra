using RichEnum;

namespace VolunteerManager.Data.Enums.RichEnums;

public class EmailViewLocation : RichEnum<string>
{
    private const string Base = "/Views/EmailTemplates/";

    public static EmailViewLocation ResetPasswordEmail =>
        new($"{Base}ResetPasswordEmail/ResetPasswordEmail.cshtml");

    private EmailViewLocation(string value) : base(value)
    {
    }
}