namespace SmartGallery.Server.Models;
public class Review : BaseEntity
{
    public string CustomerId { get; set; } = null!;
    public int ServiceId { get; set; }
    public Customer Customer { get; set; } = null!;
    public Service Service { get; set; } = null!;
    public int Rating { get; set; }
    public string? Comment { get; set; }
}
