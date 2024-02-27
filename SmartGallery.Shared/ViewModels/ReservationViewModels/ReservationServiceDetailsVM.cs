using SmartGallery.Shared.ViewModels.ItemViewModels;

namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public record ReservationServiceDetailsVM(
        int Id,
        string ProblemDescription,
        StatusEnum Status,
        DateOnly ReservationDate,
        TimeOnly ReservationTime,
        int serviceId,
        string serviceName,
        int ItemId,
        string itemName
    );