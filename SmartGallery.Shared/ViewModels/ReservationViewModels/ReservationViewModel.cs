﻿namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public record ReservationViewModel(
        string ProblemDescription,
        StatusEnum Status,
        DateOnly ReservationDate,
        TimeOnly ReservationTime
    );
public record ReservationDetailsVM(
        string ProblemDescription,
        StatusEnum Status,
        DateOnly ReservationDate,
        TimeOnly ReservationTime,
        string customerId,
        string customerEmail,
        int serviceId,
        string serviceName
    );
