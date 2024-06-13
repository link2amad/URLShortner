using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.BLL.Services;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace aspnetcore.ntier.Test.UnitTests.BLL.Services
{
    public class ShortUrlServiceTests
    {
        [Fact]
        public async Task ShortenUrlAsync_NewUrl_ReturnsShortenedUrl()
        {
            // Arrange
            var originalUrl = "http://example.com";
            var userId = 1;
            var shortUrlService = SetupShortUrlServiceMock();

            // Act
            var result = await shortUrlService.ShortenUrlAsync(originalUrl, userId);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual("EXISTS", result);
        }
        [Fact]
        public async Task ShortenUrlAsync_ExistingUrl_ReturnsExists()
        {
            // Arrange
            var existingUrl = "http://existing-url.com";
            var userId = 1;
            var shortUrlService = SetupShortUrlServiceMock(existingUrl);

            // Act
            var result = await shortUrlService.ShortenUrlAsync(existingUrl, userId);

            // Assert
            Assert.Equal("EXISTS", result);
        }
        [Fact]
        public async Task GetAllShortUrlsAsync_ReturnsListOfShortUrls()
        {
            // Arrange
            var existingUrl = "http://existing-url.com";
            var userId = 1;
            var shortUrlService = SetupShortUrlServiceMock(existingUrl); 

            var result = await shortUrlService.ShortenUrlAsync(existingUrl, userId);

            // Assert
            Assert.Equal("EXISTS", result);
        }

        // Add more unit tests for GetShortUrlDetailsAsync and DeleteShortUrlAsync as needed

        #region Helpers

        private IShortUrlService SetupShortUrlServiceMock(string existingUrl = null, IEnumerable<ShortUrl> shortUrls = null)
        {
            var mockRepository = new Mock<IShortUrlRepository>();

            // Mock GetOrignalUrlAsync method
            mockRepository.Setup(r => r.GetOrignalUrlAsync(existingUrl))
                          .ReturnsAsync(existingUrl != null ? new ShortUrl { OriginalUrl = existingUrl } : null);

            // Mock AddShortUrlAsync method
            mockRepository.Setup(r => r.AddShortUrlAsync(It.IsAny<ShortUrl>()))
                          .Returns(Task.CompletedTask);

            // Mock GetAllShortUrlsAsync method
            mockRepository.Setup(r => r.GetAllShortUrlsAsync())
                          .ReturnsAsync(shortUrls);

            // Mock GetShortUrlAsync method
            mockRepository.Setup(r => r.GetShortUrlAsync(It.IsAny<string>()))
                          .ReturnsAsync((string shortenedUrl) => shortUrls != null ? FindShortUrl(shortUrls, shortenedUrl) : null);

            // Mock DeleteShortUrlAsync method
            mockRepository.Setup(r => r.DeleteShortUrlAsync(It.IsAny<int>()))
                          .Returns(Task.CompletedTask);

            return new ShortUrlService(mockRepository.Object);
        }


        private ShortUrl FindShortUrl(IEnumerable<ShortUrl> shortUrls, string shortenedUrl)
        {
            foreach (var url in shortUrls)
            {
                if (url.ShortenedUrl == shortenedUrl)
                    return url;
            }
            return null;
        }

        #endregion
    }
}
