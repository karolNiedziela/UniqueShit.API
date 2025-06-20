namespace UniqueShit.Application.Features.AppUsers.Queries.GetUser
{
    public sealed record GetUserResponse(
        Guid Id,
        string Email,
        string DisplayName,
        string? PhoneNumber,
        string? AboutMe,
        string? City);
}
