using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Domain.Enitities;

namespace UniqueShit.Application.Features.Models.Queries.GetModels
{
    public sealed class GetModelsQueryHandler : IQueryHandler<GetModelsQuery, List<GetModelsResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetModelsQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetModelsResponse>> Handle(GetModelsQuery request, CancellationToken cancellationToken)
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
                .Select(m => new GetModelsResponse(
                    m.Id,
                    m.Name,
                    m.ProductCategoryId,
                    m.BrandId
                    ))
                .Take(PagedBase.MinPageSize)
                .ToListAsync(cancellationToken);


            return models;
        }
    }
}
