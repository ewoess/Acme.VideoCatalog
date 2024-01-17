using Acme.VideoCatalog.Domain.DataModels;
using Acme.VideoCatalog.Domain.Models;
using AutoMapper;

namespace Acme.VideoCatalog.Services.Profiles;

public class VideoProfile : Profile
{
    public VideoProfile()
    {
        CreateMap<VideoData, Video>();
    }
}