using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace GrislyGrotto
{
    public class GrislyGrottoDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        private readonly string connString;

        public GrislyGrottoDbContext(string connString) => this.connString = connString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connString);
        }
    }
}