using Microsoft.EntityFrameworkCore;

namespace bot.Entity
{
    public class BotDbContext : DbContext
    {
        public DbSet<BotUser> Users { get; set; }
        public BotDbContext(DbContextOptions<BotDbContext> options)
            :base(options) {  }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // options.UseNpgsql("Host=localhost;Database=postgresbot;Username=SA;Password=abubakr0902!");
            base.OnConfiguring(options);
        }

    }
}