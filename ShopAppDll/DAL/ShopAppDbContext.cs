using Microsoft.EntityFrameworkCore;
using ShopAppDll.Configurations;
using ShopAppPB301Practice.Entities;
using System.Reflection;

namespace ShopAppPB301Practice.DAL
{
    public class ShopAppDbContext : DbContext
    {
        public DbSet<Group>Groups { get; set; }
        public DbSet<Student>Students { get; set; }
        public ShopAppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
