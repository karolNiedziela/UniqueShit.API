using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Enitities
{
    public sealed class Model : Entity
    {
        public string Name { get; private set; } = default!;

        public int ProductCategoryId { get; private set; }

        public int ManufacturerId { get; private set; }

        public Manufacturer Manufacturer { get; private set; } = default!;

        private Model()
        {
        }

        public Model(string name, int productCategoryId, int manufacturerId)
        {
            Name = name;
            ProductCategoryId = productCategoryId;
            ManufacturerId = manufacturerId;
        }
    }
}
