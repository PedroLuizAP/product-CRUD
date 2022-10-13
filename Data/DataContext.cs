using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("");
        public DbSet<Product> Products { get; set; }
    }
}
