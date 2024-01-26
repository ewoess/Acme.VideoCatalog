using Acme.VideoCatalog.Domain.Models;

namespace Acme.VideoCatalog.Domain.Services;

/// <summary>
/// Defines the contract for a service that manages video catalog operations.
/// </summary>
public interface IVideoCatalogService
{
    /// <summary>
    /// Asynchronously retrieves all videos from the video catalog, sorted in alphabetical order.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="ServiceResult{T}"/> 
    /// where T is <see cref="IReadOnlyList{T}"/> of <see cref="Video"/>. This result object includes a list of all videos 
    /// in the catalog, ordered alphabetically, and information about the success or failure of the operation.
    /// </returns>
    Task<ServiceResult<IReadOnlyList<Video>>> GetAllAsync();
}