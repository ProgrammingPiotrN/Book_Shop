using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Interfaces
{
    public interface IBookShopRepository
    {
        Task Create(Domain.Entities.BookShop bookShop);
        Task<Domain.Entities.BookShop?> GetByName(string name);
        Task<IEnumerable<Domain.Entities.BookShop>> GetAll();
        Task<Domain.Entities.BookShop> GetByEncodedName(string encodedName);
        Task Books();
    }
}
