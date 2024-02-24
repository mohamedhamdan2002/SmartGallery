using Microsoft.VisualBasic.CompilerServices;

namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public abstract class ReservationForManipulationViewModel
{
    public string? ProblemDescription { get; set; }
    public DateOnly ReservationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public TimeOnly ReservationTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
}
