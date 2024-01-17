using Acme.VideoCatalog.Domain.Models;

namespace Acme.VideoCatalog.Domain.Services;

public interface IVideoCatalogStore
{
    /// <summary>
    /// Gets all videos from the store in alphabetical order.
    /// </summary>
    /// <returns>All videos in the store alphabetically</returns>
    Task<IReadOnlyList<Video>> GetAllAsync();
}