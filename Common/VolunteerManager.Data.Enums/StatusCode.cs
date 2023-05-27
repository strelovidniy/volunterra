namespace VolunteerManager.Data.Enums;

public enum StatusCode
{
    MethodNotAvailable = 0,
    Unauthorized = 1,
    Forbidden = 2,

    OrganizationNotFound = 101,
    AccountNotFound = 102,
    UserNotFound = 103,
    RequestNotFound = 104,

    DescriptionTooLong = 201,

    EmailSendingError = 301,


    YouAreNotCreatorOfRequest = 701,
    YouAreNotAllowedToSeeThisIncoming = 702,
    YouCanOnlyAddIncomingForYourself = 703,
    YouCanOnlyAddSpendingForYourself = 704,
    YouCanOnlyUpdateIncomingForYourself = 705,
    YouCanOnlyUpdateSpendingForYourself = 706,
    YouCanOnlyDeleteIncomingForYourself = 707,
    YouCanOnlyDeleteSpendingForYourself = 708,


    LastNameRequired = 1001,
    FirstNameRequired = 1002,
    PasswordRequired = 1004,
    EmailRequired = 1005,
    VerificationCodeRequired = 1007,
    ConfirmPasswordRequired = 1008,
    OrganizationNameRequired = 1009,
    RequestDescriptionRequired = 1010,
    RequestNameRequired = 1011,
    TotalAmountRequired = 1012,

    PasswordLengthExceeded = 1109,

    PasswordMustHaveAtLeast8Characters = 1201,
    PasswordMustHaveNotMoreThan32Characters = 1202,
    PasswordMustHaveAtLeastOneUppercaseLetter = 1203,
    PasswordMustHaveAtLeastOneLowercaseLetter = 1204,
    PasswordMustHaveAtLeastOneDigit = 1205,
    OrganizationDescriptionMustHaveNotMoreThan2000Characters = 1206,
    RequestDescriptionMustHaveNotMoreThan2000Characters = 1207,
    RequestNameMustHaveNotMoreThan250Characters = 1208,
    ServedCountMustBeGreaterThanOrEqualToZero = 1209,

    InvalidEmailFormat = 1301,
    InvalidEmailModel = 1302,
    InvalidVerificationCode = 1303,
    InvalidFileExtension = 1304,

    FirstNameShouldNotContainWhiteSpace = 1401,
    LastNameShouldNotContainWhiteSpace = 1402,

    IncorrectPassword = 1501,
    PasswordsDoNotMatch = 1502,
    OldPasswordIncorrect = 1503,

    EmailAlreadyExists = 1603,
    OrganizationNameAlreadyExists = 1604,
    RequestAlreadyHasDonations = 1605
}