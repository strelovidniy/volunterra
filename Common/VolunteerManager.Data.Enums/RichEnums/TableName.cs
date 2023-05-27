﻿using RichEnum;

namespace VolunteerManager.Data.Enums.RichEnums;

public class TableName : RichEnum<string>
{
    public static TableName Users => new("Users");

    public static TableName Organizations => new("Organizations");

    public static TableName OrganizationRequests => new("OrganizationRequests");
    public static TableName ContactInfo => new("ContactInfo");

    private TableName(string value) : base(value)
    {
    }
}