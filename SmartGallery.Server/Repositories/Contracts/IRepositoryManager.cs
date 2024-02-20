namespace SmartGallery.Server.Repositories.Contracts;

public interface IRepositoryManager
{
    IServiceRepository Service { get; }
    IReservationRepository Reservation { get; }
    Task SaveChangesAsync();
}
