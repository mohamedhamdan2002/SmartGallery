namespace SmartGallery.Server.Repositories.Contracts;

public interface IRepositoryManager
{
    IServiceRepository Service { get; }
    IReservationRepository Reservation { get; }
    IItemRepository Item { get; }
    IReviewRepository Review { get; }
    Task SaveChangesAsync();
}
