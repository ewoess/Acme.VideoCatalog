using Acme.VideoCatalog.Domain.DataModels;
using Acme.VideoCatalog.Domain.Models;
using Acme.VideoCatalog.Domain.Services;
using Acme.VideoCatalog.Services.Repositories;
using AutoMapper;

namespace Acme.VideoCatalog.Services;

public class ApiVideoCatalogStore : IVideoCatalogStore
{
    private readonly IRepository<VideoData> _videoRepository;
    private readonly IMapper _mapper;

    public ApiVideoCatalogStore(IRepository<VideoData> videoRepository, IMapper mapper)
    {
        _videoRepository = videoRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<Video>> GetAllAsync()
    {
        IReadOnlyList<VideoData> videoDatas = await _videoRepository.GetAllAsync();
        IOrderedEnumerable<VideoData> sortedVideoDatas = videoDatas.OrderBy(x => x.Title);
        return _mapper.Map<IReadOnlyList<Video>>(sortedVideoDatas);
    }
}