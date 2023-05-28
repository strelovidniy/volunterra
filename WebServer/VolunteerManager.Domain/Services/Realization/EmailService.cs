using System.Net;
using System.Net.Mail;
using VolunteerManager.Data.Enums;
using VolunteerManager.Data.Enums.RichEnums;
using VolunteerManager.Domain.Exceptions;
using VolunteerManager.Domain.Models.ViewModels;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Settings.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace VolunteerManager.Domain.Services.Realization;

internal class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly IEmailSettings _emailSettings;
    private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

    public IConfiguration Configuration { get; }

    public EmailService(
        IConfiguration configuration,
        IRazorViewToStringRenderer razorViewToStringRenderer,
        IEmailSettings emailSettings,
        ILogger<EmailService> logger
    )
    {
        Configuration = configuration;
        _razorViewToStringRenderer = razorViewToStringRenderer;
        _emailSettings = emailSettings;
        _logger = logger;
    }

    public async Task SendEmailAsync<TModel>(
        string toAddresses,
        EmailSubject subject,
        TModel viewModel,
        IEnumerable<(string Path, string FileName)>? attachments = null,
        CancellationToken cancellationToken = default
    ) where TModel : class, IEmailViewModel
    {
        var path = typeof(TModel).Name switch
        {
            nameof(ResetPasswordEmailViewModel) => EmailViewLocation.ResetPasswordEmail,
            nameof(NewApplicantEmailViewModel) => EmailViewLocation.NewApplicantEmail,
            _ => throw new ApiException(StatusCode.InvalidEmailModel)
        };

        var content = await _razorViewToStringRenderer.RenderViewToStringAsync(path, viewModel);

        await SendEmailAsync(toAddresses, subject, content, true, attachments, cancellationToken);
    }

    private async Task SendEmailAsync(
        string email,
        string subject,
        string content,
        bool isHtmlContent = false,
        IEnumerable<(string Path, string FileName)>? attachments = null,
        CancellationToken cancellationToken = default
    )
    {
        using var smtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(_emailSettings.FromEmail, _emailSettings.Password),
            EnableSsl = true
        };

        using var message = new MailMessage
        {
            Body = content,
            IsBodyHtml = isHtmlContent,
            To =
            {
                new MailAddress(email)
            },
            Subject = subject,
            From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromDisplayName)
        };

        try
        {
            attachments?
                .ToList()
                .ForEach(attachment =>
                    message
                        .Attachments
                        .Add(new Attachment(
                            File.OpenRead(attachment.Path),
                            attachment.FileName
                        )));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorMessage.EmailAttachmentAddingError);
        }

        try
        {
            await smtpClient.SendMailAsync(message, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ErrorMessage.EmailNotSent(message.Subject, message.To.FirstOrDefault()?.Address));

            _logger.LogError(ex, ErrorMessage.EmailSendingError);

            throw new ApiException(StatusCode.EmailSendingError);
        }
    }
}