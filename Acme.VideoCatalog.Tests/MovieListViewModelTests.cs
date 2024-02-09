using Acme.VideoCatalog.Domain.Models;
using Acme.VideoCatalog.Domain.Services;
using Acme.VideoCatalog.ViewModels;
using Moq;

namespace Acme.VideoCatalog.Tests
{
    public class MovieListViewModelTests
    {
        private readonly Mock<IVideoCatalogService> _mockService = new Mock<IVideoCatalogService>();
        private readonly MovieListViewModel _viewModel;

        public MovieListViewModelTests()
        {
            _viewModel = new MovieListViewModel(_mockService.Object);
        }
        
        // [Fact]
        // public async Task OnNavigatedTo_ShouldLoadVideoCatalog()
        // {
        //     // Arrange
        //     var videos = new List<Video>
        //     {
        //         new Video { Title = "Video 1" },
        //         new Video { Title = "Video 2" }
        //     };
        //
        //     _mockService
        //         .Setup(service => service.GetAllAsync())
        //         .ReturnsAsync(new ServiceResult<IReadOnlyList<Video>>
        //             { IsSuccess = true, Data = videos, ErrorMessage = null });
        //     
        //     // Act
        //     await _viewModel.OnNavigatedTo(null);
        // }
    }
}