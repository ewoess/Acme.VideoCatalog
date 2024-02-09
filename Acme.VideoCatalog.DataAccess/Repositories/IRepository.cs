namespace Acme.VideoCatalog.DataAccess.Repositories;

/// <summary>
/// Represents a generic repository for managing entities of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of entity the repository manages. This type must be a class.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Asynchronously retrieves all entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains a read-only list of all entities of type <typeparamref name="T"/>.
    /// </returns>
    Task<IReadOnlyList<T>> GetAllAsync();
}