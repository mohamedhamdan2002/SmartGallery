//using SmartGallery.Server.Services.Contracts;

//namespace SmartGallery.Server.Services;

//public class MailService : IMailService
//{
//    private readonly IConfiguration _configuration;

//    public MailService(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }
//    // get Sendgrid from nugetPackage
//    public async Task SendEmailAsync(string toEmail, string subject, string content)
//    {
//        var apiKey = _configuration["SendGridAPIKey"];
//        var client = new SendGridClient(apiKey);
//        var from = new EmailAddress("SmartGallery@Authentication.com","Email Confirmation");
//        var to = new EmailAddress(toEmail);
//        var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
//        var response = await client.SendEmailAsync(msg);

//    }
//}

