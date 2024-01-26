using Acme.VideoCatalog.Domain.DataModels;
using Acme.VideoCatalog.Domain.Models;
using Acme.VideoCatalog.Domain.Services;
using Acme.VideoCatalog.Services;
using Acme.VideoCatalog.Services.Exceptions;
using Acme.VideoCatalog.Services.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;

namespace Acme.VideoCatalog.ServicesTests
{
    public class VideoCatalogServicesTests
    {
        private readonly Mock<IRepository<VideoData>> _mockVideoRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILoggerFactory> _mockLoggerFactory;
        private readonly Mock<ILogger<VideoCatalogService>> _mockLogger;
        private /*readonly*/ VideoCatalogService _service;

        public VideoCatalogServicesTests()
        {
            _mockVideoRepo = new Mock<IRepository<VideoData>>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<VideoCatalogService>>();

            _mockLoggerFactory = new Mock<ILoggerFactory>();
            _mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(_mockLogger.Object);

            _service = new VideoCatalogService(_mockVideoRepo.Object, _mockMapper.Object, _mockLoggerFactory.Object);
        }

        [Fact]
        public async void GetAll_WithVideosInCatalog_ShouldReturnVideosAllVideosInCatalog()
        {
            // Arrange
            _mockVideoRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<VideoData>
            {
                new VideoData { },
                new VideoData { },
                new VideoData { }
            });

            var videoList = new List<Video>
            {
                new Video { },
                new Video { },
                new Video { }
            };
            _mockMapper.Setup(mapper => mapper.Map<IReadOnlyList<Video>>(It.IsAny<IReadOnlyList<VideoData>>()))
                .Returns(videoList);
            
            // Act
            ServiceResult<IReadOnlyList<Video>> actual = await _service.GetAllAsync();

            // Assert
            Assert.True(actual.IsSuccess);
            Assert.NotEmpty(actual.Data);
            Assert.Equal(videoList, actual.Data);
        }

        [Fact]
        public async Task GetAllAsync_WhenDataRetrievalFails_ShouldReturnFailureResult()
        {
            // Arrange
            _mockVideoRepo.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new DataRetrievalException("Error", new Exception()));
            
            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Error retrieving Videos.", result.ErrorMessage);
        }

        [Fact]
        public async Task GetAllAsync_WhenCatalogIsEmpty_ShouldReturnEmptyList()
        {
            // Arrange
            _mockVideoRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<VideoData>());
            // Act
            var result = await _service.GetAllAsync();
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GetAllAsync_WhenDataRetrievalExceptionOccurs_ShouldLogError()
        {
            // Arrange
            var exception = new DataRetrievalException("Test exception", new Exception());
            _mockVideoRepo.Setup(repo => repo.GetAllAsync()).ThrowsAsync(exception);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.False(result.IsSuccess);
            _mockLogger.Verify(
                    logger => logger.Log(
                        LogLevel.Error,
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error getting videos from the repository")),
                        It.Is<Exception>(ex => ex == exception),
                        It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                    Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WhenCatalogSucceeds_ShouldReturnOrderedList()
        {
            // Arrange
            var unsortedVideoDatas = new List<VideoData>
            {
                new VideoData { Title = "Zebra" },
                new VideoData { Title = "Apple" }
            };

            _mockVideoRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(unsortedVideoDatas);

            // Setup AutoMapper to map VideoData objects to Video objects directly
            _mockMapper.Setup(mapper => mapper.Map<List<Video>>(It.IsAny<List<VideoData>>()))
                .Returns((List<VideoData> v) => v.Select(videoData => new Video { Title = videoData.Title }).ToList());

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.True(result.IsSuccess);
            var videos = result.Data;
            Assert.Equal(2, videos.Count);
            Assert.Equal("Apple", videos[0].Title); // Expecting "Apple" to be first after sorting
            Assert.Equal("Zebra", videos[1].Title); // Expecting "Zebra" to be second
        }
    }
}