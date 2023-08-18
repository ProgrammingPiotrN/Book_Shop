using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Infrastructure.Database
{
    public class BookShopDbContext : IdentityDbContext
    {
        public BookShopDbContext(DbContextOptions<BookShopDbContext> options) : base(options) 
        {

        }
        public DbSet<Domain.Entities.BookShop> BookShops { get; set; }
        public DbSet<Domain.Entities.BookShopService> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Entities.BookShop>()
                .OwnsOne(p => p.ContactDetails);

            modelBuilder.Entity<Domain.Entities.BookShop>()
                .HasMany(p => p.Services)
                .WithOne(p => p.BookShop)
                .HasForeignKey(p => p.BookShopId);
        }
    }
}
