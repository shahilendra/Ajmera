using Ajmera.Model;
using Microsoft.EntityFrameworkCore;

namespace Ajmera.Repository
{
    public class AjmeraContext : DbContext
    {
        public AjmeraContext(DbContextOptions<AjmeraContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>();
        }
    }
}
