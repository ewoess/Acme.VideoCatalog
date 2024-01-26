using Acme.VideoCatalog.Domain.DataModels;
using Acme.VideoCatalog.Domain.Models;
using AutoMapper;

namespace Acme.VideoCatalog.Services.Profiles;

/// <summary>
/// Defines the mapping profile for converting VideoData objects to Video objects.
/// </summary>
/// <remarks>
/// This class is used by AutoMapper to map properties from the VideoData model to the Video model.
/// </remarks>
public class VideoProfile : Profile
{
    public VideoProfile()
    {
        CreateMap<VideoData, Video>();
    }
}
