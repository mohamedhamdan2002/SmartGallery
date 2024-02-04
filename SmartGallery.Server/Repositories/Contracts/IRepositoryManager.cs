namespace SmartGallery.Server.Repositories.Contracts;

public interface IRepositoryManager
{
    TRepository GetRepository<TRepository>() where TRepository : IRepository;
    Task SaveChangesAsync();
}
