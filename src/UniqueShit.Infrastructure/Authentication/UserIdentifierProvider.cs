using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using UniqueShit.Application.Core.Authentication;

namespace UniqueShit.Infrastructure.Authentication
{
    internal sealed class UserIdentifierProvider : IUserIdentifierProvider
    {
        public Guid UserId { get; }

        public UserIdentifierProvider(IHttpContextAccessor httpContextAccessor)
        {
            string userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue(AzureADClaimTypes.ObjectIdentifier)
                ?? throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor));

            UserId = new Guid(userIdClaim);
        }
    }
}
