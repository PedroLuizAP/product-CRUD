using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().Property(p => p.Description).HasMaxLength(500).IsRequired(false);
            builder.Entity<Product>().Property(p => p.Name).HasMaxLength(120).IsRequired();
            builder.Entity<Product>().Property(p => p.Code).HasMaxLength(20).IsRequired();
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
