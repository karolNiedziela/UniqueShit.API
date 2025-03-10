using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Extensions;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Domain.Enitities;

namespace UniqueShit.Application.Features.Manufacturers.Queries
{
    public sealed class GetManufacturersQueryHandler : IQueryHandler<GetManufacturersQuery, PagedList<GetManufacturersResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetManufacturersQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedList<GetManufacturersResponse>> Handle(GetManufacturersQuery request, CancellationToken cancellationToken)
        {
            var manufacturersQuery = _dbContext.Set<Manufacturer>()
               .AsNoTracking()
               .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                manufacturersQuery = manufacturersQuery.Where(m => m.Name.Contains(request.SearchTerm));
            }

            var manufacturers = await manufacturersQuery
                .OrderBy(x => x.Name)
                .Select(m => new GetManufacturersResponse(m.Id, m.Name))
                .PaginateAsync(PagedBase.DefaultPageNumber, PagedBase.DefaultPageSize, cancellationToken);


            return manufacturers;
        }
    }
}
