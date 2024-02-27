using SmartGallery.Shared.ViewModels.ItemViewModels;

namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public record ReservationViewModel(
        int Id,
        string ProblemDescription,
        StatusEnum Status,
        DateOnly ReservationDate,
        TimeOnly ReservationTime,
        int ItemId
    );
public record ReservationDetailsVM(
        int Id,
        string ProblemDescription,
        StatusEnum Status,
        DateOnly ReservationDate,
        TimeOnly ReservationTime,
        string customerId,
        string customerEmail,
        int serviceId,
        string serviceName,
        int ItemId,
        string itemName
    );
