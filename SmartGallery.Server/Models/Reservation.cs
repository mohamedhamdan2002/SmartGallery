using SmartGallery.Shared;

namespace SmartGallery.Server.Models;

public class Reservation
{
    public string UserId { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    // here should be navigation property for user entity
    public string ProblemDescription { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Pending;
}




