using Microsoft.EntityFrameworkCore;
using System.Globalization;
using UniqueShit.Application.Core.Extensions;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Queries;
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
                .Where(m => m.ProductCategoryId == request.ProductCategoryId)
                .Where(m => m.ManufacturerId == request.ManufacturerId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                modelsQuery = modelsQuery.Where(m => m.Name.Contains(request.SearchTerm));
            }

            var models = await modelsQuery
                .OrderBy(x => x.Name)
                .Select(m => new GetModelsResponse(m.Id, m.Name))
                .PaginateAsync(PagedBase.DefaultPageNumber, PagedBase.DefaultPageSize, cancellationToken);
                

            return models;
        }
    }
}
