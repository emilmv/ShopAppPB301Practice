using Microsoft.EntityFrameworkCore;
using ShopAppPB301Practice.Entities;

namespace ShopAppPB301Practice.DAL
{
    public class ShopAppDbContext : DbContext
    {
        public DbSet<Group>Groups { get; set; }
        public DbSet<Student>Students { get; set; }
        public ShopAppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
