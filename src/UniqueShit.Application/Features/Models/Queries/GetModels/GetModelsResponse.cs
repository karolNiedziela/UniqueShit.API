using UniqueShit.Application.Core.Responses;
using UniqueShit.Application.Features.Offers.Contracts.Responses;

namespace UniqueShit.Application.Features.Models.Queries.GetModels
{
    public sealed record GetModelsResponse(int Id, string Name, EnumerationResponse ProductCategory, BrandResponse Brand);
}
