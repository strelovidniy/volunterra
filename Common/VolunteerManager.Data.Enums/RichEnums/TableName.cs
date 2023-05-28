using RichEnum;

namespace VolunteerManager.Data.Enums.RichEnums;

public class TableName : RichEnum<string>
{
    public static TableName Users => new("Users");

    public static TableName Organizations => new("Organizations");

    public static TableName OrganizationRequests => new("OrganizationRequests");
    public static TableName OrganizationRequestReply => new("OrganizationRequestReply");
    public static TableName ContactInfo => new("ContactInfo");
    public static TableName Skills => new("Skills");
    public static TableName Achievements => new("Achievements");

    private TableName(string value) : base(value)
    {
    }
}