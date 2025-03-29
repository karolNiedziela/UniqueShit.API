using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Enitities
{
    public sealed class Model : Entity
    {
        public string Name { get; private set; } = default!;

        public int ProductCategoryId { get; private set; }

        public int BrandId { get; private set; }

        public Brand Brand { get; private set; } = default!;

        private Model()
        {
        }

        public Model(string name, int productCategoryId, int brandId)
        {
            Name = name;
            ProductCategoryId = productCategoryId;
            BrandId = brandId;
        }
    }
}
