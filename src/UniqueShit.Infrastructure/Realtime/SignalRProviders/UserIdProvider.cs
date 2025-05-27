using Microsoft.AspNetCore.SignalR;
using System.Globalization;
using UniqueShit.Application.Core.Authentication;

namespace UniqueShit.Chatting.SignalRProviders
{
    public sealed class UserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(AzureADClaimTypes.ObjectIdentifier)?.Value.ToLower(CultureInfo.InvariantCulture);
        }
    }
}
