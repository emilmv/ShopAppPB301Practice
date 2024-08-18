using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopAppDll.Entities;
using ShopAppPB301Practice.Entities;
using System.Reflection;

namespace ShopAppPB301Practice.DAL
{
    public class ShopAppDbContext : IdentityDbContext<AppUser>
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

            List<IdentityRole> roles = new()
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            
            var hasher = new PasswordHasher<AppUser>();
            List<AppUser> users = new()
            {
                new AppUser
                {
                    UserName="Admin",
                    Email="admin@example.com",
                    NormalizedUserName="ADMIN",
                    NormalizedEmail="ADMIN@EXAMPLE.COM",
                    PasswordHash=hasher.HashPassword(null,"DefaultPassword123!"),
                    EmailConfirmed=true
                },
                new AppUser
                {
                    UserName="User",
                    Email="user@example.com",
                    NormalizedUserName="USER",
                    NormalizedEmail="USER@EXAMPLE.COM",
                    PasswordHash=hasher.HashPassword(null,"DefaultPassword123!"),
                    EmailConfirmed=true
                }
            };
            modelBuilder.Entity<AppUser>().HasData(users);

            List<IdentityUserRole<string>> userRoles = new()
            {
                new IdentityUserRole<string>
                {
                    RoleId=roles.First(r=>r.Name=="Admin").Id,
                    UserId=users.First(u=>u.UserName=="Admin").Id,
                },
                new IdentityUserRole<string>
                {
                    RoleId=roles.Last(r=>r.Name=="User").Id,
                    UserId=users.Last(u=>u.UserName=="User").Id
                }
            };
            base.OnModelCreating(modelBuilder);
        }
    }
}
