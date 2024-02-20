using SmartGallery.Shared;
using SmartGallery.Shared.ViewModels.ReservationViewModels;
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
    public ReservationViewModel ToViewModel()
    {
        return new ReservationViewModel(
            ProblemDescription: this.ProblemDescription,
            Status: this.Status,
            ReservationDate: this.ReservationDate,
            ReservationTime: this.ReservationTime
        );
    }
}