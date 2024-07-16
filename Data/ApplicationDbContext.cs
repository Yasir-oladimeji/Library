using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
