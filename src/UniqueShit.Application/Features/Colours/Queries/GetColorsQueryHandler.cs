using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Domain.Enumerations;

namespace UniqueShit.Application.Features.Colours.Queries
{
    public sealed class GetColorsQueryHandler : IQueryHandler<GetColorsQuery, List<EnumerationResponse>>
    {
        public Task<List<EnumerationResponse>> Handle(GetColorsQuery request, CancellationToken cancellationToken)
        {
            var colours = Colour.List.Select(x => new EnumerationResponse(x.Id, x.Name))
                .OrderBy(x => x.Name)
                .ToList();

            return Task.FromResult(colours);
        }
    }
}
