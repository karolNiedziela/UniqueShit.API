using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Enitities;

namespace UniqueShit.Application.Features.Brands.Queries
{
    public sealed class GetBrandsQueryHandler : IQueryHandler<GetBrandsQuery, List<GetBrandsResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetBrandsQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetBrandsResponse>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
           var brandsQuery = _dbContext.Set<Brand>()
               .AsNoTracking()
               .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                brandsQuery = brandsQuery.Where(m => m.Name.StartsWith(request.SearchTerm));
            }

            var brands = await brandsQuery
                .OrderBy(x => x.Name)
                .Select(m => new GetBrandsResponse(m.Id, m.Name))
                .ToListAsync(cancellationToken);


            return brands;
        }
    }
}
