using BookShop.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infrastructure.Seeders
{
    public class BookShopSeeder
    {
        private readonly BookShopDbContext _dbContext;
        public BookShopSeeder(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Seed()
        {
            if(await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.BookShops.Any())
                {
                    var SwiatKsiazek = new Domain.Entities.BookShop()
                    {
                        Name = "Świat książek",
                        Description = "Książka fantasy, komedie, przygodowe",
                        ContactDetails = new()
                        {
                            City = "Kalisz",
                            Street = "Ostrowska 25",
                            PostalCode = "62 800",
                            NumberPhone = "+48458326310"
                        }
                        
                    };

                    SwiatKsiazek.CodeUrl();

                    _dbContext.BookShops.Add(SwiatKsiazek);
                    await _dbContext.SaveChangesAsync();

                }
            }
        }
    }
}
