using Shouldly;
using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Offers.ValueObjects;

namespace UniqueShit.Domain.UnitTests.Offers
{
    public class DescriptionTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Create_ShouldReturnDescriptionNullOrEmptyError_WhenValueIsNullOrWhiteSpace(string? value)
        {
            var description = Description.Create(value!);

            description.IsFailure.ShouldBeTrue();
            description.Errors[0].ShouldBe(DomainErrors.Description.NullOrEmpty);
        }

        [Fact]
        public void Create_ShouldReturnDescriptionLongerThanAllowedError_WhenValueIsLongerThanMaxLength()
        {
            var value = new string('a', Description.MaxLength + 1);

            var description = Description.Create(value);

            description.IsFailure.ShouldBeTrue();
            description.Errors[0].ShouldBe(DomainErrors.Description.LongerThanAllowed);
        }

        [Fact]
        public void Create_ShouldReturnDescriptionInstance_WhenValueIsValid()
        {
            var value = "Just a brief description";

            var description = Description.Create(value);

            description.IsSuccess.ShouldBeTrue();
            description.Value.Value.ShouldBe(value);
        }
    }
}
