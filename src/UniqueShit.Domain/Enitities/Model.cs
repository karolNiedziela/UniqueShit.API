using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Enumerations;

namespace UniqueShit.Domain.Enitities
{
    public sealed class Model : Entity
    {
        public string Name { get; private set; } = default!;

        public int ProductCategoryId { get; private set; }

        public ProductCategory ProductCategory { get; private set; } = default!;

        public int BrandId { get; private set; }

        public Brand Brand { get; private set; } = default!;

        public Guid? AppUserId { get; private set; } = default!;

        private Model()
        {
        }

        public Model(string name, int productCategoryId, int brandId, Guid? appUserId = null)
        {
            Name = name;
            ProductCategoryId = productCategoryId;
            BrandId = brandId;
            AppUserId = appUserId;
        }
    }
}
