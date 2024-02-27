namespace SmartGallery.Server.Models;
public class Review : BaseEntity
{
    public string CustomerId { get; set; } = null!;
    public Customer Customer { get; set; } = null!;

    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;
    public int Rating { get; set; }
    public string? Comment { get; set; }
}
