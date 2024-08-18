using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopAppDll.Entities;
using ShopAppPB301Practice.DAL;
using ShopAppPB301Practice.DTOs.GroupDTOs;
using ShopAppPB301Practice.Profiles;

namespace ShopAppPB301Practice
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services,IConfiguration config)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<GroupCreateDTOValidator>();
            services.AddFluentValidationRulesToSwagger();
            services.AddHttpContextAccessor();
            services.AddDbContext<ShopAppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper(options =>
            {
                options.AddProfile(new MapProfile(new HttpContextAccessor()));
            });
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric=true;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase=true;
                options.Password.RequireLowercase=true;
                options.Password.RequireDigit=true;
            }).AddEntityFrameworkStores<ShopAppDbContext>().AddDefaultTokenProviders();
        }
    }
}
