using Acme.VideoCatalog.DataAccess.Dtos;
using Acme.VideoCatalog.DataAccess.Exceptions;
using Acme.VideoCatalog.DataAccess.Repositories;
using Acme.VideoCatalog.Domain.Models;
using Acme.VideoCatalog.Domain.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Acme.VideoCatalog.Services;

/// <summary>
/// Provides services for managing video catalog operations.
/// </summary>
/// <remarks>
/// This class implements the <see cref="IVideoCatalogService"/> interface.
/// It uses an instance of <see cref="IRepository{VideoData}"/> for data access,
/// an instance of <see cref="IMapper"/> for object mapping, and an instance of <see cref="ILoggerFactory"/> for logging.
/// </remarks>

public class VideoCatalogService : IVideoCatalogService
{
    private readonly IRepository<VideoDto> _videoRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<VideoCatalogService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="VideoCatalogService"/> class.
    /// </summary>
    /// <param name="videoRepository">The repository for managing video data.</param>
    /// <param name="mapper">The object mapper for mapping between different object types.</param>
    /// <param name="loggerFactory">The factory for creating loggers.</param>
    public VideoCatalogService(IRepository<VideoDto> videoRepository, IMapper mapper, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<VideoCatalogService>();
        _videoRepository = videoRepository;
        _mapper = mapper;
    }

    /// <summary>
/// Asynchronously retrieves all videos from the video catalog.
/// </summary>
/// <returns>
/// A <see cref="Task"/> representing the asynchronous operation, containing a <see cref="ServiceResult{T}"/>. 
/// The <see cref="ServiceResult{T}.Data"/> property of the <see cref="ServiceResult{T}"/> contains an 
/// <see cref="IReadOnlyList{T}"/> of <see cref="Video"/> objects. If the operation is successful, 
/// <see cref="ServiceResult{T}.IsSuccess"/> is true, otherwise false.
/// </returns>
/// <remarks>
/// This method will return an empty list if the video catalog is empty.
/// If there is an error during data retrieval, this method will log the error and return a failure result.
/// </remarks>
/// <exception cref="Acme.VideoCatalog.DataAccess.Exceptions.DataRetrievalException">Thrown when there is an error retrieving the videos from the repository.</exception>
    public async Task<ServiceResult<IReadOnlyList<Video>>> GetAllAsync()
    {
        IReadOnlyList<VideoDto> videoDtos;

        try
        {
            videoDtos = await _videoRepository.GetAllAsync();
        }
        catch (DataRetrievalException e)
        {
            _logger.LogError(e, "Error getting videos from the repository");
            return ServiceResult<IReadOnlyList<Video>>.Failure("Error retrieving Videos.");
        }

        List<VideoDto> sortedVideoDtos = videoDtos.OrderBy(x => x.Title).ToList();
        var data = _mapper.Map<List<Video>>(sortedVideoDtos) ?? new List<Video>();
        return ServiceResult<IReadOnlyList<Video>>.Success(data);
    }
}