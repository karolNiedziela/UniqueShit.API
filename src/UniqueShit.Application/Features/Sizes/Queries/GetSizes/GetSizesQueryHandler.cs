using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Enitities;

namespace UniqueShit.Application.Features.Sizes.Queries.GetSizes
{
    public sealed class GetSizesQueryHandler : IQueryHandler<GetSizesQuery, List<GetSizesResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetSizesQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetSizesResponse>> Handle(GetSizesQuery request, CancellationToken cancellationToken)
        {
            var sizes = await _dbContext.Set<Size>()
                .Where(s => s.ProductCategoryId == request.ProductCategoryId)
                .Select(s => new GetSizesResponse
                {
                    Id = s.Id,
                    Value = s.Value
                })
                .ToListAsync(cancellationToken);

            return sizes;
        }
    }
}
