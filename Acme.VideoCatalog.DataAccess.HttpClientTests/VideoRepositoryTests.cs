using System.Net;
using Acme.VideoCatalog.DataAccess.Dtos;
using Acme.VideoCatalog.DataAccess.Exceptions;
using Acme.VideoCatalog.DataAccess.HttpClient;
using Acme.VideoCatalog.DataAccessTests;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace Acme.VideoCatalog.DataAccess.HttpClientTests
{
    public class VideoRepositoryTests
    {
        private readonly Mock<MockableHttpMessageHandler> _mockHandler;
        private readonly System.Net.Http.HttpClient _httpClient;
        public VideoRepositoryTests()
        {
            _mockHandler = new Mock<MockableHttpMessageHandler> { CallBase = true };
            _httpClient = new System.Net.Http.HttpClient(_mockHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/")
            };
        }

        [Fact]
        public async Task GetAllAsync_ApiReturnBadRequest_ShouldThrowDataRetrievalException()
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);
            var repository = new VideoRepository<VideoDto>(_httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<DataRetrievalException>(async () => await repository.GetAllAsync());
        }

        [Fact]
        public async Task GetAllAsync_ApiReturnsValidData_ShouldReturnData()
        {
            // Arrange
            var expectedVideos = new List<VideoDto> { new VideoDto(), new VideoDto() };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                                       JsonConvert.SerializeObject(expectedVideos))
            };
            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                                       "SendAsync",
                                                          ItExpr.IsAny<HttpRequestMessage>(),
                                                          ItExpr.IsAny<CancellationToken>()
                                                      )
                .ReturnsAsync(response);
            var repository = new VideoRepository<VideoDto>(_httpClient);

            // Act
            IReadOnlyList<VideoDto> videos = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(videos);
            Assert.Equal(2, videos.Count);
        }

        [Fact]
        public async Task GetAllAsync_ApiReturnsInvalidData_ShouldThrowDataParsingException()
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Invalid data")
            };
            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                                       "SendAsync",
                                                          ItExpr.IsAny<HttpRequestMessage>(),
                                                          ItExpr.IsAny<CancellationToken>()
                                                      )
                .ReturnsAsync(response);
            var repository = new VideoRepository<VideoDto>(_httpClient);
            // Act & Assert
            await Assert.ThrowsAsync<DataParsingException>(async () => await repository.GetAllAsync());
        }

        [Fact]
        public async Task GetAllAsync_ApiReturnsNullData_ShouldReturnEmptyList()
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };
            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            var repository = new VideoRepository<VideoDto>(_httpClient);

            // Act
            IReadOnlyList<VideoDto> videos = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(videos);
            Assert.Empty(videos);
        }
    }
}