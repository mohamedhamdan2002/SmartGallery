namespace SmartGallery.Server.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message) {}
}
