using RichEnum;

namespace VolunteerManager.Data.Enums.RichEnums;

public class ErrorMessage : RichEnum<string>
{
    public static ErrorMessage ApiExceptionOccurred =>
        new("Api Exception occurred");

    public static ErrorMessage ValidationExceptionOccurred =>
        new("Validation Exception occurred");

    public static ErrorMessage InternalServerErrorOccurred =>
        new("Internal Server Error Occurred");

    public static ErrorMessage EmailAttachmentAddingError =>
        new("Error while attaching files to email");

    public static ErrorMessage EmailSendingError =>
        new("Error while sending email");


    private ErrorMessage(string value) : base(value)
    {
    }

    public static ErrorMessage EmailNotSent(
        string? subject,
        string? receiver
    ) => new($"Email was not sent | Subject - {subject} | Receiver - {receiver}");
}