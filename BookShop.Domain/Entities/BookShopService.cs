
namespace BookShop.Domain.Entities
{
    public class BookShopService
    {
        public int Id { get; set; }

        public string Description { get; set; } = default!;
        public string Cost { get; set; } = default!;

        public int BookShopId { get; set; } = default!;
        public BookShop BookShop { get; set; } = default!;
    }
}
