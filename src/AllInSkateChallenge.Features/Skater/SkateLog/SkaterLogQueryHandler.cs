using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Skater.SkateLog;

public class SkaterLogQueryHandler(ApplicationDbContext context) : IRequestHandler<SkaterLogQuery, SkaterLogQueryResponse>
{
    private readonly ApplicationDbContext context = context;

    public async Task<SkaterLogQueryResponse> Handle(SkaterLogQuery request, CancellationToken cancellationToken)
    {
        List<SkateLogEntry> allEntries = request.IncludeTeam
            ? await context.SkateLogEntries.Where(x => x.ApplicationUser.Team.Equals(request.Skater.Team)).ToListAsync(cancellationToken)
            : await context.SkateLogEntries.Where(x => x.ApplicationUserId.Equals(request.Skater.Id)).ToListAsync(cancellationToken);

        return new SkaterLogQueryResponse
        {
            TeamEntries = allEntries,
            Entries = allEntries.Where(x => x.ApplicationUserId.Equals(request.Skater.Id, StringComparison.OrdinalIgnoreCase)).ToList()
        };
    }
}
