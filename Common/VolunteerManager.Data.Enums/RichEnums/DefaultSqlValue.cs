using RichEnum;

namespace VolunteerManager.Data.Enums.RichEnums;

public class DefaultSqlValue : RichEnum<string>
{
    public static DefaultSqlValue NewGuid => new("NEWID()");

    public static DefaultSqlValue NowUtc => new("GETUTCDATE()");

    private DefaultSqlValue(string value) : base(value)
    {
    }
}