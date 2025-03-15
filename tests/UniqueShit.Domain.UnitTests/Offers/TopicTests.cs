using Shouldly;
using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Offers.ValueObjects;

namespace UniqueShit.Domain.UnitTests.Offers
{
    public class TopicTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Create_ShouldReturnTopicNullOrEmptyError_WhenValueIsNullOrWhiteSpace(string? value)
        {            
            var topic = Topic.Create(value!);

            topic.IsFailure.ShouldBeTrue();
            topic.Errors[0].ShouldBe(DomainErrors.Topic.NullOrEmpty);
        }

        [Fact]
        public void Create_ShouldReturnTopicLongerThanAllowedError_WhenValueIsLongerThanMaxLength()
        {
            var value = new string('a', Topic.MaxLength + 1);

            var topic = Topic.Create(value);

            topic.IsFailure.ShouldBeTrue();
            topic.Errors[0].ShouldBe(DomainErrors.Topic.LongerThanAllowed);
        }

        [Fact]
        public void Create_ShouldReturnTopicInstance_WhenValueIsValid()
        {
            var value = "topic";

            var topic = Topic.Create(value);

            topic.IsSuccess.ShouldBeTrue();
            topic.Value.Value.ShouldBe(value);
        }
    }
}
