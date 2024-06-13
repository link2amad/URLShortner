using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.ntier.BLL.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _repository;

        public ShortUrlService(IShortUrlRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> ShortenUrlAsync(string originalUrl, int userId)
        {
            var existingUrl = await _repository.GetOrignalUrlAsync(originalUrl);
            if (existingUrl != null)
            {
                return "EXISTS";
            }

            var shortUrl = GenerateShortUrl(originalUrl);

            var shortUrlObj = new ShortUrl
            {
                OriginalUrl = originalUrl,
                ShortenedUrl = shortUrl,
                CreatedDate = DateTime.UtcNow,
                CreatedById = userId
            };

            await _repository.AddShortUrlAsync(shortUrlObj);

            return shortUrlObj.ShortenedUrl;
        }

        private string GenerateShortUrl(string url)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(url));
                return Convert.ToBase64String(hash).Substring(0, 8).Replace("/", "_").Replace("+", "-");
            }
        }
        public async Task<IEnumerable<ShortUrl>> GetAllShortUrlsAsync()
        {
            return await _repository.GetAllShortUrlsAsync();
        }

        public async Task<ShortUrl> GetShortUrlDetailsAsync(string shortenedUrl)
        {
            return await _repository.GetShortUrlAsync(shortenedUrl);
        }

        public async Task DeleteShortUrlAsync(int id, int userId)
        {
            await _repository.DeleteShortUrlAsync(id);
        }
    }
}