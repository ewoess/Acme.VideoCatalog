using Acme.VideoCatalog.Domain.DataModels;
using Acme.VideoCatalog.Domain.Models;
using Acme.VideoCatalog.Services;
using Acme.VideoCatalog.Services.Repositories;
using AutoMapper;
using Moq;

namespace Acme.VideoCatalog.ServicesTests
{
    public class ApiVideoCatalogStoreTests
    {
        [Fact]
        public async void GetAll_WithVideosInCatalog_ShouldReturnVideosAllVideosInCatalog()
        {
            // Arrange
            var mockVideoRepo = new Mock<IRepository<VideoData>>();
            mockVideoRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<VideoData>
            {
                new VideoData
                {
                    Title = "The Matrix",
                    BulletText = "Welcome to the Real World",
                    Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                    RunningTime = 136,
                    Id = "1",
                    ArtUrl = "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg",
                    RelatedIds = new List<string> { "2", "3" }
                },
                new VideoData
                {
                    Title = "The Matrix Reloaded",
                    BulletText = "Free your mind",
                    Description = "Freedom fighters use extraordinary skills and weaponry to revolt against machines.",
                    RunningTime = 138,
                    Id = "2",
                    ArtUrl = "https://upload.wikimedia.org/wikipedia/en/b/ba/Poster_-_The_Matrix_Reloaded.jpg",
                    RelatedIds = new List<string> { "1", "3" }
                },
                new VideoData
                {
                    Title = "The Matrix Revolutions",
                    BulletText = "Everything that has a beginning has an end",
                    Description = "The human city of Zion defends itself against the massive invasion of the machines as Neo fights to end the war at another front while also opposing the rogue Agent Smith.",
                    RunningTime = 129,
                    Id = "3",
                    ArtUrl = "https://upload.wikimedia.org/wikipedia/en/3/34/Matrix_revolutions_ver7.jpg",
                    RelatedIds = new List<string> { "1", "2" }
                }
            });
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VideoData, Video>());
            IMapper? mapper = config.CreateMapper();

            var sut = new ApiVideoCatalogStore(mockVideoRepo.Object, mapper);

            // Act
            IReadOnlyList<Video> actual = await sut.GetAllAsync();

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(3, actual.Count);
        }

        [Fact]
        public async void GetAll_WithNoVideosInCatalog_ShouldReturnEmptyList()
        {
            // Arrange
            var mockVideoRepo = new Mock<IRepository<VideoData>>();
            mockVideoRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<VideoData>());
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VideoData, Video>());
            IMapper? mapper = config.CreateMapper();
            var sut = new ApiVideoCatalogStore(mockVideoRepo.Object, mapper);

            // Act
            IReadOnlyList<Video> actual = await sut.GetAllAsync();

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public async void GetAll_WithNullVideosInCatalog_ShouldReturnEmptyList()
        {
            // Arrange
            var mockVideoRepo = new Mock<IRepository<VideoData>>();
            mockVideoRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync((List<VideoData>)null!);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VideoData, Video>());
            IMapper? mapper = config.CreateMapper();
            var sut = new ApiVideoCatalogStore(mockVideoRepo.Object, mapper);

            // Act
            IReadOnlyList<Video> actual = await sut.GetAllAsync();

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public async void GetAll_WithVideosInCatalog_ShouldReturnAllVideosAlphabetically()
        {
            // Arrange
            var mockVideoRepo = new Mock<IRepository<VideoData>>();
            mockVideoRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<VideoData>
            {
                new VideoData
                {
                    Title = "The Matrix",
                    BulletText = "Welcome to the Real World",
                    Description =
                        "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                    RunningTime = 136,
                    Id = "1",
                    ArtUrl = "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg",
                    RelatedIds = new List<string> { "2", "3" }
                },
                new VideoData
                {
                    Title = "Bohemian Rhapsody",
                    BulletText = "The only thing more extraordinary than their music is his story",
                    Description =
                        "The story of the legendary British rock band Queen and lead singer Freddie Mercury, leading up to their famous performance at Live Aid (1985).",
                    RunningTime = 134,
                    Id = "2",
                    ArtUrl = "https://upload.wikimedia.org/wikipedia/en/2/2e/Bohemian_Rhapsody_poster.png",
                    RelatedIds = new List<string> { "1", "3" }
                },
                new VideoData
                {
                    Title = "Star Wars: Episode IV - A New Hope",
                    BulletText = "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee and two droids to save the galaxy from the Empire's world-destroying battle station",
                    Description =
                        "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee and two droids to save the galaxy from the Empire's world-destroying battle station, while also attempting to rescue Princess Leia from the mysterious Darth Vader.",
                    RunningTime = 121,
                    Id = "3",
                    ArtUrl = "https://upload.wikimedia.org/wikipedia/en/8/87/StarWarsMoviePoster1977.jpg",
                    RelatedIds = new List<string> { "1", "2" }
                }
            });
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VideoData, Video>());
            IMapper? mapper = config.CreateMapper();

            var sut = new ApiVideoCatalogStore(mockVideoRepo.Object, mapper);
            
            // Act
            IReadOnlyList<Video> actual = await sut.GetAllAsync();

            // Assert
            Assert.Equal("Bohemian Rhapsody", actual[0].Title);
            Assert.Equal("Star Wars: Episode IV - A New Hope", actual[1].Title);
            Assert.Equal("The Matrix", actual[2].Title);

        }
    }
}