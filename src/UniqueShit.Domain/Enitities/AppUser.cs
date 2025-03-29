using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Enitities
{
    public sealed class AppUser : Entity
    {
        public string DisplayName { get; private set; } = default!;

        public string Email { get; private set; } = default!;

        public string City { get; private set; } = default!;

        public Guid ADObjectId { get; private set; }

        private AppUser() { }

        public AppUser(string displayName, string email, string city, Guid adObjectId)
        {
            DisplayName = displayName;            
            Email = email;
            City = city;
            ADObjectId = adObjectId;
        }
    }
}
