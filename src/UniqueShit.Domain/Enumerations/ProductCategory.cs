using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Enumerations
{
    public sealed class ProductCategory : Enumeration<ProductCategory>
    {
        public static readonly ProductCategory Shoes = new(1, "Shoes");
        public static readonly ProductCategory TShirts = new(2, "T-shirts");
        public static readonly ProductCategory Hoodies = new(3, "Hoodies");
        public static readonly ProductCategory Caps = new(4, "Caps");
        public static readonly ProductCategory Socks = new(5, "Socks");
        public static readonly ProductCategory Jackets = new(6, "Jackets");
        public static readonly ProductCategory Tracksuits = new(7, "Tracksuits");
        public static readonly ProductCategory Trousers = new(8, "Trousers");
        public static readonly ProductCategory Underwear = new(9, "Underwear");

        public ProductCategory(int id, string name) : base(id, name)
        {
        }
    }
}
