using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.ValueObjects;

namespace UniqueShit.Domain.Enitities
{
    public sealed class AppUser : Entity
    {
        public string DisplayName { get; private set; } = default!;

        public string Email { get; private set; } = default!;

        public string? City { get; private set; } = default!;

        public PhoneNumber? PhoneNumber { get; private set; } = default!;

        public string? AboutMe { get; private set; } = default!;

        public Guid ADObjectId { get; private set; }

        private AppUser() { }

        public AppUser(string displayName, string email, Guid adObjectId)
        {
            DisplayName = displayName;            
            Email = email;
            ADObjectId = adObjectId;
        }

        public void Update(PhoneNumber? phoneNumber, string? aboutMe, string? city)
        {
            PhoneNumber = phoneNumber;
            AboutMe = aboutMe;
            City = city;
        }
    }
}
