using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Enitities
{
    public sealed class Size : Entity
    {
        public string Value { get; private set; } = default!;
        
        public int ProductCategoryId { get; private set; }

        private Size()
        {
        }

        public Size(string value, int productCategoryId)
        {
            Value = value;
            ProductCategoryId = productCategoryId;
        }
    }
}
