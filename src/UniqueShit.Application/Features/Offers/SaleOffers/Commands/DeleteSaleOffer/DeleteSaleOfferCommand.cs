using MediatR;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Application.Features.Offers.SaleOffers.Commands.DeleteSaleOffer
{
    public sealed record DeleteSaleOfferCommand(int Id) : ICommand<Result>;
}
