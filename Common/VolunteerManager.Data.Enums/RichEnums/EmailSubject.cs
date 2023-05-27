using RichEnum;

namespace VolunteerManager.Data.Enums.RichEnums;

public class EmailSubject : RichEnum<string>
{
    public static EmailSubject ResetPassword =>
        new("Reset your password");

    public static EmailSubject CreateAccount =>
        new("Create your account");

    private EmailSubject(string value) : base(value)
    {
    }
}