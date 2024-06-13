using aspnetcore.ntier.DAL.DataContext;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.ntier.DAL.Repositories
{
    public class ShortUrlRepository: IShortUrlRepository
{
    private readonly AspNetCoreNTierDbContext _context;

        public ShortUrlRepository(AspNetCoreNTierDbContext context)
        {
            _context = context;
        }

        public async Task<ShortUrl> GetShortUrlAsync(string shortenedUrl)
        {
            try
            {
                var shortUrlsWithUserName = await _context.ShortUrls
            .Include(s => s.CreatedBy) // Include the navigation property to User
            .Select(s => new ShortUrl
            {
                Id = s.Id,
                OriginalUrl = s.OriginalUrl,
                ShortenedUrl = s.ShortenedUrl,
                CreatedDate = s.CreatedDate,
                CreatedById = s.CreatedById,
                CreatedBy = s.CreatedBy, // Assuming User entity has UserName property
                // You can include other properties from User as needed
            })
            .SingleOrDefaultAsync(u => u.ShortenedUrl == shortenedUrl);
             return shortUrlsWithUserName;

               // return await _context.ShortUrls.SingleOrDefaultAsync(u => u.ShortenedUrl == shortenedUrl);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ShortUrl> GetOrignalUrlAsync(string orignalUrl)
        {
            try
            {

                return await _context.ShortUrls.SingleOrDefaultAsync(u => u.OriginalUrl == orignalUrl);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IEnumerable<ShortUrl>> GetAllShortUrlsAsync()
        {
            try
            {
                var shortUrlsWithUserName = await _context.ShortUrls
            .Include(s => s.CreatedBy) // Include the navigation property to User
            .Select(s => new ShortUrl
            {
                Id = s.Id,
                OriginalUrl = s.OriginalUrl,
                ShortenedUrl = s.ShortenedUrl,
                CreatedDate = s.CreatedDate,
                CreatedById = s.CreatedById,
                CreatedBy = s.CreatedBy, // Assuming User entity has UserName property
                // You can include other properties from User as needed
            })
            .ToListAsync();
                return shortUrlsWithUserName;
            }
            catch (Exception ex)
            {

                throw;
            }
             
            //return await _context.ShortUrls.ToListAsync();
        }

        public async Task AddShortUrlAsync(ShortUrl shortUrl)
        {
            _context.ShortUrls.Add(shortUrl);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShortUrlAsync(int id)
        {
            var url = await _context.ShortUrls.FindAsync(id);
            if (url != null)
            {
                _context.ShortUrls.Remove(url);
                await _context.SaveChangesAsync();
            }
        }

        // Other necessary methods
    }
}
