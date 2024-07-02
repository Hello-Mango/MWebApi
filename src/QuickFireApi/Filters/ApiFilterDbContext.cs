using Microsoft.EntityFrameworkCore;

namespace QuickFireApi.Filters
{
    public class ApiFilterDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
