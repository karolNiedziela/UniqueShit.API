﻿using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Enumerations;

namespace UniqueShit.Application.Features.OfferFilters.GetFilters
{
    public sealed class OfferFiltersResponse
    {
        public List<ProductCategory> ProductCategories { get; set; } = [];

        public List<Colour> Colours { get; set; } = [];

        public List<ItemCondition> ItemConditions { get; set; } = [];

        public List<Manufacturer> Manufacturers { get; set; } = [];
    }
}
