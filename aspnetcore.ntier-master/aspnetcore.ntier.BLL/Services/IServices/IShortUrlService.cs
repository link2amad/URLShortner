using aspnetcore.ntier.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.ntier.BLL.Services.IServices
{
    public interface IShortUrlService
    {
        Task<string> ShortenUrlAsync(string originalUrl, int userId);
        Task<IEnumerable<ShortUrl>> GetAllShortUrlsAsync();
        Task<ShortUrl> GetShortUrlDetailsAsync(string shortenedUrl);
        Task DeleteShortUrlAsync(int id, int userId);
    }
}
