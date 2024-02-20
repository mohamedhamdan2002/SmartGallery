using Microsoft.VisualBasic.CompilerServices;

namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public abstract record ReservationForManipulationViewModel
{
    public DateOnly ReservationDate { get; init; }
    public TimeOnly ReservationTime { get; init; }
}
