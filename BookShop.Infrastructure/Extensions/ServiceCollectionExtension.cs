using BookShop.Domain.Interfaces;
using BookShop.Infrastructure.Database;
using BookShop.Infrastructure.Repositories;
using BookShop.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookShopDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("BookShop")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BookShopDbContext>();

            services.AddScoped<BookShopSeeder>();

            services.AddScoped<IBookShopRepository, BookShopRepository>();
            services.AddScoped<IBookShopServiceRepository, BookShopServiceRepository>();

        }
    }
}
