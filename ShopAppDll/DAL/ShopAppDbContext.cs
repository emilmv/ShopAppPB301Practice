using Microsoft.EntityFrameworkCore;
using ShopAppDll.Configurations;
using ShopAppDll.Entities;
using ShopAppPB301Practice.Entities;
using System.Reflection;

namespace ShopAppPB301Practice.DAL
{
    public class ShopAppDbContext : DbContext
    {
        public DbSet<Group>Groups { get; set; }
        public DbSet<Student>Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }


        public ShopAppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
