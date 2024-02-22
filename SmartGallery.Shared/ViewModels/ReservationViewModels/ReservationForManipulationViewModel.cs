using Microsoft.VisualBasic.CompilerServices;

namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public abstract record ReservationForManipulationViewModel
{
    public string? ProblemDescription { get; init; }
    public DateOnly ReservationDate { get; init; }
    public TimeOnly ReservationTime { get; init; }
}
