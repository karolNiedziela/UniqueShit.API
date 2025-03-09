using UniqueShit.Domain.Enitities;

namespace UniqueShit.Application.Features.OfferFilters.GetSizesFilter
{
    public sealed class SizesFiltersResponse
    {
        public List<SizeFilterResponse> Sizes { get; set; } = [];
    }

    public record SizeFilterResponse(int Id, string Value);
}
