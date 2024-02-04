using SmartGallery.Shared;
namespace SmartGallery.Server.Models;

public class Reservation
{
    public string CustomerId { get; set; } = null!;
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public string ProblemDescription { get; set; } = null!;
    public StatusEnum Status { get; set; } = StatusEnum.Pending;
    public TimeOnly ReservationTime { get; set; }
    public DateOnly ReservationDate { get; set; }
}