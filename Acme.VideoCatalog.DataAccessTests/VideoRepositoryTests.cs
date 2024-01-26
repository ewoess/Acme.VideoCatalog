using System.Net;
using Acme.VideoCatalog.DataAccess;
using Acme.VideoCatalog.DataAccess.Dtos;
using Acme.VideoCatalog.Services.Exceptions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace Acme.VideoCatalog.DataAccessTests
{
    public class VideoRepositoryTests
    {
        private readonly string _getAllUrl = "http://fake-url.com";

        [Fact]
        public async Task GetAllAsync_ApiReturnBadRequest_ShouldThrowDataRetrievalException()
        {
            // Arrange
            var mockHandler = new Mock<MockableHttpMessageHandler> { CallBase = true };
            var httpClient = new HttpClient(mockHandler.Object);
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);
            var repository = new VideoRepository<VideoDto>(httpClient, _getAllUrl);

            // Act & Assert
            await Assert.ThrowsAsync<DataRetrievalException>(async () => await repository.GetAllAsync());
        }

        [Fact]
        public async Task GetAllAsync_ApiReturnsValidData_ShouldReturnData()
        {
            // Arrange
            var mockHandler = new Mock<MockableHttpMessageHandler> { CallBase = true };
            var httpClient = new HttpClient(mockHandler.Object);
            var expectedVideos = new List<VideoDto> { new VideoDto(), new VideoDto() };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                                       JsonConvert.SerializeObject(expectedVideos))
            };
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                                       "SendAsync",
                                                          ItExpr.IsAny<HttpRequestMessage>(),
                                                          ItExpr.IsAny<CancellationToken>()
                                                      )
                .ReturnsAsync(response);
            var repository = new VideoRepository<VideoDto>(httpClient, _getAllUrl);

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
            var mockHandler = new Mock<MockableHttpMessageHandler> { CallBase = true };
            var httpClient = new HttpClient(mockHandler.Object);
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Invalid data")
            };
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                                       "SendAsync",
                                                          ItExpr.IsAny<HttpRequestMessage>(),
                                                          ItExpr.IsAny<CancellationToken>()
                                                      )
                .ReturnsAsync(response);
            var repository = new VideoRepository<VideoDto>(httpClient, _getAllUrl);
            // Act & Assert
            await Assert.ThrowsAsync<DataParsingException>(async () => await repository.GetAllAsync());
        }

        [Fact]
        public async Task GetAllAsync_ApiReturnsNullData_ShouldReturnEmptyList()
        {
            // Arrange
            var mockHandler = new Mock<MockableHttpMessageHandler> { CallBase = true };
            var httpClient = new HttpClient(mockHandler.Object);
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            var repository = new VideoRepository<VideoDto>(httpClient, _getAllUrl);

            // Act
            IReadOnlyList<VideoDto> videos = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(videos);
            Assert.Empty(videos);
        }
    }
}