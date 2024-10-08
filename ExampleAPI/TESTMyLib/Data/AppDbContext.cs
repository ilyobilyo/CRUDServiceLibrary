using Microsoft.EntityFrameworkCore;
using TESTMyLib.Data.Models;

namespace TESTMyLib.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
