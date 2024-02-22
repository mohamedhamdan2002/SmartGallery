namespace SmartGallery.Server.Repositories.Contracts;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T  entity);
    void Delete(T  entity);
    void Update(T entity);
}
