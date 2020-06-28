using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class AnimeContext : DbContext
    {
        public AnimeContext(DbContextOptions<AnimeContext> options) : base (options)
        {
        }

        public DbSet<Anime> AnimeList { get; set; }
    }
}