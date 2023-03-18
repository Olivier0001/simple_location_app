using Microsoft.EntityFrameworkCore;
using SimpleLocation.Models;

namespace SimpleLocationWeb.DateAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<CarType> CarType { get; set; }
    }
}
