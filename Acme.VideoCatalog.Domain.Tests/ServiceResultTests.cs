using Acme.VideoCatalog.Domain.Services;

namespace Acme.VideoCatalog.Domain.Tests
{
    public class ServiceResultTests
    {
        [Fact]
        public void Success_WithData_ShouldSetIsSuccessTrue()
        {
            // Act
            var result = ServiceResult<int>.Success(123);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Success_WithData_ShouldSetDataCorrectly()
        {
            // Arrange
            int expectedData = 123;

            // Act
            var result = ServiceResult<int>.Success(expectedData);

            // Assert
            Assert.Equal(expectedData, result.Data);
        }

        [Fact]
        public void Failure_WithMessage_ShouldSetIsSuccessFalse()
        {
            // Act
            var result = ServiceResult<int>.Failure("Error");

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Failure_WithMessage_ShouldSetErrorMessageCorrectly()
        {
            // Arrange
            string expectedMessage = "Error";

            // Act
            var result = ServiceResult<int>.Failure(expectedMessage);

            // Assert
            Assert.Equal(expectedMessage, result.ErrorMessage);
        }

        [Fact]
        public void Failure_WithMessage_ShouldSetDataToDefault()
        {
            // Act
            var result = ServiceResult<int>.Failure("Error");

            // Assert
            Assert.Equal(default(int), result.Data);
        }
    }
}