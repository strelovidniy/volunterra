using RichEnum;

namespace VolunteerManager.Data.Enums.RichEnums;

public class ContentType : RichEnum<string>
{
    public static ContentType ImagePng => new("image/png");

    public static ContentType ApplicationProblem => new("application/problem+json");

    public static ContentType ApplicationPdf => new("application/pdf");

    public static ContentType ApplicationJson => new("application/json");

    public static ContentType ApplicationOctetStream => new("application/octet-stream");

    private ContentType(string value) : base(value)
    {
    }
}