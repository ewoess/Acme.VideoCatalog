namespace Acme.VideoCatalog.Services.Repositories;

public interface IRepository<T> where T : class
{
    /// <summary>
    /// Gets all items asynchronously.
    /// </summary>
    /// <returns>Readonly list of items</returns>
    Task<IReadOnlyList<T>> GetAllAsync();
}