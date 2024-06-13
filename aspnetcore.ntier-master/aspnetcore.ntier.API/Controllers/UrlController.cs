using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.ntier.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IShortUrlService _service;

        public UrlController(IShortUrlService service)
        {
            _service = service;
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] UrlRequestDTO request)
        {
            try
            {
                var shortenedUrl = await _service.ShortenUrlAsync(request.OriginalUrl, request.UserId);
                return Ok(new { ShortenedUrl = shortenedUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllShortUrls()
        {
            var urls = await _service.GetAllShortUrlsAsync();
            return Ok(urls);
        }

        [HttpGet("{shortenedUrl}")]
        public async Task<IActionResult> GetShortUrlDetails(string shortenedUrl)
        {
            var url = await _service.GetShortUrlDetailsAsync(shortenedUrl);
            if (url == null)
            {
                return NotFound();
            }
            return Ok(url);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShortUrl(int id, [FromQuery] int userId)
        {
            try
            {
                await _service.DeleteShortUrlAsync(id, userId);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
