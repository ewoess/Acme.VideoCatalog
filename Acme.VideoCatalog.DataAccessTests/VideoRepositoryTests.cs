using System.Net;
using Acme.VideoCatalog.DataAccess;
using Acme.VideoCatalog.DataAccess.Dtos;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace Acme.VideoCatalog.DataAccessTests
{
    public class VideoRepositoryTests
    {
        private readonly Mock<MockableHttpMessageHandler> _mockHandler;
        private readonly HttpClient _httpClient;

        [Fact]
        public async Task GetAllAsync_ApiReturnsNull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = null
            };

            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);
            var repository = new VideoRepository<VideoDto>(_httpClient, "http://fake-url.com");

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await repository.GetAllAsync());
        }

        [Fact]
        public async Task GetAllAsync_ApiReturnsValidData_ShouldReturnData()
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                                       JsonConvert.SerializeObject(new List<VideoDto> { new VideoDto(), new VideoDto() }))
            };
            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                                       "SendAsync",
                                                          ItExpr.IsAny<HttpRequestMessage>(),
                                                          ItExpr.IsAny<CancellationToken>()
                                                      )
                .ReturnsAsync(response);
            var repository = new VideoRepository<VideoDto>(_httpClient, "http://fake-url.com");
            // Act
            IReadOnlyList<VideoDto> videos = await repository.GetAllAsync();
            // Assert
            Assert.Equal(2, videos.Count);
        }
    }
}