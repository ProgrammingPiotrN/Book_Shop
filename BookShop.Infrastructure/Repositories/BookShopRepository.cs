using BookShop.Domain.Interfaces;
using BookShop.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infrastructure.Repositories
{
    internal class BookShopRepository : IBookShopRepository
    {
        private readonly BookShopDbContext _dbContext;
        public BookShopRepository(BookShopDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public Task Books()
        => _dbContext.SaveChangesAsync();

        public async Task Create(Domain.Entities.BookShop bookShop)
        {
            _dbContext.Add(bookShop);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Entities.BookShop>> GetAll()
        => await _dbContext.BookShops.ToListAsync();

        public async Task<Domain.Entities.BookShop> GetByEncodedName(string encodedName)
        => await _dbContext.BookShops.FirstAsync(p => p.EncodedName == encodedName);

        public Task<Domain.Entities.BookShop?> GetByName(string name)
        => _dbContext.BookShops.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
        
    }
}
