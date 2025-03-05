using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}