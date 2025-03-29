namespace UniqueShit.Application.Core.Authentication
{
    public interface IUserIdentifierProvider
    {
        Guid UserId { get; }
    }
}
