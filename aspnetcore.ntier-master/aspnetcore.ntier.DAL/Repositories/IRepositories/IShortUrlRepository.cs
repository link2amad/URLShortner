using aspnetcore.ntier.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.ntier.DAL.Repositories.IRepositories
{
    public interface IShortUrlRepository
    {
        Task<ShortUrl> GetShortUrlAsync(string shortenedUrl);
        Task<ShortUrl> GetOrignalUrlAsync(string shortenedUrl);
        Task<IEnumerable<ShortUrl>> GetAllShortUrlsAsync();
        Task AddShortUrlAsync(ShortUrl shortUrl);
        Task DeleteShortUrlAsync(int id);
    }
}
