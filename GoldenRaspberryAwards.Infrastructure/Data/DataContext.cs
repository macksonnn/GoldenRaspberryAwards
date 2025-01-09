

using GoldenRaspberryAwards.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberryAwards.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<MovieEntity> Movies { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("GoldenRaspberryAwardsDb");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieEntity>().HasKey(m => m.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
