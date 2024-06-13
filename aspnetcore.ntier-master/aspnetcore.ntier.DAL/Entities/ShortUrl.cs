using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.ntier.DAL.Entities
{
    public class ShortUrl
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }
    }
}
