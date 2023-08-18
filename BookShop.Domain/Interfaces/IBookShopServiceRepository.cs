using BookShop.Domain.Entities;

namespace BookShop.Domain.Interfaces
{
    public interface IBookShopServiceRepository
    {
        Task Create(BookShopService bookShopService);
        Task<IEnumerable<BookShopService>> GetAllByEncodedName(string encodedName);
    }
}
