﻿using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Extensions;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Application.Features.Offers.Contracts.Responses;
using UniqueShit.Domain.Enitities;

namespace UniqueShit.Application.Features.Models.Queries.GetModels
{
    public sealed class GetModelsQueryHandler : IQueryHandler<GetModelsQuery, PagedList<GetModelsResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetModelsQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedList<GetModelsResponse>> Handle(GetModelsQuery request, CancellationToken cancellationToken)
        {
            var modelsQuery = _dbContext.Set<Model>()
                .AsNoTracking()
                .AsQueryable();

            if (request.ProductCategoryId.HasValue)
            {
                modelsQuery = modelsQuery.Where(m => m.ProductCategoryId == request.ProductCategoryId);
            }

            if (request.BrandId.HasValue)
            {
                modelsQuery = modelsQuery.Where(m => m.BrandId == request.BrandId);
            }

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                modelsQuery = modelsQuery.Where(m => m.Name.Contains(request.SearchTerm));
            }

            var models = await modelsQuery
                .OrderBy(x => x.Name)
                .Select(m => new GetModelsResponse(
                    m.Id, 
                    m.Name,
                    new EnumerationResponse(m.ProductCategoryId, m.ProductCategory.Name),
                    new BrandResponse(m.BrandId, m.Brand.Name)
                    ))
                .PaginateAsync(PagedBase.DefaultPageNumber, PagedBase.DefaultPageSize, cancellationToken);
                

            return models;
        }
    }
}
