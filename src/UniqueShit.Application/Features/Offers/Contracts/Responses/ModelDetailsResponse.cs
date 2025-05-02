using UniqueShit.Application.Core.Responses;

namespace UniqueShit.Application.Features.Offers.Contracts.Responses
{
    public sealed record ModelDetailsResponse(int Id, string Name, BrandResponse Brand, EnumerationResponse ProductCategory);
}
