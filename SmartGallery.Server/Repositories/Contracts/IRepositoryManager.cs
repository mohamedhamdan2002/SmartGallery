using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SmartGallery.Server.Repositories.Contracts;

public interface IRepositoryManager
{
    TRepository GetRepository<TRepository>() where TRepository : IRepository;
    Task SaveChangesAsync();
}
