using MediatR;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Application.Features.Offers.Commands.DeleteOffer
{
    public sealed record DeleteOfferCommand(int Id) : ICommand<Result>;
}
