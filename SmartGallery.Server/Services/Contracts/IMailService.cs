namespace SmartGallery.Server.Services.Contracts;

public interface IMailService
{
	Task SendEmailAsync(string toEmail, string subject, string content);
}