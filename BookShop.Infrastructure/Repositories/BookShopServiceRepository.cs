using BookShop.Application.BookShop.Queries.GetBookShopByEncodedName;
using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces;
using BookShop.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Infrastructure.Repositories
{
    public class BookShopServiceRepository : IBookShopServiceRepository
    {
        private readonly BookShopDbContext _dbContext;

        public BookShopServiceRepository(BookShopDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task Create(BookShopService bookShopService)
        {
            _dbContext.Services.Add(bookShopService);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookShopService>> GetAllByEncodedName(string encodedName)
        => await _dbContext.Services
            .Where(s => s.BookShop.EncodedName == encodedName)
            .ToListAsync();
    }
}
