using System;
namespace SmartGallery.Server.Services;

public interface IMailService
{
	Task SendEmailAsync(string toEmail, string subject, string content);
}

public class MailService : IMailService
{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    // get Sendgrid from nugetPackage
    public Task SendEmailAsync(string toEmail, string subject, string content)
    {
        // go to the sengrid thing and go to setup the api and copy the code
        // ____________________________


        throw new NotImplementedException();
    }
}

