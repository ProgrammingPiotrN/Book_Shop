using Microsoft.AspNetCore.Identity;

namespace BookShop.Domain.Entities
{
    public class BookShop
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? About { get; set; }
        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public BookShopContactDetails ContactDetails { get; set; } = default!;
        public string EncodedName { get; private set; } = default!;

        public List<BookShopService> Services { get; set; } = new();

        public void CodeUrl() => EncodedName = Name.ToLower().Replace(" ", "-");
 
        
    }
}
